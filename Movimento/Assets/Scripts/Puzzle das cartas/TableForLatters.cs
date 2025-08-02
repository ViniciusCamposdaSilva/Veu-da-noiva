using System;
using Unity.VisualScripting;
using UnityEngine;

public class TableForLatters : MonoBehaviour, INterfaceInteractor
{
    [SerializeField] private string _prompt;
    public string InteractionPrompt => _prompt;
    public PuzzleLetter puzzleLetter;
    



    public bool Interact(Interactor interactor)
    {
        if (puzzleLetter.isActive == false)
        {

            if (Letters.quantidadeAtualCartas < Letters.quantidadeTotalCartas)
            {
                Debug.Log("Ainda falta alguma coisa"); //Provavelmente colcocar algo visual para indicar isso
            }
            else
            {
                CameraPuzzle cameraPuzzle = GetComponent<CameraPuzzle>();
                cameraPuzzle.IniciarPuzzle(interactor);
                puzzleLetter.isActive = true;


            }
        }
        else
        {
            CameraPuzzle cameraPuzzle = GetComponent<CameraPuzzle>();
            cameraPuzzle.ParaPuzzle(interactor);
            puzzleLetter.isActive = false;
            Debug.Log("Era para sair do puzzle");
                
            
        }
        return true;
    }
    


}
