using System.Globalization;

namespace ThousandAcreWoods.UI.Components.Util;

public static class NumberExtensions
{
    //public static string AsCssPercent(this double value)
    private static CultureInfo Us = CultureInfo.GetCultureInfo("en-US");

    public static string AsCssPercent(this double value) => AsCssPercent(Convert.ToDecimal(value));
    public static string AsCssPercent(this decimal value) => (value * 100m).ToString("F2", Us) + "%";

    public static string AsCssSeconds(this double value) => AsCssSeconds(Convert.ToDecimal(value));
    public static string AsCssSeconds(this decimal value) => value.ToString("F2", Us) + "s";

    public static string AsCssNumber(this double value) => AsCssNumber(Convert.ToDecimal(value));
    public static string AsCssNumber(this decimal value) => value.ToString("F2", Us);
}
