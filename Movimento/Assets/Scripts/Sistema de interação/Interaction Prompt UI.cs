using System;
using TMPro;
using UnityEngine;

public class InteractionPromptUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _promptText;
    [SerializeField] private GameObject _uiPannel;
    private void Start()
    {
        _uiPannel.SetActive(false);
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
