using System;
using TIK.Core.Domain;

namespace TIK.Domain.TheSet
{
    public class CommonStockInfo : BaseModel<Int32>
    {
        public CommonStockInfo()
        {
        }
        public Int32 StockId
        {
            get { return Id; }
        }

        public string Symbol
        {
            get;
            set;
        }
        public string Address
        {
            get;set;
        }
        public string Telephone
        {
            get;
            set;
        }
        public string Fax
        {
            get;
            set;
        }
        public string WebSite
        {
            get;
            set;
        }

        public string Market
        {
            get;
            set;
        }
        public string SecurityName
        {
            get;
            set;
        }

        /// <summary> 
        /// กลุ่มอุตสาหกรรม
        /// </summary>
        /// <value>The industry.</value>
        public string Industry
        {
            get;
            set;
        }
        /// <summary>
        /// หมวดธุรกิจ
        /// </summary>
        /// <value>The sector.</value>
        public string Sector
        {
            get;
            set;
        }
        /// <summary>
        /// วันที่เข้าซื้อขายวันแรก
        /// </summary>
        /// <value>The first trade date.</value>
        public DateTime FirstTradeDate
        {
            get;
            set;
        }
        /// <summary>
        /// รายละเอียดเกี่ยวกับทุน -> ราคาพาร์
        /// </summary>
        /// <value>The par value.</value>
        public decimal ParValue
        {
            get;
            set;
        }
        /// <summary>
        /// รายละเอียดเกี่ยวกับทุน -> ทุนจดทะเบียน
        /// </summary>
        /// <value>The authorized capital.</value>
        public decimal AuthorizedCapital
        {
            get;
            set;
        }

        /// <summary>
        /// รายละเอียดเกี่ยวกับทุน -> ทุนจดทะเบียนชำระแล้ว
        /// </summary>
        /// <value>The paid up capital.</value>
        public decimal PaidUpCapital
        {
            get;
            set;
        }

        /// <summary>
        /// รายละเอียดเกี่ยวกับจำนวนหุ้น -> จำนวนหุ้นจดทะเบียนกับตลท.
        /// </summary>
        /// <value>The listed share.</value>
        public decimal ListedShare
        {
            get;
            set;
        }
        /// <summary>
        /// รายละเอียดเกี่ยวกับจำนวนหุ้น -> จำนวนหุ้นชำระแล้ว 
        /// </summary>
        /// <value>The paid up stock.</value>
        public decimal PaidUpStock
        {
            get;
            set;
        }
    }
}
