using UnityEngine;
using UnityEngine.InputSystem;

public class Relógio : MonoBehaviour, INterfaceInteractor
{
    [SerializeField] private string _prompt;
    public string InteractionPrompt => _prompt;

    // Var algo ai, anote depois
    private bool _relogioAtivo = true;
    PlayerControls controls;
    public Camera SubCamera;
    

    public bool Interact(Interactor interactor)
    {
        controls = new PlayerControls();

        if (_relogioAtivo == true)
        {
            controls.Enable();
            controls.Clock.Select.performed += _ => SelecionarPonteiro();
            CameraPuzzle cameraPuzzle = GetComponent<CameraPuzzle>();
            cameraPuzzle.IniciarPuzzle(interactor);
            _relogioAtivo = false;
        }
        else
        {
            CameraPuzzle cameraPuzzle = GetComponent<CameraPuzzle>();
            cameraPuzzle.ParaPuzzle(interactor);
            _relogioAtivo = true;
            controls.Clock.Disable();
            Debug.Log("Era para sair do puzzle");
        }
        return true;
    }

    private void SelecionarPonteiro()
    {
        //raycast
        LayerMask puzzleMask = LayerMask.GetMask("Puzzles");

        Ray ray = SubCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, puzzleMask))
        {
            if (hit.collider.CompareTag("ClockHand"))
            {
                Debug.Log("Era para selecionar o ponteiro");
            }
        }      

        Debug.DrawRay(ray.origin, ray.direction * 200f, Color.green);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
