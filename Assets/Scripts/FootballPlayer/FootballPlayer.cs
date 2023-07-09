using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;

public class FootballPlayer : MonoBehaviour
{
    Vector3 vMove;
    [SerializeField]Rigidbody rigidbody;
    [SerializeField] Legs legs;
    [SerializeField] Transform soccerGoalTransform;
    [SerializeField] float followBallMultiplier;
    [SerializeField]float moveSpeed;
    [SerializeField]float actualSpeed;
    [SerializeField] float maxSpeed;
    [SerializeField] float shootForcePerSecond;
    public float maxShootForce;
    [SerializeField] float vectorShootMultiplierMove;
    [SerializeField] float vectorShootMultiplierGoal;
    [SerializeField] Animator animator;
    public  float actualShootForce = 0;
    Vector3 vectorShoot = Vector3.zero;


    private void FixedUpdate()
    {
        Move();
        AdjustSpeed();
        AdjustLookAtDirection();
        animator.SetFloat("Speed", actualSpeed);
    }   


    public void SetVectorMove(float xValue, float yValue)
    {
        vMove.x = xValue;
        vMove.z = yValue;
        vectorShoot.x += xValue;
        vectorShoot.z += yValue;
    }
    internal void MoveToBallWithPosition(Vector3 ballPosition)
    {
        vMove.x = (ballPosition.x - transform.position.x);
        vMove.z = (ballPosition.z - transform.position.z);
    }

    public void FollowBallWithPosition(Vector3 ballPosition)
    {

        vMove.x += (vMove.x != 0)? (ballPosition.x - transform.position.x) * followBallMultiplier: (ballPosition.x - transform.position.x);
        vMove.z += (vMove.z != 0) ? (ballPosition.z - transform.position.z) * followBallMultiplier : (ballPosition.z - transform.position.z);
    }

    public void ChargeShoot()
    {
        if (actualShootForce > 0) return;
        vectorShoot = Vector3.zero;
        StartCoroutine(Charge());
    }

    public void MakeShoot()
    {
        if (actualShootForce > 0)
        {
            StopAllCoroutines();
            SendShootToLegs();
            ResetShootsValues();
        }
    }

    void Move() =>
        rigidbody.AddForce(Vector3.ClampMagnitude(vMove, 1) * Time.deltaTime * moveSpeed, ForceMode.Impulse);

    void AdjustLookAtDirection()
    {
        if (rigidbody.velocity.sqrMagnitude > 0.1f) transform.rotation = Quaternion.LookRotation(rigidbody.velocity);
    }

    void AdjustSpeed()
    {
        if (SurpassMaxSpeed())
        {
            rigidbody.velocity = Vector3.ClampMagnitude(rigidbody.velocity, maxSpeed);
        }
    }

    bool SurpassMaxSpeed()
    {
        actualSpeed = rigidbody.velocity.magnitude ;
        return actualSpeed > maxSpeed;
    }     
    

    void ResetShootsValues()
    {
        actualShootForce = 0;
        vectorShoot = Vector3.zero;
    }

    void SendShootToLegs()
    {
        vectorShoot = CalculateVectorMove() + CalculateVectorSoccerGoal();
        legs.SetShoot(MathF.Min(actualShootForce, maxShootForce), vectorShoot);
    }

    Vector3 CalculateVectorSoccerGoal() => 
        (soccerGoalTransform.position - transform.position).normalized * vectorShootMultiplierGoal;

    Vector3 CalculateVectorMove() =>
        vectorShoot.normalized * vectorShootMultiplierMove ;



    IEnumerator Charge()
    {
        while (true)
        {
            actualShootForce += shootForcePerSecond * 0.05f;
            yield return new WaitForSeconds(0.05f);
        }
    }

    
}
