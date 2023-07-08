using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLogic : MonoBehaviour
{
    [SerializeField] Transform transformToFollow;
    [SerializeField] float moveMultiplier;

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, transformToFollow.position, Vector3.Distance(transform.position, transformToFollow.position) * moveMultiplier * Time.deltaTime);
    }

}
