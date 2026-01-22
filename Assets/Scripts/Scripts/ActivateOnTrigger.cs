using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace AccuChekVRGame
{
    public class ActivateOnTrigger : MonoBehaviour
    {
        [SerializeField] XRBaseController rightController;
        private void OnCollisionEnter(Collision other)
        {
            //if (other.TryGetComponent(out ITriggerEnter<ActivateOnTrigger> triggerEnter))
            //    triggerEnter.TriggerEnter(this);
            if(other.gameObject.tag == "Ball")
            {
                Debug.Log("Vibrate ball hit");
                // Send haptic impulse to the right controller
                rightController.SendHapticImpulse(1.0f, 1f);
            }

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Stadium" || other.gameObject.tag == "Pitch")
            {
                Debug.Log("Vibrate stadium hit");
                SoundManager.Instance.PlaySound((int)E_SoundClip.batHitPitch);
                // Send haptic impulse to the right controller
                rightController.SendHapticImpulse(0.20f, 0.2f);
            }
        }

        //private void OnTriggerExit(Collider other)
        //{
        //    if (other.TryGetComponent(out ITriggerExit<ActivateOnTrigger> triggerEnter))
        //        triggerEnter.TriggerExit(this);
        //}
    }
}
