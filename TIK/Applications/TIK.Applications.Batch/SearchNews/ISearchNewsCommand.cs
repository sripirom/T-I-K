using System;
using System.ComponentModel;
using TIK.Core.Application;
using TIK.Domain.SearchNews;

namespace TIK.Applications.Batch.SearchNews
{
    public interface ISearchNewsCommand : IAppService
    {
        [DisplayName("SearchNewsBankOfThailand")]
        void SearchNewsBankOfThailand(CriteriaSearchNews criteriaSearchNews);

        [DisplayName("AnalysisResult")]
        void AnalysisResult(CriteriaSearchNews criteriaSearchNews);

        [DisplayName("ReportingResult")]
        void ReportingResult(CriteriaSearchNews criteriaSearchNews);
    }
}
