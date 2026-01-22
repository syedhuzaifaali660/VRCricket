using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "DataQuestions", menuName = "Data/QuestionStorage")]
public class SO_Questions : ScriptableObject
{
    public DataDocument dataDoc = new DataDocument();


    private void OnValidate()
    {
        for (int i = 0; i < dataDoc.rounds.Count; i++)
        {
            dataDoc.rounds[i].Validate();
        }
        for (int i = 0; i < dataDoc.rounds.Count; i++)
        {
            dataDoc.rounds[i].name = "Round " + i;
            dataDoc.rounds[i].rules.Validate();
        }

    }
    [ContextMenu("Random Run")]
    public void QuestionRandomizer()
    {
        int count = 0;
        dataDoc.rounds[count].questionRandom.Clear();
        while (count <= 2)
        {
            int Rno = UnityEngine.Random.Range(0, dataDoc.rounds[count].questionData.Count);
            if (!dataDoc.rounds[count].questionRandom.Contains(Rno))
            {
                dataDoc.rounds[count].questionRandom.Add(Rno);
            }
            if (dataDoc.rounds[count].questionRandom.Count >= dataDoc.rounds[count].questionData.Count)
            {
                count++;
                if(count < 3)
                dataDoc.rounds[count].questionRandom.Clear();
            }
        }
    }
}


[Serializable]
public class DataDocument
{
    [NonReorderable]
    public List<RoundsData> rounds;
    public bool IsDollyScene, IsDebug;
}

[Serializable]
public class RoundsData
{
    [HideInInspector]
    public string name;
    public GameRules rules;
    public int score;
    public int deductScore;
    [Tooltip("total questions in this entire round.")]
    public int totalRoundQuestions;
    [Tooltip("max questions each team can play for in each turn.")]
    public int totalQuestionsPerTurn;
    [Tooltip("Random List to put question")][NonReorderable] public List<int> questionRandom;
    [NonReorderable]
    public List<QuestionsData> questionData;

    public void Validate()
    {
        for (int i = 0; i < questionData.Count; i++)
        {
            questionData[i].name = "Question " + i;
        }
    }

}
 

[Serializable]
public class QuestionsData
{
    [HideInInspector]
    public string name;
    public string question;
    public string answer1, answer2, answer3, answer4;
    public string correctAnswer;
    public bool IsTrueFalseQues;

}


[Serializable]
public class GameRules
{
    public bool hasCountDown;
    public float countdownTime;
    public bool canDeductScore;
    //public GameType gameType;
    //public E_Approach approach;
    [Range(1, 5)]
    [Tooltip("How many times each team plays this round.")]
    public int totalTeamTurns = 1;
    bool firstrun = true;
    public void Validate()
    {
        //if (firstrun) { questionsLimit = 1; firstrun = false; }
        Debug.Log(totalTeamTurns);
    }
}

