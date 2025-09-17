using System;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class InteractionPromptUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _promptText;
    
    [SerializeField] private GameObject _uiPannel;
    [SerializeField]public UIDocument UIDocument;
    private VisualElement rootCommand;
    private Label LabelCommand;
    private VisualElement painelCommand;

    public void Awake()
    {
        rootCommand = UIDocument.rootVisualElement;
        LabelCommand = rootCommand.Q<Label>("command-label");
        painelCommand = rootCommand.Q<VisualElement>("painel-comandos");
        painelCommand.style.display = DisplayStyle.None;
    }

    private void Start()
    {
        _uiPannel.SetActive(false);
        rootCommand.visible = false;

    }

    public bool IsDisplayed = false;
    public void SetUp(string promptText)
    {
        _promptText.text = promptText;
        _uiPannel.SetActive(true);
        IsDisplayed = true;
    }
        
    public void Close()
    {
        _uiPannel.SetActive(false);
        IsDisplayed = false;
    }
}
