using UnityEngine;

namespace AccuChekVRGame
{
    public static class GameConfig
    {
        //PLAYER PREFS
        public static string GameCountSaved = "GameCount";
        public static string NameSaved = "Name";
        public static string NumberSaved = "Number";
        public static string ScoreSaved = "Score";
        public static string MinutesSaved = "Minutes";
        public static string SecondsSaved = "Seconds";
        public static string MiliSecondsSaved = "MiliSeconds";
        public static string QuestionIndexSaved = "QuesIndex";

        //GAME DATA        
        public static bool isOut; //THIS VARIABLE INDICATES THAT THE PLAYER HAS LOST BY WICKET
        public static bool isTryBall; //THIS VARIABLE INDICATES THAT THE FIRST BALL IS TRY BALL
        public static int gameCount; //THIS VARIABLE WILL BE INCREMENTED AFTER EACH GAME AND WILL BE USED TO GET DATA OF ALL USERS

        //PLAYER INFORMATION
        public static string name;
        public static string number;
        public static int score;
        public static int minutes;
        public static int seconds;
        public static int miliseconds;

        public static int QuesNo;

        public static void StartGame()
        {
            isTryBall = true;
            isOut = false;
            gameCount = PlayerPrefs.GetInt(GameCountSaved);
            QuesNo = PlayerPrefs.GetInt(QuestionIndexSaved);
            ResetData();
        }
        static void ResetData()
        {
            name = "";
            number = "";
            score = 0;
            minutes = 0;
            seconds = 0;
            miliseconds = 0;
        }
        public static void GameEnded()
        {

            Debug.Log("Game Count"+gameCount);
            SavePlayerDataToPref();
            gameCount++;
            PlayerPrefs.SetInt(GameCountSaved, gameCount);
        }

        #region SET DATA
        public static void SetDataToConfig(string _Name, string _number,int _Score, int _Minutes, int _Seconds, int _MiliSeconds)
        {
            name = _Name;
            number = _number;
            score = _Score;
            minutes = _Minutes;
            seconds = _Seconds;
            miliseconds = _MiliSeconds;

#if UNITY_EDITOR
            //for testing purposes only
            GameEnded();
#endif
        }        
        public static void SetDataToConfig(string _Name, string _Number, int _Score)
        {
            name = _Name;
            number = _Number;
            score = _Score;
        }          
        public static void SetDataToConfig(string _Name, string _Number)
        {
            name = _Name;
            number = _Number;
        }        
        public static void SetDataToConfig(int _Minutes, int _Seconds, int _MiliSeconds)
        {
            minutes = _Minutes;
            seconds = _Seconds;
            miliseconds = _MiliSeconds;
        }

        #endregion

        static void SavePlayerDataToPref()
        {
            PlayerPrefs.SetString(NameSaved+ gameCount, name);
            Debug.Log("NAME SAVED AS ---> "+NameSaved + gameCount);
            PlayerPrefs.SetString(NumberSaved + gameCount, number);
            PlayerPrefs.SetInt(ScoreSaved + gameCount, score);
            PlayerPrefs.SetInt(MinutesSaved + gameCount, minutes);
            PlayerPrefs.SetInt(SecondsSaved + gameCount, seconds);
            PlayerPrefs.SetInt(MiliSecondsSaved + gameCount, miliseconds);
            PlayerPrefs.SetInt(QuestionIndexSaved, QuesNo);
        }        

        public static string GetPlayerPrefString(string prefName)
        {
            return PlayerPrefs.GetString(prefName);
        }
        public static int GetPlayerPrefsInt(string prefName)
        {
            return PlayerPrefs.GetInt(prefName);
        }
    }
}
