using System;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections;

public class TableForLatters : MonoBehaviour, INterfaceInteractor
{
    [SerializeField] private string _prompt;
    public string InteractionPrompt => _prompt;
    public PuzzleLetter puzzleLetter;
    // private AccentColor _accentColor;
    
    //Var para as cartas em cima da mesa aparecerem
    public GameObject[] objetosParaAparecer;
    public float delayEntreObjetos = 0.3f;

    void Start()
    {
        // Aqui esconde todas as cartas que estavam na mesa
        foreach (GameObject obj in objetosParaAparecer)
        {
            obj.SetActive(false);
        }
    }
    public bool Interact(Interactor interactor)
    {
        if (puzzleLetter.puzzleAtivado == false)
        {

            if (Letters.quantidadeAtualCartas < Letters.quantidadeTotalCartas)
            {
                Debug.Log("Ainda falta alguma coisa"); //Provavelmente colcocar algo visual para indicar isso
            }
            else
            {
                //_accentColor.ReturnOriginialColor();
                StartCoroutine(AparecerObjetosComDelay());
                Letters.RemoverDestaque();
                CameraPuzzle cameraPuzzle = GetComponent<CameraPuzzle>();
                cameraPuzzle.IniciarPuzzle(interactor);
                puzzleLetter.puzzleAtivado = true;


            }
        }
        else
        {
            CameraPuzzle cameraPuzzle = GetComponent<CameraPuzzle>();
            cameraPuzzle.ParaPuzzle(interactor);
            puzzleLetter.puzzleAtivado = false;
            Debug.Log("Era para sair do puzzle");

        }
        return true;
    }
    
    //isso revela as cartas que estavam escondidas
    private IEnumerator AparecerObjetosComDelay()
    {
        foreach (GameObject obj in objetosParaAparecer)
        {
            obj.SetActive(true);
            yield return new WaitForSeconds(delayEntreObjetos);
        }
    }

}
