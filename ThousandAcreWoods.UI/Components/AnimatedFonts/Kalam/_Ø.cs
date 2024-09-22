﻿
namespace ThousandAcreWoods.UI.Components.AnimatedFonts.Kalam;

public class _Ø : AnimatedLetter
{
    public _Ø() : base(
        paths: [
            new AnimatedLetterPath(
                PathSpecification: @"
                       M 98.62,51.50
                       C 98.62,51.50 88.25,34.25 69.38,35.50
                         50.50,36.75 21.00,80.25 14.88,116.62
                         8.75,153.00 35.25,160.00 37.50,160.12
                         39.75,160.25 82.50,156.12 105.12,96.12
                         110.00,79.64 103.82,58.09 97.65,51.65",
                PathLength: 420),
            new AnimatedLetterPath(
                PathSpecification: @"
                       M 115.64,34.55
                       C 106.70,40.86 10.27,154.64 5.18,164.73",
                PathLength: 180)

             ],
        letter: "Ø")
    {
    }
    protected override int ViewBoxWidth => 120;
}

