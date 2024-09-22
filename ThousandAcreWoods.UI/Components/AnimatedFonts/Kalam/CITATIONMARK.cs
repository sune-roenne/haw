
namespace ThousandAcreWoods.UI.Components.AnimatedFonts.Kalam;

public class CITATIONMARK : AnimatedLetter
{
    public CITATIONMARK() : base(
        paths: [
            new AnimatedLetterPath(
                PathSpecification: @"
                    M 42.25,40.25
                    C 43.50,50.25 44.75,71.25 44.25,82.25",
                PathLength: 60),
    new AnimatedLetterPath(
                PathSpecification: @"
                    M 68.50,39.75
                    C 70.75,52.00 70.50,76.75 68.75,83.00",
                PathLength: 60)


             ],
        letter: "CITATIONMARK")
    {
    }
    protected override int ViewBoxWidth => 120;
}
