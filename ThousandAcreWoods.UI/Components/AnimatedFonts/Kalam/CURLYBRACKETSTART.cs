﻿
namespace ThousandAcreWoods.UI.Components.AnimatedFonts.Kalam;

public class CURLYBRACKETSTART : AnimatedLetter
{
    public CURLYBRACKETSTART() : base(
        paths: [
            new AnimatedLetterPath(
                PathSpecification: @"
                   M 53.67,6.00
                   C 46.00,18.00 23.00,29.67 34.67,55.67
                     36.75,60.56 42.88,65.88 45.00,69.33
                     38.44,74.19 27.81,76.38 19.38,83.00
                     21.38,91.12 35.44,93.00 43.00,96.00
                     42.94,100.62 19.50,111.81 16.19,137.94
                     12.88,164.06 33.88,183.81 60.31,169.06",
                PathLength: 400)

             ],
        letter: "CURLYBRACKETSTART")
    {
    }
    protected override int ViewBoxWidth => 80;
}
