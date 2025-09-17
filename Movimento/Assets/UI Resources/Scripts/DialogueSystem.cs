using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogueSystem : MonoBehaviour
{
    // Variaveis para o dialogo
    [SerializeField] public UIDocument dUiDocument; //var que vai pegar a visual tree pelo inspector
    private VisualElement dRootDialogue; //var que vai guardar a raiz da visual tree
    private Label dLabelText; //var que vai guardar a label da visual tree
    private VisualElement dPainelDialogue; //var que vai guardar o panel da visual tree
    public bool dialogueAtivado = false;


    // Variaveis para o indicador de controle
    [SerializeField] public UIDocument icUiDocument; 
    private VisualElement icRootDialogue; 
    private Label icLabelText; 
    private VisualElement icPainelDialogue; 
    public bool indicadorControleAtivo = false;


    public void OnEnable()
    {
        //Só vai settar os valores das var do dialogo
        dRootDialogue = dUiDocument.rootVisualElement;
        dLabelText = dRootDialogue.Q<Label>("dialogue-text");
        dPainelDialogue = dRootDialogue.Q<VisualElement>("dialogue-panel");
        dPainelDialogue.style.display = DisplayStyle.None;

        //Settar os valores das var do Indicador de Controle
        icRootDialogue = icUiDocument.rootVisualElement;
        icLabelText = icRootDialogue.Q<Label>("IndicadorControle-text");
        icPainelDialogue = icRootDialogue.Q<VisualElement>("IndicadorControle-panel");
        icPainelDialogue.style.display = DisplayStyle.None;
    }

    public void ShowDialogue(string text, float duration)
    {
        StartCoroutine(DialogueCoroutine(text, duration));
        Debug.Log("Era para ter dialogo parte 1");
    }

    public IEnumerator DialogueCoroutine(string textDialogue, float timeDialogue)
    {
        dLabelText.text = textDialogue;
        dPainelDialogue.style.display = DisplayStyle.Flex;
        yield return new WaitForSeconds(timeDialogue);
        dPainelDialogue.style.display = DisplayStyle.None;
        dLabelText.text = "";
        Debug.Log("Era para ter dialogo parte 2");
    }

    public void IndicadorControleTrue(string textControl)
    {
        icLabelText.text = textControl;
        icPainelDialogue.style.display = DisplayStyle.Flex;
        indicadorControleAtivo = true;
    }

    public void IndicadorControleFalse()
    {
        icPainelDialogue.style.display = DisplayStyle.None;
        indicadorControleAtivo = false;
    }

}
