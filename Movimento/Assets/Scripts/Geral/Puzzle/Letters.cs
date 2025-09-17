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
            dialogueSystem.ShowDialogue("Já devo ter pego todos esse pedaçoes de papel, agora só preciso de algum lugar para lê isso, parece uma carta...", 7);
        }
    }

}