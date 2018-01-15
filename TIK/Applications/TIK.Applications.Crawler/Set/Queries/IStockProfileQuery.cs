using System;
using System.ComponentModel;
using TIK.Core.Application;
using TIK.Domain.Crawler;
using TIK.Domain.TheSet;

namespace TIK.Applications.Crawler.Set.Queries
{
    public interface IStockProfileQuery : IAppService
    {
        [DisplayName("SearchStockProfile")]
        CommonStockInfo SearchStockProfile(SearchStockProfileCriteria criteria);
         
    }
} 
