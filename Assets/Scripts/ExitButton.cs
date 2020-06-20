using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ExitButton : MonoBehaviour
{
    public Button exitbutton;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Button btn = exitbutton.GetComponent<Button>();
        btn.onClick.AddListener(QuitApplication);

    }

    void QuitApplication()
    {
        Application.Quit();
    }


}
