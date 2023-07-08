using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallReset : MonoBehaviour
{
   [SerializeField]  Transform resetBallTransform;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Ball>())
        {
            collision.transform.position = resetBallTransform.position;
            collision.rigidbody.velocity = Vector3.zero;
        }
    }
}
