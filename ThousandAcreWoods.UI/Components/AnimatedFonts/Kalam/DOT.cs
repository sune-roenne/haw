
namespace ThousandAcreWoods.UI.Components.AnimatedFonts.Kalam;

public class DOT : AnimatedLetter
{
    public DOT() : base(
        paths: [
            new AnimatedLetterPath(
                PathSpecification: @"
                    M 38.36,154.91
                    C 40.91,163.45 44.18,168.18 47.45,171.82",
                PathLength: 60)

             ],
        letter: "DOT")
    {
    }
    protected override int ViewBoxWidth => 80;
}
