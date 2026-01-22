using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PitchTrigger : MonoBehaviour {
	
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Ball") {

			try {
				//Debug.Log("Pitch Hit Spin");
				other.gameObject.GetComponent<SpinBall1x> ().pitchHit = true;
			} catch (NullReferenceException e) {
			}

		}
	}
}
