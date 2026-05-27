using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class balaController : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float velocidade = 500f;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * velocidade, ForceMode.Impulse);
        Destroy(gameObject, 4f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
