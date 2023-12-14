using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip background, walking, pickUp;

    public AudioSource src;
    // Start is called before the first frame update
    void Start()
    {
        src.clip = background;
        src.loop = true;
        src.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
