using System.Xml;
using ThousandAcreWoods.SVGTools.Model;
using ThousandAcreWoods.SVGTools.Util;

namespace ThousandAcreWoods.SVGTools.Parsing;
public static class SVGParser
{
    public static readonly string SampleDirectory = 
        Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())!.FullName,
            "ThousandAcreWoods.SVGTools",
            "samples"
            );
    public static void ParseSample()
    {


        var returnee = new List<GroupElement>();
        var fileName = Path.Combine(SampleDirectory, "simple-chart.svg");
        var xml = new XmlDocument();
        xml.Load(fileName);

        XmlNamespaceManager ns = new XmlNamespaceManager(xml.NameTable);
        ns.AddNamespace("svg", "http://www.w3.org/2000/svg");
        var gs = xml.SelectNodes("//svg:g", ns)!;
        foreach(var g in gs)
        {
            
            if(g is XmlElement el)
            {
                var parent = el.ParentNode;
                if(el.HasAttribute("data-Animate"))
                {
                    var value = el.GetAttribute("data-Animate");
                    if(value?.Trim()?.ToLower() == "true")
                    {
                        var grp = ParseGroupElement(el, ns);
                        if(grp != null) 
                            returnee.Add(grp);  

                    }
                }
            }
        }
    }

    public static GroupElement? ParseGroupElement(XmlElement el, XmlNamespaceManager ns)
    {
        try
        {
            var (paths, subGroups, attributes) =  (new List<PathElement>(), new List<GroupElement>(), ParseAttributes(el));
            foreach(var p in el.SelectNodes("//svg:path", ns)!.AsList<XmlElement>())
            {
                var pathElementOpt = ParsePathElement(p, ns);
                if (pathElementOpt != null)
                    paths.Add(pathElementOpt);
            }
            foreach (var g in el.SelectNodes("//svg:g", ns)!.AsList<XmlElement>())
            {
                var groupElementOpt = ParseGroupElement(g, ns);
                if (groupElementOpt != null)
                    subGroups.Add(groupElementOpt);
            }
            if(subGroups.Any() || paths.Any() || attributes != null)
                return new GroupElement(Paths: paths, ContainedGroups: subGroups, Attributes: attributes);
        }
        catch(Exception e) 
        {
            var tessa = e;
        }
        return null;
    }



    public static PathElement? ParsePathElement(XmlElement el, XmlNamespaceManager ns)
    {
        if(!el.HasAttribute("d"))
            return null;
        var d = el.GetAttribute("d")!;
        var pathCommands = d.ParseAsPathCommandAttribute();

        return null;

    }



    public static DrawingAttributes? ParseAttributes(XmlElement el)
    {
        var nullString = (string?)null;
        var (stroke, strokeWidth, strokeOpacity, fill, fillOpacity) = (nullString, nullString, nullString, nullString, nullString);
        OptUpdate(StrokeName, ref stroke, el);
        OptUpdate(StrokeWidthName, ref strokeWidth, el);
        OptUpdate(StrokeOpacityName, ref strokeOpacity, el);
        OptUpdate(FillName, ref fill, el);
        OptUpdate(FillOpacityName, ref fillOpacity, el);
        if (new List<string?> { stroke, strokeWidth, strokeOpacity, fill, fillOpacity }.Any(_ => _ != null))
            return new DrawingAttributes(
                Stroke: stroke,
                StrokeWidth: strokeWidth,
                StrokeOpacity: strokeOpacity,
                Fill: fill,
                FillOpacity: fillOpacity
                );
        return null;

    }

    private const string StrokeName = "stroke";
    private const string StrokeWidthName = "stroke-width";
    private const string StrokeOpacityName = "stroke-opacity";
    private const string FillName = "fill";
    private const string FillOpacityName = "fill-opacity";


    private static void OptUpdate(string attributeName, ref string? valueReference, XmlElement el)
    {
        if (el.HasAttribute(attributeName))
            valueReference = el.GetAttribute(attributeName);
    }



}
