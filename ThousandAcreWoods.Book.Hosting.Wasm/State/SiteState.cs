using ThousandAcreWoods.Book.Hosting.Wasm.Configuration;
using ThousandAcreWoods.Book.Hosting.Wasm.Pages.Chapters.Model;
using ThousandAcreWoods.Book.Hosting.Wasm.Persistence;

namespace ThousandAcreWoods.Book.Hosting.Wasm.State;

public record SiteState(
    Action OnUpdate,
    ILocalStorageRepository StorageRepo,
    IChapterLoader ChapterLoader
    )
{

    private const string LocalStorageKey = "TAW.SITE.STATE";
    private const int SecondsBeforeChapterCounts = 30;

    public event EventHandler<bool> OnAutomaticReadingChange;

    private bool _isInitialized = false;
    private int _currentChapter = 0;
    private int _currentParagraphIndex = 0;
    private bool _currentIsAtBottom = false;
    private DateTime _enteredChapterAt = DateTime.Now;
    private bool AllowSavingScrollProgress => (DateTime.Now - _enteredChapterAt).TotalSeconds > SecondsBeforeChapterCounts;
    private SitePageSelection _pageSelection = SitePageSelection.Chapters;
    private bool _isAutomatedReadingEnabled = false;
    private decimal _currentReadingSpeed = 0.5m;
    private int _totalNumberOfChapters = 0;
    private bool _isFunctionalityDisabled = true;


    public SitePageSelection CurrentPageSelection { get => _pageSelection; set => WrapUpdate(() => _pageSelection = value); }
    public int CurrentChapter => _currentChapter;
    public int CurrentParagraphIndex => _currentParagraphIndex;
    public bool IsAutomatedReadingEnabled => _isAutomatedReadingEnabled;
    public decimal CurrentReadingSpeed => _currentReadingSpeed;

    public bool CurrentChapterIsFirstChapter => _currentChapter == 0;
    public bool CurrentChapterIsLastChapter => _currentChapter == _totalNumberOfChapters - 1;

    public bool IsFunctionalityDisabled => _isFunctionalityDisabled;



    public async Task Initialize()
    {
        if (!_isInitialized)
        {
            var fromStorage = await LoadState();
            if(fromStorage != null)
            {
                _currentChapter = fromStorage.CurrentChapter;
                _currentParagraphIndex = fromStorage.CurrentParagraphIndex;
                SetReadingSpeed(fromStorage.CurrentReadingSpeed, saveReadingSpeed: false);
            }
            _totalNumberOfChapters = (await ChapterLoader.LoadChapters()).Count;
            _isInitialized = true;
        }
    }

    public void AutomateReading(bool automate)
    {
        if(automate != _isAutomatedReadingEnabled)
        {
            _isAutomatedReadingEnabled = automate;
            OnAutomaticReadingChange?.Invoke(this, automate);
            OnUpdate();
        }
    }

    public void SetFunctionalityDisablement(bool isDisabled)
    {
        if(isDisabled != _isFunctionalityDisabled)
        {
            _isFunctionalityDisabled = isDisabled;
            OnUpdate();
        }
    }

    public void SetReadingSpeed(decimal speed, bool saveReadingSpeed = true)
    {
        if(speed > SiteRenderingConstants.MaxReadingSpeed)
            speed = SiteRenderingConstants.MaxReadingSpeed;
        else if(speed < SiteRenderingConstants.MinReadingSpeed)
            speed = SiteRenderingConstants.MinReadingSpeed;
        _currentReadingSpeed = speed;
        if(saveReadingSpeed)
        {
            _ = SaveReadingSpeed();
        }
    }


    public void ChangeChapter(int newChapter)
    {
        if(newChapter == _currentChapter || newChapter < 0 || newChapter >= _totalNumberOfChapters) 
            return;
        _currentChapter = newChapter;
        _currentParagraphIndex = 0;
        _currentIsAtBottom = false;
        _enteredChapterAt = DateTime.Now;
        OnUpdate();

    }

    public void OnScrollingStatusChanged(DisplayScrollingStatus scrollingStatus)
    {
        if (scrollingStatus.ChapterIndex == null)
            return;
        var chapterIndex = scrollingStatus.ChapterIndex.Value;
        if (chapterIndex != _currentChapter)
            return;
        _currentParagraphIndex = scrollingStatus.ParagraphIndex;
        _currentIsAtBottom = scrollingStatus.IsAtBottom;
        if(AllowSavingScrollProgress)
        {
            _ = SaveState();
        }
   }

    public void IncrementScrollingStatusFromAnimation()
    {
        _currentParagraphIndex = _currentParagraphIndex + 1;
        _ = SaveState();
    }


    private void WrapUpdate(Action action)
    {
        action();
        OnUpdate();
    }


    private async Task<StorageEntry?> LoadState()
    {
        var loaded = await StorageRepo.Get<StorageEntry>(LocalStorageKey);
        return loaded;
    }

    private async Task SaveReadingSpeed()
    {
        var loaded = await LoadState();
        if(loaded == null || loaded.CurrentReadingSpeed == _currentReadingSpeed) return;
        var toSave = loaded with
        {
            CurrentReadingSpeed = _currentReadingSpeed
        };
        await StorageRepo.Set(LocalStorageKey, toSave);
    }

    private async Task SaveState()
    {
        var toSave = new StorageEntry(
            CurrentChapter: _currentChapter,
            CurrentParagraphIndex: _currentParagraphIndex,
            CurrentReadingSpeed: _currentReadingSpeed
            );
        await StorageRepo.Set(LocalStorageKey, toSave);
    }
    private record StorageEntry(
        int CurrentChapter = 0,
        int CurrentParagraphIndex = 0,
        decimal CurrentReadingSpeed = 0.5m
        );


}
