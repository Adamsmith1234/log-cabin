using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverScript : MonoBehaviour
{

    public GameObject Book;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onFlipEnded(){
        Book.GetComponent<BookScript>().onFlip2();
    }

    public void onUnFlipEnded(){
        Book.GetComponent<BookScript>().onUnFlip2();
    }
}
