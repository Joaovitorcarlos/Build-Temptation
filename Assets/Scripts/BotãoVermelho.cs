using System.Collections.Generic;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine;

public class BotãoVermelho : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
     public GameObject Plane;
     public GameObject PontoformaVermelho;
     public GameObject OtimoV;
     public GameObject BomV;
    public float input;
    public float sensitivity = 5;
    bool IsPressed;

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
        PontoformaVermelho.SetActive(true);
        OtimoV.SetActive(true);
        BomV.SetActive(true);
        Plane.SetActive(!Plane.activeInHierarchy);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        IsPressed = false;
        Plane.SetActive(true);
        PontoformaVermelho.SetActive(false);
        OtimoV.SetActive(false);
        BomV.SetActive(false);
    }
    
     void Update()
    {
        if (IsPressed){
            input += sensitivity * Time.deltaTime;
        }
            else{
                input -= sensitivity * Time.deltaTime;
            }
        input = Mathf.Clamp(input, 0, 1);
        
    }

}