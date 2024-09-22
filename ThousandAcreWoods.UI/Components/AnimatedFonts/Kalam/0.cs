
namespace ThousandAcreWoods.UI.Components.AnimatedFonts.Kalam;

public class ZERO : AnimatedLetter
{
    public ZERO() : base(
        paths: [
            new AnimatedLetterPath(
                PathSpecification: @"
                    M 45.50,34.50
                    C 32.00,66.00 16.00,108.50 24.50,146.50
                      33.00,184.50 92.00,147.50 90.50,136.00
                      103.50,112.00 111.00,83.00 95.50,58.00
                      78.50,31.00 41.00,41.50 34.00,43.50",
                PathLength: 400)

             ],
        letter: "0")
    {
    }
    protected override int ViewBoxWidth => 120;
}
