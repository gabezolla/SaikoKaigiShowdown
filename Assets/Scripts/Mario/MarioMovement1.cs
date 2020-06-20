using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioMovement1 : MonoBehaviour
{
    public float moveSpeed = 20f;
    float horizontalmove = 0f;
    public bool isGround = false;
    public Animator animator;
    int direction;
    public bool isShooting;
    private GameObject ball;
    private GameObject ultimate;
    private GameObject aura;
    public float charge;
    private bool isUltimate = false;
    public float ultimateduration;
    private bool duringUltimate = false;  

    // Start is called before the first frame update
    void Start()
    {
        ball = GameObject.FindGameObjectWithTag("Ball");
        ultimate = GameObject.FindGameObjectWithTag("Ultimate");
        aura = GameObject.FindGameObjectWithTag("Aura");
        ultimate.SetActive(false);
        aura.SetActive(false);        
    }

    void Update()
    {

        if (Input.GetKey(KeyCode.D))
            direction = 1;

        else if (Input.GetKey(KeyCode.A))
            direction = -1;

        else
            direction = 0;

        horizontalmove = direction * moveSpeed;
        Vector3 movement = new Vector3(horizontalmove, 0f, 0f);
        transform.position += movement * Time.deltaTime;
        animator.SetFloat("moveSpeed", Mathf.Abs(horizontalmove));            
                
        Jump();
        Kick();

        if (isGround == false)
            animator.SetBool("isJumping", true);

        else
            animator.SetBool("isJumping", false);

        charge += Time.deltaTime;
        GameObject.Find("UltimateP1").GetComponent<Animator>().SetFloat("isCharged", charge);

        if (duringUltimate == true)
        {
            ultimateduration += Time.deltaTime;

            if (ultimateduration >= 10)
                StopUltimate();
        }

        if (charge >= 20f && Input.GetKeyDown("r")){
            charge = 0;
            aura.SetActive(true);
            isUltimate = true;
        }

    }

    void Ultimate()
    {
        duringUltimate = true;
        Vector3 characterPosition = new Vector3(transform.position.x, -2.9f, 0);
        Vector3 increasedScale = new Vector3(10, 10, 0);
        gameObject.transform.position = characterPosition;
        gameObject.transform.localScale = increasedScale;
    }

    void StopUltimate()
    {
        Vector3 originalScale = new Vector3(5, 5, 0);
        gameObject.transform.localScale = originalScale;
        ultimateduration = 0;
        duringUltimate = false;
    }
    
    void Kick()
    {
        if (Input.GetKeyDown("f"))
        {
            isShooting = true;
            animator.SetBool("isKicking", true);
            Invoke("StopKick", 0.15f);
        }
    }
       
    void StopKick()
    {
        animator.SetBool("isKicking", false);
        isShooting = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball" && isShooting == true) // adicionar a condição de pegar no pé
        {
            ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(4, 5));
        }

        else if(collision.gameObject.tag == "Ball" && isShooting == false)
            ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(1, 2));

        if (collision.gameObject.tag == "Ball" && isUltimate == true)
        {
            Vector3 ultimatePosition = new Vector3(transform.position.x, 0.8f, 0);
            aura.SetActive(false);
            ultimate.SetActive(true);
            ultimate.transform.position = ultimatePosition;

        }

        if(collision.gameObject.tag == "Ultimate") {
            Ultimate();            
            ultimate.SetActive(false);
            isUltimate = false;                        
        }
    }

    void Jump()
    {
        if(Input.GetKeyDown("w") && isGround == true)
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 5f), ForceMode2D.Impulse);
            if (Input.GetKeyDown("f"))
            {
                animator.SetBool("isKicking", true);
                Invoke("StopKick", 0.15f);
                }
            
        }
    }
    
}
