using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck_Mario1 : MonoBehaviour
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
            Player.GetComponent<MarioMovement1>().isGround = true;
                    
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            Player.GetComponent<MarioMovement1>().isGround = false;
        }

    }


}
