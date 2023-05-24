using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class QuestionsLoader
{
    public int Count { get; private set; }

    public QuestionsLoader(int count)
    {
        Count = count;
    }

    private string path = "EnemyQuestions/";

    public Question GetQuestion()
    {
        int index = UnityEngine.Random.Range(1, Count);
        Question question= new Question();
        question.Text = Resources.Load<TextAsset>(path + "Q (" + index + ")").text;
        string[] lines = Resources.Load<TextAsset>(path + "A (" + index + ")").text.Split('\r');

        // Преобразование первой строки в int
        question.RightAnswer = int.Parse(lines[0]);

        // Получение оставшихся строк в виде массива строк
        string[] remainingLines = lines.Skip(1).ToArray();
        foreach (string line in remainingLines)
            question.answers.Add(line);
     
        return question;
    }

}

