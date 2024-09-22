
namespace ThousandAcreWoods.UI.Components.AnimatedFonts.Kalam;

public class q : AnimatedLetter
{
    public q() : base(
        paths: [
            new AnimatedLetterPath(
                PathSpecification: @"
                    M 101.25,80.00
                    C 95.25,70.00 89.75,66.25 80.25,68.75
                      70.75,71.25 49.25,89.75 40.00,108.00
                      30.75,126.25 31.50,129.75 31.25,139.25
                      31.00,148.75 41.75,153.75 52.50,149.75
                      63.25,145.75 90.25,124.25 97.00,103.50
                      91.00,139.50 70.50,177.75 66.50,189.00
                      95.00,180.25 92.75,175.25 99.00,169.00",
                PathLength: 495)
             ],
        letter: "q")
    {
    }

    protected override int ViewBoxWidth => 120;
}
