using UnityEngine;

public class MainMenuButtons : MonoBehaviour
{
    public void PlayGame(string sceneName)
    {
        if (SceneFader.Instance != null)
            SceneFader.Instance.FadeTo(sceneName);
        else
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
