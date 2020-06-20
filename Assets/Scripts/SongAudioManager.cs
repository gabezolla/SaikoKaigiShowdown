using UnityEngine.Audio;
using UnityEngine;

public class SongAudioManager : MonoBehaviour
{
    AudioSource myAudio;

    // Start is called before the first frame update
    void Start()
    {
        myAudio = GetComponent<AudioSource>();
        myAudio.PlayDelayed(3.5f);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
