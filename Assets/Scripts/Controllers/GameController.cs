using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [Header("Referências")]
    [SerializeField] private NarrativeController narrativeController;
    [SerializeField] private NarrativeUI narrativeUI;

    [Header("Narrativa")]
    [SerializeField] private NarrativeEvent initialEvent;
    [SerializeField] private NarrativeEvent centralEvent;

    private float elapsedTime;

    private bool isGameActive;

    void Start()
    {
        StarGame();
    }

    private void FixedUpdate()
    {
        if (!isGameActive) return;

        elapsedTime += Time.deltaTime;
    }
    public void StarGame()
    {
        elapsedTime = 0f;
        isGameActive = true;

        narrativeController.StarEvent(initialEvent);
    }

    public void EndGame()
    {
        isGameActive = false;

        GameData.elapsedTime = elapsedTime;

        SceneManager.LoadScene((int)GameData.SceneEnum.ENDGAME);
    }

    private void OnEnable()
    {
        narrativeController.OnNarrativeFinished += EndGame;
    }

    private void OnDisable()
    {
        narrativeController.OnNarrativeFinished -= EndGame;
    }
}
