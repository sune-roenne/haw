using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ThousandAcreWoods.SVGTools.Model;

namespace ThousandAcreWoods.SVGTools.Parsing;
internal static class SvgPathParser
{

    public static IReadOnlyCollection<PathCommand> ParseAsPathCommandAttribute(this string commandString)
    {
        var returnee = new List<PathCommand>();
        var allTokens = AnyTokenRegex.Matches(commandString);
        foreach (var token in allTokens)
        {
            var tokenString = token.ToString()!;
            if (MoveRegex.Matches(tokenString).Any())
            {
                var parsed = ParseAsMove(tokenString);
                returnee.AddRange(parsed);
            }
            else if (LineRegex.Matches(tokenString).Any())
            {
                var parsed = ParseAsLine(tokenString);
                returnee.AddRange(parsed);
            }
            else if (CubicBezierRegex.Matches(tokenString).Any())
            {
                var parsed = ParseAsCubicBezier(tokenString);
                returnee.AddRange(parsed);
            }
            else if (QuadBezierRegex.Matches(tokenString).Any())
            {
                var parsed = ParseAsCubicBezier(tokenString);
                returnee.AddRange(parsed);
            }
            else if(ClosePathRegex.Matches(tokenString).Any()) 
            {
                returnee.Add(new ClosePathCommand());
            }

        }

        return returnee; 
    }

    private static CultureInfo US = CultureInfo.GetCultureInfo("en-US");
    private static Regex SingleNumberRegex = BuildRegex([RegNum()]);
    private static Regex SinglePointRegex = BuildRegex([RegPnt()]);
    private static Regex DoublePointRegex = BuildRegex([RegPnt(), RegPnt()]);
    private static Regex TriplePointRegex = BuildRegex([RegPnt(), RegPnt(), RegPnt()]);

    private static decimal ParseSingleNumber(string str)
    {
        str = str.Trim();
        var returnee = decimal.Parse(str, US);
        return returnee;
    }

    private static readonly Regex PointXYSepatatorRegex = new Regex("(,|( +))");
    private static Point ParseSinglePoint(string str)
    {
        str = str.Trim();
        var split = PointXYSepatatorRegex.Split(str)
            .Select(_ => _.Trim().Replace(",", ""))
            .Where(_ => _.Length > 0)
            .ToArray();
        var x = ParseSingleNumber(split[0]);
        var y = ParseSingleNumber(split[1]);
        return new Point(x, y);
    }

    private static (Point Point1, Point Point2) ParseDoublePoints(string str)
    {
        var points = SinglePointRegex.Matches(str)
            .Select(_ => _.ToString()!)
            .Select(ParseSinglePoint)
            .ToArray();
        var returnee = (points[0], points[1]);
        return returnee;
    }
    private static (Point Point1, Point Point2, Point Point3) ParseTriplePoints(string str)
    {
        var points = SinglePointRegex.Matches(str)
            .Select(_ => _.ToString()!)
            .Select(ParseSinglePoint)
            .ToArray();
        var returnee = (points[0], points[1], points[2]);
        return returnee;
    }

    private static Func<TVal, PathCommand> Either<TVal>(string str, string letter, Func<TVal, PathCommand> ifUpper, Func<TVal, PathCommand> ifLower) =>
        str.Contains(letter.ToLower()) ? ifLower : ifUpper;

    private static IReadOnlyCollection<PathCommand> ParseAsMove(string str) => ParseAsCommand(
        str, 
        SinglePointRegex, 
        ParseSinglePoint,
        Either<Point>(str, "m", _ => new MoveAbsoluteCommand(_), _ => new MoveRelativeCommand(_)));
    private static IReadOnlyCollection<PathCommand> ParseAsLine(string str) => ParseAsCommand(
        str,
        SinglePointRegex,
        ParseSinglePoint,
        Either<Point>(str, "l", _ => new LineToAbsoluteCommand(_), _ => new LineToRelativeCommand(_)));
    private static IReadOnlyCollection<PathCommand> ParseAsVertLine(string str) => ParseAsCommand(
        str,
        SingleNumberRegex,
        ParseSingleNumber,
        Either<decimal>(str, "v", _ => new LineToAbsoluteCommand(new Point(0m,_)), _ => new LineToRelativeCommand(new Point(0m,_))));
    private static IReadOnlyCollection<PathCommand> ParseAsHoriLine(string str) => ParseAsCommand(
        str,
        SingleNumberRegex,
        ParseSingleNumber,
        Either<decimal>(str, "h", _ => new LineToAbsoluteCommand(new Point(_,0m)), _ => new LineToRelativeCommand(new Point(_,0m))));
    private static IReadOnlyCollection<PathCommand> ParseAsCubicBezier(string str) => ParseAsCommand(
        str,
        TriplePointRegex,
        ParseTriplePoints,
        Either<(Point,Point,Point)>(str, "c", _ => new CubicBezierAbsoluteCommand(_.Item1, _.Item2, _.Item3), _ => new CubicBezierRelativeCommand(_.Item1, _.Item2, _.Item3)));
    private static IReadOnlyCollection<PathCommand> ParseAsQuadBezier(string str) => ParseAsCommand(
        str,
        DoublePointRegex,
        ParseDoublePoints,
        Either<(Point, Point)>(str, "q", _ => new QuadraticBezierAbsoluteCommand(_.Item1, _.Item2), _ => new QuadraticBezierRelativeCommand(_.Item1, _.Item2)));




    private static IReadOnlyCollection<PathCommand> ParseAsCommand<TVal>(string str, Regex regex, Func<string, TVal> valExtractor, Func<TVal, PathCommand> commandCreator )
    {
        var values = new List<TVal>();
        foreach(var m in regex.Matches(str))
        {
            values.Add(valExtractor(m.ToString()!));
        }
        var returnee = values
            .Select(val => commandCreator(val))
            .ToList();

        return returnee;
    }



    private static readonly RegexPart[] MoveRegexParts = [RegLet("M"), RegMultStart(), RegPnt(), RegMultEnd()];
    private static readonly RegexPart[] LineRegexParts = [RegLet("L"), RegMultStart(), RegPnt(), RegMultEnd()];
    private static readonly RegexPart[] LineVertRegexParts = [RegLet("V"), RegMultStart(), RegNum(), RegMultEnd()];
    private static readonly RegexPart[] LineHoriRegexParts = [RegLet("H"), RegMultStart(), RegNum(), RegMultEnd()];
    private static readonly RegexPart[] CubicBezierRegexParts = [RegLet("C"), RegMultStart(), RegPnt(), RegPnt(), RegPnt(), RegMultEnd()];
    private static readonly RegexPart[] CubicBezierSmoothRegexParts = [RegLet("S"), RegMultStart(), RegPnt(), RegPnt(), RegMultEnd()];
    private static readonly RegexPart[] QuadBezierRegexParts = [RegLet("Q"), RegMultStart(), RegPnt(), RegPnt(), RegMultEnd()];
    private static readonly RegexPart[] QuadBezierSmoothRegexParts = [RegLet("T"), RegMultStart(), RegPnt(), RegMultEnd()];
    private static readonly RegexPart[] CloseRegexParts = [RegLet("Z")];

    private static readonly RegexPart[] AnyTokenRegexParts = [
        RegOptStart(), .. MoveRegexParts, RegOptEnd(),
        RegOptStart(), .. LineRegexParts, RegOptEnd(),
        RegOptStart(), .. LineVertRegexParts, RegOptEnd(),
        RegOptStart(), .. LineHoriRegexParts, RegOptEnd(),
        RegOptStart(), .. CubicBezierRegexParts, RegOptEnd(),
        RegOptStart(), .. CubicBezierSmoothRegexParts, RegOptEnd(),
        RegOptStart(), .. QuadBezierRegexParts, RegOptEnd(),
        RegOptStart(), .. QuadBezierSmoothRegexParts, RegOptEnd(),
        RegOptStart(), .. CloseRegexParts, RegOptEnd()
        ];


    private static readonly Regex NumRegex = new Regex(NumStr);
    private static readonly Regex MoveRegex = BuildRegex(MoveRegexParts);
    private static readonly Regex LineRegex = BuildRegex(LineRegexParts);
    private static readonly Regex LineVertRegex = BuildRegex(LineVertRegexParts);
    private static readonly Regex LineHoriRegex = BuildRegex(LineHoriRegexParts);
    private static readonly Regex CubicBezierRegex = BuildRegex(CubicBezierRegexParts);
    private static readonly Regex CubicBezierSmoothRegex = BuildRegex(CubicBezierSmoothRegexParts);
    private static readonly Regex QuadBezierRegex = BuildRegex(QuadBezierRegexParts);
    private static readonly Regex QuadBezierSmoothRegex = BuildRegex(QuadBezierSmoothRegexParts);
    private static readonly Regex ClosePathRegex = BuildRegex(CloseRegexParts);
    private static readonly Regex AnyTokenRegex = BuildRegex(AnyTokenRegexParts);


    private const string NumStr = @"-? *\d+(\.\d+)? *";


    private static Regex BuildRegex(IEnumerable<RegexPart> parts)
    {
        var pattern = new StringBuilder();
        var lastTokenWasEndOption = false;
        foreach (var part in parts)
        {
            if (part is RegexIdLetter idLet)
                pattern.Append($"({idLet.IdLetter.ToUpper()}|{idLet.IdLetter.ToLower()}) *");
            else if (part is RegexPoint pnt && pnt.IsMandatory)
                pattern.Append($"{NumStr}(,|( +)){NumStr}");
            else if (part is RegexPoint)
                pattern.Append($"({NumStr}(,|( +)){NumStr})?");
            else if (part is RegexNumber nm && nm.IsMandatory)
                pattern.Append($"{NumStr}");
            else if (part is RegexNumber)
                pattern.Append($"({NumStr})?");
            else if (part is RegexMultiStart)
                pattern.Append("(");
            else if (part is RegexMultiEnd)
                pattern.Append(")+");
            else if (part is RegexOptionStart && lastTokenWasEndOption)
                pattern.Append("|(");
            else if (part is RegexOptionStart)
                pattern.Append("(");
            else if (part is RegexOptionEnd)
                pattern.Append(")");

            if (part is RegexOptionEnd)
                lastTokenWasEndOption = true;
            else lastTokenWasEndOption = false;
        }
        var returne = new Regex(pattern.ToString());
        return returne;

    }


    private abstract record RegexPart();
    private record RegexIdLetter(string IdLetter) : RegexPart();
    private record RegexPoint(bool IsMandatory = true) : RegexPart();
    private record RegexNumber(bool IsMandatory = true) : RegexPart();
    private record RegexMultiStart() : RegexPart();
    private record RegexMultiEnd() : RegexPart();
    private record RegexOptionStart() : RegexPart();
    private record RegexOptionEnd() : RegexPart();

    private static RegexIdLetter RegLet(string IdLetter) => new RegexIdLetter(IdLetter);
    private static RegexPoint RegPnt(bool isMandatory = true) => new RegexPoint(isMandatory);
    private static RegexNumber RegNum(bool isMandatory = true) => new RegexNumber(isMandatory);
    private static RegexMultiStart RegMultStart() => new RegexMultiStart();
    private static RegexMultiEnd RegMultEnd() => new RegexMultiEnd();

    private static RegexOptionStart RegOptStart() => new RegexOptionStart();
    private static RegexOptionEnd RegOptEnd() => new RegexOptionEnd();

}
