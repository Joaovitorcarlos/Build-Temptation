using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AparecerDepois : MonoBehaviour
{
    public GameObject objeto;
    public TMP_Text pontosText;
    private object pontosMenager;

    void Start()
    {
        objeto.SetActive(false);

        StartCoroutine(MostrarDepois());
    }

    IEnumerator MostrarDepois()
    {
        yield return new WaitForSeconds(50f);

        objeto.SetActive(true);
    }

    private void Update()
    {
        pontosText.text = "Pontos: " + pontosMenager.ToString(); 
    }
}
