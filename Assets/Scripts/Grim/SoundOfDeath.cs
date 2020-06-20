using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundOfDeath : MonoBehaviour
{
    public float slowduration;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player2")
        {
            GameObject.Find("Player1").GetComponent<GrimMovement1>().slowduration = 0;
            GameObject.Find("Player2").GetComponent<PikachuMovement2>().moveSpeed = 5f; // slow
            GameObject.Find("Player2").GetComponent<PikachuMovement2>().charge = 0f; // suga poder            
            gameObject.SetActive(false);

        }
    }
}
