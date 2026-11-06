using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonEvents : MonoBehaviour
{
    public enum SceneEnum
    {
        PLAY = 0,
        TITLE = 1,
        CREDITS = 2
    }

    public void Play()
    {
        SceneManager.LoadScene((int)SceneEnum.PLAY);
    }
    public void ReturnToTitleScreen()
    {
        SceneManager.LoadScene((int)SceneEnum.TITLE);
    }
    public void Credits()
    {
        SceneManager.LoadScene((int)SceneEnum.CREDITS);
    }
    public void Quit()
    {
        Application.Quit();
        
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
