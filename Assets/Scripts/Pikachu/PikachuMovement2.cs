using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PikachuMovement2 : MonoBehaviour
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
    private GameObject player1;
    public float stunduration;

    // Start is called before the first frame update
    void Start()
    {
        ultimate = GameObject.FindGameObjectWithTag("Ultimate2");
        player1 = GameObject.FindGameObjectWithTag("Players");
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
        Vector3 movement = new Vector3(direction * moveSpeed, 0f, 0f);
        transform.position += (movement * Time.deltaTime) / 4;

        Jump();
        Kick();

        if (isGround == false)
            animator2.SetBool("isJumping", true);

        else
            animator2.SetBool("isJumping", false);

        charge += Time.deltaTime;
        stunduration += Time.deltaTime;

        GameObject.Find("UltimateP2").GetComponent<Animator>().SetFloat("isCharged", charge);

        if (charge >= 2f && Input.GetKeyDown(KeyCode.End))
        {
            charge = 0;
            aura.SetActive(true);
            isUltimate = true;
        }

        if(stunduration >= 10)
        {
            player1.GetComponent<GrimMovement1>().movementSpeed = 5f;
        }
        
    }

    void Ultimate()
    {
        ultimate.transform.position = new Vector3(player1.transform.position.x, ultimate.transform.position.y, ultimate.transform.position.z);
        animator2.SetBool("isUltimating", false);
        ultimate.SetActive(true);
        Invoke("StopUltimate", 0.2f);
        isUltimate = false;
    }

    void StopUltimate()
    {
        ultimate.SetActive(false);
        stunduration = 0;
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
            aura.SetActive(false);
            Invoke("Ultimate", 0.5f);
        }
             

    }
}

