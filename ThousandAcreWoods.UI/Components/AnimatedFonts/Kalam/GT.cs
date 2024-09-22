
namespace ThousandAcreWoods.UI.Components.AnimatedFonts.Kalam;

public class GT : AnimatedLetter
{
    public GT() : base(
        paths: [
            new AnimatedLetterPath(
                PathSpecification: @"
                    M 30.25,91.25
                    C 50.75,107.00 86.50,131.75 96.50,141.50
                      87.00,149.00 38.75,161.50 25.25,167.00",
                PathLength: 200)

             ],
        letter: "GT")
    {
    }

    protected override int ViewBoxWidth => 120;
}
