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

    private int num1;
    private int num2;

    private int score = 0;
    private int maxScore = 10;

    private bool waitingForNext = false;
    private bool isFlashing = false;

    void Start()
    {
        UpdateScore();
        GenerateQuestion();
        inputField.onEndEdit.AddListener(CheckAnswer);
    }

    void GenerateQuestion()
    {
        num1 = Random.Range(1, 13);
        num2 = Random.Range(1, 13);

        questionText.text = num1 + " + " + num2 + " =";
        questionText.color = new Color32(255, 0, 0, 255); // red

        inputField.text = "";
        inputField.interactable = true;

        // Focus input
        inputField.ActivateInputField();
        inputField.Select();
    }

    void CheckAnswer(string value)
    {
        if (waitingForNext || isFlashing) return;

        if (int.TryParse(value, out int result))
        {
            if (result == num1 + num2)
            {
                // ✅ Correct
                questionText.text = num1 + " + " + num2 + " = " + result;
                questionText.color = new Color32(0, 0, 255, 255); // blue
                inputField.interactable = false;

                if (audioSource && correctSound)
                    audioSource.PlayOneShot(correctSound);

                score++;
                UpdateScore();
                CheckMilestones();

                StartCoroutine(NextQuestionDelay());
            }
            else
            {
                // ❌ Wrong
                if (audioSource && wrongSound)
                    audioSource.PlayOneShot(wrongSound);

                StartCoroutine(FlashWrong());
            }
        }
        else
        {
            // Not a number → refocus
            RefocusInput();
        }
    }

    IEnumerator FlashWrong()
    {
        isFlashing = true;

        // Flash effect
        questionText.color = Color.red;
        yield return new WaitForSeconds(0.15f);

        questionText.color = Color.white;
        yield return new WaitForSeconds(0.15f);

        questionText.color = new Color32(255, 0, 0, 255);

        isFlashing = false;

        // ✅ FIX: restore typing ability
        RefocusInput();
    }

    IEnumerator NextQuestionDelay()
    {
        waitingForNext = true;

        yield return new WaitForSeconds(2f);

        if (score < maxScore)
        {
            GenerateQuestion();
            waitingForNext = false;
        }
        else
        {
            questionText.text = "Well Done!";
            inputField.gameObject.SetActive(false);
        }
    }

    void UpdateScore()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score + "/" + maxScore;
    }

    void CheckMilestones()
    {
        if (score == 5)
        {
            Debug.Log("Reached 5! Trigger dialogue here.");
            // Example:
            // dialogueManager.ShowNextLine();
        }
    }

    void RefocusInput()
    {
        inputField.ActivateInputField();
        inputField.Select();
    }
}