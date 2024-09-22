
namespace ThousandAcreWoods.UI.Components.AnimatedFonts.Kalam;

public class BRACKETSTART : AnimatedLetter
{
    public BRACKETSTART() : base(
        paths: [
            new AnimatedLetterPath(
                PathSpecification: @"
                   M 53.67,15.67
                   C 40.00,15.33 26.33,27.00 16.67,33.00
                     17.00,62.00 19.67,163.00 19.67,173.00
                     41.33,172.00 49.33,165.67 63.33,165.00",
                PathLength: 250)

             ],
        letter: "BRACKETSTART")
    {
    }
    protected override int ViewBoxWidth => 80;
}
