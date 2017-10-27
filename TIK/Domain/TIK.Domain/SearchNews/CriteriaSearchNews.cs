using System;
using TIK.Core.Domain;

namespace TIK.Domain.SearchNews
{
    public class CriteriaSearchNews : BaseModel<String>
    {
        public CriteriaSearchNews()
        {
        }
        public string Target { get; set; }
    }
}
