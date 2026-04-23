using UnityEngine;
using UnityEngine.SceneManagement;

// ---------------------------------------------------------------------------
//  DayManager  — tracks the current day across the whole game
//  Persistent singleton, survives all scene loads.
//  Day 0 = Intro, Day 1 = Monday, Day 2 = Tuesday ... Day 7 = Sunday
// ---------------------------------------------------------------------------
public class DayManager : MonoBehaviour
{
    public static DayManager Instance { get; private set; }

    [Header("Current Day (0 = Intro)")]
    public int currentDay = 0;

    [Header("First scene to load for each day")]
    [Tooltip("Index 0 = Intro start scene, Index 1 = Day 1 start scene, etc.")]
    public string[] dayStartScenes;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Call this at the end of each day to move to the next
    public void AdvanceDay()
    {
        currentDay++;
        Debug.Log($"[DayManager] Advancing to Day {currentDay}");
    }

    // Load the first scene of the current day
    public void LoadCurrentDayStart()
    {
        if (dayStartScenes == null || currentDay >= dayStartScenes.Length)
        {
            Debug.LogWarning("[DayManager] No start scene defined for day " + currentDay);
            return;
        }
        SceneManager.LoadScene(dayStartScenes[currentDay]);
    }

    // Load the first scene of the next day (advances day counter first)
    public void AdvanceToNextDay()
    {
        AdvanceDay();
        LoadCurrentDayStart();
    }
}
