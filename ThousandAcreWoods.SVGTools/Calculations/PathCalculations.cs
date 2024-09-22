using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThousandAcreWoods.SVGTools.Model;
using ThousandAcreWoods.Domain.Util;

namespace ThousandAcreWoods.SVGTools.Calculations;
public static class PathCalculations
{



    public static decimal CalculateLength(PathLine line)
    {
        var currentState = new CommandEvaluationResult(Point.Origin, 0m);
        foreach (var comm in line.Commands)
            currentState += Simulate(comm, currentState.NextPoint);
        return currentState.Length;
    }

    private static CommandEvaluationResult Simulate(PathCommand command, Point currentPoint) => command switch
    {
        ClosePathCommand comm => Simulate(comm, currentPoint),
        CubicBezierAbsoluteCommand comm => Simulate(comm, currentPoint),
        CubicBezierRelativeCommand comm => Simulate(comm, currentPoint),
        LineToAbsoluteCommand comm => Simulate(comm, currentPoint),
        LineToRelativeCommand comm => Simulate(comm, currentPoint),
        MoveAbsoluteCommand comm => Simulate(comm),
        MoveRelativeCommand comm => Simulate(comm, currentPoint),
        QuadraticBezierAbsoluteCommand comm => Simulate(comm, currentPoint),
        QuadraticBezierRelativeCommand comm => Simulate(comm, currentPoint),
        _ => throw new NotImplementedException()
    };


    private static CommandEvaluationResult Simulate(ClosePathCommand command, Point currentPoint) => new CommandEvaluationResult(currentPoint, 0m);
    private static CommandEvaluationResult Simulate(CubicBezierAbsoluteCommand command, Point currentPoint) => new CommandEvaluationResult(
        NextPoint: command.EndPoint,
        CalculateLengthOfCubicCurve(
            start: currentPoint, 
            controlStart: command.StartControlPoint, 
            controlEnd: command.EndControlPoint, 
            end: command.EndControlPoint
        ));

    private static CommandEvaluationResult Simulate(CubicBezierRelativeCommand command, Point currentPoint) => new CommandEvaluationResult(
        NextPoint: currentPoint + command.EndPointOffset,
        CalculateLengthOfCubicCurve(
            start: currentPoint,
            controlStart: currentPoint + command.StartControlPointOffset,
            controlEnd: currentPoint + currentPoint + command.EndControlPointOffset,
            end: currentPoint + command.EndPointOffset
        ));



    private static CommandEvaluationResult Simulate(LineToAbsoluteCommand command, Point currentPoint) => new CommandEvaluationResult(
        NextPoint: command.Point,
        Length: currentPoint.DistanceTo(command.Point)
        );
    private static CommandEvaluationResult Simulate(LineToRelativeCommand command, Point currentPoint) => new CommandEvaluationResult(
        NextPoint: currentPoint + command.OffsetBy,
        Length: currentPoint.DistanceTo(currentPoint + command.OffsetBy)
        );

    private static CommandEvaluationResult Simulate(MoveAbsoluteCommand command) => new CommandEvaluationResult(command.MoveTo, 0m);
    private static CommandEvaluationResult Simulate(MoveRelativeCommand command, Point currentPoint) => new CommandEvaluationResult(currentPoint + command.OffsetBy, 0m);

    private static CommandEvaluationResult Simulate(QuadraticBezierAbsoluteCommand command, Point currentPoint) => new CommandEvaluationResult(
        NextPoint: command.EndPoint,
        Length: CalculateLengthOfQuadrativCurve(
            start: currentPoint,
            controlPoint: command.ControlPoint,
            end: command.EndPoint
            ));

    private static CommandEvaluationResult Simulate(QuadraticBezierRelativeCommand command, Point currentPoint) => new CommandEvaluationResult(
        NextPoint: currentPoint + command.EndPointOffset,
        Length: CalculateLengthOfQuadrativCurve(
            start: currentPoint,
            controlPoint: currentPoint + command.ControlPointOffset,
            end: currentPoint + command.EndPointOffset
            ));


    public static decimal CalculateLengthOfQuadrativCurve(Point start, Point controlPoint, Point end, int numberOfPointsForCalculation = 100)
    {
        var returnee = 0m;
        var stepSize = 1m / numberOfPointsForCalculation;
        Point? lastPoint = null;
        for (var curStep = 0m; curStep <= 1m; curStep += stepSize)
        {
            var curPoint =
                controlPoint +
                (1m - curStep).Pow(2) * (start - controlPoint) + 
                curStep.Pow(2) * (end - controlPoint);
            if (lastPoint != null)
            {
                returnee += curPoint.DistanceTo(lastPoint.Value);
            }
            lastPoint = curPoint;
        }
        return returnee;
    }


    public static decimal CalculateLengthOfCubicCurve(Point start, Point controlStart, Point controlEnd, Point end, int numberOfPointsForCalculation = 100)
    {
        var returnee = 0m;
        var stepSize = 1m / numberOfPointsForCalculation;
        Point? lastPoint = null;
        for (var curStep = 0m; curStep <= 1m; curStep += stepSize)
        {
            var curPoint =
                (1m - curStep).Pow(3) * start +
                3m * (1m - curStep).Pow(2) * curStep * controlStart +
                3m * (1m - curStep) * curStep.Pow(2) * controlEnd +
                curStep.Pow(3) * end;
            if (lastPoint != null)
            {
                returnee += curPoint.DistanceTo(lastPoint.Value);
            }
            lastPoint = curPoint;
        }
        return returnee;
    }

    private record CommandEvaluationResult(
        Point NextPoint,
        decimal Length
        )
    {
        public static CommandEvaluationResult operator +(CommandEvaluationResult oldRes, CommandEvaluationResult newRes) => newRes with
        {
            Length = oldRes.Length + newRes.Length
        };
    }

}
