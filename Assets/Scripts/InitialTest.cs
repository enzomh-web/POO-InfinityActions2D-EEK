using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InitialTest : MonoBehaviour
{
    [SerializeField] private Button buttonA;
    [SerializeField] private Button buttonB;
    [SerializeField] private Button buttonC;
    [SerializeField] private Button buttonD;
    [SerializeField] private Button buttonE;
    [SerializeField] private Button buttonF;
    [SerializeField] private TMP_Text tmp;

    public void Awake()
    {
        ButtonClicker(buttonA, "Botão A pressionado");
        ButtonClicker(buttonB, "Botão B pressionado");
        ButtonClicker(buttonC, "Botão C pressionado");
        ButtonClicker(buttonD, "Botão D pressionado");
        ButtonClicker(buttonE, "Botão E pressionado");
        ButtonClicker(buttonF, "Botão F pressionado");
    }

    public void OnButtonClick(string txt = "Lorem Ipsum")
    {
        tmp.text = txt;
    }

    public void ButtonClicker(Button btn, string txt)
    {
        btn.onClick.AddListener(() => OnButtonClick(txt));
    }

}
