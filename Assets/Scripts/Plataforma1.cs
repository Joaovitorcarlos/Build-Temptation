using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class Plataforma1 : MonoBehaviour
{
    [Header("Audio")]
    public AudioClip hitSound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("nota"))
        {
            AudioSource.PlayClipAtPoint(hitSound, transform.position);

            Destroy(other.gameObject);
        }
    }
}