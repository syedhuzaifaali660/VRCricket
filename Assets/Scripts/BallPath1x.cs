using AccuChekVRGame;
using UnityEngine;

public class BallPath1x : MonoBehaviour {
	public BallLauncher1x ballLauncher1x;
	public Vector3 velocity;
    public VariableManager vm;
	public Rigidbody batRigidBody;
	public TextManager tm;

	void OnCollisionEnter(Collision other){
	
		if(other.gameObject.tag=="Ball") {

			Debug.Log("Ball Hit the Bat");
			SoundManager.Instance.PlaySound((int)E_SoundClip.Shot);
			vm.SetBatHit (true);

			velocity = ballLauncher1x.velocity;
			//other.gameObject.GetComponent<Rigidbody> ().AddForce (new Vector3(velocity.x,velocity.y,velocity.z), ForceMode.Impulse);
			other.gameObject.GetComponent<Rigidbody> ().AddRelativeForce(new Vector3(velocity.x,velocity.y,velocity.z), ForceMode.Impulse);
		}

	}
}
