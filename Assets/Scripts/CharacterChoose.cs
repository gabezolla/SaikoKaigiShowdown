using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterChoose : MonoBehaviour
{
    // Start is called before the first frame update
    public string sceneName;
    public Button character1;
    public Button character2;

    // Start is called before the first frame update
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        Button comp1 = character1.GetComponent<Button>();
        Button comp2 = character2.GetComponent<Button>();
        comp2.onClick.AddListener(ChangeScene);
    }

    void ChangeScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
