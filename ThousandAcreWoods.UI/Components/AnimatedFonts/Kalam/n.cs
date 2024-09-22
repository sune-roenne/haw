
namespace ThousandAcreWoods.UI.Components.AnimatedFonts.Kalam;

public class n : AnimatedLetter
{
    public n() : base(
        paths: [
            new AnimatedLetterPath(
                PathSpecification: @"
                    M 38.00,76.18
                    C 42.00,88.00 28.73,149.64 24.18,160.55
                      36.00,153.45 73.27,77.82 89.09,84.73
                      104.91,91.64 101.27,112.18 83.82,166.36",
                PathLength: 395)
             ],
        letter: "n")
    {
    }

    protected override int ViewBoxWidth => 120;
}
