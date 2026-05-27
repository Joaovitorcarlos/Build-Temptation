using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtirarController : MonoBehaviour
{
    public GameObject bocaDaArma;
    public GameObject bala;
    void Start()
    {
        
    }


    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Instantiate(bala, 
                bocaDaArma.transform.position, 
                bocaDaArma.transform.rotation);
        }
    }
}
