using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FightMenu : MonoBehaviour
{
    public static Action OnChoseRightAnswer;
    public static Action OnChoseNotRightAnswer;

    [SerializeField] private TextMeshProUGUI QuestionText;
    [SerializeField] private List<TextMeshProUGUI> Answers;

    [SerializeField] private int CountQ ;

    private int NumberRightQuestion;

    private void OnEnable()
    {
        LoadQuestion();
    }

    private void LoadQuestion()
    {
        QuestionsLoader questionsLoader = new QuestionsLoader(CountQ);
        Question question = questionsLoader.GetQuestion();
        QuestionText.text = question.Text;
        NumberRightQuestion = question.RightAnswer;
        for (int i = 0; i < 4; i++)
        {
            Answers[i].text = question.answers[i];
        }
    }

    public void ChoseAnswer(int Number)
    {
        if(Number == NumberRightQuestion)
        {
            OnChoseRightAnswer?.Invoke();
        }else
        {
            OnChoseNotRightAnswer?.Invoke();
        }

        LoadQuestion();
    }
}
