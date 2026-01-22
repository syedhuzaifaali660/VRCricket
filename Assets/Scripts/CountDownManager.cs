using AccuChekVRGame;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CountDownManager : MonoBehaviour
{
    public static CountDownManager instance;
    public TextMeshProUGUI countDownText;
    public bool startCountDown = false;
    public float timeRemaining = 5;
    private float minutes, seconds;
    bool runOneTime; //it is used to run the code only one time if time remaining is up
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    // Update is called once per frame
    void Update()
    {
        UpdateCountDown();
    }

    void UpdateCountDown()
    {
        if (startCountDown)
        {
            if (timeRemaining <= 1)
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
        
                if (runOneTime) { StopCountDown(); BallLauncher1x.instance.isAllowBall = true; runOneTime = false; }
            }
            else
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
        }
    }
    void DisplayTime(float timeToDisplay)
    {
        minutes = Mathf.FloorToInt(timeToDisplay / 60);
        seconds = Mathf.FloorToInt(timeToDisplay % 60);
        countDownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        //Debug.Log(string.Format("{0:00}:{1:00}", minutes, seconds));
    }
    public void StopCountDown()
    {
        if (!startCountDown) return;
        Debug.Log("************CountDown Stopped");
        startCountDown = false;
    }
    public void CountDownTextOff()
    {
        countDownText.transform.parent.gameObject.SetActive(false);
    }
    public void StartCountDown()
    {
        if (BallLauncher1x.instance.ChecksBowlsLeft() && !GameManager.instance.variableManager.GetIsStumpedOut())
        {
            timeRemaining = 5;
            startCountDown = true;
            runOneTime = true;
            countDownText.transform.parent.gameObject.SetActive(true);
            BallLauncher1x.instance.SetRandomBowlData();
        }
    }
}
