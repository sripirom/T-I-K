using System;
namespace TIK.WebPortal.Models.StockViewerViewModels
{
    public class StockViewModel
    {
        public int StockId { get; set; }

        public string Symbol
        {
            get;
            set;
        }
        public string SecurityName
        {
            get;
            set;
        }
        public string Market
        {
            get;
            set;
        }
    }
}
