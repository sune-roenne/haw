
namespace ThousandAcreWoods.UI.Components.AnimatedFonts.Kalam;

public class _S : AnimatedLetter
{
    public _S() : base(
        paths: [
            new AnimatedLetterPath(
                PathSpecification: @"
                       M 102.88,32.12
                       C 102.88,32.12 54.38,35.00 42.12,57.12
                         29.88,79.25 101.12,108.38 89.25,136.12
                         82.72,147.23 69.63,155.73 60.38,158.38
                         25.74,168.88 8.12,134.99 8.25,133.00",
                PathLength: 350)
             ],
        letter: "S")
    {
    }

    protected override int ViewBoxWidth => 120;
}

