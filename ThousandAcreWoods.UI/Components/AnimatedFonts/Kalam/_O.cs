
namespace ThousandAcreWoods.UI.Components.AnimatedFonts.Kalam;

public class _O : AnimatedLetter
{
    public _O() : base(
        paths: [
            new AnimatedLetterPath(
                PathSpecification: @"
                       M 78.44,31.30
                       C 56.81,32.39 16.08,76.02 14.44,128.02
                         19.53,153.66 37.38,157.38 44.99,155.84
                         93.31,143.50 107.34,84.24 106.19,79.56
                         107.27,72.74 105.42,58.30 104.31,56.31
                         101.50,45.81 98.06,41.25 92.50,36.56
                         85.97,32.14 79.38,31.38 79.38,31.38",
                PathLength: 700)


             ],
        letter: "O")
    {
    }
    protected override int ViewBoxWidth => 120;
}

