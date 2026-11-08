using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonEvents : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene((int)GameData.SceneEnum.PLAY);
    }

    public void ReturnToTitleScreen()
    {
        SceneManager.LoadScene((int)GameData.SceneEnum.TITLE);
    }

    public void Credits()
    {
        SceneManager.LoadScene((int)GameData.SceneEnum.CREDITS);
    }

    public void EndGameScreen()
    {
        SceneManager.LoadScene((int)GameData.SceneEnum.CREDITS);
    }
    
    public void Quit()
    {
        Application.Quit();
        
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
