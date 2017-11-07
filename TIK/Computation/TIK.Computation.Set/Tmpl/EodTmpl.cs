using System;
using FileHelpers;
namespace TIK.Computation.Set.Tmpl
{
    [DelimitedRecord(","), IgnoreFirst(1)]
    [IgnoreEmptyLines(true)]
    public class EodTmpl
    {
        public string Symbol;

        [FieldConverter(ConverterKind.Date, "yyyyMMdd")]
        public DateTime EodDate;

        public decimal Open;

        public decimal High;

        public decimal Low;

        public decimal Close;

        public Int64 Volumn;

    }
}
 