using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXComponent : MonoBehaviour
{

    AudioSource audio;
    [SerializeField]
    AudioClip footsStepsWalk;
    [SerializeField]
    AudioClip footsStepsRun;
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playsoundFoot(int index)
    {
        if(index==0)
        {
            audio.volume = 0.05f;
            audio.PlayOneShot(footsStepsWalk);
        }
        else
        {
            audio.volume = 0.1f;
            audio.PlayOneShot(footsStepsRun);
        }
    }
}
