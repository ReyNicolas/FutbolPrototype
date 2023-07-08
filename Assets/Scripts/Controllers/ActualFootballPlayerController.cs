using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActualFootballPlayerController : MonoBehaviour
{
    [SerializeField]FootballPlayer actualFootballPlayerScript;

    public void Update()
    {
        MoveFootballPlayer();
        if(Input.GetKey(KeyCode.E)|| Input.GetKey(KeyCode.Q)) FollowBall();
        if (Input.GetKeyDown(KeyCode.Q)) ChargeShoot();
        if (Input.GetKeyUp(KeyCode.Q)) MakeShoot();
        
    }

    void MakeShoot()
    {
        actualFootballPlayerScript.MakeShoot();
    }

    void ChargeShoot()
    {
        actualFootballPlayerScript.ChargeShoot();
    }

    public void SetActualFootballPlayer(FootballPlayer footballPlayerScript)
    {
        actualFootballPlayerScript = footballPlayerScript;
    }

    void MoveFootballPlayer()
    {
        actualFootballPlayerScript.SetVectorMove(Input.GetAxis("Horizontal") , Input.GetAxis("Vertical") );
    }

    void FollowBall()
    {
        actualFootballPlayerScript.FollowBallWithPosition(BallInMatchController.instance.GiveMeBallPosition() );
    }
}

