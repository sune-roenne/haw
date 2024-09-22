using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThousandAcreWoods.Domain.Util;
public static class MathExtensions
{

    public static decimal? SafeDivide(this decimal dividend, decimal divisor, decimal? ifZero = null) => divisor == 0m ? 
        ifZero : 
        (dividend / divisor);

    public static decimal Pow(this decimal val, int power) =>
        new RichDecimal(val) ^ power;

    public static decimal Sqrt(this decimal val) =>
        new RichDecimal(val).Sqrt();

    public record struct RichDecimal(decimal D)
    {

        public static implicit operator RichDecimal(decimal d) => new RichDecimal(d);

        public static decimal operator ^(RichDecimal left, int right) =>
            (decimal) Math.Pow((double)left.D, right);
        public decimal Pow(RichDecimal left, int right) =>
            (decimal)Math.Pow((double)left.D, right);

        public decimal Sqrt() =>
            (decimal)Math.Sqrt((double) D);


    }






}
