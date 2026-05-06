public static class MinigameResult
{
    public static bool hasPlayed = false;
    public static bool passed = false;
    public static bool mathsPlayed = false;

    public static void Reset()
    {
        hasPlayed = false;
        passed = false;
        mathsPlayed = false;
    }
}

public static class DayFlags
{
    public static int lunchLilyChoice = 1;
    public static bool schoolCompleted = false;

    public static void Reset()
    {
        lunchLilyChoice = 1;
        schoolCompleted = false;
    }
}
