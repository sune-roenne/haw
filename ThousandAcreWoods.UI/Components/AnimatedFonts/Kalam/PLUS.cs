
namespace ThousandAcreWoods.UI.Components.AnimatedFonts.Kalam;

public class PLUS : AnimatedLetter
{
    public PLUS() : base(
        paths: [
            new AnimatedLetterPath(
                PathSpecification: @"
                    M 60.00,54.50
                    C 63.50,64.00 57.50,122.50 62.50,147.50",
                PathLength: 110),
            new AnimatedLetterPath(
                PathSpecification: @"
                    M 18.50,106.00
                    C 35.00,105.50 89.50,99.00 95.00,95.00
                      100.50,91.00 93.00,90.00 93.00,90.00",
                PathLength: 110)
             ],
        letter: "PLUS")
    {
    }

    protected override int ViewBoxWidth => 120;
}
