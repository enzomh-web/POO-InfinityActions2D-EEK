using Unity.VisualScripting;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("Referências")]
    [SerializeField] private NarrativeController narrativeController;

    [Header("Narrativa")]
    [SerializeField] private NarrativeEvent initialEvent;

    void Start()
    {
        narrativeController.StarEvent(initialEvent);
    }
}
