using UnityEngine;

public class AutoDialogueTrigger : MonoBehaviour
{
    [Header("Dialogue Data")]
    public DialogueSceneConfig sceneConfig;
    public DialogueTree dialogueTree;

    [Header("Scene Transition")]
    [Tooltip("Tick this to automatically load a scene when the conversation ends")]
    public bool loadSceneOnEnd = false;
    [Tooltip("Exact name of the scene to load")]
    public string sceneToLoad;

    void Start()
    {
        if (DialogueScreenManager.Instance == null)
        {
            Debug.LogWarning("AutoDialogueTrigger: DialogueScreenManager not found.");
            return;
        }

        System.Action onEnd = null;
        if (loadSceneOnEnd && !string.IsNullOrEmpty(sceneToLoad))
        {
            onEnd = () =>
            {
                if (SceneFader.Instance != null)
                    SceneFader.Instance.FadeTo(sceneToLoad);
                else
                    UnityEngine.SceneManagement.SceneManager.LoadScene(sceneToLoad);
            };
        }

        DialogueScreenManager.Instance.StartDialogue(sceneConfig, dialogueTree, onEnd);
    }
}
