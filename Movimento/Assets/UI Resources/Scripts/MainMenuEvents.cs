using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuEvents : MonoBehaviour
{
    private UIDocument _document;
    private Button _button;
    private List<Button> _menuButtons = new List<Button>();

    private void Awake()
    {
        _document = GetComponent<UIDocument>();
        _button = _document.rootVisualElement.Q("StartButton") as Button;
        _button.RegisterCallback<ClickEvent>(OnPlayGameClick);

        _document = GetComponent<UIDocument>();
        _button = _document.rootVisualElement.Q("OptionsButton") as Button;
        _button.RegisterCallback<ClickEvent>(OnSettingsClick);

        _document = GetComponent<UIDocument>();
        _button = _document.rootVisualElement.Q("RToDesktopButton") as Button;
        _button.RegisterCallback<ClickEvent>(OnRTDClick);

        _menuButtons = _document.rootVisualElement.Query<Button>().ToList();
        for (int i = 0; i < _menuButtons.Count; i++)
        {
            _menuButtons[i].RegisterCallback<ClickEvent>(OnAllButtonClick);
        }
    }
    private void OnDisable()
    {
        _button.UnregisterCallback<ClickEvent>(OnPlayGameClick);

        for (int i = 0; i < _menuButtons.Count; i++)
        {
            _menuButtons[i].UnregisterCallback<ClickEvent>(OnAllButtonClick);
        }
    }

    private void OnPlayGameClick(ClickEvent Event)
    {
        Debug.Log("Botão pressionado");
    }

    private void OnSettingsClick(ClickEvent Event)
    {
        Debug.Log("Foi para as configurações");
    }

    private void OnRTDClick(ClickEvent Event)
    {
        Debug.Log("Foi para o desktop");
    }

    private void OnAllButtonClick(ClickEvent Event)
    {

    }
}
