using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    public GameObject Ground;
    public GameObject SoccerBall;
    private GameObject Line1;
    private GameObject Line2;
    private GameObject Player1;
    private GameObject Player2;
    private GameObject GoalNotice;
    AudioSource myaudio;
    public int p1score = 0, p2score = 0;
    Vector3 p1original = new Vector3(-4.53f, -2.48f, 0f);
    Vector3 p2original = new Vector3(4.53f, -2.48f, 0f);
    Vector3 balloriginal = new Vector3(0f, 1.45f, 0f);

    void Start()
    {
        Ground = GameObject.FindGameObjectWithTag("Ground");
        SoccerBall = GameObject.FindGameObjectWithTag("Ball");
        Line1 = GameObject.FindGameObjectWithTag("Line1");
        Line2 = GameObject.FindGameObjectWithTag("Line2");
        Player1 = GameObject.FindGameObjectWithTag("Players");
        Player2 = GameObject.FindGameObjectWithTag("Player2");
        GoalNotice = GameObject.FindGameObjectWithTag("GoalNotice");
        myaudio = GameObject.Find("GoalSound").GetComponent<AudioSource>();

    }
    void Update()
    {
        
    }
  
    private void Reset() // função que reseta os personagens e objetos para seu estado inicial
    {
        GoalNotice.GetComponent<CanvasGroup>().alpha = 0;
        Player1.transform.position = p1original;
        Player2.transform.position = p2original;
        Line2.SetActive(true);
        Line1.SetActive(true);        
        SoccerBall.transform.position = balloriginal;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Players" || collision.gameObject.tag == "Player2") 
        {
            ContactPoint2D contact = new ContactPoint2D();
            Vector2 normal = contact.normal;
            Vector2 velocity = contact.relativeVelocity;
            Vector2 point = contact.point;
            SoccerBall.GetComponent<Rigidbody2D>().AddForceAtPosition(100*normal*velocity*-1, point);            
        }

        if(collision.gameObject.tag == "Ground")
        {
            Vector2 position = new Vector2(SoccerBall.transform.position.x, SoccerBall.transform.position.y);
            SoccerBall.GetComponent<Rigidbody2D>().AddForce(-1*position);
        }

        if (collision.gameObject.tag == "Line2") 
        {
            myaudio.Play();
            p1score++;
            Line1.SetActive(false); // setActive para evitar que a bola fique saindo e entrando e contabilizando vários gols
            Line2.SetActive(false);
            GoalNotice.GetComponent<CanvasGroup>().alpha = 1;                         
            Invoke("Reset", 1.0f);
        }

        if (collision.gameObject.tag == "Line1")    
        {
            myaudio.Play();
            p2score++;
            Line1.SetActive(false);
            Line2.SetActive(false);
            GoalNotice.GetComponent<CanvasGroup>().alpha = 1;
            Invoke("Reset", 1.0f);
        }

    }
}
