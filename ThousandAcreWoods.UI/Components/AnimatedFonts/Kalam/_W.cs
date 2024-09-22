
namespace ThousandAcreWoods.UI.Components.AnimatedFonts.Kalam;

public class _W : AnimatedLetter
{
    public _W() : base(
        paths: [
            new AnimatedLetterPath(
                PathSpecification: @"
                       M 19.23,28.86
                       C 23.08,33.37 9.32,128.60 13.64,154.55
                         15.11,167.46 58.48,98.48 66.00,98.18
                         66.00,98.18 71.64,149.64 82.18,154.36
                         101.59,168.51 124.70,116.74 127.82,104.55
                         135.80,62.35 110.74,33.26 83.45,32.36",
                PathLength: 600)
             ],
        letter: "W")
    {
    }

    protected override int ViewBoxWidth => 140;
}

