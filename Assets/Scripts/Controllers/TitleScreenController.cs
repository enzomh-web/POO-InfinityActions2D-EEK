using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class TitleScreenController : MonoBehaviour
{
    [SerializeField] private Button[] buttons;


    private void Start()
    {
        buttons[0].onClick.AddListener(()=> Play());
        buttons[1].onClick.AddListener(() => Credits());
        buttons[2].onClick.AddListener(() => Quit());

    }
    private void Play()
    {
        SceneManager.LoadScene(0);
    }

    private void Credits()
    {
        SceneManager.LoadScene(2);
    }

    private void Quit()
    {
        Application.Quit();
        
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}