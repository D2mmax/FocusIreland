using System.Collections;
using TMPro;
using UnityEngine;

public class QuestionFlow : MonoBehaviour
{
    public TextMeshProUGUI questionText;
    public TMP_InputField inputField;

    private int currentNumber = 1;
    private int maxNumber = 10;
    private int multiplier = 10;

    private bool waitingForNext = false;

    void Start()
    {
        ShowQuestion();
        inputField.onEndEdit.AddListener(CheckAnswer);
    }

    void ShowQuestion()
    {
        questionText.text = currentNumber + " x " + multiplier + " =";
        questionText.color = Color.red;
        inputField.text = "";
        inputField.interactable = true;
        inputField.ActivateInputField();
    }

    void CheckAnswer(string value)
    {
        if (waitingForNext) return;

        if (int.TryParse(value, out int result))
        {
            if (result == currentNumber * multiplier)
            {
                questionText.color = Color.blue;
                inputField.interactable = false;
                StartCoroutine(NextQuestionDelay());
            }
        }
    }

    IEnumerator NextQuestionDelay()
    {
        waitingForNext = true;

        yield return new WaitForSeconds(2f); // delay before next question

        currentNumber++;

        if (currentNumber <= maxNumber)
        {
            ShowQuestion();
            waitingForNext = false;
        }
        else
        {
            questionText.text = "Well Done!";
            inputField.gameObject.SetActive(false);
        }
    }
}