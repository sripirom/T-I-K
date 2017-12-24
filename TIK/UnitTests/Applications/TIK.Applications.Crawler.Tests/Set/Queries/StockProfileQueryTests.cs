using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;
using HtmlAgilityPack;
using Newtonsoft.Json;
using TIK.Applications.Crawler.Set.Queries;
using TIK.Domain.Crawler;
using TIK.Domain.TheSet;
using TIK.Persistance.ElasticSearch;
using TIK.Persistance.ElasticSearch.Repositories;
using Xunit;
using Xunit.Abstractions;
using System.Collections.Generic;

namespace TIK.Applications.Crawler.Tests.Set.Queries
{
    public class StockProfileQueryTests : IClassFixture<CrawlerFixture>
    {
        private readonly CrawlerFixture _fixture;
        private readonly ITestOutputHelper _output;

        public StockProfileQueryTests(CrawlerFixture fixture, ITestOutputHelper output)
        { 
            _fixture = fixture;
            _output = output;
        }

        [Fact]
        public void SearchStockProfileTest()
        {
            IStockProfileQuery query = new StockProfileQuery();
            var criteria = new SearchStockProfileCriteria();
            criteria.Symbol = "A";
            criteria.StockId = 1;

            var stockInfo =  query.SearchStockProfile(criteria);

            string result = JsonConvert.SerializeObject(stockInfo);
        }

        [Fact]
        public void IndexCommonStockInfo()
        {
            var context = new EsContext(new Uri("http://localhost:9200"), "set");

            var commonStockRepo = new CommonStockRepository(context);
            var commonStocks = commonStockRepo.List().ToList().OrderBy(a=>a.Id);

            IStockProfileQuery stockProfileQuery = new StockProfileQuery();
            var path = Path.GetFullPath(@"./../../../SourceFiles/");
            WriteFile(commonStocks.Skip(0).Take(100), Path.Combine(path, "commonStockProfileEn_0-100.json"));
            /*
            var amt = commonStocks.Count();
            int start = 0;
            do
            {
                WriteFile(commonStocks.Skip(start).Take(100), Path.Combine(path, $"commonStockProfileEn_{start}-{start+100}.json"));
                start += (start+101 < amt ? start+(amt-start): 101);
            } while (start <= commonStocks.Count());

            */
        }

        private static void WriteFile(IEnumerable<CommonStock> commonStocks, string fileName)
        {
                
            IStockProfileQuery stockProfileQuery = new StockProfileQuery();
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(fileName))
            {
                foreach (var stock in commonStocks)
                {
                    try
                    {
                        Console.WriteLine("StockId:" + stock.Id);
                        Task.Delay(1000).Wait();
                        var criteria = new SearchStockProfileCriteria() { StockId = stock.Id, Symbol = stock.Symbol };
                        var info = stockProfileQuery.SearchStockProfile(criteria);

                        string index = $"{{ \"index\": {{ \"_index\": \"set_commonstockinfo\", \"_type\": \"commonstockinfo\", \"_id\": \"{info.Id}\"}} }}";
                        file.WriteLine(index);
                        //string result = JsonConvert.SerializeObject(info);
                        string indexData = $"{{ \"id\":{info.Id}, \"stockId\":{info.StockId},\"symbol\":\"{info.Symbol}\",\"address\":\"{info.Address}\",\"telephone\":\"{info.Telephone}\",\"fax\":\"{info.Fax}\",\"webSite\":\"{info.WebSite}\",\"market\":\"{info.Market}\",\"securityName\":\"{info.SecurityName}\",\"industry\":\"{info.Industry}\",\"sector\":\"{info.Sector}\",\"firstTradeDate\":\"{info.FirstTradeDate}\",\"parValue\":{info.ParValue},\"authorizedCapital\":{info.AuthorizedCapital},\"paidUpCapital\":{info.PaidUpCapital},\"listedShare\":{info.ListedShare},\"paidUpStock\":{info.PaidUpStock} }}";
                        file.WriteLine(indexData);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }

        [Fact]
        public void SearchStockProfileSampleFileTest()
        {
            string rootNodeDoc = @"//*[contains(@class,'table-reponsive')]";
            string xPathStrong = @"//div/strong[starts-with(normalize-space(text()),'{0}')]/parent::div/following-sibling::div";
            string xPathStrongA = @"//div/strong[starts-with(normalize-space(text()),'{0}')]/parent::div/following-sibling::div/a";
            string xPathDiv = @"//div[starts-with(normalize-space(text()),'Authorized Capital')]/following-sibling::div";


            var path = Path.GetFullPath(@"./../../../SourceFiles/commonStockProfileEn.html");

            if(!File.Exists(path)){
                throw new FileNotFoundException(path);
            }

            var doc = new HtmlDocument();
            doc.Load(path);

            var rootDoc = doc.DocumentNode.SelectNodes(rootNodeDoc).FirstOrDefault();

            var info = new CommonStockInfo() { Id = 1, Symbol = "A", };
            info.SecurityName = SelectNode(rootDoc, xPathStrong, "Company Name");
            info.Address = SelectNode(rootDoc, xPathStrong, "Address");
            info.Telephone = SelectNode(rootDoc, xPathStrong, "Telephone");
            info.Fax = SelectNode(rootDoc, xPathStrong, "Fax");
            info.WebSite = SelectNode(rootDoc, xPathStrongA, "Website");
            info.Market = SelectNode(rootDoc, xPathStrong, "Market");
            info.Industry = SelectNode(rootDoc, xPathStrong, "Industry");
            info.Sector = SelectNode(rootDoc, xPathStrong, "Sector");
            info.FirstTradeDate = ConvertToDateTime(SelectNode(rootDoc, xPathStrong, "First Trade Date"), "dd MMM yyyy");
            info.ParValue = ConvertToDecimal(SelectNode(rootDoc, xPathStrong, "Par Value"));
            info.AuthorizedCapital = ConvertToDecimal(SelectNode(rootDoc, xPathDiv, "Authorized Capital"));
            info.PaidUpCapital = ConvertToDecimal(SelectNode(rootDoc, xPathDiv, "Paid-up Capital"));
            info.ListedShare = ConvertToDecimal(SelectNode(rootDoc, xPathDiv, "Listed Share"));
            info.PaidUpStock = ConvertToDecimal(SelectNode(rootDoc, xPathDiv, "Paid-up Stock"));

            string result = JsonConvert.SerializeObject(info);
            _output.WriteLine(result);
            Console.WriteLine(result);

        }

        private string SelectNode(HtmlNode node, string xPath, string target)
        {
            var patternXPath = string.Format(xPath, target);
            var result = node.SelectNodes(patternXPath);

            if (result != null && result.Count() > 0)
            {
                return result.FirstOrDefault().InnerText;
            }

            return "";
        }



        private decimal ConvertToDecimal(string value)
        {
            
            decimal result = 0;

            if (string.IsNullOrEmpty(value)) return result;
                
            var tmp = value.Replace("Baht", "").Replace("Shares", "").Replace(",", "").Trim();
            if(!Decimal.TryParse(tmp, out result))
            {
                
            }

            return result;
        }

        private DateTime ConvertToDateTime(string value, string format)
        {

            if (string.IsNullOrEmpty(value)) return DateTime.MinValue;

            var culture = CultureInfo.CreateSpecificCulture("en-US");
            var styles = DateTimeStyles.None;

            DateTime result;


            if(!DateTime.TryParseExact(value, format, culture, styles, out result))
            {
                result = DateTime.MinValue;
            }

            return result;
        }
    }
}
