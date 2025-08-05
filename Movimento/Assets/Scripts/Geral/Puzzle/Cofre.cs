using UnityEngine;
using UnityEngine.InputSystem;
public class Cofre : MonoBehaviour, INterfaceInteractor
{
    [SerializeField] private string _prompt;
    public string InteractionPrompt => _prompt;

    private float rotationValue = 36f;  //Isso vai defini o quanto q ele vai rotacionar

    private float currentRotation;
    PlayerControls controls;
    [SerializeField] GameObject ponteiroCofre;

    private bool _cofreAtivo = false;

    //Interação com o cofre
    public bool Interact(Interactor interactor)
    {
        CameraPuzzle cameraPuzzle = GetComponent<CameraPuzzle>();

        if (_cofreAtivo)
        {
            cameraPuzzle.IniciarPuzzle(interactor);
            _cofreAtivo = true;

        }
        else
        {

            cameraPuzzle.ParaPuzzle(interactor);
            _cofreAtivo = false;

            Debug.Log("Era para sair do puzzle");
        }

        return true;
    }

    // Controle do Cofre:
    void Awake()
    {
        controls = new PlayerControls();
    }

    void OnEnable()
    {
        controls.Cofre.Enable();
        controls.Cofre.Rotation.performed += ctx => ControleCofre();
    }

    void OnDisable()
    {
        controls.Cofre.Disable();
    }


    public void ControleCofre()
    {



        
    }
}
