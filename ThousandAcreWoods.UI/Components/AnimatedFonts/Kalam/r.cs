
namespace ThousandAcreWoods.UI.Components.AnimatedFonts.Kalam;

public class r : AnimatedLetter
{
    public r() : base(
        paths: [
            new AnimatedLetterPath(
                PathSpecification: @"
                    M 54.50,79.25
                    C 58.52,102.12 47.11,147.24 44.25,163.50
                      54.00,149.25 55.75,87.50 100.00,77.25",
                PathLength: 255)
             ],
        letter: "r")
    {
    }
    protected override int ViewBoxWidth => 120;
}
