using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrimMovement1 : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float horizontalmove;
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
    Vector3 ultpos;
    public float frequency = 10.0f; // Speed of sine movement
    public float magnitude = 1.0f; //  Size of sine movement, its the amplitude of the side curve
    public float ultspeed = 5.0f;
    float SoundOfDeath;   
    public float slowduration;

    // Start is called before the first frame update
    void Start()
    {
        ball = GameObject.FindGameObjectWithTag("Ball");
        ultimate = GameObject.FindGameObjectWithTag("Ultimate");
        aura = GameObject.FindGameObjectWithTag("Aura");
        aura.SetActive(false);
        ultimate.SetActive(false);
    }

    void Update()
    {

        if (Input.GetKey(KeyCode.D))
            direction = 1;

        else if (Input.GetKey(KeyCode.A))
            direction = -1;

        else
            direction = 0;

        horizontalmove = direction * movementSpeed;
        Vector3 movement = new Vector3(horizontalmove, 0f, 0f);
        transform.position += movement * Time.deltaTime;
                
        Jump();
        Kick();

        if (isGround == false)
            animator.SetBool("isJumping", true);

        else
            animator.SetBool("isJumping", false);

        charge += Time.deltaTime;
        SoundOfDeath += Time.deltaTime;
        slowduration += Time.deltaTime;

        GameObject.Find("UltimateP1").GetComponent<Animator>().SetFloat("isCharged", charge);
       
        if (charge >= 20f && Input.GetKeyDown("r")){
            charge = 0;
            aura.SetActive(true);
            isUltimate = true;
        }

        if(ultimate.activeSelf == true)
        {
           ultimate.transform.position = ultpos + new Vector3(2f * SoundOfDeath, Mathf.Sin(Time.time * frequency) * magnitude, 0f); // y = A sin(B(x)), A is Amplitude, and axis * magnitude is acting as amplitude.
        }

        slowduration += Time.deltaTime;

        if (slowduration >= 8f)
        {
            GameObject.Find("Player2").GetComponent<PikachuMovement2>().moveSpeed = 20f;
        }


    }

    void Ultimate()
    {
        SoundOfDeath = 0;
        ultimate.SetActive(true);
        animator.SetBool("isUltimating", false);
        ultpos.x = transform.position.x + 1.0f;
        ultpos.y = transform.position.y - 0.5f;
        isUltimate = false;
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
            aura.SetActive(false);
            animator.SetBool("isUltimating", true);
            Invoke("Ultimate", 1.0f);
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
