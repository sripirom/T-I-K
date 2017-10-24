using System;
using TIK.Core.Application;
using TIK.Domain.SearchNews;

namespace TIK.Applications.Batch.Commands.SearchNews
{
    public interface ISearchNewsCommand : IAppService
    {
        void SearchNewsBankOfThailand(CriteriaSearchNews criteriaSearchNews);
        void AnalysisResult(CriteriaSearchNews criteriaSearchNews);
        void ReportingResult(CriteriaSearchNews criteriaSearchNews);
    }
}
