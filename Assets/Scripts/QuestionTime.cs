using System.Collections;
using TMPro;
using UnityEngine;

public class QuestionFlow : MonoBehaviour
{
    public TextMeshProUGUI questionText;
    public TMP_InputField inputField;

    private int num1;
    private int num2;

    private int questionsAnswered = 0;
    private int maxQuestions = 10;

    private bool waitingForNext = false;

    void Start()
    {
        GenerateQuestion();
        inputField.onEndEdit.AddListener(CheckAnswer);
    }

    void GenerateQuestion()
    {
        num1 = Random.Range(1, 13); // 1–12
        num2 = Random.Range(1, 13);

        questionText.text = num1 + " + " + num2 + " =";
        questionText.color = new Color32(255, 0, 0, 255); // red

        inputField.text = "";
        inputField.interactable = true;
        inputField.ActivateInputField();
    }

    void CheckAnswer(string value)
    {
        if (waitingForNext) return;

        if (int.TryParse(value, out int result))
        {
            if (result == num1 + num2)
            {
                questionText.color = new Color32(0, 0, 255, 255); // blue
                inputField.interactable = false;
                questionsAnswered++;

                StartCoroutine(NextQuestionDelay());
            }
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
            questionText.text = "Well Done!";
            inputField.gameObject.SetActive(false);
        }
    }
}