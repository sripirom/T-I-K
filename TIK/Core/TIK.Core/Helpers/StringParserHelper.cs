using System;
using TIK.Core.Exceptions;

namespace TIK.Core.Helpers
{
    public static class StringParserHelper
    {
        public static decimal ToDecimal(this string value, string expCode = "E-00001-01", decimal defValue = 0m)
        {
            decimal result = defValue;
            if (!Decimal.TryParse(value, out result))
            {
                if (!string.IsNullOrEmpty(expCode))
                    throw new SelfException(expCode);
            }

            return result;
        }
    }
}
