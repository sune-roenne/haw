
namespace ThousandAcreWoods.UI.Components.AnimatedFonts.Kalam;

public class SIX : AnimatedLetter
{
    public SIX() : base(
        paths: [
            new AnimatedLetterPath(
                PathSpecification: @"
                    M 77.82,33.82
                    C 35.27,53.27 16.36,103.82 24.18,134.36
                      29.82,153.45 45.09,170.55 81.45,151.09
                      117.82,131.64 114.55,103.27 108.18,98.18
                      101.82,93.09 68.00,95.27 62.91,134.18
                      59.06,157.69 59.06,165.12 63.09,174.91",
                PathLength: 500)

             ],
        letter: "6")
    {
    }
    protected override int ViewBoxWidth => 120;
}
