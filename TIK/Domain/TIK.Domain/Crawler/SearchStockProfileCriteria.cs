using System;
namespace TIK.Domain.Crawler
{
    public class SearchStockProfileCriteria
    {
        public int StockId
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the html target by Url.
        /// </summary>
        /// <value>The html target.</value>
        public string Symbol
        {
            get;
            set;
        }

    }
}
