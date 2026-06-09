using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject optionsMenu;

    [Header("UI Sounds")]
    public AudioSource audioSource;
    public AudioClip buttonClickSound;

    private bool optionsOpen = false;
    private bool isMuted = false;

    void Start()
    {
        optionsMenu.SetActive(false);
        optionsOpen = false;
        isMuted = false;
        AudioListener.volume = 1f;
    }

    public void PlayButtonSound()
    {
        audioSource.PlayOneShot(buttonClickSound);
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

        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}