using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Otimo : MonoBehaviour
{
   private Pontos pontos;

    void Start()
    {
        pontos = FindFirstObjectByType<Pontos>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("nota"))
        {
            Destroy(other.gameObject);

            if (pontos != null)
            {
                pontos.pontosMenager += 100;
            }
        }
    }
}
