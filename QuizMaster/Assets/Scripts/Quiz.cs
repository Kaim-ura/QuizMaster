using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] QuestionSO question;

    [Header("Answers")]
    [SerializeField] GameObject[] answersButtons;

    [Header("Button colors")]
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;
    private Image buttonImage;

    [Header("Timer")]
    [SerializeField] Image timerImage;
    private Timer timer;

    private void Start()
    {
        timer = FindObjectOfType<Timer>();
        DisplayQuestion();
    }

    private void Update()
    {
        timerImage.fillAmount = timer.fillFraction;
        if (timer.loadNextQuestion)
        {
            GetNextQuestion();
            timer.loadNextQuestion = false;
        }
        else if (!timer.isAnsweringQuestion)
        {
            OnAnswerSelected(-1);
        }
    }
    public void OnAnswerSelected(int index)
    {
        int correctAnswerIndex = question.GetCorrectAnswerIndex();
        string correctAnswer = question.GetAnswer(correctAnswerIndex);
    
        correctAnswerIndex = question.GetCorrectAnswerIndex();

        if (index == correctAnswerIndex)
        {
            questionText.text = "Correct!";
        }
        else
        {
            questionText.text = "Incorrect!\n" + $"Correct answer is: {correctAnswer}";
        }

        SetButtonState(false);
        buttonImage = answersButtons[correctAnswerIndex].GetComponent<Image>();
        buttonImage.sprite = correctAnswerSprite;
        timer.CancelTimer();
 
    }
    
    private void DisplayQuestion()
    {
        questionText.text = question.GetQuestion();

        for (int i = 0; i < answersButtons.Length; i++)
        {
            TextMeshProUGUI buttonText = answersButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = question.GetAnswer(i);
        }
    }

    private void GetNextQuestion()
    {
        SetButtonState(true);
        SetButtonsDefaultSprite();
        DisplayQuestion();
    }

    private void SetButtonState(bool state)
    {
        for (int i = 0; i < answersButtons.Length; i++)
        {
            Button button = answersButtons[i].GetComponent<Button>();
            button.interactable = state;
        }
    }

    private void SetButtonsDefaultSprite()
    {
        for (int i = 0; i < answersButtons.Length; i++)
        {

            buttonImage = answersButtons[i].GetComponent<Image>();
            buttonImage.sprite = defaultAnswerSprite;
        }
    }

}
