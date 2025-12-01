using UnityEngine;

public class Porta : MonoBehaviour, INterfaceInteractor
{
    [SerializeField] private string _prompt;
    public string InteractionPrompt => _prompt;
    public bool hasKey = false;

    //Var para o som
    [SerializeField] AudioSource audioSourcePortaAbrindo;

    private void Start()
    {
    }

    public bool Interact(Interactor interactor)
    {
        if (hasKey == true)
        {
            audioSourcePortaAbrindo.Play();
            Debug.Log("Abriu a porta");
            
        }
        else
        {
            Debug.Log("Ainda precisa da chave");
        }
        return true;
    }

}
