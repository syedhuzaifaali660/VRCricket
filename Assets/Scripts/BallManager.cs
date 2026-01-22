using AccuChekVRGame;
using ARCTechnologies;
using Unity.XR.Oculus;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR.Interaction.Toolkit;

public class BallManager : MonoBehaviour, ITriggerEnter<ActivateOnTrigger>, ITriggerExit<ActivateOnTrigger>
{
    bool batHitFlag, canRender;
    int ballBounceCount, score;
    float distance;
    VariableManager vm;
    TextManager tm;

    [SerializeField] XRBaseController rightController;
    public float stoppingMagnitude;
    public bool checkStoppingMagnitude;
    public Ball ballType;
    public Vector3 direction;
    private float spinBy; // value to spin the ball by

    // Use this for initialization
    void Start()
    {
        batHitFlag = false;
        canRender = true;
        ballBounceCount = 0;
        vm = FindObjectOfType<VariableManager>();
        tm = FindObjectOfType<TextManager>();
        //Destroy(this.gameObject, 12f);
    }

    void FixedUpdate()
    {
        distance = Vector3.Magnitude(new Vector3(this.gameObject.transform.position.x, 0, this.gameObject.transform.position.z));
        //Debug.Log("distanc " +distance);
        if (!GameConfig.isTryBall && distance > 82.5f && batHitFlag)
        {
            batHitFlag = false;

            score = vm.GetScoreCount();
            if (ballBounceCount > 0)
            {
                score += 4;
            }
            else
            {
                //vm.SetPlayFireworksFlag (true);
                score += 6;
            }
            tm.DisableRenderTexture();
            SoundManager.Instance.PlayBoundarySound();
            SoundManager.Instance.CrowdSound();
            GameManager.instance.PlayFireworks();
            Debug.Log(score);
            vm.SetScoreCount(score);
            tm.SetScoreText(score.ToString());
        }

        //Debug.Log(GetComponent<Rigidbody>().velocity.magnitude);
        //if (checkStoppingMagnitude)
        //{
        //    if (GetComponent<Rigidbody>().velocity.magnitude <= stoppingMagnitude )
        //    {
        //        Debug.Log($"Shot end:");
        //        batHitFlag = false;
        //        GetComponent<Rigidbody>().velocity = Vector3.zero;
        //        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        //        //GetComponent<Rigidbody>().isKinematic = true;
        //        //transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z));
        //        //onShotEnd?.Invoke();
        //        //resetMagnitude();
        //        return;
        //    }
        //}


    }

    [SerializeField] bool enableRenderCanvas;
    public void hitball()
    {
        if (batHitFlag)
            return;
        
        Debug.Log("bat true");
        batHitFlag = true;
        //hitFrame = Time.frameCount;

        if (canRender)
        {
            canRender = false;
            if (enableRenderCanvas)
            {
                tm.EnableRenderTexture();

            }

        }

    }
    private bool firstBounce;
    private float angle;
    [SerializeField] float bounceScalar;

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Stadium")
        {
            Debug.Log("Collided with stadium");

            if (ballType.enum_ballType == E_BallTypes.Fast_LegCutter || ballType.enum_ballType == E_BallTypes.Spin_LegBreak)
            {
                spinBy = ballType.getSpin() / ballType.currenSpeed; // change spinBy to a positive value based on the spinScalar value and the ball's speed

            }
            else if (ballType.enum_ballType == E_BallTypes.Spin_Offbreak || ballType.enum_ballType == E_BallTypes.Spin_Offbreak)
            {
                spinBy = -ballType.getSpin() / ballType.currenSpeed; // change spinBy to a negative value based on the spinScalar value and the ball's speed
            }
            else if (ballType.enum_ballType == E_BallTypes.NormalBall || ballType.enum_ballType == E_BallTypes.Fast_Yorker || ballType.enum_ballType == E_BallTypes.Fast_Bouncer)
            {
                spinBy = direction.x; // don't change spinBy 
            }

            //switch (ballType.ballType)
            //{ // check the ballType and set the spinBy value depending on the ball's speed
            //    case E_BallTypes.NormalBall:
            //        spinBy = direction.x; // don't change spinBy 
            //        break;
            //    case E_BallTypes.Spin_LegBreak:
            //        spinBy = ballType.getSpin() / ballType.currenSpeed; // change spinBy to a positive value based on the spinScalar value and the ball's speed
            //        break;
            //    case E_BallTypes.Spin_Offbreak:
            //        spinBy = -ballType.getSpin() / ballType.currenSpeed; // change spinBy to a negative value based on the spinScalar value and the ball's speed
            //        break;
            //    case E_BallTypes.Fast_LegCutter:
            //        spinBy = ballType.getSpin() / ballType.currenSpeed; // change spinBy to a positive value based on the spinScalar value and the ball's speed
            //        break;
            //    case E_BallTypes.Fast_OffCutter:
            //        spinBy = -ballType.getSpin() / ballType.currenSpeed; // change spinBy to a negative value based on the spinScalar value and the ball's speed
            //        break;
            //}s

            if (!firstBounce)
            { // if firstBounce is false i.e. when the ball hits the ground for the first time then the expression returns true 
                firstBounce = true; // set the firstBounce bool to true
                //GetComponent<Rigidbody>().useGravity = true; // allow the gravity to affect the ball

                // change the y value of the direction to the negative of it's present value multiplied by the bounceScalar and ball's speed
                // of the ball i.e. the bounce will be more if the ball's speed is more compared to a slower one
                direction = new Vector3(spinBy, -direction.y * (bounceScalar * 1), direction.z);
                direction = Vector3.Normalize(direction); // normalize the direction value

                angle = Mathf.Atan2(direction.y, direction.z) * Mathf.Rad2Deg; // calculte the bounce angle from the direction vector

                // Add an instant force impulse in the direction vector multiplied by ballSpeed to the ball considering its mass
                GetComponent<Rigidbody>().AddForce(direction * ballType.currenSpeed, ForceMode.Impulse);
                //rb.velocity = direction * ballSpeed; // update the balls velocity
                //CanvasManagerScript.instance.UpdateBallsBounceAngleUI(angle); // Update the balls bounce angle ui text
            }
            //AudioManagerScript.instance.PlayBounceAudio(); // play the ball bounce sound

        }

        if (other.gameObject.tag == "Bat")
        {
        
            Debug.Log("bat true");
            hitball();
        
        }

        if (batHitFlag && other.gameObject.CompareTag("Stadium"))
        {
            //Debug.Log("stadium");

            ballBounceCount++;
        }

        if (other.gameObject.tag == "BoxCollider")
        {
            //Debug.Log("stop");
            this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }


    [SerializeField] Rigidbody rigidbody;
    [SerializeField] SphereCollider collider;
    public void stopBall()
    {
        rigidbody.isKinematic = true;
        collider.enabled = false;
    }


    public void resetMagnitude()
    {
        stoppingMagnitude = 0.4f;
    }

    public void TriggerEnter(ActivateOnTrigger other)
    {

    }

    public void TriggerExit(ActivateOnTrigger other)
    {

    }
}
