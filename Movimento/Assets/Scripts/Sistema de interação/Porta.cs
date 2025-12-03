using UnityEngine;

public class Porta : MonoBehaviour, INterfaceInteractor
{
    [SerializeField] private string _prompt;
    public string InteractionPrompt => _prompt;
    public bool hasKey = false;
    public bool portaAberta = false;

    //Var para o som
    [SerializeField] AudioSource audioSourcePortaAbrindo;
    [SerializeField] public Animator animator;

    private void Start()
    {
    }

    public bool Interact(Interactor interactor)
    {
        if (hasKey && portaAberta == false)
        {
            audioSourcePortaAbrindo.Play();
            Debug.Log("Abriu a porta");
            animator.SetTrigger("PortaAbriu");
            portaAberta = true;
        }
        else if (hasKey && portaAberta == true)
        {
            animator.SetTrigger("PortaFechou");
            Debug.Log("Ainda precisa da chave");
            portaAberta = false;
        }
        else
        {
            Debug.Log("Ainda precisa da chave");
        }

        return true;
    }

}
