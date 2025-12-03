using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class FinalizarDemo : MonoBehaviour
{
    private UIDocument _document;
    private Button _buttonRestart;
    private Button _buttonQuit;

    private void Awake()
    {
        _document = GetComponent<UIDocument>();

        _buttonRestart = _document.rootVisualElement.Q<Button>("RestartButton");
        _buttonRestart.RegisterCallback<ClickEvent>(BackHomeScreen);

        _buttonQuit = _document.rootVisualElement.Q<Button>("QuitButton");
        _buttonQuit.RegisterCallback<ClickEvent>(ExitGame);
    }

    private void OnDisable()
    {
        _buttonRestart.UnregisterCallback<ClickEvent>(BackHomeScreen);
        _buttonQuit.UnregisterCallback<ClickEvent>(ExitGame);
    }

    public void BackHomeScreen(ClickEvent evt)
    {
        SceneManager.LoadScene("Menu Principal");
    }

    public void ExitGame(ClickEvent evt)
    {
        Debug.Log("Era para sair do jogo");
        Application.Quit();
    }
}
