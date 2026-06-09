using UnityEngine;
using UnityEngine.EventSystems;

public class BotaoAzul : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameObject Plane;
    public GameObject PontoformaAzul;
    public GameObject OtimoA;
    public GameObject BomA;

    private float tempoPressionado;
    private bool IsPressed;

    [SerializeField]
    private float tempoMaxClique = 0.2f; // máximo para ser considerado clique

    void Start()
    {
        Plane.SetActive(true);
        PontoformaAzul.SetActive(false);
        OtimoA.SetActive(false);
        BomA.SetActive(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        IsPressed = true;
        tempoPressionado = 0f;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        IsPressed = false;

        // Só executa se foi um clique rápido
        if (tempoPressionado <= tempoMaxClique)
        {
            PontoformaAzul.SetActive(true);
            OtimoA.SetActive(true);
            BomA.SetActive(true);

            Invoke(nameof(DesativarPlataforma), 0.1f); // tempo que fica ativa
        }
    }

    void Update()
    {
        if (IsPressed)
        {
            tempoPressionado += Time.deltaTime;
        }
    }

    void DesativarPlataforma()
    {
        PontoformaAzul.SetActive(false);
        OtimoA.SetActive(false);
        BomA.SetActive(false);
    }
}
