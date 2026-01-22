using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using AccuChekVRGame;

public class SpeedManager : MonoBehaviour {
	public TextManager tm;
	public VariableManager vm;
	public CountDownManager cm;
	public Rigidbody[] StumpsRigidbody;
	public BoxCollider speed_BoxCollider;
	public float ballLaunchTime;

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Ball")
		{
			speed_BoxCollider.enabled = false;
            try
			{
				Debug.Log("Speed Trigger");
				tm.SetSpeedText(((int)other.gameObject.GetComponent<Rigidbody>().velocity.magnitude * 3.6).ToString() + "Km/hr");
				Invoke(nameof(nextBall), ballLaunchTime);
				if (GameConfig.isTryBall)
					Invoke(nameof(TryBallDone), 13f);

			}
			catch (NullReferenceException e)
			{
			}

		}

		//Destroy (this.gameObject.GetComponent<SpeedManager>());
	}
	void nextBall()
	{
        cm.StartCountDown();
    }
	void TryBallDone()
	{
		foreach (Rigidbody item in StumpsRigidbody)
		{
			item.isKinematic = false;
		}
        GameConfig.isTryBall = false;
        tm.TryBallText.gameObject.SetActive(false);
    }
}
