public static class MinigameResult
{
    public static bool hasPlayed = false;
    public static bool passed = false;

    public static void Reset()
    {
        hasPlayed = false;
        passed = false;
    }
}

public static class DayFlags
{
    // Day 1 lunch choice: 1 = not really (default), 2 = stay in or why
    public static int lunchLilyChoice = 1;

    public static void Reset()
    {
        lunchLilyChoice = 1;
    }
}
