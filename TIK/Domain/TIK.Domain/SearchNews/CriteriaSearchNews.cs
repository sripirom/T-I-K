using System;
using TIK.Core.Domain;

namespace TIK.Domain.SearchNews
{
    public class CriteriaSearchNews : BaseModel<Int32>
    {
        public CriteriaSearchNews()
        {
        }
        public string Target { get; set; }
    }
}
