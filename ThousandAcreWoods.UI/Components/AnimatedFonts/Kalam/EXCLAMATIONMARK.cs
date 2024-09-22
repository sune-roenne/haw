
namespace ThousandAcreWoods.UI.Components.AnimatedFonts.Kalam;

public class EXCLAMATIONMARK : AnimatedLetter
{
    public EXCLAMATIONMARK() : base(
        paths: [
            new AnimatedLetterPath(
                PathSpecification: @"
                    M 56.00,30.75
                    C 51.75,60.50 48.50,102.75 54.50,130.00",
                PathLength: 150),
            new AnimatedLetterPath(
                PathSpecification: @"
                    M 53.50,153.75
                    C 53.50,153.75 56.50,162.00 62.00,165.25",
                PathLength: 60)


             ],
        letter: "DOT")
    {
    }
    protected override int ViewBoxWidth => 120;
}
