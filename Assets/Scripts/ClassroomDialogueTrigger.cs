using UnityEngine;

public class ClassroomDialogueTrigger : MonoBehaviour
{
    [Header("Intro Dialogue (before Pack Your Bag minigame)")]
    public DialogueSceneConfig introConfig;
    public DialogueTree introTree;

    [Header("Pack Your Bag Minigame Scene")]
    public string minigameScene;

    [Header("Pass Dialogue (after Pack Your Bag — passed)")]
    public DialogueSceneConfig passConfig;
    public DialogueTree passTree;

    [Header("Fail Dialogue (after Pack Your Bag — failed)")]
    public DialogueSceneConfig failConfig;
    public DialogueTree failTree;

    [Header("Maths Intro Dialogue (before maths minigame)")]
    public DialogueSceneConfig mathsIntroConfig;
    public DialogueTree mathsIntroTree;

    [Header("Maths Minigame Scene")]
    public string mathsMinigameScene;

    [Header("Post Maths Dialogue (after maths minigame)")]
    public DialogueSceneConfig postMathsConfig;
    public DialogueTree postMathsTree;

    [Header("Lunch Dialogue")]
    public DialogueSceneConfig lunchConfig;
    public DialogueTree lunchTree;

    [Header("Darragh Dialogue (after lunch timeskip)")]
    public DialogueSceneConfig darraghConfig;
    public DialogueTree darraghTree;

    [Header("Next Scene (after Darragh)")]
    public string nextScene;

    [Header("Timeskip Settings")]
    public float timeSkipHoldDuration = 2.5f;

    void Start()
    {
        if (DialogueScreenManager.Instance == null)
        {
            Debug.LogWarning("ClassroomDialogueTrigger: DialogueScreenManager not found.");
            return;
        }

        if (!MinigameResult.hasPlayed)
        {
            // First time — intro then Pack Your Bag
            System.Action onIntroEnd = () =>
            {
                if (SceneFader.Instance != null)
                    SceneFader.Instance.FadeTo(minigameScene);
                else
                    UnityEngine.SceneManagement.SceneManager.LoadScene(minigameScene);
            };
            DialogueScreenManager.Instance.StartDialogue(introConfig, introTree, onIntroEnd);
        }
        else if (!MinigameResult.mathsPlayed)
        {
            // Returned from Pack Your Bag — pass/fail then maths intro then maths minigame
            DialogueSceneConfig config = MinigameResult.passed ? passConfig : failConfig;
            DialogueTree tree = MinigameResult.passed ? passTree : failTree;

            System.Action onPassFailEnd = () =>
            {
                DialogueScreenManager.Instance.StartDialogue(mathsIntroConfig, mathsIntroTree, () =>
                {
                    if (SceneFader.Instance != null)
                        SceneFader.Instance.FadeTo(mathsMinigameScene);
                    else
                        UnityEngine.SceneManagement.SceneManager.LoadScene(mathsMinigameScene);
                });
            };
            DialogueScreenManager.Instance.StartDialogue(config, tree, onPassFailEnd);
        }
        else
        {
            // Returned from maths minigame — post maths then lunch then timeskip then Darragh then next scene
            MinigameResult.Reset();

            System.Action onPostMathsEnd = () =>
            {
                DialogueScreenManager.Instance.StartDialogue(lunchConfig, lunchTree, () =>
                {
                    string timeskipText = "Last class. Almost there. Then Darragh opened his mouth.";
                    SceneFader.Instance.FadeToBlackWithText(timeskipText, timeSkipHoldDuration, () =>
                    {
                        DialogueScreenManager.Instance.StartDialogue(darraghConfig, darraghTree, () =>
                        {
                            DayFlags.schoolCompleted = true;
                            if (!string.IsNullOrEmpty(nextScene))
                            {
                                if (SceneFader.Instance != null)
                                    SceneFader.Instance.FadeTo(nextScene);
                                else
                                    UnityEngine.SceneManagement.SceneManager.LoadScene(nextScene);
                            }
                        });
                    });
                });
            };
            DialogueScreenManager.Instance.StartDialogue(postMathsConfig, postMathsTree, onPostMathsEnd);
        }
    }
}
