using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class GameController : MonoBehaviour
{
    [Header("Referências")]
    [SerializeField] private NarrativeController narrativeController;

    [Header("Narrativa")]
    [SerializeField] private NarrativeEvent initialEvent;

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

        Debug.Log("Receba");

        // Futramente, inserir troca para a cena que mostra o tempo de sobrevivencia 
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
