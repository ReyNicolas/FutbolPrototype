using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Legs : MonoBehaviour
{
    [SerializeField] float shootForce;
    [SerializeField] float moveWaitTime;
    [SerializeField] float moveForce;
    [SerializeField] Vector3 shootDirection;
    [SerializeField] float vectorShootMultiplier;
    [SerializeField] Rigidbody playerRigidbody;
    [SerializeField] bool canMove = true;


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<Ball>())
        {
            MoveBall(other);
            Shoot(other);
        }
    }

    private void MoveBall(Collider collider)
    {
        if (canMove)
        {
            collider.attachedRigidbody.AddForce(playerRigidbody.velocity * moveForce, ForceMode.Impulse);
            StartCoroutine(Move());
        }
           
    }

    private void Shoot(Collider collider)
    {
        if (shootForce > 0)
        {
            collider.attachedRigidbody.AddForce(FinalDirection(collider.transform.position) * shootForce, ForceMode.Impulse);
            ResetShootForce();
        }
    }

    private void ResetShootForce()
    {
        shootDirection = Vector3.zero;
        shootForce = 0;
    }

    Vector3 FinalDirection(Vector3 position)
    {
        shootDirection += (position - transform.position).normalized * vectorShootMultiplier;
        return shootDirection.normalized;
    }
        

    public void SetShoot(float force, Vector3 direction )
    {
        shootForce= force;
        shootDirection= direction;
        StartCoroutine(ResetShoot());
    }

    IEnumerator Move()
    {
        canMove= false;
        yield return new WaitForSeconds(moveWaitTime);
        canMove= true;

    }
    IEnumerator ResetShoot()
    {
        yield return new WaitForSeconds(2);
        ResetShootForce();

    }


}
