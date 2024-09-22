
namespace ThousandAcreWoods.UI.Components.AnimatedFonts.Kalam;

public class HASHTAG : AnimatedLetter
{
    public HASHTAG() : base(
        paths: [
            new AnimatedLetterPath(
                PathSpecification: @"
                    M 17.50,81.50
                    C 43.75,78.75 90.00,80.00 112.25,76.75",
                PathLength: 120),
            new AnimatedLetterPath(
                PathSpecification: @"
                    M 7.00,122.75
                    C 30.25,121.25 101.00,119.75 110.00,114.25",
                PathLength: 120),
            new AnimatedLetterPath(
                PathSpecification: @"
                    M 52.50,31.25
                    C 52.00,57.25 30.00,98.00 27.25,167.25",
                PathLength: 150),
            new AnimatedLetterPath(
                PathSpecification: @"
                    M 87.50,32.25
                    C 88.75,60.00 76.00,96.25 73.00,167.00",
                PathLength: 150)
             ],
        letter: "HASHTAG")
    {
    }
    protected override int ViewBoxWidth => 150;
}
