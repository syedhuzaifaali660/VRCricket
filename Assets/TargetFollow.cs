using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AccuChekVRGame
{
    public class TargetFollow : MonoBehaviour
    {
        public BallLauncher1x ballLauncher1X;
       
        //public Vector3 offset;

        //// Update is called once per frame
        //void Update()
        //{
        //    if(ballLauncher1X.ball == null)
        //    {
        //        return;
        //    }
        //    transform.LookAt(ballLauncher1X.ball.transform.GetChild(1).transform.position);
        //    transform.position = Vector3.MoveTowards( transform.position,ballLauncher1X.ball.transform.GetChild(1).transform.position);
        //}

        public Transform target; // The target GameObject (the ball) to follow
        public Vector3 offset; // The offset from the target
        public float followSpeed = 5f; // Adjust this to control the speed of following
        public Vector3 OffsetLookAt;
        private Vector3 previousPos;

        float x, z;

        bool checker = false;
        void Update()
        {
            if (ballLauncher1X.ball == null) checker = false;

            if (ballLauncher1X.ball != null)
            {
                if (!checker)
                {
                    previousPos = ballLauncher1X.ball.transform.position;
                    checker = true;
                }
                target = ballLauncher1X.ball.transform;
                // Calculate the desired camera position with the offset
                Vector3 desiredPosition = target.position + offset;

                // Smoothly interpolate between the current position and the desired position
                Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);

                // Update the camera's position
                transform.position = smoothedPosition;

                Vector3 lookAtPos = target.position + OffsetLookAt;

                Vector3 targetDir = (target.position - previousPos).normalized;
                Vector3 targetDir2 = (target.position - previousPos);
                transform.LookAt(lookAtPos);

                //Debug.Log("Look At Direction Normalized " + targetDir);
                //Debug.Log("Look At Direction " + targetDir2);
            
                offset.z = SetOffset(ballLauncher1X.ball.transform.position.z,offset.z);
                //offset.x = SetOffset(ballLauncher1X.ball.transform.position.x,offset.x);        
            }


        }

        /// <summary>
        /// CHECKING IF BALL IS NEGATIVE IN Z 
        /// THEN CHANGE THE OFFSET TO POSITIVE
        /// SO THAT THE CAMERA CAN FOLLOW CORRECTLY
        /// </summary>
        /// <param name="ball"></param>
        /// <param name="off"></param>
        private float SetOffset(float ball,float off)
        {
            if (ball < 0 && off < 0)
            {
                //Debug.Log("-----> Postive Z 2");
                //BALL IS NEGATIVE NUMBER
                return 2; //CONVERTING NUMBER TO POSITIVE
            }
            if (ball > 0 && off > 0)
            {
                Debug.Log("-----> Negative Z -2");
                //BALL IS POSITIVE NUMBER
                return -2; //CONVERTING NUMBER TO NEGATIVE
            }
            return off;
        }
    }

}
