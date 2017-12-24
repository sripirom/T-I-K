using System;
using System.IO;
using System.Text;
using Xunit;

namespace TIK.Persistance.ElasticSearch.Tests
{
    public class ElasticIndexing
    {
        private const string path = "/Users/Tikclicker/SripiromDev/GitHub/tik/T-I-K/innit/data/commonStocks/";
        public ElasticIndexing()
        {
        }

        [Fact]
        public void Gen()
        {


            StringBuilder build = new StringBuilder();

            foreach (var line in File.ReadLines(path + "SET_StockList.csv"))
            {
                string[] values = line.Split(',');
                string indexline = $"{{ \"index\": {{ \"_index\": \"set_commonstock\", \"_type\": \"commonstock\", \"_id\": \"{values[0]}\"}} }}";
                string data = $"{{ \"id\": \"{values[0]}\", \"symbol\": \"{values[1]}\", \"market\": \"{values[2]}\", \"securityNameTh\": \"{values[3]}\", \"securityName\": \"{values[4]}\" }}";
               
                build.AppendLine(indexline);
                build.AppendLine(data);
            }

            File.WriteAllText(path + "SET_StockList.json" ,build.ToString().Replace("&sbquo;",","));
        }
    }
}
