using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    public void VoltarAoMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}