using System.Collections;
using TMPro;
using UnityEngine;

public class QuestionFlow : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI questionText;
    public TMP_InputField inputField;
    public TextMeshProUGUI scoreText;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip correctSound;
    public AudioClip wrongSound;

    [Header("Scene Transition")]
    public string sceneToLoadOnComplete = "SchoolScene";

    private int num1;
    private int num2;
    private int correctAnswer;
    private string operatorSymbol;

    private int score = 0;
    private int questionsAnswered = 0;
    private int maxQuestions = 10;

    private bool waitingForNext = false;

    void Start()
    {
        UpdateScore();
        GenerateQuestion();
        inputField.onEndEdit.AddListener(CheckAnswer);
    }

    void GenerateQuestion()
    {
        int operation = Random.Range(0, 3);

        if (operation == 0)
        {
            // Addition: up to 20
            num1 = Random.Range(5, 20);
            num2 = Random.Range(5, 20);
            correctAnswer = num1 + num2;
            operatorSymbol = "+";
        }
        else if (operation == 1)
        {
            // Subtraction: up to 20, result always positive
            num1 = Random.Range(10, 25);
            num2 = Random.Range(1, num1);
            correctAnswer = num1 - num2;
            operatorSymbol = "-";
        }
        else
        {
            // Multiplication: 1-9 x 1-9
            num1 = Random.Range(2, 10);
            num2 = Random.Range(2, 10);
            correctAnswer = num1 * num2;
            operatorSymbol = "x";
        }

        questionText.text = num1 + " " + operatorSymbol + " " + num2 + " =";
        questionText.color = new Color32(255, 0, 0, 255);

        inputField.text = "";
        inputField.interactable = true;

        inputField.ActivateInputField();
        inputField.Select();
    }

    void CheckAnswer(string value)
    {
        if (waitingForNext) return;

        if (int.TryParse(value, out int result))
        {
            if (result == correctAnswer)
            {
                questionText.text = num1 + " " + operatorSymbol + " " + num2 + " = " + result;
                questionText.color = new Color32(0, 0, 255, 255);
                inputField.interactable = false;

                if (audioSource && correctSound)
                    audioSource.PlayOneShot(correctSound);

                score++;
                questionsAnswered++;
                UpdateScore();
                StartCoroutine(NextQuestionDelay());
            }
            else
            {
                // Wrong — show correct answer then move on
                questionText.text = num1 + " " + operatorSymbol + " " + num2 + " = " + correctAnswer;
                questionText.color = new Color32(255, 0, 0, 255);
                inputField.interactable = false;

                if (audioSource && wrongSound)
                    audioSource.PlayOneShot(wrongSound);

                questionsAnswered++;
                UpdateScore();
                StartCoroutine(NextQuestionDelay());
            }
        }
        else
        {
            RefocusInput();
        }
    }

    IEnumerator NextQuestionDelay()
    {
        waitingForNext = true;

        yield return new WaitForSeconds(2f);

        if (questionsAnswered < maxQuestions)
        {
            GenerateQuestion();
            waitingForNext = false;
        }
        else
        {
            questionText.text = "Finished! " + score + "/" + maxQuestions;
            inputField.gameObject.SetActive(false);
            StartCoroutine(CompleteMinigame());
        }
    }

    IEnumerator CompleteMinigame()
    {
        yield return new WaitForSeconds(1.5f);

        MinigameResult.hasPlayed = true;
        MinigameResult.mathsPlayed = true;

        if (SceneFader.Instance != null)
            SceneFader.Instance.FadeTo(sceneToLoadOnComplete);
        else
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneToLoadOnComplete);
    }

    void UpdateScore()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score + "/" + questionsAnswered;
    }

    void RefocusInput()
    {
        inputField.ActivateInputField();
        inputField.Select();
    }
}
