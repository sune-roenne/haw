
namespace ThousandAcreWoods.UI.Components.AnimatedFonts.Kalam;

public class AMPERSAND : AnimatedLetter
{
    public AMPERSAND() : base(
        paths: [
            new AnimatedLetterPath(
                PathSpecification: @"
                   M 85.25,104.25
                   C 85.00,121.75 73.25,133.50 64.50,144.00
                     58.75,152.50 26.25,161.25 12.75,154.50
                     -0.75,147.75 9.50,118.88 18.25,112.62
                     29.75,104.38 90.25,68.88 80.00,54.12
                     67.00,32.00 45.75,70.00 39.50,82.50
                     33.25,95.00 43.62,152.50 95.12,148.50
                     107.00,146.75 117.12,127.75 116.62,120.12",
                PathLength: 600)

             ],
        letter: "AMPERSAND")
    {
    }
    protected override int ViewBoxWidth => 150;
}
