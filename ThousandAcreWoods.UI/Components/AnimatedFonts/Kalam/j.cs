
namespace ThousandAcreWoods.UI.Components.AnimatedFonts.Kalam;

public class j : AnimatedLetter
{
    public j() : base(
        paths: [
            new AnimatedLetterPath(
                PathSpecification: @"
                    M 90.55,73.09
                    C 92.36,80.91 71.82,152.91 70.73,160.00
                      69.09,166.91 64.91,177.64 59.27,187.64
                      53.64,193.64 42.00,189.27 39.45,187.27
                      36.91,185.27 36.91,187.82 31.82,178.18",
                PathLength: 200),
            new AnimatedLetterPath(
                PathSpecification: @"
                    M 97.45,32.73
                    C 97.45,32.73 96.00,36.00 102.00,43.09",
                PathLength: 50)

             ],
        letter: "j")
    {
    }

    protected override int ViewBoxWidth => 120;
}
