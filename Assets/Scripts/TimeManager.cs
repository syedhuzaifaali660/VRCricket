using AccuChekVRGame;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script is used to save overall time spent by each team individualy in all rounds.
/// </summary>
public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;
    [Tooltip("HOW MUCH TIME HAS PASSED")] public float timeElapsed = 0;
    [Tooltip("CHECKS IF THE TIMER IS RUNNING")] public bool timerIsRunning = false;
    public Text timeText;
    private float minutes, highestMin; // minutes here is the variable in which current minute value will stored, highestMin will hold the value after adding previous + current min value 
    private float ValueTempStoring, seconds, highestSec; // seconds here is the variable in which current seconds value will stored, highestSec will hold the value after adding previous + current sec value
    private float ValueTempStoring2, milliSeconds, highestMili; // miliseconds here is the variable in which current miliseconds value will stored, highestMiliSec will hold the value after adding previous + current milisec value

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Update()
    {
        if (timerIsRunning)
        {
            if (timeElapsed >= 0)
            {
                timeElapsed += Time.deltaTime;
                DisplayTime(timeElapsed);
            }
            else
            {
                timeElapsed = 0;
                timerIsRunning = false;
            }
        }
    }

    /// <summary>
    /// CALCULATING TIMER
    /// </summary>
    /// <param name="timeToDisplay"></param>
    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        minutes = Mathf.FloorToInt(timeToDisplay / 60);
        seconds = Mathf.FloorToInt(timeToDisplay % 60);
        milliSeconds = (timeToDisplay % 1) * 60;
        //HUM YAHA ROUND TO INT ISLIYE KR RHE TAAKE MILLISECONDS SRF 2 DIGIT MAI DIKHE
        milliSeconds = Mathf.RoundToInt(milliSeconds);
        //Debug.Log("mili" + milliSeconds); 
        timeText.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliSeconds);
        //Debug.Log(string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliSeconds)); 
    }

    public void ResetTimer()
    {
        timerIsRunning = false;
        //ADDING PREVOIS AND CURRENT TIME
        AddLastAndCurrentTime();
    }

    /// <summary>
    /// SAVING TIME DATA
    /// </summary>
    /// <param name="highMin"></param>
    /// <param name="highSec"></param>
    /// <param name="highMili"></param>
    private void SavingTimeData(int highMin, int highSec, int highMili)
    {
        GameConfig.SetDataToConfig(highMin, highSec, highMili);
        //GameConfig.SaveTime(GameConfig.GetStringToSaveTime(E_TimeFormat.miliseconds), (int)highMili);
        //GameConfig.SaveTime(GameConfig.GetStringToSaveTime(E_TimeFormat.seconds), (int)highSec);
        //GameConfig.SaveTime(GameConfig.GetStringToSaveTime(E_TimeFormat.minutes), (int)highMin);
    }

    /// <summary>
    /// CHECK IF TIME IN ANY FORMAT IS GREATER THAN 60
    /// THEN ADD 1 IN THE NEXT HIGHEST FORMAT.
    /// EG# IF MILISECONDS IS GREATER THAN 60 THEN MINUS 60 FROM MILLI SECONDS AND ADD 1 IN SECONDS
    /// AND REMAINING VALUES STAYS IN MILISECOND.
    /// </summary>
    private void AddLastAndCurrentTime()
    {
        Debug.Log("TIME BEFORE SAVING \n");
        //Debug.Log("Minute " + PlayerPrefs.GetInt(GameConfig.GetStringToSaveTime(E_TimeFormat.minutes)));
        //Debug.Log("Second " + PlayerPrefs.GetInt(GameConfig.GetStringToSaveTime(E_TimeFormat.seconds)));
        //Debug.Log("Milisecond " + PlayerPrefs.GetInt(GameConfig.GetStringToSaveTime(E_TimeFormat.miliseconds)));

        highestMin = minutes;
        highestSec = seconds;
        highestMili = milliSeconds;
        Debug.Log("highest Minute " + highestMin);
        Debug.Log("highest Second " + highestSec);
        Debug.Log(" highest Milisecond " + highestMili);

        if (highestMili >= 60)
        {
            print("1");
            print("Highest MiliSecond > 60  = " + highestMili);
            ValueTempStoring2 = highestMili - 60;
            highestMili = ValueTempStoring2;
            //After 1 is added in seconds
            //highestSec = PlayerPrefs.GetInt(GameConfig.GetStringToSaveTime(E_TimeFormat.seconds)) + 1;
            highestSec = highestSec + 1;

            //Save seconds as well as miliseconds
            SavingTimeData((int)highestMin, (int)highestSec, (int)highestMili);
            ResetTimerValues();
        }
        if (highestSec >= 60)
        {
            print("2");
            print("HighestSec > 60  = " + highestSec);
            ValueTempStoring = highestSec - 60;
            highestSec = ValueTempStoring;

            //After 1 is added in minutes
            highestMin = highestMin + 1;
            //Save seconds as well as miliseconds
            SavingTimeData((int)highestMin, (int)highestSec, (int)highestMili);
            ResetTimerValues();
        }
        else
        {
            print("3");
            print("HighestSec < 60 " + highestSec);
            SavingTimeData((int)highestMin, (int)highestSec, (int)highestMili);
            ResetTimerValues();
        }

        Debug.Log("\n TIME AFTER SAVING \n");
        //Debug.Log("Minute " + PlayerPrefs.GetInt(GameConfig.GetStringToSaveTime(E_TimeFormat.minutes)));
        //Debug.Log("Second " + PlayerPrefs.GetInt(GameConfig.GetStringToSaveTime(E_TimeFormat.seconds)));
        //Debug.Log("Milisecond " + PlayerPrefs.GetInt(GameConfig.GetStringToSaveTime(E_TimeFormat.miliseconds)));
    }

    private void ResetTimerValues()
    {
        timeElapsed = 0;
        minutes = 0;
        seconds = 0;
        milliSeconds = 0;
    }

    public void StartTimer()
    {
        timerIsRunning = true;
    }
    public void ShowScore_OnClick()
    {

    }
}

