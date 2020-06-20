using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarutoMovement2 : MonoBehaviour
{
    public float moveSpeed = 20f;
    int direction = 0;
    public bool isGround = false;
    public Animator animator2;
    private bool isShooting = false;
    private GameObject ball;
    private GameObject ultimate;
    private GameObject aura;
    public float charge;
    public float ultimateduration;
    private bool isUltimate = false;
    public Sprite rasengan;
    public bool rasenganMoves = false;  

    // Start is called before the first frame update
    void Start()
    {
        ultimate = GameObject.FindGameObjectWithTag("Ultimate2");
        ball = GameObject.FindGameObjectWithTag("Ball");
        aura = GameObject.FindGameObjectWithTag("Aura2");
        aura.SetActive(false);
        ultimate.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Keypad6))
            direction = 1;
        

        else if (Input.GetKey(KeyCode.Keypad4))
            direction = -1;
        
        else
            direction = 0;

        animator2.SetFloat("moveSpeed", Mathf.Abs(direction));
        Vector3 movement = new Vector3(direction*moveSpeed, 0f, 0f);
        transform.position += (movement * Time.deltaTime)/4;
        
        Jump();
        Kick();

        if (isGround == false)
            animator2.SetBool("isJumping", true);

        else
            animator2.SetBool("isJumping", false);

        charge += Time.deltaTime;
        GameObject.Find("UltimateP2").GetComponent<Animator>().SetFloat("isCharged", charge);

        if (charge >= 20f && Input.GetKeyDown(KeyCode.End))
        {
            charge = 0;
            aura.SetActive(true);
            isUltimate = true;
        }

        if(rasenganMoves == true)
        {
            Vector3 ultimatemovement = new Vector3(1f, 0f, 0f);
            ultimate.transform.position += ultimatemovement * Time.deltaTime * -15;
        }
                
    }

    void Ultimate()
    {
        rasenganMoves = true;        
        Vector3 ultimateposition = (new Vector3(gameObject.transform.position.x - 1.5f, gameObject.transform.position.y, gameObject.transform.position.z));
        ultimate.transform.position = ultimateposition;
        ultimate.SetActive(true);        
    }

    void StopUltimate()
    {
        animator2.SetBool("isUltimating", false);
        isUltimate = false;
    }

    void Kick()
    {
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            isShooting = true;
            animator2.SetBool("isKicking", true);
            Invoke("StopKick", 0.15f);
        }
    }

    void StopKick()
    {
        animator2.SetBool("isKicking", false);
        isShooting = false;
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Keypad8) && isGround == true)
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 5f), ForceMode2D.Impulse);
            if (Input.GetKeyDown(KeyCode.Delete))
            {
                animator2.SetBool("isKicking", true);
                Invoke("StopKick", 0.15f);
            }

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball" && isShooting == true) // adicionar a condição de pegar no pé
        {
            ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(4, 5));
        }

        else if (collision.gameObject.tag == "Ball" && isShooting == false)
            ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(1, 2));

        if (collision.gameObject.tag == "Ball" && isUltimate == true)
        {
            gameObject.transform.position = (new Vector3(gameObject.transform.position.x, -3.54071f, gameObject.transform.position.z));
            animator2.SetBool("isUltimating", true);
            Invoke("StopUltimate", 0.5f);
            aura.SetActive(false);
            Invoke("Ultimate", 1.9f);
        }

    }
}

