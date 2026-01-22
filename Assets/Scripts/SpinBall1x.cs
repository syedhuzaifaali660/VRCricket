using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinBall1x : MonoBehaviour {

	public bool pitchHit;
	VariableManager vm;
	BallLauncher1x bl;
	public float magnus = 0.03f;
	public Rigidbody rb;

    void Start () {
		vm = FindObjectOfType<VariableManager>();
		pitchHit = false;		
		bl = FindObjectOfType<BallLauncher1x> ();
		rb = GetComponent<Rigidbody>();
	}

	void FixedUpdate() {
       float distance = Vector3.Magnitude(new Vector3(this.gameObject.transform.position.x, 0, this.gameObject.transform.position.z));
		//Debug.Log("distance  ball " + distance);
		if (distance >= 11.0f) return;

        if (pitchHit && !vm.GetBatHit ()) {
			if (bl.randomX >= -0.02f && bl.randomX <= -0.01f){
                Debug.Log("Spin 1");
                //this.gameObject.GetComponent<Rigidbody> ().AddForce(magnus * rb.velocity,ForceMode.Impulse); //0.28
                this.gameObject.GetComponent<Rigidbody> ().AddForce (new Vector3 (0.09f, 0f, 0f) * 0.3f, ForceMode.Impulse); //0.28
			}if (bl.randomX >= 0.015f && bl.randomX <= 0.025f){
                Debug.Log("Spin 2");
                this.gameObject.GetComponent<Rigidbody> ().AddForce (new Vector3 (-0.09f, 0f, 0f) * 0.3f, ForceMode.Impulse);
                //this.gameObject.GetComponent<Rigidbody>().AddForce(magnus * rb.velocity, ForceMode.Impulse); //0.28
            }

			/*if (bl.randomX >= -0.08f && bl.randomX <= -0.01f){
				this.gameObject.GetComponent<Rigidbody> ().AddForce (new Vector3 (0.05f, 0f, 0f) * 0.28f, ForceMode.Impulse);
			} else if (bl.randomX >= 0.02f && bl.randomX <= 0.08f){
				this.gameObject.GetComponent<Rigidbody> ().AddForce (new Vector3 (-0.05f, 0f, 0f) * 0.28f, ForceMode.Impulse);
			}*/

		} 
		else {
			pitchHit = false;
		}

	}
}
