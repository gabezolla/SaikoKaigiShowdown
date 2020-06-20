using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    AudioSource myAudio;
    public float delay;

    // Start is called before the first frame update
    void Start()
    {
        myAudio = GetComponent<AudioSource>();
        myAudio.PlayDelayed(delay);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
