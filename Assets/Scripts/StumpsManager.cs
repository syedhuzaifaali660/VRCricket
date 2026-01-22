using AccuChekVRGame;
using UnityEngine;

public class StumpsManager : MonoBehaviour {

	private void OnTriggerEnter(Collider other)
	{
        if (other.gameObject.tag == "Ball" || other.gameObject.tag == "Bat")
        {
           

            if (!GameConfig.isTryBall && !GameConfig.isOut)
            {
                Debug.Log("Ball Collided with stumps ");
                //Debug.Log("local Out " + isOut);
                //Debug.Log("Satic Out " + GameConfig.isOut);

                SoundManager.Instance.PlaySound((int)E_SoundClip.Stumps);
                GameManager.instance.StumpedOut();
                GameManager.instance.GameEnded();
            }
        }
    }
	//void resetStumps()
	//{
	//	this.transform.localPosition = initialPos;
	//	this.transform.localRotation = initialRot;
	//}

}
