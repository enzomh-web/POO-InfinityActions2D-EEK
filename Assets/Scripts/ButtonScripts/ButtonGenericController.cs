using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ButtonGenericController : MonoBehaviour
{
    [Header("Função do botão")]
    [SerializeField] private UnityEvent buttonEvent;
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        button.onClick.AddListener(RunEvent); 
    }

    private void OnDisable()
    {
        button.onClick.RemoveListener(RunEvent);
    }

    private void RunEvent()
    {
        buttonEvent?.Invoke();
    }


}
