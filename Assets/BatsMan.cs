using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARCTechnologies
{
    public class BatsMan : MonoBehaviour
    {
        [SerializeField] GameObject marker;
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag("Ball"))
            {
                ContactPoint contact = collision.contacts[0];
                Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
                Vector3 pos = contact.point;
                Instantiate(marker, new Vector3(pos.x,pos.y,pos.z+0.1f), rot);
                collision.gameObject.GetComponent<BallManager>().stopBall();

                //Destroy(gameObject);
            }
        }
    }
}
