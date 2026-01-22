using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using System;
using UnityEngine.SceneManagement;
using AccuChekVRGame;
using TMPro;

public class QuestionAnswerManager : MonoBehaviour
{
    public static QuestionAnswerManager instance;
    [Header("HANDLER")]
    //public CountDownManager _cdm;
    public TimeManager _tm;
    [Header("OTHER")]
    public GameObject QuestionText;
    public GameObject[] OptionsText, OptionsBtns;
    private int selectedAnswer, teamRound2Counter, teamRound3Counter;
    [HideInInspector]
    public bool canAnswer = true;
    QuestionsData q_Data;
    public SO_Questions SO_Data;
    //public GameObject LifeLine50;
    private int optIndex;
    private int optCounter;
    public GameObject QuestionPanel;
    public GameObject LeaderboardPanel;
    public GameObject PopUpPanel;
    //public float delayTime_Buzzer = 20f;
    //public GameObject RandomListText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        if (GameConfig.isOut)
        {
            OpenPanel(PopUpPanel);
        }
        else
        {
            setNewQuestion();
        }
    }
    public void setNewQuestion()
    {
        OpenPanel(QuestionPanel);
        setTimerAndOtherThings();
        AddQuestInfo(); // adding question and ans in their respective fields
    }
    void setTimerAndOtherThings()
    {
        canAnswer = true;
        //if (CheckHasTimer() == true) _cdm.StartCountDown(); //question timer
        _tm.StartTimer(); //Time spend by each team
        //GameConfig.canMinusScore = true;
    }
    public void OnAnswered()
    {
        if (canAnswer == false) return;
        if (canAnswer) canAnswer = false;
        TimeManager.instance.ResetTimer();
        AfterAnswered();
        StartCoroutine(addDelay (() => {
            OpenPanel(PopUpPanel);
        },4f));
    }

    public IEnumerator addDelay(Action action, float time)
    {
        yield return new WaitForSeconds(time);
        action();
    }
    void AddQuestInfo()
    {
        //SoundManager.instance.StopOtherSound(true, E_SoundName.CorrectAns.ToString());
        //SoundManager.instance.StopOtherSound(true, E_SoundName.WrongAns.ToString());
        foreach (GameObject item in OptionsBtns)
        {
            item.GetComponent<Image>().color = Color.white;
            item.GetComponent<Button>().interactable = true;
            item.transform.GetChild(0).GetComponent<TMP_Text>().gameObject.SetActive(true);
        }
        //q_Data = GameManager.Instance.UpdateQuestionsIdex(GameConfig.currentRound);
        if(GameConfig.QuesNo >= 10)
        {
            GameConfig.QuesNo = 0;
        }
        q_Data = SO_Data.dataDoc.rounds[0].questionData[GameConfig.QuesNo];
        QuestionText.GetComponent<TMP_Text>().text = q_Data.question;
        OptionsText[0].GetComponent<TMP_Text>().text = q_Data.answer1.ToUpper();
        OptionsText[1].GetComponent<TMP_Text>().text = q_Data.answer2.ToUpper();
        OptionsText[2].GetComponent<TMP_Text>().text = q_Data.answer3.ToUpper();
        OptionsText[3].GetComponent<TMP_Text>().text = q_Data.answer4.ToUpper();

        if (q_Data.IsTrueFalseQues)
        {
            OptionsBtns[2].gameObject.SetActive(false);
            OptionsBtns[3].gameObject.SetActive(false);
        }
        else
        {
            OptionsBtns[2].gameObject.SetActive(true);
            OptionsBtns[3].gameObject.SetActive(true);
        }
        GameConfig.QuesNo++;
    }

    void AfterAnswered()
    {
        foreach (GameObject item in OptionsBtns) { item.GetComponent<Image>().color = Color.white; }
        string buttonName = EventSystem.current.currentSelectedGameObject.name;
        selectedAnswer = int.Parse(buttonName);

        if (int.Parse(q_Data.correctAnswer) == selectedAnswer)
        {
            SoundManager.Instance.PlaySound((int)E_SoundClip.CorrectAns);
            OptionsBtns[selectedAnswer - 1].GetComponent<Image>().color = Color.green;
            var tempScore = GameConfig.score;
            tempScore += 10;
            GameConfig.score = tempScore;
            GameConfig.GameEnded();
            //SAVE SCORE HERE
        }
        else
        {
            SoundManager.Instance.PlaySound((int)E_SoundClip.WrongAns);
            Debug.Log("Sound");
            OptionsBtns[selectedAnswer - 1].GetComponent<Image>().color = Color.red;
            OptionsBtns[int.Parse(q_Data.correctAnswer) - 1].GetComponent<Image>().color = Color.green;
            GameConfig.GameEnded();
            //SAVE SCORE HERE

        }
    }
    public bool CheckHasTimer()
    {
        return SO_Data.dataDoc.rounds[0].rules.hasCountDown;
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void OpenPanel(GameObject go)
    {
        QuestionPanel.SetActive(go.Equals(QuestionPanel));
        PopUpPanel.SetActive(go.Equals(PopUpPanel));
        LeaderboardPanel.SetActive(go.Equals(LeaderboardPanel));
    }

    /// <summary>
    /// question counter check krta hai k total round question limit se kam hai toh counter 
    /// increment krdeta hai wrna round complete krdeta hai
    /// </summary>
    /// <param name="currentRound"></param>
    //public void updateQuestion()
    //{
    //    //int QuesCounter = PlayerPrefs.GetInt(GameConfig.currentRound.ToString() + "quesCounter");
    //    print("update quest round = " + currentRound + "update ques index = " + QuesCounter);
    //    if (QuesCounter <= GameConfig.currentRoundData.questionData.Count)
    //    {
    //        if (GameConfig.currentRoundData.totalRoundQuestions == QuesCounter)
    //        {
    //            PlayerPrefs.SetInt(GameConfig.currentGameType.ToString(), 1);
    //            PlayerPrefs.SetInt(GameConfig.currentRound.ToString() + "quesCounter", 0);
    //            Debug.Log(GameConfig.currentRound + "completed");
    //            return;
    //        }
    //        print("counter before " + QuesCounter);
    //        QuesCounter++;

    //        PlayerPrefs.SetInt(GameConfig.currentRound.ToString() + "quesCounter", QuesCounter);
    //        print("counter after " + QuesCounter);

    //    }
    //}


    //public void ShowRandomListText(bool state)
    //{
    //    if (!state) return;
    //    RandomListText.SetActive(state);
    //    RandomListText.GetComponent<Text>().text = string.Empty;
    //    int count = 0;
    //    foreach (var a in GameManager.Instance.data.dataDoc.rounds[((int)GameConfig.currentRound)].questionRandom)
    //    {
    //        RandomListText.GetComponent<Text>().text += "Q" + count + " - " + a.ToString() + "\n";
    //        count++;
    //    }
    //}

   
}
