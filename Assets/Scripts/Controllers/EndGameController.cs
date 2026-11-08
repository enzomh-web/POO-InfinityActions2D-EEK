using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndGameController : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    private float initialDelay = 1f;
    private float inputTime;

    private void Start()
    {   
        inputTime = Time.time + initialDelay;
        text.text = FormatTime(GameData.elapsedTime);
    }

    private void Update()
    {
        if (Time.time < inputTime) return;

        if (Input.anyKeyDown) SceneManager.LoadScene((int)GameData.SceneEnum.TITLE);
    }

    private string FormatTime(float time)
    {
        int min = Mathf.FloorToInt(time / 60);
        int sec = Mathf.FloorToInt(time % 60);

        return $"Parabéns! Sua aventura durou {min:D2}:{sec:D2}";
    }
}
