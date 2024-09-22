using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThousandAcreWoods.Domain.Util;

namespace ThousandAcreWoods.SVGTools.Model;
public record struct Point(
    decimal X,
    decimal Y
    )
{
    public static Point operator +(Point a) => a;
    public static Point operator -(Point a) => new Point(-a.X, -a.Y);
    public static Point operator +(Point a, Point b) => new Point(a.X + b.X, a.Y + b.Y);
    public static Point operator -(Point a, Point b) => new Point(a.X - b.X, a.Y - b.Y);
    public static Point operator *(decimal factor, Point a) => new Point(a.X * factor, a.Y  * factor);
    public static Point operator *(Point a, decimal factor) => new Point(a.X * factor, a.Y * factor);
    public static Point? operator /(Point a, decimal divisor) => divisor == 0m ? null : new Point(a.X / divisor, a.Y / divisor);

    public static Point Origin = new Point(0m, 0m);

    public decimal DistanceTo(Point other) =>(
        (X - other.X).Pow(2) + 
        (Y - other.Y).Pow(2)
    ).Sqrt();


    public override string ToString() => $"X: {X}, Y: {Y}";


}
