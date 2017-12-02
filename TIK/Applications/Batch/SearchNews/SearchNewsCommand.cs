using System;
using System.Threading.Tasks;
using TIK.Core.Application;
using TIK.Core.Logging;
using TIK.Domain.SearchNews;

namespace TIK.Applications.Batch.SearchNews
{
    public class SearchNewsCommand : BaseAppService, ISearchNewsCommand
    {
  

        public SearchNewsCommand()
        {
           
        }

        public void SearchNewsBankOfThailand(CriteriaSearchNews criteriaSearchNews)
        {
            //Logger.Info("SearchNewsBankOfThailand");
            Task.Delay(TimeSpan.FromSeconds(10)).Wait();
        }

        public void AnalysisResult(CriteriaSearchNews criteriaSearchNews)
        {
            //Logger.Info("AnalysisResult");
            Task.Delay(TimeSpan.FromSeconds(5)).Wait();
        }

        public void ReportingResult(CriteriaSearchNews criteriaSearchNews)
        {
            //Logger.Info("ReportingResult");
            Task.Delay(TimeSpan.FromSeconds(2)).Wait();
        }


    }
}
