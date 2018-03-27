using System;
using System.Globalization;
using System.Linq;
using HtmlAgilityPack;
using TIK.Core.Application;
using TIK.Core.Helpers;
using TIK.Core.Logging;
using TIK.Domain.Crawler;
using TIK.Domain.TheSet;

namespace TIK.Applications.Crawler.Set.Queries
{
    public class StockProfileQuery : BaseAppService, IStockProfileQuery
    {
        private const string htmlTarget = @"https://www.set.or.th/set/companyprofile.do?symbol={0}&ssoPageId=4&language=en&country=US";
        private const string rootNodeDoc = @"//*[contains(@class,'table-reponsive')]";
        private const string xPathStrong = @"//div/strong[starts-with(normalize-space(text()),'{0}')]/parent::div/following-sibling::div";
        private const string xPathStrongA = @"//div/strong[starts-with(normalize-space(text()),'{0}')]/parent::div/following-sibling::div/a";
        private const string xPathDiv = @"//div[starts-with(normalize-space(text()),'Authorized Capital')]/following-sibling::div";


        public StockProfileQuery()
        {
           
        }

        public CommonStockInfo SearchStockProfile(SearchStockProfileCriteria criteria)
        {
            CommonStockInfo info = new CommonStockInfo() { Id = criteria.StockId, Symbol = criteria.Symbol };
            var html = string.Format(htmlTarget, criteria.Symbol);

            HtmlWeb web = new HtmlWeb();

            var htmlDoc = web.Load(html);

            var rootDoc = GetRootNode(htmlDoc);
            if(rootDoc !=null)
            {
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
            }




            return info;
        }

        private HtmlNode GetRootNode(HtmlDocument htmlDoc)
        {
           var nodes =  htmlDoc.DocumentNode.SelectNodes(rootNodeDoc);
            if(nodes!=null){
                return nodes.FirstOrDefault();
            }else{
                return null;
            }
        }

        private string SelectNode(HtmlNode node, string xPath, string target)
        {
            var patternXPath = string.Format(xPath, target);
            var result = node.SelectNodes(patternXPath);

            if(result !=null && result.Count() > 0)
            {
                return result.FirstOrDefault().InnerText;
            }

            return "";
        }


        private decimal ConvertToDecimal(string value)
        {

            var tmp = value.Replace("Baht", "").Replace("Shares", "").Replace(",", "").Trim();

            return tmp.ToDecimal(expCode: null, defValue:0);
        }

        private DateTime ConvertToDateTime(string value, string format)
        {

            if (string.IsNullOrEmpty(value)) return DateTime.MinValue;

            var culture = CultureInfo.CreateSpecificCulture("en-US");
            var styles = DateTimeStyles.None;

            DateTime result;


            if (!DateTime.TryParseExact(value, format, culture, styles, out result))
            {
                result = DateTime.MinValue;
            }

            return result;
        }
    }
}
