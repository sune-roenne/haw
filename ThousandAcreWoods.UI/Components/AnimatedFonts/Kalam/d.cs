﻿
namespace ThousandAcreWoods.UI.Components.AnimatedFonts.Kalam;

public class d : AnimatedLetter
{
    public d() : base(
        paths: [
            new AnimatedLetterPath(
                PathSpecification: @"
                    M 84.25,119.25
                    C 84.25,119.25 85.25,89.88 75.62,79.00
                      66.00,68.12 54.62,77.38 50.12,79.00
                      45.62,80.62 31.38,99.12 25.12,111.25
                      20.25,124.12 15.50,135.12 18.62,146.88
                      21.75,158.62 37.38,161.75 45.00,156.75
                      52.62,151.75 69.50,137.12 77.38,125.00
                      84.88,112.12 87.25,87.75 96.75,58.88
                      103.12,39.38 102.38,6.38 103.38,17.00
                      104.38,27.62 90.50,79.50 88.12,95.75
                      85.75,112.00 76.62,140.12 75.75,160.12",
                PathLength: 495)
             ],
        letter: "d")
    {
    }
    protected override int ViewBoxWidth => 120;
}
