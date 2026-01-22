using UnityEngine;
using AccuChekVRGame;

public class VariableManager : MonoBehaviour {
	int score, ballCount;
	public int totalBalls = 3;
	bool batHit,  gameEnded, startGame;
	const string resetPassword = "vrc123";
	void Start () {
		gameEnded = false;
        batHit = false;
		score = 0;
		ballCount = 0;
    }

	public bool GetBatHit() {
		return batHit;
	}
	public string GetResetPass() {
		return resetPassword;
	}
	public void SetIsStumpedOut(bool value){
		GameConfig.isOut = value;
	}	
	public bool GetIsStumpedOut() {
		return GameConfig.isOut;
	}

	public void SetBatHit(bool value){
		batHit = value;
	}
	public bool GetIsGameEnded() {
		return gameEnded;
	}

	public void SetIsGameEnded(bool value){
		gameEnded = value;
	}
	public bool GetIsGameStarted() {
		return startGame;
	}

	public void SetIsGameStarted(bool value){
        startGame = value;
	}
	public int GetBallCount (){
		return ballCount;
	}

	public int GetScoreCount (){
		return score;
	}
	public void SetBallCount (int value){
		ballCount = value;	
	}

	public void SetScoreCount (int value){
		score = value;
	}

	//public bool GetSpinBowling() {
	//	return spinBowling;
	//}

	//public void SetSpinBowling(bool value){
	//	spinBowling = value;
	//}

	//public bool GetFastSpin() {
	//	return fastSpin;
	//}

	//public void SetFastSpin(bool value){
	//	fastSpin = value;
	//}

	//public bool GetFastBowling() {
	//	return fastBowling;
	//}

	//public void SetFastBowling(bool value){
	//	fastBowling = value;
	//}

	//public bool GetPlayFireworksFlag() {
	//	return playFireworks;
	//}

	//public void SetPlayFireworksFlag(bool value){
	//	playFireworks = value;
	//}


}
