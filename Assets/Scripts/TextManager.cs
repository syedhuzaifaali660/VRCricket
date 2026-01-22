using AccuChekVRGame;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TextManager : MonoBehaviour {

	public VariableManager variableManager;
	public TextMeshProUGUI scoreText, ballText, speedText, nameText, bowlingTypeText, TryBallText;
    public TMP_InputField Email, Fullname, phoneNumberInput, resetPasswordInput;
	public GameObject LoginFailed, loginCanvas, startCanvas, resetPasswordFailed, resetPasswordSuccess, LeftCanvas, RightCanvas, renderCanvas;
	public string playerEmail, playerName, playerPhoneNo;
    public GameObject VR_UIRay, VR_Bat;
    public Button LoginConfirmBtn;

    public void UIAtStart()
    {
        //startCanvas.SetActive(true);
        //LeftCanvas.SetActive(false);
        //RightCanvas.SetActive(false);
        //TryBallText.gameObject.SetActive(true);
        //Fullname.text = "";
        //resetPasswordInput.text = "";
        //phoneNumberInput.text = "";
        SetNameText("Test");
        GameManager.instance.GameStarted();
    }

    public void EnableRenderTexture()
    {
        renderCanvas.SetActive(true);
        Invoke(nameof(DisableRenderTexture), 13f);
    }   
    public void DisableRenderTexture()
    {
        renderCanvas.SetActive(false);
    }
    public void SetScoreText(string value) {
		scoreText.text = value;
	}

	public void SetballText(string value) {
		ballText.text = string.Format("{0:0}",value) + " Balls";
	}
	public void SetSpeedText(string value) {
		speedText.text = value;
	}

	public void SetNameText(string value) {
		nameText.text = value;
	}

	public void SetBowlingTypeText(string value) {
		bowlingTypeText.text = value;
	}

    public bool CheckResetPassword()
    {
        bool IsCorrectPass = resetPasswordInput.text == variableManager.GetResetPass();
        if (!string.IsNullOrEmpty(resetPasswordInput.text) && IsCorrectPass)
        {
            return true;
        }else
            return false;
    }

    public void EnableConfirmButton()
    {
        if(phoneNumberInput.text.Length > 2)
        {
            LoginConfirmBtn.gameObject.SetActive(true);
        }
        else
        {
            LoginConfirmBtn.gameObject.SetActive(false);
        }
    }

    #region PhoneNo

    public void SaveNameAndCellNo_Onclick()
    {
        Debug.Log("Name " + Fullname.text + " Count: " + Fullname.text.Length);
        Debug.Log("PhoneNo " + phoneNumberInput.text);
        // Get the phone number input text
        string phoneNumber = phoneNumberInput.text;

        // Define a regular expression pattern for an 11-digit Pakistani phone number starting with '0'
        string pattern = @"^0\d{2,10}$";

        // Use Regex.IsMatch to check if the input matches the pattern
        bool isPhoneNumberValid = Regex.IsMatch(phoneNumber, pattern);

        // Display validation result
        if (!string.IsNullOrEmpty(phoneNumberInput.text) && !string.IsNullOrWhiteSpace(phoneNumberInput.text) && isPhoneNumberValid && !string.IsNullOrEmpty(Fullname.text) && !string.IsNullOrWhiteSpace(Fullname.text) && Fullname.text.Length <= 40)
        {
            playerName = Fullname.text;
            playerPhoneNo = phoneNumberInput.text;
            GameConfig.SetDataToConfig(playerName, playerPhoneNo);
            SetNameText(playerName);
            Debug.Log("PhoneNo Checked Starting Game");
            GameManager.instance.GameStarted();
            LeftCanvas.SetActive(true);
            RightCanvas.SetActive(true);
            loginCanvas.SetActive(false);
            VR_UIRay.SetActive(false);
            VR_Bat.SetActive(true);
        }
        else
        {
            LoginFailed.SetActive(true);
        }
    }
    #endregion 
}
