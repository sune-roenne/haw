
namespace ThousandAcreWoods.UI.Components.AnimatedFonts.Kalam;

public class COMMA : AnimatedLetter
{
    public COMMA() : base(
        paths: [
            new AnimatedLetterPath(
                PathSpecification: @"
                    M 43.45,150.55
                    C 43.45,165.64 27.09,186.36 24.18,191.64",
                PathLength: 200)

             ],
        letter: "COMMA")
    {
    }

    protected override int ViewBoxWidth => 120;
}
