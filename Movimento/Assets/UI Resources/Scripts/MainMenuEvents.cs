using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenuEvents : MonoBehaviour
{
    private UIDocument _document;
    private Button _buttonStart;
    private Button _buttonQuit;

    private List<Button> _menuButtons = new List<Button>();

    private void Awake()
    {
        _document = GetComponent<UIDocument>();

        _buttonStart = _document.rootVisualElement.Q("StartButton") as Button;
        _buttonStart.RegisterCallback<ClickEvent>(OnPlayGameClick);

        _buttonQuit = _document.rootVisualElement.Q("QuitButton") as Button;
        _buttonQuit.RegisterCallback<ClickEvent>(QuitGame);

        /* _menuButtons = _document.rootVisualElement.Query<Button>().ToList();
        for (int i = 0; i < _menuButtons.Count; i++)
        {
            _menuButtons[i].RegisterCallback<ClickEvent>(OnAllButtonClick);
        } */
    }
    private void OnDisable()
    {
        _buttonStart.UnregisterCallback<ClickEvent>(OnPlayGameClick);
        _buttonQuit.UnregisterCallback<ClickEvent>(QuitGame);
    }

    private void OnPlayGameClick(ClickEvent Event)
    {
        SceneManager.LoadScene("Casa");
    }

    private void QuitGame(ClickEvent Event)
    {
        Debug.Log("Era para sair do jogo");
        Application.Quit();
    }

    private void OnAllButtonClick(ClickEvent Event)
    {

    }
}
