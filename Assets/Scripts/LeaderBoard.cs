using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace AccuChekVRGame
{
    public class LeaderBoard : MonoBehaviour
    {
        public List<AllPlayersData> PlayersDataAll;
        public List<AllPlayersData> TopFifteenPlayers;
        [SerializeField] GameObject[] playerObjects;
        
        public void Leaderboard_OnClick()
        {
            PlayersDataAll.Clear();
            TopFifteenPlayers.Clear();
            GetAllPlayerData();
            AddTopPlayersDataToList();
            GenerateDataInLeaderBoard();
        }
        //1ST MUST BE CALLED 1ST AS IT ADDS THE DATA TO ALL DATA LIST 
        public void GetAllPlayerData()
        {

            int GameCount = GameConfig.GetPlayerPrefsInt(GameConfig.GameCountSaved) -1;
            Debug.Log("Game Count ->"+ GameCount);

            for (int i = 0; i <= GameCount; i++)
            {
                var b = GameConfig.GetPlayerPrefString(GameConfig.NameSaved + 0);
                if(string.IsNullOrEmpty(b)) return;

                Debug.Log("Game Count in loop->"+ i);  
                var _name = GameConfig.GetPlayerPrefString(GameConfig.NameSaved + i);
                Debug.Log(_name);
                var _number = GameConfig.GetPlayerPrefString(GameConfig.NumberSaved + i);
                Debug.Log(_number);
                var _score = GameConfig.GetPlayerPrefsInt(GameConfig.ScoreSaved + i);
                Debug.Log(_score);
                var _min = GameConfig.GetPlayerPrefsInt(GameConfig.MinutesSaved + i);
                Debug.Log(_min);
                var _sec = GameConfig.GetPlayerPrefsInt(GameConfig.SecondsSaved + i);
                Debug.Log(_sec);
                var _mili = GameConfig.GetPlayerPrefsInt(GameConfig.MiliSecondsSaved + i);
                Debug.Log(_mili);

                //ADDING DATA TO DUMMY LIST
                AllPlayersData dataComplete = new AllPlayersData();
                dataComplete.name = _name;
                dataComplete.number = _number;
                dataComplete.score = _score;
                dataComplete.min = _min;
                dataComplete.sec = _sec;
                dataComplete.mili = _mili;

                PlayersDataAll.Add(dataComplete);

                //SORTING LIST IN DESCENDING ORDER OF SCORE AND ASSESCENDING ORDER OF MINUTES,SECONDS & MILISECONDS
                PlayersDataAll = PlayersDataAll.OrderByDescending(x => x.score).ThenBy(x => x.min).ThenBy(x => x.sec).ThenBy(x => x.mili).ToList();
            }
        }

        //2ND MUST BE CALLED AFTER THE DATA HAS BEEN ADDED TO ALL DATA LIST
        public void AddTopPlayersDataToList()
        {
            for (int i = 0; i < 15; i++)
            {
                if (i < PlayersDataAll.Count)
                {
                    Debug.Log(PlayersDataAll.Count + "=" + i);
                    TopFifteenPlayers.Add(PlayersDataAll[i]);
                }
            }
        }

        //3RD MUST BE CALLED AFTER THE TOP 15 HAS BEEN ADDED TO TOP 15 LIST
        public void GenerateDataInLeaderBoard()
        {
            Debug.Log("List Count for Top 15 Player -> "+ TopFifteenPlayers.Count);
            for (int i = 0; i < TopFifteenPlayers.Count; i++)
            {
                
                GameObject go = playerObjects[i];
                go.transform.Find("Pos_Text (Legacy)").GetComponent<TextMeshProUGUI>().text = (i+1).ToString();
                go.transform.Find("Name_Text (Legacy)").GetComponent<TextMeshProUGUI>().text = TopFifteenPlayers[i].name;
                go.transform.Find("Phone_Text (Legacy)").GetComponent<TextMeshProUGUI>().text = TopFifteenPlayers[i].number;
                go.transform.Find("Score_Text (Legacy)").GetComponent<TextMeshProUGUI>().text = TopFifteenPlayers[i].score.ToString();
                go.transform.Find("Time_Text (Legacy)").GetComponent<TextMeshProUGUI>().text = TopFifteenPlayers[i].min+":"+TopFifteenPlayers[i].sec+":"+ TopFifteenPlayers[i].mili;
            }
        }




        #if UNITY_EDITOR

        [ContextMenu("1ST Generate Dummy Data in Pref")]
        public void GenerateData()
        {
            GenerateData(10);
        }

        [ContextMenu("2nd ADD DUMMY DATA TO ALL PLAYER LIST")]
        public void AddDummyEntries()
        {
            //GenerateData(40);
            GetAllPlayerData();
        }        

        [ContextMenu("3RD SORTING & ADDING DATA TO TOP 15 LIST")]
        public void Sorting()
        {
            //SORTING LIST IN DESCENDING ORDER OF SCORE AND ASSESCENDING ORDER OF MINUTES,SECONDS & MILISECONDS
            PlayersDataAll = PlayersDataAll.OrderByDescending(x => x.score).ThenBy(x => x.min).ThenBy(x => x.sec).ThenBy(x => x.mili).ToList();

            for (int i = 0; i < 15; i++)
            {
                TopFifteenPlayers.Add(PlayersDataAll[i]);
            }
        }

        void GenerateData(int numEntries)
        {
            
            for (int i = 0; i < numEntries; i++)
            {
                AllPlayersData entry = new AllPlayersData();
                entry.name = GenerateRandomName();
                Debug.Log(entry.name);
                entry.number = GenerateRandomNumber();
                Debug.Log(entry.number);
                entry.score = Random.Range(0, 50); // Adjust the score range as needed
                Debug.Log(entry.score);
                entry.min = Random.Range(0, 60); // Minutes should be between 0 and 59
                Debug.Log(entry.min);
                entry.sec = Random.Range(0, 60); // Seconds should be between 0 and 59
                Debug.Log(entry.sec);
                entry.mili = Random.Range(0, 60); // Milliseconds should be between 0 and 999
                Debug.Log(entry.mili);

                GameConfig.SetDataToConfig(entry.name, entry.number, entry.score, entry.min, entry.sec, entry.mili);
            }



        }

        private string GenerateRandomName()
        {
            // Implement your logic to generate random names here
            // You can use arrays of names and concatenate them randomly
            // Or use a name generator library if needed
            return "RandomName" + Random.Range(1, 1000);
        }

        private string GenerateRandomNumber()
        {
            // Ensure the first digit is not 0
            int firstDigit = Random.Range(1, 10);

            // Generate the remaining 10 digits
            string phoneNumber = firstDigit.ToString();
            for (int i = 1; i < 11; i++)
            {
                int digit = Random.Range(0, 10);
                phoneNumber += digit.ToString();
            }

            return phoneNumber;
        }

        #endif


    }
    [System.Serializable]
    public class AllPlayersData
    {
        public string name;
        public string number;
        public int score;
        public int min;
        public int sec;
        public int mili;
    }
    
}
