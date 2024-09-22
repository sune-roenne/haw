
namespace ThousandAcreWoods.UI.Components.AnimatedFonts.Kalam;

public class e : AnimatedLetter
{
    public e() : base(
        paths: [
            new AnimatedLetterPath(
                PathSpecification: @"
                    M 30.88,111.88
                    C 30.88,111.88 82.12,121.62 91.62,107.75
                      101.12,93.88 72.12,69.50 63.75,74.25
                      53.88,77.50 27.00,109.25 24.25,127.25
                      21.50,145.25 33.62,153.12 39.50,156.38
                      48.00,160.88 76.38,159.50 87.00,145.62",
                PathLength: 295)
             ],
        letter: "e")
    {
    }
    protected override int ViewBoxWidth => 120;
}
