using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARCTechnologies
{
    [CreateAssetMenu(fileName = "Ball")]
    public class Ball : ScriptableObject
    {
        public E_BallTypes enum_ballType;
        public E_BallLength enum_BallLength;
        public BallLength ballLength;

        //public float MinSpeed;
        //public float MaxSpeed;

        public float MaxSpin;
        public float MinSpin;
    
        //public float getZLength()
        //{
        //    switch (enum_BallLength)
        //    {
        //        case E_BallLength.Bouncer:
        //            {
        //                return Random.Range(ballLength.ShortLenghtMinZ, ballLength.ShortLenghtMaxZ);
        //            }
        //        case E_BallLength.Short:
        //            {
        //                return Random.Range(ballLength.ShortLenghtMinZ, ballLength.ShortLenghtMaxZ);
        //            }
        //        case E_BallLength.Good:
        //            {
        //                return Random.Range(ballLength.GoodLenghtMinZ, ballLength.GoodLenghtMaxZ);
        //            }
        //        case E_BallLength.Full:
        //            {
        //                return Random.Range(ballLength.FullLenghtMinZ, ballLength.FullLenghtMaxZ);
        //            }
        //        case E_BallLength.Yorker:
        //            {
        //                return Random.Range(ballLength.YorkerLenghtMinZ, ballLength.YorkerLenghtMaxZ);
        //            }
        //        case E_BallLength.Random:
        //            {
        //                return Random.Range(ballLength.randomlengthMinZ, ballLength.randomlengthMaxZ);
        //            }
        //    }
        //    return 0;   
        //}

        //public float getXLength()
        //{
        //    switch (enum_BallLength)
        //    {
        //        case E_BallLength.Bouncer:
        //            {
        //                return Random.Range(ballLength.ShortLenghtMinX, ballLength.ShortLenghtMaxX);
        //            }
        //        case E_BallLength.Short:
        //            {
        //                return Random.Range(ballLength.ShortLenghtMinX, ballLength.ShortLenghtMaxX);
        //            }
        //        case E_BallLength.Good:
        //            {
        //                return Random.Range(ballLength.GoodLenghtMinX, ballLength.GoodLenghtMaxX);
        //            }
        //        case E_BallLength.Full:
        //            {
        //                return Random.Range(ballLength.FullLenghtMinX, ballLength.FullLenghtMaxX);
        //            }
        //        case E_BallLength.Yorker:
        //            {
        //                return Random.Range(ballLength.YorkerLenghtMinX, ballLength.YorkerLenghtMaxX);
        //            }
        //        case E_BallLength.Random:
        //            {
        //                return Random.Range(ballLength.randomlengthMinX, ballLength.randomlengthMaxX);
        //            }
        //    }
        //    return 0;
        //}
        //public float getSpeed()
        //{
        //    float speed = Random.Range(MinSpeed, MaxSpeed);
        //    currenSpeed = speed;
        //    Debug.Log(speed);
        //    return speed;
        //}

        public float getSpin()
        {
            float spin = Random.Range(MinSpin, MaxSpin);
            currenSpeed = spin;
            Debug.Log(spin);
            return spin;
        }

        public float currenSpeed { get; private set; }
    }
}
