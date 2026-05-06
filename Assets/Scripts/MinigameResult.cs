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
    public static bool basketballCompleted = false;
    public static int mathsScore = 0;
    public static int shelterState = 0; // 0 = morning before school, 1 = evening after school, 2 = next morning

    public static void Reset()
    {
        lunchLilyChoice = 1;
        schoolCompleted = false;
        basketballCompleted = false;
        mathsScore = 0;
        shelterState = 0;
    }
}
