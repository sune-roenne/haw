using NYK.Collections.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ThousandAcreWoods.UI.Components.Util;

namespace ThousandAcreWoods.UI.Components.Common;
public abstract record ColorSpecification
{
    public abstract string AsCssString { get; }

    public RgbColorSpecification ToRgb() => this switch
    {
        RgbColorSpecification rgb => rgb,
        HexColorSpecification hex => new RgbColorSpecification(hex.RedHex, hex.GreenHex, hex.BlueHex),
        _ => throw new NotImplementedException() 
    };

}

public record HexColorSpecification(int RedHex, int GreenHex, int BlueHex) : ColorSpecification
{
    public HexColorSpecification(string rgbString) : this(Parse(rgbString)) { }


    private static int Num(string hexNum) => int.Parse(hexNum, NumberStyles.HexNumber);

    public static HexColorSpecification Parse(string hexString)
    {
        hexString = hexString
            .Trim()
            .ToLower()
            .Replace("#", "");
        var splitted = hexString
            .Chunk(2)
            .Select(_ => _.MakeString(""))
            .ToArray();
        var (r,g,b) = (Num(splitted[0]), Num(splitted[1]),  Num(splitted[2]));
        return new HexColorSpecification(r,g,b);
    }

    public override string AsCssString => ToRgb().AsCssString;

    public override string ToString() => new int[] {RedHex, GreenHex, BlueHex}
       .Select(_ => _.ToString("X"))
       .MakeString("#","","");

}


public record RgbColorSpecification(int Red, int Green, int Blue, decimal? Opacity = null) : ColorSpecification
{
    public RgbColorSpecification(string rgbString) : this(Parse(rgbString)) { }



    private static readonly Regex RgbCleanerRegex = new Regex("[^0-9,\\.]", RegexOptions.IgnoreCase);

    private static readonly CultureInfo EnUs = CultureInfo.GetCultureInfo("en-US");

    private static decimal Num(string numStr) => decimal.Parse(numStr,EnUs);
    public static RgbColorSpecification Parse(string rgbString)
    {
        rgbString = RgbCleanerRegex.Replace(rgbString, "");
        var split = rgbString.Split(',');
        var (rStr, gStr, bStr, opStr) = (split[0], split[1], split[2], split.Length > 3 ? split[3] : null);
        var (r, g, b, op) = ((int)Num(rStr), (int)Num(gStr), (int)Num(bStr), opStr?.PipeOpt(Num));
        return new RgbColorSpecification(r, g, b, op);
    }




    public override string AsCssString => Opacity switch
    {
        decimal op => $"rgba({Red},{Green},{Blue},{op.AsCssNumber()})",
        null => $"rgb({Red},{Green},{Blue})"
    };
}
