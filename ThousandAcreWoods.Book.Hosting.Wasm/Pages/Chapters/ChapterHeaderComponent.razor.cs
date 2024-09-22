using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using ThousandAcreWoods.Book.Hosting.Wasm.Model;
using ThousandAcreWoods.Book.Hosting.Wasm.Pages.Chapters.Model;
using ThousandAcreWoods.Book.Hosting.Wasm.Persistence;
using ThousandAcreWoods.Book.Hosting.Wasm.State;

namespace ThousandAcreWoods.Book.Hosting.Wasm.Pages.Chapters;

public partial class ChapterHeaderComponent
{
    [CascadingParameter]
    public SiteChapter Chapter { get; set; }

    [CascadingParameter]
    public SiteState SiteState { get; set; }

    [Inject]
    public IChapterLoader ChapterLoader { get; set; }

    private SiteChapter[]? _allChapters;
    private ChapterPlacementData? _placementData;
    private IDictionary<DateTime, List<int>> _chaptersPerDay = new Dictionary<DateTime, List<int>>();
    private bool _isDraggingSlider = false;
    private string _sliderLabelText = "";


    protected override async Task OnParametersSetAsync()
    {
        if(_allChapters == null)
        {
            _allChapters = (await ChapterLoader.LoadChapters()).ToArray();
            _placementData = new ChapterPlacementData(_allChapters.Length) { CurrentIndex = Chapter.ChapterOrder };
            _chaptersPerDay.Clear();
            _allChapters
                .GroupBy(_ => _.ChapterDate)
                .Select(_ => (Date: _.Key, Orders: _.Select(_ => _.ChapterOrder).Order().ToList()))
                .ToList()
                .ForEach(en => _chaptersPerDay[en.Date] = en.Orders);
                
        }
        if(_placementData != null && _placementData.CurrentIndex != Chapter.ChapterOrder)
        {
            _placementData.CurrentIndex = Chapter.ChapterOrder;
            await UpdateSliderText();
        }
        if(_sliderLabelText.Length == 0)
            await UpdateSliderText();

    }

    private async Task UpdateSliderText(int? forIndex = null)
    {
        if (_allChapters == null)
            return;
        var index = forIndex ?? _placementData?.CurrentIndex ?? Chapter.ChapterOrder;
        var dateOnIndex = _allChapters[index].ChapterDate;
        string? chaptersOnDatePart = null;
        var chapsOnDate = _chaptersPerDay[dateOnIndex];
        if(chapsOnDate.Count > 1)
        {
            var currentIsIndex = chapsOnDate.Count(_ => _ <= index);
            chaptersOnDatePart = $" [{currentIsIndex}/{chapsOnDate.Count}]";
        }
        var newText = $"{dateOnIndex.ToString("dd-MM-yyyy")}{chaptersOnDatePart}";
        if(_isDraggingSlider)
        {
            newText = newText + $" ({_allChapters[index].ChapterName})";
        }
        if(newText != _sliderLabelText)
        {
            _sliderLabelText = newText;
            await InvokeAsync(StateHasChanged);
        }

    } 

    private void OnSliderDragEnd(ChangeEventArgs args)
    {
        _isDraggingSlider = true;
        var index = int.Parse(args!.Value!.ToString()!);
        _ = UpdateSliderText(index);
    }
    private void OnSliderValueCHange(ChangeEventArgs args)
    {
        _isDraggingSlider = false;
        var index = int.Parse(args!.Value!.ToString()!);
        SiteState.ChangeChapter(index);
        _ = UpdateSliderText(index);
    }

    private record ChapterPlacementData(int TotalNumberOfChapters)
    {
        public int CurrentIndex { get; set; }
    }


}
