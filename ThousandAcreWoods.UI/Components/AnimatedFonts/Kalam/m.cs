
namespace ThousandAcreWoods.UI.Components.AnimatedFonts.Kalam;

public class m : AnimatedLetter
{
    public m() : base(
        paths: [
            new AnimatedLetterPath(
                PathSpecification: @"
                    M 23.64,70.36
                    C 26.55,82.00 11.64,143.64 12.00,152.55
                      21.09,147.09 56.18,72.91 77.64,76.36
                      98.18,78.36 72.00,146.00 72.18,150.91
                      79.82,142.91 105.27,76.91 130.18,78.36
                      151.45,79.82 127.27,141.64 125.27,155.45",
                PathLength: 495)
             ],
        letter: "m")
    {
    }
    protected override int ViewBoxWidth => 150;
}
