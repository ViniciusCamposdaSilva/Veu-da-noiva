using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.SceneManagement;

public class Relogio : MonoBehaviour, INterfaceInteractor
{
    [SerializeField] private string _prompt;
    public string InteractionPrompt => _prompt;

    [SerializeField] public Transform transformVerificadorHora;
    [SerializeField] public Transform transformMarcadorHora;
    [SerializeField] public Transform transformVerificadorMin;
    [SerializeField] public Transform transformMarcadorMin;
    [SerializeField] public Animator animator;

    [SerializeField] private GameObject _chave;
    [SerializeField] private DialogueSystem dialogueSystem;
    [SerializeField] private FinalizarDemo _finalizarDemo;

    private PlayerControls _controls;
    public Camera SubCamera;
    public bool relogioAtivo = false;

    private bool puzzleFinalizado = false;

    private bool _selecaoVar = false;
    private bool _selecao
    {
        get => _selecaoVar;
        set
        {
            if (_selecaoVar == true && value == false)
            {
                VerificarPonteiros();
            }
            _selecaoVar = value;
        }
    }

    private GameObject _ponteiroSelecionado;

    private Vector3 anguloInicialDoPonteiro;

    [SerializeField] private GameObject[] objetos;

    void Start()
    {
        foreach (GameObject obj in objetos)
        {
            Renderer rend = obj.GetComponent<Renderer>();
            if (rend != null)
            {
                rend.enabled = false;
            }
        }
    }

    void Update()
    {
        if (_selecaoVar == true)
        {
            GirarPonteiro();
        }
    }

    public bool Interact(Interactor interactor)
    {
        if (puzzleFinalizado)
            return false; 

        if (relogioAtivo) 
        {
            _controls = new PlayerControls();
            Debug.Log("Entrou no puzzle");
            _controls.Clock.Enable();
            _controls.Clock.Select.performed += _ => SelecionarPonteiro();
            CameraPuzzle cameraPuzzle = GetComponent<CameraPuzzle>();
            cameraPuzzle.IniciarPuzzle(interactor);
            relogioAtivo = false; 
        }
        else
        {
            Debug.Log("Saiu do puzzle");
            _controls.Clock.Disable();
            CameraPuzzle cameraPuzzle = GetComponent<CameraPuzzle>();
            cameraPuzzle.ParaPuzzle(interactor);
            relogioAtivo = true; 
        }
        return true;
    }

    private void SelecionarPonteiro()
    {
        if (puzzleFinalizado)
            return; 

        LayerMask puzzleMask = LayerMask.GetMask("Puzzles");

        Ray ray = SubCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, puzzleMask))
        {
            if (hit.collider.CompareTag("ClockHand"))
            {
                GameObject ponteiroRaycast = hit.collider.gameObject;

                if (ponteiroRaycast == _ponteiroSelecionado)
                {
                    _selecao = false;
                    _ponteiroSelecionado = null;
                }
                else
                {
                    _selecao = true;
                    _ponteiroSelecionado = ponteiroRaycast;

                    anguloInicialDoPonteiro = _ponteiroSelecionado.transform.localEulerAngles;
                }
            }
            else
            {
                _selecao = false;
                _ponteiroSelecionado = null;
            }
        }
        else
        {
            _selecao = false;
            _ponteiroSelecionado = null;
        }

        Debug.DrawRay(ray.origin, ray.direction * 200f, Color.green);
    }

    void GirarPonteiro()
    {
        if (_ponteiroSelecionado == null || puzzleFinalizado)
            return;

        Vector2 mousePos = Mouse.current.position.ReadValue();
        Vector2 centerScreenPos = SubCamera.WorldToScreenPoint(_ponteiroSelecionado.transform.position);

        Vector2 dir = mousePos - centerScreenPos;

        float angle = -Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        angle += 90f;

        Vector3 novaRot = anguloInicialDoPonteiro;
        novaRot.z = angle;

        _ponteiroSelecionado.transform.localEulerAngles = novaRot;
    }

    bool EstaColidindo(Transform a, Transform b)
    {
        Collider colA = a.GetComponent<Collider>();
        Collider colB = b.GetComponent<Collider>();
        return colA.bounds.Intersects(colB.bounds);
    }

    void VerificarPonteiros()
    {
        if (puzzleFinalizado)
            return;

        Debug.Log("Boa pergunta ai mermão");
        bool horaCorreta = EstaColidindo(transformVerificadorHora, transformMarcadorHora);
        bool minutoCorreto = EstaColidindo(transformVerificadorMin, transformMarcadorMin);

        if (horaCorreta && minutoCorreto)
        {
            StartCoroutine(FinalizarDemo());
        }
    }

    public IEnumerator FinalizarDemo()
    {
        puzzleFinalizado = true; 

        _controls.Clock.Disable();
        animator.SetTrigger("PuzzleCompletado");
        yield return new WaitForSeconds(2.5f);

        dialogueSystem.ShowDialogue("Outra chave?", 2);
        yield return new WaitForSeconds(2.0f);

        dialogueSystem.ShowDialogue("Como que eu nunca tinha visto ela antes?", 3.0f);
        yield return new WaitForSeconds(3.5f);

        Destroy(_chave, 1.0f);
        yield return new WaitForSeconds(0.5f);

        animator.SetTrigger("FinalizarRelogio");
        yield return new WaitForSeconds(3.0f);

        SceneManager.LoadScene("Final Menu");
    }
}
