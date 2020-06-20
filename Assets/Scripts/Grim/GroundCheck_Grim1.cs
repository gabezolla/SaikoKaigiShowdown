using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck_Grim1 : MonoBehaviour
{
    GameObject Player;
    void Start()
    {
        Player = gameObject.transform.parent.gameObject;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            Player.GetComponent<GrimMovement1>().isGround = true;

        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            Player.GetComponent<GrimMovement1>().isGround = false;
        }

    }


}
