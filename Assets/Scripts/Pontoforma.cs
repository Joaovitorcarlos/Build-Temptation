using UnityEngine;

public class Pontoforma : MonoBehaviour
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
                pontos.pontosMenager += 250;
            }
        }
    }
}