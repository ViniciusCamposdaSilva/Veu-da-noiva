using UnityEngine;
using UnityEngine.InputSystem;

public class PuzzleLetter : MonoBehaviour
{
    private Camera mainCamera;
    private PlayerControls controls;
    private GameObject selectedObject;
    public float scrollSpeed; //setar a velocidade com que a foto gira

    public bool puzzleAtivado = false; // bool para ativar e desativar o codigo

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
         mainCamera = Camera.main;
     }

    //raycast
    private void OnClick(InputAction.CallbackContext context)
    {
        if (!puzzleAtivado) return;

        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.CompareTag("Letters"))
            {
                selectedObject = hit.collider.gameObject;
            }
        }
    }

    // aqui para mover o objeto, usando a posição do mouse, mas mantendo o y fixo
    void Update()
    {
        if (!puzzleAtivado) return;

        if (selectedObject != null)
        {
            Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            Plane plane = new Plane(Vector3.up, selectedObject.transform.position);
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
            Vector2 scrollValue = controls.Letter.Rotacionarcarta.ReadValue<Vector2>();
            float scrollrotacaoY = scrollValue.y;

            if (scrollrotacaoY != 0f)
            {
                selectedObject.transform.Rotate(Vector3.up, scrollrotacaoY * scrollSpeed * Time.deltaTime, Space.World);
            }
        }

    }
}
