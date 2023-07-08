using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallInMatchController : MonoBehaviour
{
    [SerializeField]Ball actualBallScript;
    public static BallInMatchController instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void SetActualBallScript(Ball ballScript)
    {
        actualBallScript = ballScript;
    }

    public Vector3 GiveMeBallPosition() =>
        actualBallScript.transform.position;

}
