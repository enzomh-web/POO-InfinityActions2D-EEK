using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndGameController : MonoBehaviour
{
    [SerializeField] private TMP_Text text;

    private void Start()
    {
        text.text = FormatTime(GameData.elapsedTime);
    }

    private void Update()
    {
        if (Input.anyKeyDown) SceneManager.LoadScene(0);
    }

    private string FormatTime(float time)
    {
        int min = Mathf.FloorToInt(time / 60);
        int sec = Mathf.FloorToInt(time % 60);

        return $"Parabéns! Sua aventura durou {min}:{sec}";
    }
}
