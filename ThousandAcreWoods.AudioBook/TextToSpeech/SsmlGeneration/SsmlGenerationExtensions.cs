using NYK.Collections.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThousandAcreWoods.AudioBook.TextToSpeech.SsmlGeneration;
internal static class SsmlGenerationExtensions
{

    private static readonly CultureInfo EnUs = new CultureInfo("en-US");


    public static string? ToUsFormatString(this decimal? value, int numberOfDecimals = 2) => value?.Pipe(
        val => val.ToString($"F{numberOfDecimals}", EnUs)
        );


    public static string? ToUsFormatString(this decimal value, int numberOfDecimals = 2) => value.ToString($"F{numberOfDecimals}", EnUs);

    public static string? AsPercentChange(this decimal? inPercent) => inPercent switch
    {
        null => null,
        0 => null,
        decimal d => d.AsPercentChange()
    };
    public static string? AsPercentChange(this decimal inPercent) => inPercent switch
    {
        0m => "+1%",
        decimal d when d < 0m => $"{d.ToString("F0", EnUs)}%",
        decimal d => $"+{d.ToString("F0", EnUs)}%"
    };
    public static string OptAttr(this string? attrValue, string attrName) => attrValue.PipeOpt(_ => $" {attrName}=\"{_}\"") ?? "";


}
