using System;
using System.IO;
using System.Linq;
using FileHelpers;
using TIK.Computation.Set.Tmpl;
using TIK.Persistance.ElasticSearch;
using TIK.Persistance.ElasticSearch.Repositories;
using TIK.Domain.TheSet;
using System.Text;

namespace TIK.Computation.Set
{

    /// <summary>
    /// Program.
    /// curl -XPOST 192.168.99.100:32817/set_stock_eod/eod/_bulk --data-binary  @/Users/Tikclicker/SripiromDev/GitHub/TouchIntegrationKit/sampleData/ElasticSearchData/EOD_1975-05-06.json
    /// </summary>
    class Program
    {

        static EsContext context = new EsContext(new Uri("http://192.168.99.100:32813"), "set_eod");

        //var repoIndex = new IndexRepository(context.CreateClient());
        //repoIndex.IndexData<UserAccount>(userAccount, "member", "userAccount");

      

        static void Main(string[] args)
        {
            var repo = new EodRepository(context.CreateClient(), context.IndexName);

            string path = Path.GetFullPath("../../../sampleData/set-archive_EOD_UPDATE");
            string targetPath = Path.GetFullPath("../../../sampleData/ElasticSearchData");
            if(!Directory.Exists(targetPath)){
                Directory.CreateDirectory(targetPath);
            }
            Console.WriteLine(path);
            var allFiles =  Directory.GetFiles(path);

            var files = allFiles.ToList();
            //var files = allFiles.Where(a => a.Contains("1975"));

            if (files.Any())
            {
                string _index = "set_eod";
                string _type = "eod";
                foreach (var file in files)
                {
                    StringBuilder builder = new StringBuilder();

                    Console.WriteLine(string.Format("FileName:D {0}",file));
                    string fileName = Path.GetFileName(file);
                    string targetFileName = Path.Combine(targetPath, fileName.Replace(".csv", ".json"));



                    var engine = new FileHelperEngine<EodTmpl>();
                  
                    // To Read Use:
                    var records = engine.ReadFile(file);
                    // result is now an array of Customer

                    foreach (var dataRecord in records)
                    {
                        var ent = MapTo(dataRecord);
                        string a = string.Format(@"{{""index"": {{""_index"": ""{0}"", ""_type"": ""{1}"", ""_id"": ""{2}""}}}}", _index, _type, ent.Id);
                        builder.AppendLine(a);
                        Console.WriteLine(a);

                        var s = string.Format(@"{{""symbol"": ""{0}"", ""eodDate"": ""{1}"", ""open"": {2}, ""high"": {3}, ""low"": {4}, ""close"": {5}, ""volume"": {6}, ""id"": ""{7}"" }}", 
                                              ent.Symbol, ent.EodDate.ToString("yyyy-MM-ddTHH:mm:ss"), ent.Open, ent.High, ent.Low, ent.Close, ent.Volume, ent.Id );
                        builder.AppendLine(s);
                        Console.WriteLine(s);

                    }


                   File.WriteAllText(targetFileName, builder.ToString());
                }
               
            }

            Console.ReadKey();

            Console.ReadLine();



        }

        static Eod MapTo(EodTmpl eodCsv)
        {
            return new Eod()
            {
                Id = string.Format("{0}_{1}", eodCsv.EodDate.ToString("yyyyMMdd"), eodCsv.Symbol),
                Symbol = eodCsv.Symbol,
                EodDate = eodCsv.EodDate,
                Open = eodCsv.Open,
                High = eodCsv.High,
                Low = eodCsv.Low,
                Close = eodCsv.Close,
                Volume = eodCsv.Volumn
            }; 
        }
    }
}
