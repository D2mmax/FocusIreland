using UnityEngine;

public class ClassroomDialogueTrigger : MonoBehaviour
{
    [Header("Intro Dialogue (before minigame)")]
    public DialogueSceneConfig introConfig;
    public DialogueTree introTree;

    [Header("Minigame Scene")]
    public string minigameScene;

    [Header("Pass Dialogue (after minigame — passed)")]
    public DialogueSceneConfig passConfig;
    public DialogueTree passTree;

    [Header("Fail Dialogue (after minigame — failed)")]
    public DialogueSceneConfig failConfig;
    public DialogueTree failTree;

    [Header("Time Skip Hold Duration")]
    public float timeSkipHoldDuration = 2.5f;

    [Header("Lunch Dialogue (after time skip)")]
    public DialogueSceneConfig lunchConfig;
    public DialogueTree lunchTree;

    [Header("Next Scene (after lunch dialogue)")]
    public string nextScene;

    void Start()
    {
        if (DialogueScreenManager.Instance == null)
        {
            Debug.LogWarning("ClassroomDialogueTrigger: DialogueScreenManager not found.");
            return;
        }

        if (!MinigameResult.hasPlayed)
        {
            System.Action onEnd = () =>
            {
                if (SceneFader.Instance != null)
                    SceneFader.Instance.FadeTo(minigameScene);
                else
                    UnityEngine.SceneManagement.SceneManager.LoadScene(minigameScene);
            };
            DialogueScreenManager.Instance.StartDialogue(introConfig, introTree, onEnd);
        }
        else
        {
            DialogueSceneConfig config = MinigameResult.passed ? passConfig : failConfig;
            DialogueTree tree = MinigameResult.passed ? passTree : failTree;

            System.Action onEnd = () =>
            {
                MinigameResult.Reset();
                TriggerTimeSkip();
            };
            DialogueScreenManager.Instance.StartDialogue(config, tree, onEnd);
        }
    }

    void TriggerTimeSkip()
    {
        if (SceneFader.Instance == null)
        {
            Debug.LogWarning("ClassroomDialogueTrigger: SceneFader not found.");
            return;
        }

        // First timeskip is always the fixed morning text
        string morningText = "The rest of the morning was uneventful. Double maths, then Irish. The lunch bell rang.";

        SceneFader.Instance.FadeToBlackWithText(morningText, timeSkipHoldDuration, () =>
        {
            if (lunchConfig != null && lunchTree != null)
            {
                // Snapshot connection before lunch to detect which choice was made
                int connectionBefore = EHCManager.Instance != null ? EHCManager.Instance.connection : 0;

                System.Action onLunchEnd = () =>
                {
                    // If connection went up, choice 2 or 3 was picked — update flag
                    if (EHCManager.Instance != null && EHCManager.Instance.connection > connectionBefore)
                        DayFlags.lunchLilyChoice = 2;

                    // Second timeskip — afternoon, conditional on Lily choice
                    string afternoonText = DayFlags.lunchLilyChoice == 1
                        ? "The rest of the school day flew by. He kept thinking about Lily's question. Maybe this weekend wouldn't be so bad."
                        : "The rest of the school day flew by. Lily hadn't pushed. He appreciated that more than he could say.";

                    if (SceneFader.Instance != null)
                    {
                        SceneFader.Instance.FadeToBlackWithText(afternoonText, timeSkipHoldDuration, () =>
                        {
                            if (!string.IsNullOrEmpty(nextScene))
                                SceneFader.Instance.FadeTo(nextScene);
                        });
                    }
                    else if (!string.IsNullOrEmpty(nextScene))
                    {
                        UnityEngine.SceneManagement.SceneManager.LoadScene(nextScene);
                    }
                };
                DialogueScreenManager.Instance.StartDialogue(lunchConfig, lunchTree, onLunchEnd);
            }
        });
    }
}
