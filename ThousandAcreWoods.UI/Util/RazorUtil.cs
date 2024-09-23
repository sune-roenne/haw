using Microsoft.AspNetCore.Components;
using ThousandAcreWoods.Language.Extensions;

namespace ThousandAcreWoods.UI.Util;

public static class RazorUtil
{

    public static string CssRule(this ElementId elementId, string className, IEnumerable<(string Key, string Rule)> cssBody, IEnumerable<(string Key, string Rule)>? before = null) =>
$@".{elementId.IdFor(className)} {{
   {cssBody.CssFormat()}
   {before.PipeOpt(bef => 
        $@"&:: {{
           {bef.CssFormat()}           
        }}")}
}}";

    public static string CssAnimationRule(this ElementId elementId, string animationName, IEnumerable<(int Percentage,  IEnumerable<(string Key, string Rule)> Rules)> timepoints) =>
$@"@keyframes {elementId.IdFor(animationName)} {{
   {timepoints
        .Select(tp => $"{tp.Percentage}% {{ \r\n  { tp.Rules.CssFormat()} \r\n }} " ) 
        .MakeString("\r\n")
    }
}}";


    public static string CssFormat(this IEnumerable<(string Key, string Rule)> cssBody) => $@"{cssBody.Select(p => $"{p.Key}: {p.Rule};").MakeString("\r\n")}";



}
