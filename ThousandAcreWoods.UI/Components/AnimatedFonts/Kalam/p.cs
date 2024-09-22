
namespace ThousandAcreWoods.UI.Components.AnimatedFonts.Kalam;

public class p : AnimatedLetter
{
    public p() : base(
        paths: [
            new AnimatedLetterPath(
                PathSpecification: @"
                    M 43.75,64.50
                    C 50.00,89.00 41.62,117.75 39.88,125.12
                      38.12,132.50 22.62,184.00 21.88,191.62
                      25.00,181.75 41.12,102.00 67.00,80.25
                      83.12,66.75 94.12,70.75 98.00,73.00
                      106.50,80.88 106.00,93.00 101.75,107.12
                      97.50,121.25 86.00,131.62 78.12,138.50
                      70.25,145.38 61.38,149.25 54.88,150.88
                      48.38,152.50 33.25,151.75 31.94,151.62",
                PathLength: 450)
             ],
        letter: "p")
    {
    }
    protected override int ViewBoxWidth => 120;
}
