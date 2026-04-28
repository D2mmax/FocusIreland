using UnityEngine;

// Attach to any GameObject in the school scene.
// Resets the flag at scene start and marks choice 1 (Not really) as default.
// If Connection goes up during the lunch dialogue, it was choice 2 or 3.
public class LunchChoiceTracker : MonoBehaviour
{
    private int connectionAtLunchStart;

    void Start()
    {
        // Default to choice 1 (Not really) — overridden if Connection changes
        DayFlags.lunchLilyChoice = 1;
    }

    public void OnLunchChoiceMade(int choiceIndex)
    {
        // Called externally if needed — 1 = Not really, 2 = Stay in, 3 = Why
        DayFlags.lunchLilyChoice = choiceIndex + 1;
    }
}
