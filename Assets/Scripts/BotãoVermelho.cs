using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BotãoVermelho : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameObject Plane;
    public GameObject PontoformaVermelho;
    public GameObject OtimoV;
    public GameObject BomV;

    public float input;
    public float sensitivity = 5;

    private bool IsPressed;
    private float tempoPressionado;

    [SerializeField]
    private float tempoMaxClique = 0.2f;

    void Start()
    {
        Plane.SetActive(true);
        PontoformaVermelho.SetActive(false);
        OtimoV.SetActive(false);
        BomV.SetActive(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        IsPressed = true;
        tempoPressionado = 0f;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        IsPressed = false;

        if (tempoPressionado <= tempoMaxClique)
        {
            PontoformaVermelho.SetActive(true);
            OtimoV.SetActive(true);
            BomV.SetActive(true);

            Plane.SetActive(!Plane.activeInHierarchy);

            Invoke(nameof(DesativarPlataforma), 0.1f);
        }
    }

    private void DesativarPlataforma()
    {
        Plane.SetActive(true);
        PontoformaVermelho.SetActive(false);
        OtimoV.SetActive(false);
        BomV.SetActive(false);
    }

    void Update()
    {
        if (IsPressed)
        {
            tempoPressionado += Time.deltaTime;
            input += sensitivity * Time.deltaTime;
        }
        else
        {
            input -= sensitivity * Time.deltaTime;
        }

        input = Mathf.Clamp(input, 0, 1);
    }
}