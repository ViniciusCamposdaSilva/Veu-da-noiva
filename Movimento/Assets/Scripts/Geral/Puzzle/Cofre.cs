using UnityEngine;

public class Cofre : MonoBehaviour, INterfaceInteractor
{
    [SerializeField] private string _prompt;
    public string InteractionPrompt => _prompt;



    private bool cofreAtivo = false;
    
    public bool Interact(Interactor interactor)
    {
        CameraPuzzle cameraPuzzle = GetComponent<CameraPuzzle>();

        if (!cofreAtivo)
        {
            cameraPuzzle.IniciarPuzzle(interactor);
            cofreAtivo = true;
        }
        else
        {

            cameraPuzzle.ParaPuzzle(interactor);
            cofreAtivo = false;

            Debug.Log("Era para sair do puzzle");
        }

        return true;
    }
}
