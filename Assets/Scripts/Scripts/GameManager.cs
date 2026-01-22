using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AccuChekVRGame
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;
        public VariableManager variableManager;
        public TextManager tm;
        public GameObject FireworksParticles;

        private void Awake()
        {
            if(instance == null)
            {
                instance = this;
            }
        }

        private void Start()
        {
            GameConfig.StartGame();
            tm.UIAtStart();
        }
        public void StumpedOut()
        {
            variableManager.SetIsStumpedOut(true);
            //GameConfig.GameEnded();
        }
        public void GameStarted()
        {
            variableManager.SetIsGameStarted(true);
            CountDownManager.instance.StartCountDown();
        }

        public void GameEnded()
        {

            Debug.Log(GameConfig.GetPlayerPrefsInt(GameConfig.GameCountSaved) - 1);
            Debug.Log("Game Ended.. Either Bowled or total Bowls delivered");
            GameConfig.SetDataToConfig(tm.playerName, tm.playerPhoneNo, variableManager.GetScoreCount());
            variableManager.SetIsGameEnded(true);
            CountDownManager.instance.StopCountDown();
            Invoke(nameof(GoToQuesScene), 4f);
        }

        void GoToQuesScene()
        {
            SceneManager.LoadScene(1);
        }

        public void PlayFireworks()
        {
            FireworksParticles.SetActive(true);
            SoundManager.Instance.PlaySound((int)E_SoundClip.Fireworks);
            Invoke(nameof(StopFireworks), 7f);
        }

        public void StopFireworks()
        {
            FireworksParticles.SetActive(false);        
        }
        
        public void ResetAllPlayerPref()
        {
            if (tm.CheckResetPassword())
            {
                PlayerPrefs.DeleteAll();
                GameConfig.gameCount = 0;
                tm.resetPasswordSuccess.SetActive(true );
            }
            else
            {
                tm.resetPasswordFailed.SetActive(true);
            }
        }
    }
}
