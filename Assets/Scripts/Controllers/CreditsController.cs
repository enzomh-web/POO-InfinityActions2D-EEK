using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CreditsController : MonoBehaviour
{
    [SerializeField] private Button[] buttons;

    private void Start()
    {
        buttons[0].onClick.AddListener(()=> Return());
    }
    private void Return()
    {
        SceneManager.LoadScene(1);
    }

}
