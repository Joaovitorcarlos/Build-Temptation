using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class Plataforma2 : MonoBehaviour
{
   private void OnTriggerEnter(Collider other)
    {
    if (other.CompareTag("nota"))
        {
        Destroy(other.gameObject);
        }
    }

}


