using UnityEngine;

public class MoverParaBaixo : MonoBehaviour
{
    [Tooltip("Velocidade do movimento em metros por segundo")]
    public float velocidade = 5.0f;

    void Update()
    {
        // Vector3.down é um atalho para (0, -1, 0)
        // Time.deltaTime garante que o movimento seja suave e independente do FPS
        transform.Translate(Vector3.down * velocidade * Time.deltaTime);
    }
}
