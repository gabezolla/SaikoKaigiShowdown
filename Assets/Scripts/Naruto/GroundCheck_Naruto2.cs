using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck_Naruto2 : MonoBehaviour
{
    GameObject Player2;

    void Start()
    {
        Player2 = gameObject.transform.parent.gameObject;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            Player2.GetComponent<NarutoMovement2>().isGround = true;

        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            Player2.GetComponent<NarutoMovement2>().isGround = false;
        }

    }
}
