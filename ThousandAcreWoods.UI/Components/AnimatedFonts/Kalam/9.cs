
namespace ThousandAcreWoods.UI.Components.AnimatedFonts.Kalam;

public class NINE : AnimatedLetter
{
    public NINE() : base(
        paths: [
            new AnimatedLetterPath(
                PathSpecification: @"
                    M 104.25,40.75
                    C 92.00,62.75 56.75,123.50 24.75,115.50
                      -7.25,107.50 14.00,63.00 25.00,49.50
                      39.25,32.50 75.00,10.25 87.50,23.25
                      101.75,37.75 95.75,43.75 95.75,70.75
                      95.75,97.75 98.25,141.00 95.75,163.25",
                PathLength: 600)

             ],
        letter: "9")
    {
    }
    protected override int ViewBoxWidth => 120;
}
