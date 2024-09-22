using Microsoft.AspNetCore.Components;
using ThousandAcreWoods.Domain.Story.Model;
using ThousandAcreWoods.UI.Util;

namespace ThousandAcreWoods.UI.Pages.Story;

public partial class ImageScene
{
    private readonly ElementId _id = ElementId.For("story-image-scene");
    public string Image => $"images/{Scene.Image!.ImagePath}";

    [Parameter]
    public int Width { get; set; } = 1920;

    [Parameter]
    public int Height { get; set; } = 1080;

    [Parameter]
    public Scene Scene { get; set; }


    private int FrameWidth => (int)(Width * 0.1m);
    private int FrameHeight => (int)(Height * 0.1m);

    private int ImageWidth => (int)(Width * 0.95m);
    private int ImageHeight => (int)(Height * 0.95m);



}
