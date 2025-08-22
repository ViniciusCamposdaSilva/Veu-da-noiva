using UnityEngine;

public class Letters : MonoBehaviour
{
    PuzzleLetter puzzleLetter;
    DialogueSystem dialogueSystem;

    private void Awake()
    {
        puzzleLetter = Object.FindFirstObjectByType<PuzzleLetter>();
        dialogueSystem = Object.FindFirstObjectByType<DialogueSystem>();
    }
    private void OnDestroy()
    {
        PuzzleLetter.quantidadeAtualCartas++;
        Debug.Log("Pegou uma carta");

        if (PuzzleLetter.quantidadeAtualCartas >= PuzzleLetter.quantidadeTotalCartas)
        {
            Debug.Log("Todas as cartas foram obtidas");
            puzzleLetter.AdicionarDestaque();
            dialogueSystem.ShowDialogue("Acho que já peguei todos esses pedaços, só preciso de uma mesa para organizá-los", 3);
        }
    }

}