using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rasengan : MonoBehaviour
{

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
        if(collision.gameObject.tag == "Players") {
            GameObject.Find("Player").GetComponent<Rigidbody2D>().AddForce(new Vector2(-10f, 30f), ForceMode2D.Impulse);
            gameObject.SetActive(false);
        }
    }
}
