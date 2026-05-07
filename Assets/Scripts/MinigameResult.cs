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
    public static bool crayonSortCompleted = false;
    public static int mathsScore = 0;
    public static int shelterState = 0; // 0 = morning before school, 1 = evening after school, 2 = next morning

    // Mode tracking
    public static int humourChoices = 0;
    public static int honestChoices = 0;
    public static int shutdownChoices = 0;

    public static string GetDominantMode()
    {
        // Honest wins any tie
        if (honestChoices >= humourChoices && honestChoices >= shutdownChoices)
            return "honest";

        // Humour and shutdown tied, honest is lower — random
        if (humourChoices == shutdownChoices)
            return UnityEngine.Random.Range(0, 2) == 0 ? "humour" : "shutdown";

        // Otherwise highest wins
        if (humourChoices > shutdownChoices)
            return "humour";

        return "shutdown";
    }

    public static void Reset()
    {
        lunchLilyChoice = 1;
        schoolCompleted = false;
        basketballCompleted = false;
        crayonSortCompleted = false;
        mathsScore = 0;
        shelterState = 0;
        humourChoices = 0;
        honestChoices = 0;
        shutdownChoices = 0;
    }
}
