using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogueSystem : MonoBehaviour
{
    [SerializeField]  public UIDocument uiDocument; //var que vai pegar a visual tree pelo inspector
     private VisualElement rootDialogue; //var que vai guardar a raiz da visual tree
     private Label labelText; //var que vai guardar a label da visual tree
     private VisualElement painelDialogue; //var que vai guardar o panel da visual tree
    private bool dialogueAtivado = false;


    public void OnEnable()
    {
        //Só vai settar os valores das var
        rootDialogue = uiDocument.rootVisualElement;
        labelText = rootDialogue.Q<Label>("dialogue-text");
        painelDialogue = rootDialogue.Q<VisualElement>("dialogue-panel");
        painelDialogue.style.display = DisplayStyle.None;

    }

        public void ShowDialogue(string text, float duration)
    {
        StartCoroutine(DialogueCoroutine(text, duration));
    }

    public IEnumerator DialogueCoroutine(string textDialogue, float timeDialogue)
    {
            labelText.text = textDialogue;
            painelDialogue.style.display = DisplayStyle.Flex;
            yield return new WaitForSeconds(timeDialogue);
            painelDialogue.style.display = DisplayStyle.None;
            //labelText.text = "";
    }

}
