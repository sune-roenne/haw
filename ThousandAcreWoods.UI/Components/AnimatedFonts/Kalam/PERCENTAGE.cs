
namespace ThousandAcreWoods.UI.Components.AnimatedFonts.Kalam;

public class PERCENTAGE : AnimatedLetter
{
    public PERCENTAGE() : base(
        paths: [
            new AnimatedLetterPath(
                PathSpecification: @"
                   M 32.00,42.00
                   C 21.50,49.50 15.50,56.50 11.00,65.50
                     6.50,74.50 2.50,91.50 16.50,91.50
                     30.50,91.50 49.98,72.54 51.48,62.04
                     52.98,51.54 49.75,54.75 48.38,50.00
                     61.94,57.94 98.89,37.61 105.88,40.69
                     117.77,49.49 80.12,127.12 78.25,147.62",
                PathLength: 550),
            new AnimatedLetterPath(
                PathSpecification: @"
                   M 140.36,99.64
                   C 150.73,92.18 170.91,89.64 173.45,112.91
                     176.00,136.18 164.55,159.64 147.64,159.64
                     130.73,159.64 125.82,152.00 124.73,140.55
                     123.64,129.09 128.00,116.00 133.82,107.27",
                PathLength: 250)
             ],
        letter: "PERCENTAGE")
    {
    }
    protected override int ViewBoxWidth => 180;
}
