using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PuzzleLetter : MonoBehaviour
{
    public Camera mainCamera;
    public Camera SubCamera;
    private PlayerControls controls;
    private GameObject selectedObject;
    public float scrollSpeed; //setar a velocidade com que a foto gira
    public bool puzzleAtivado = false; // bool para ativar e desativar o codigo
    public static int quantidadeTotalCartas = 8;
    public static int quantidadeAtualCartas = 0;

    // variaveis para o destque da mesa:
    [SerializeField] private GameObject objetoParaDestacar;
    private Color corDestaque = Color.yellow;
    private static Renderer rendererDoObjeto;
    public static bool destaqueFeito = false;
    private static Color corOriginal;

    private void Start()
    {
        quantidadeAtualCartas = 0;

        if (objetoParaDestacar != null && rendererDoObjeto == null)
        {
            rendererDoObjeto = objetoParaDestacar.GetComponent<Renderer>();
            corOriginal = rendererDoObjeto.material.color;
        }
    }
    private void Awake()
    {
        controls = new PlayerControls();
    }

    private void OnEnable()
    {
        controls.Letter.Enable();
        controls.Letter.Selecionar.performed += OnClick;
    }

    private void OnDisable()
    {
        controls.Letter.Selecionar.performed -= OnClick;
        controls.Letter.Disable();
    }



    //raycast
    private void OnClick(InputAction.CallbackContext context)
    {
        if (!puzzleAtivado) return;
        LayerMask puzzleMask = LayerMask.GetMask("Puzzles");

        Ray ray = SubCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit, puzzleMask))
        {
            if (hit.collider.CompareTag("Letters"))
            {
                selectedObject = hit.collider.gameObject;

            }
        }
    }

 
    void Update()
    {

        //Código de controle dentro do puzzle
        if (!puzzleAtivado) return;

        if (selectedObject != null)
        {
            Ray ray = SubCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            Plane plane = new Plane(Vector3.up, selectedObject.transform.position);
            if (plane.Raycast(ray, out float distance))
            {
                Vector3 mouseWorldPosition = ray.GetPoint(distance);
                Vector3 currentPosition = selectedObject.transform.position;
                selectedObject.transform.position = new Vector3(mouseWorldPosition.x, currentPosition.y, mouseWorldPosition.z);
                Debug.Log("Era para pegar o objeto");
            }

            if (!Mouse.current.leftButton.isPressed)
            {
                selectedObject = null;
                Debug.Log("Soltou o objeto");
            }

            //Aqui é a parte da rotação da carta
            float girarInput = controls.Letter.Rotacionarcarta.ReadValue<float>();

            if (girarInput != 0f)
            {
                selectedObject.transform.Rotate(Vector3.up, girarInput * scrollSpeed * Time.deltaTime, Space.World);
            }

        }

    }
    //Destaque e dialogo

    public void AdicionarDestaque()
    {
        Renderer r = objetoParaDestacar.GetComponent<Renderer>();
        r.material.color = corDestaque;
        destaqueFeito = true;
    }


    public static void RemoverDestaque()
    {
        if (rendererDoObjeto != null)
        {
            rendererDoObjeto.material.color = corOriginal;
            destaqueFeito = false;
        }
    }


}
