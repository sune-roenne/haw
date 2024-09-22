
namespace ThousandAcreWoods.UI.Components.AnimatedFonts.Kalam;

public class å : AnimatedLetter
{
    public å() : base(
        paths: [
            new AnimatedLetterPath(
                PathSpecification: @"
                    M 86.55,106.00
                    C 92.00,81.27 77.09,73.27 69.45,76.18
                      49.09,84.55 30.18,105.82 24.18,140.18
                      18.18,174.55 74.09,161.45 86.64,105.82
                      84.18,153.73 80.82,146.45 82.45,160.45",
                PathLength: 355),
            new AnimatedLetterPath(
                PathSpecification: @"
                    M 94.98,27.36
                    C 75.89,20.36 72.45,56.91 91.27,54.18
                      106.18,51.55 105.70,32.15 95.07,27.24",
                PathLength: 100)

             ],
        letter: "å")
    {
    }
    protected override int ViewBoxWidth => 120;
}
