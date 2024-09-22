
namespace ThousandAcreWoods.UI.Components.AnimatedFonts.Kalam;

public class QUESTIONMARK : AnimatedLetter
{
    public QUESTIONMARK() : base(
        paths: [
            new AnimatedLetterPath(
                PathSpecification: @"
                   M 17.50,71.50
                   C 37.75,55.75 62.00,39.25 86.50,47.75
                     111.25,60.00 75.50,85.50 66.75,94.50
                     45.75,113.75 57.00,124.25 59.00,131.75",
                PathLength: 180),
            new AnimatedLetterPath(
                PathSpecification: @"
                   M 63.25,152.25
                   C 63.25,152.25 61.00,156.25 63.00,161.75",
                PathLength: 60)


             ],
        letter: "QUESTIONMARK")
    {
    }
    protected override int ViewBoxWidth => 120;
}
