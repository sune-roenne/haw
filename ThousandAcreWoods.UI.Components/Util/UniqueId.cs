namespace ThousandAcreWoods.UI.Components.Util;

public static class UniqueId
{
    private static long _currentId = 0L;
    private static object _lock = new { };
    public static long NextId()
    {
        lock (_lock)
            return _currentId++;
    }

}
