using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scoreboard : MonoBehaviour
{
    public string sceneName;
    public GameObject ending;
    float currentTime = 0f;
    float startingTime = 120f;
    public int score1;
    public int score2;

    [SerializeField] Text Timer;
    [SerializeField] Text ScorePlayer1;
    [SerializeField] Text ScorePlayer2;
     
    // Start is called before the first frame update
    void Start()
    {
        currentTime = startingTime;
        ending = GameObject.Find("EndingScreen");
        ending.SetActive(false);
    }

    void ReturnScreen()
    {
        SceneManager.LoadScene(sceneName);
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= 1 * Time.deltaTime;
        score1 = GameObject.Find("SoccerBall").GetComponent<Ball>().p1score;
        score2 = GameObject.Find("SoccerBall").GetComponent<Ball>().p2score;

        if (currentTime <= 0) {

            currentTime = 0;
            
            if (score1 > score2)
            {

                ending.SetActive(true);
                ending.transform.GetChild(0).gameObject.SetActive(true);
                ending.transform.GetChild(1).gameObject.SetActive(false);
                ending.transform.GetChild(2).gameObject.SetActive(false);
                Invoke("ReturnScreen", 2.0f);
            }

            else if (score2 > score1)
            {
                ending.SetActive(true);
                ending.transform.GetChild(0).gameObject.SetActive(false);
                ending.transform.GetChild(1).gameObject.SetActive(true);
                ending.transform.GetChild(2).gameObject.SetActive(false);
                Invoke("ReturnScreen", 2.0f);

            }

            else if(score1 == score2)
            {
                ending.SetActive(true);
                ending.transform.GetChild(0).gameObject.SetActive(false);
                ending.transform.GetChild(1).gameObject.SetActive(false);
                ending.transform.GetChild(2).gameObject.SetActive(true);
                Invoke("ReturnScreen", 2.0f);
            }
        }

        Timer.text = (currentTime).ToString("0");
        ScorePlayer1.text = score1.ToString();
        ScorePlayer2.text = score2.ToString();

        ScorePlayer1.text = GameObject.Find("SoccerBall").GetComponent<Ball>().p1score.ToString();
        ScorePlayer2.text = GameObject.Find("SoccerBall").GetComponent<Ball>().p2score.ToString();

    }

    

}
