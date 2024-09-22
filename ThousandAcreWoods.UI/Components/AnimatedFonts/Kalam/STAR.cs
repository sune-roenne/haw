
namespace ThousandAcreWoods.UI.Components.AnimatedFonts.Kalam;

public class STAR : AnimatedLetter
{
    public STAR() : base(
        paths: [
            new AnimatedLetterPath(
                PathSpecification: @"
                    M 55.75,78.75
                    C 56.75,113.25 57.75,133.00 55.25,154.25",
                PathLength: 100),
            new AnimatedLetterPath(
                PathSpecification: @"
                    M 26.50,88.25
                    C 37.00,100.25 73.25,123.50 88.50,133.00",
                PathLength: 100),
            new AnimatedLetterPath(
                PathSpecification: @"
                    M 29.75,132.75
                    C 22.50,143.50 59.50,103.50 84.25,95.25",
                PathLength: 100)


             ],
        letter: "STAR")
    {
    }

    protected override int ViewBoxWidth => 120;
}
