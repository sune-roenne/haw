
namespace ThousandAcreWoods.UI.Components.AnimatedFonts.Kalam;

public class SEMICOLON : AnimatedLetter
{
    public SEMICOLON() : base(
        paths: [
            new AnimatedLetterPath(
                PathSpecification: @"
                    M 42.73,155.64
                    C 42.36,169.82 27.82,188.91 24.00,194.18",
                PathLength: 100),
            new AnimatedLetterPath(
                PathSpecification: @"
                    M 33.27,87.09
                    C 34.55,92.73 39.64,96.18 39.82,103.45",
                PathLength: 70)


             ],
        letter: "SEMICOLON")
    {
    }
    protected override int ViewBoxWidth => 120;
}
