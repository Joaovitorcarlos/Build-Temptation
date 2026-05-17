using System.Collections.Generic;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine;

public class BotãoAzul : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameObject Plane;
    public GameObject PontoformaAzul;
    public float input;
    public float sensitivity = 5;
    bool IsPressed;

    void Start()
    {
        Plane.SetActive(true);
        PontoformaAzul.SetActive(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        IsPressed = true;
        PontoformaAzul.SetActive(true);
        Plane.SetActive(!Plane.activeInHierarchy);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        IsPressed = false;
        Plane.SetActive(true);
        PontoformaAzul.SetActive(false);
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
