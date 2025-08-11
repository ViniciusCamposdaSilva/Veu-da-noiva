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

    // var para as cartas aparecerem
    public float delayEntreObjetos = 0.3f;
    public GameObject[] objetosParaAparecer;


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

    void Start()
    {
        
        foreach (GameObject obj in objetosParaAparecer)
        {
            obj.SetActive(false);
        }
     }
         //isso revela as cartas que estavam escondidas
    public IEnumerator AparecerObjetosComDelay()
    {
        foreach (GameObject obj in objetosParaAparecer)
        {
            obj.SetActive(true);
            yield return new WaitForSeconds(delayEntreObjetos);
        }
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

    // aqui para mover o objeto
    void Update()
    {
        if (!puzzleAtivado) return;

        if (selectedObject != null)
        {
            Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            Plane plane = new Plane(Vector3.up, selectedObject.transform.position); // isso faz o y ser fixo pro objeto
            if (plane.Raycast(ray, out float distance))
            {
                Vector3 mouseWorldPosition = ray.GetPoint(distance);
                Vector3 currentPosition = selectedObject.transform.position;
                selectedObject.transform.position = new Vector3(mouseWorldPosition.x, currentPosition.y, mouseWorldPosition.z);
            }

            if (!Mouse.current.leftButton.isPressed)
            {
                selectedObject = null;
            }

            //Aqui é a parte da rotação da carta
            float girarInput = controls.Letter.Rotacionarcarta.ReadValue<float>();

            if (girarInput != 0f)
            {
            selectedObject.transform.Rotate(Vector3.up, girarInput * scrollSpeed * Time.deltaTime, Space.World);
            }

        }

    }
}
