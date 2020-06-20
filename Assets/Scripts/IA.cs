using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;


public class IA : MonoBehaviour
{

    public float RangedDefense, speed = 4f;
    public Transform defence;
    public GameObject Ball;
    public GameObject PlayerReal;
    Rigidbody2D rb;
    public bool isGrounded = false;
    public float jumpForce;
    int direction = 0;
    public float moveSpeed = 20f;
    GameObject ultimate;
    GameObject aura;
    public float charge;
    public float ultimateduration;
    private bool isUltimate = false;
    public Sprite rasengan;
    public bool rasenganMoves = false;
    public Animator animator2;


    // Start is called before the first frame update
    void Start()
    {
        Ball = GameObject.FindGameObjectWithTag("Ball");
        PlayerReal = GameObject.FindGameObjectWithTag("Players");
        rb = GetComponent<Rigidbody2D>();
        ultimate = GameObject.FindGameObjectWithTag("Ultimate2");
        aura = GameObject.FindGameObjectWithTag("Aura2");
        aura.SetActive(false);
        ultimate.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();

        charge += Time.deltaTime;
        GameObject.Find("UltimateP2").GetComponent<Animator>().SetFloat("isCharged", charge);

        if (charge >= 20f)
        {
            isUltimate = true;
            charge = 0;
            aura.SetActive(true);
        }

        if (rasenganMoves == true)
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
    }

    private void Move() // função que determina o movimento da IA
    {

        if ((Ball.transform.position.x) < 4) // script de ataque
        {
            if (Ball.transform.position.x + 0.8f >= transform.position.x)
            {
                transform.Translate(Time.deltaTime * speed, 0, 0);
            }
            else if (Ball.transform.position.x < transform.position.x)
            {
                transform.Translate(-Time.deltaTime * speed, 0, 0);
            }
            else if (Ball.transform.position.x == transform.position.x)
            {
                transform.position = new Vector2(transform.position.x + 1.5f, transform.position.y);
            }
        }
        else // script de defesa
        {

            if (transform.position.x < defence.position.x)
            {
                transform.Translate(Time.deltaTime * speed, 0, 0);
            }
            else
            {
                transform.Translate(0, 0, 0);

            }
        }
    }

    private void Jump()
    {
        float dist = Vector2.Distance(Ball.transform.position, transform.position);

        if (dist < 1f && isGrounded == true && isUltimate == false)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }

        if (collision.gameObject.tag == "Ball" && isUltimate == true)
        {            
            gameObject.transform.position = (new Vector3(gameObject.transform.position.x, -3.54071f, gameObject.transform.position.z));
            animator2.SetBool("isUltimating", true);
            Invoke("StopUltimate", 0.5f);
            aura.SetActive(false);
            Invoke("Ultimate", 1.9f);
            isUltimate = false;
        }


    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }
}