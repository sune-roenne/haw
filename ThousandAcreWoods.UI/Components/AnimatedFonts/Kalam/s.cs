
namespace ThousandAcreWoods.UI.Components.AnimatedFonts.Kalam;

public class s : AnimatedLetter
{
    public s() : base(
        paths: [
            new AnimatedLetterPath(
                PathSpecification: @"
                    M 93.25,90.50
                    C 83.25,74.75 67.00,81.75 62.25,85.00
                      56.50,88.00 43.50,97.75 43.75,107.25
                      43.25,123.75 90.75,131.00 81.50,150.50
                      71.75,164.50 50.50,170.25 22.50,152.25",
                PathLength: 495)
             ],
        letter: "s")
    {
    }
    protected override int ViewBoxWidth => 120;
}
