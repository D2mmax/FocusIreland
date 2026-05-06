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

    [Header("Next Scene (after lunch)")]
    public string nextScene;

    void Start()
    {
        Debug.Log($"[ClassroomDialogueTrigger] hasPlayed={MinigameResult.hasPlayed} mathsPlayed={MinigameResult.mathsPlayed}");

        if (DialogueScreenManager.Instance == null)
        {
            Debug.LogWarning("ClassroomDialogueTrigger: DialogueScreenManager not found.");
            return;
        }

        if (!MinigameResult.hasPlayed)
        {
            Debug.Log("[ClassroomDialogueTrigger] Branch: INTRO → Pack Your Bag minigame");
            System.Action onIntroEnd = () =>
            {
                Debug.Log($"[ClassroomDialogueTrigger] Loading Pack Your Bag scene: '{minigameScene}'");
                if (SceneFader.Instance != null)
                    SceneFader.Instance.FadeTo(minigameScene);
                else
                    UnityEngine.SceneManagement.SceneManager.LoadScene(minigameScene);
            };
            DialogueScreenManager.Instance.StartDialogue(introConfig, introTree, onIntroEnd);
        }
        else if (!MinigameResult.mathsPlayed)
        {
            Debug.Log("[ClassroomDialogueTrigger] Branch: PASS/FAIL → Maths intro → Maths minigame");
            DialogueSceneConfig config = MinigameResult.passed ? passConfig : failConfig;
            DialogueTree tree = MinigameResult.passed ? passTree : failTree;

            System.Action onPassFailEnd = () =>
            {
                Debug.Log("[ClassroomDialogueTrigger] Pass/fail done, starting maths intro");
                DialogueScreenManager.Instance.StartDialogue(mathsIntroConfig, mathsIntroTree, () =>
                {
                    Debug.Log($"[ClassroomDialogueTrigger] Loading maths minigame scene: '{mathsMinigameScene}'");
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
            Debug.Log("[ClassroomDialogueTrigger] Branch: POST MATHS → Lunch → Next scene");
            MinigameResult.Reset();

            System.Action onPostMathsEnd = () =>
            {
                DialogueScreenManager.Instance.StartDialogue(lunchConfig, lunchTree, () =>
                {
                    if (!string.IsNullOrEmpty(nextScene))
                    {
                        if (SceneFader.Instance != null)
                            SceneFader.Instance.FadeTo(nextScene);
                        else
                            UnityEngine.SceneManagement.SceneManager.LoadScene(nextScene);
                    }
                });
            };
            DialogueScreenManager.Instance.StartDialogue(postMathsConfig, postMathsTree, onPostMathsEnd);
        }
    }
}
