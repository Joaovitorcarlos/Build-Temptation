using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject optionsMenu;

    private bool optionsOpen = false;
    private bool isMuted = false;

    void Start()
    {
        optionsMenu.SetActive(false);
        optionsOpen = false;
        isMuted = false;
        AudioListener.volume = 1f;
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ToggleOptions()
    {
        optionsOpen = !optionsOpen;
        optionsMenu.SetActive(optionsOpen);
    }

    public void ToggleGeneralVolume()
    {
        isMuted = !isMuted;
        AudioListener.volume = isMuted ? 0f : 1f;
    }

    public void QuitGame()
    {
        Application.Quit();

        // it Closes the Unity Editor, just for tests..
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}