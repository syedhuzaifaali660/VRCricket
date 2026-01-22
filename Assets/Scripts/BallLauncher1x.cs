using AccuChekVRGame;
using ARCTechnologies;
using System;
using TMPro;
using UnityEditor.Sprites;
using UnityEngine;
using UnityEngine.XR;

[RequireComponent(typeof(InputData))]
public class BallLauncher1x : MonoBehaviour
{
    [SerializeField] PhysicMaterial groundPhysicsMaterial; 
    //SteamVR_Controller.Device device;
    [SerializeField] Over currentOver;
    public E_BallLength bowlingLength;
    [SerializeField] GameObject Ballmarker;

    #region VARIABLES
    [Header("Enum Ball Data")]
    public E_BowlingType bowlingType;
    public E_BowlLength selectedBall;

    [Header("Script Reference")]
    public VariableManager vm;
    public TextManager tm;
    public InputData _inputData;
    public SpeedManager speedManager;

    [Header("GameObjects")]
    public GameObject launchPad;
    public GameObject controller;
    public GameObject ballPrefab;
    public GameObject ball;

    [Header("Values")]
    public Vector3 velocity;
    public Vector3 righHandDeviceVelocity;

    public float randomX, randomY, randomZ, forceMultiplier;
    int ballCount;
    public bool isAllowBall = false;
    public bool Random;

    public static BallLauncher1x instance;

    Vector3 targetPosition,direction;
    #endregion
    void Awake()
    {
        if (instance == null) instance = this;
        ballCount = vm.GetBallCount();
    }
    E_BowlingType SetBowling()
    {
        return bowlingType;
    }
    public int ballNumber;
    E_BallTypes GetBowlingType()
    {
        return currentOver.Balls[ballNumber].enum_ballType;
    }
    E_BallLength GetBowlingLenght()
    {
        return currentOver.Balls[ballNumber].enum_BallLength;
    }

    void RandomBowlingType()
    {
        bowlingType = (E_BowlingType)UnityEngine.Random.Range(0, Enum.GetValues(typeof(E_BowlingType)).Length);
    }
    void RandomBowlingLength()
    {
        selectedBall = (E_BowlLength)UnityEngine.Random.Range(0, Enum.GetValues(typeof(E_BowlLength)).Length);
    }

    public void SetRandomBowlData()
    {
#if UNITY_EDITOR
        if (Random)
        {
            RandomBowlingType();
            RandomBowlingLength();
        }
#else
		RandomBowlingType();
        RandomBowlingLength();

#endif
        speedManager.speed_BoxCollider.enabled = true;

        if (bowlingType == E_BowlingType.Fast)
            tm.SetBowlingTypeText(bowlingType.ToString() + " / " + selectedBall.ToString());
        else
            tm.SetBowlingTypeText(bowlingType.ToString());
    }
    public bool ChecksBowlsLeft()
    {
        if (vm.GetBallCount() >= vm.totalBalls)
        {
            Debug.Log("===TOTAL BALLS DELIVERED===");
            GameManager.instance.GameEnded();
            return false;
        }
        else
        {
            return true;
        }
    }

    private void Start()
    {
        _inputData = GetComponent<InputData>();
    }
    void Update()
    {

        if (vm.GetIsGameStarted())
        {
            if (_inputData._rightController.TryGetFeatureValue(CommonUsages.deviceVelocity, out Vector3 rightVelocity))
            {
                velocity = rightVelocity;
                //Debug.Log("Device Velocity "+ velocity);
            }

            if (isAllowBall && !vm.GetIsGameEnded())  /*if (Input.GetKeyDown(KeyCode.Space))*/
            {
                if (!isAllowBall) return;
                isAllowBall = false;

                Debug.Log("shoot ball");
                vm.SetBatHit(false);
                SoundManager.Instance.PlaySound((int)E_SoundClip.Pitch);
                ball = Instantiate(ballPrefab, launchPad.transform.position, launchPad.transform.rotation) as GameObject;

                randomZ = UnityEngine.Random.Range(currentOver.Balls[0].ballLength.LenghtMinZ, currentOver.Balls[0].ballLength.LenghtMaxZ);
                randomY = UnityEngine.Random.Range(currentOver.Balls[0].ballLength.LenghtMinY, currentOver.Balls[0].ballLength.LenghtMaxY);
                randomX = UnityEngine.Random.Range(currentOver.Balls[0].ballLength.LenghtMinX, currentOver.Balls[0].ballLength.LenghtMaxX);
                forceMultiplier = UnityEngine.Random.Range(currentOver.Balls[0].ballLength.minForce, currentOver.Balls[0].ballLength.maxForce);
                //For short pitch ball the physics material should be set to maximum
                groundPhysicsMaterial.bounceCombine = PhysicMaterialCombine.Maximum;

                //if (SetBowling() == E_BowlingType.Fast)//fast ball
                //{

                //randomX = UnityEngine.Random.Range(-0.01f, 0.02f);
                //randomY = 0f;
                //randomZ = -1f;

                //switch (selectedBall)
                //{
                //    case E_BowlLength.Short:
                //        //randomY = UnityEngine.Random.Range(-0.22f, -0.18f);
                //        //randomZ =  
                //        randomZ = UnityEngine.Random.Range(currentOver.Balls[0].ballLength.LenghtMinZ, currentOver.Balls[0].ballLength.LenghtMaxZ);
                //        randomY = UnityEngine.Random.Range(currentOver.Balls[0].ballLength.LenghtMinY, currentOver.Balls[0].ballLength.LenghtMaxY);
                //        randomX = UnityEngine.Random.Range(currentOver.Balls[0].ballLength.LenghtMinX, currentOver.Balls[0].ballLength.LenghtMaxX);
                //        //forceMultiplier = UnityEngine.Random.Range(200f, 230); //300 370
                //        //forceMultiplier = UnityEngine.Random.Range(200f, 200f); //300 370
                //        forceMultiplier = UnityEngine.Random.Range(currentOver.Balls[0].ballLength.minForce, currentOver.Balls[0].ballLength.maxForce);
                //        //For short pitch ball the physics material should be set to maximum
                //        groundPhysicsMaterial.bounceCombine = PhysicMaterialCombine.Maximum;
                //        //forceMultiplier = UnityEngine.Random.Range(200f, 200f); //300 370
                //        break;
                //    case E_BowlLength.Good:

                //        //Debug.Log("selected ball " + selectedBall);
                //        randomY = -0.35f;
                //        randomZ = -2f; //0.9
                //        forceMultiplier = 140f;
                //        break;
                //    case E_BowlLength.Full:

                //        randomY = 0.03f;
                //        randomZ = -0.5f;
                //        forceMultiplier = 280f;
                //        break;
                //    case E_BowlLength.Yorker:

                //        forceMultiplier = 200f;
                //        randomY = 0.02f;
                //        randomZ = -0.9f;
                //        //randomZ = -2f;
                //        break;
                //    case E_BowlLength.Bouncer:
                //        forceMultiplier = 200f; //370
                //        randomY = -0.35f;
                //        randomZ = -1f;
                //        break;
                //    default:
                //        break;
                //}
                //}

                if (SetBowling() == E_BowlingType.Spin)
                {
                    ball.AddComponent<SpinBall1x>();
                    randomX = UnityEngine.Random.Range(-0.02f, 0.025f);
                    randomY = 0.2f; /*UnityEngine.Random.Range(0.12f, 0.15f);*/
                    randomZ = -0.75f;/*UnityEngine.Random.Range(-0.4f, -0.5f);*/ // -0.75

                    forceMultiplier = 120f; // 120 /*UnityEngine.Random.Range(200, 220);*/
                }

                //// Ball Count
                if (!GameConfig.isTryBall)
                {
                    ballCount = vm.GetBallCount();
                    ballCount++;
                    vm.SetBallCount(ballCount);
                    tm.SetballText(vm.GetBallCount().ToString());
                }
                //GameObject gb = Instantiate(Ballmarker);
                //gb.transform.position = new Vector3(currentOver.Balls[0].getXLength(), 0.032f, currentOver.Balls[0].getZLength());
                //Debug.Log("randomX= " + randomX + "/" + "randomY= " + randomY + "/" + "randomZ= " + randomZ);
                //targetPosition = gb.transform.position; // make the balls target position to the markers position
                //direction = Vector3.Normalize(targetPosition - launchPad.transform.position); // calculate the direction vector

                direction = new Vector3(randomX,randomY,randomZ); // calculate the direction vector
                ball.GetComponent<BallManager>().ballType = currentOver.Balls[0];
                ball.GetComponent<BallManager>().direction = direction;
                //ADD FORCE
                ball.GetComponent<Rigidbody>().AddForce(direction * forceMultiplier, ForceMode.Force);
                //ball.GetComponent<Rigidbody>().AddForce(direction * currentOver.Balls[0].getSpeed(), ForceMode.Impulse);
                ball.GetComponent<BallManager>().checkStoppingMagnitude = true;


            }
        }
        //ball.GetComponent<Rigidbody> ().AddForce (controller.transform.forward/*new Vector3(randomX,0,randomZ)*/ * 500f);
        //print(controller.transform.forward);
    }

    //[SerializeField]
    //public void ThrowBall()
    //{
    //    { // if the ball is not thrown, throw the ball
    //        //CanvasManagerScript.instance.EnableBatSwipePanel(); // Enable the bat swipe panel 
    //        //targetPosition = marker.transform.position; // make the balls target position to the markers position
    //        direction = Vector3.Normalize(targetPosition - startPosition); // calculate the direction vector
    //        rb.AddForce(direction * ballSpeed, ForceMode.Impulse); // Add an instant force impulse in the direction vector multiplied by ballSpeed to the ball considering its mass
    //    }
    //}


}
