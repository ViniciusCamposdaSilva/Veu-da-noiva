using UnityEngine;
using UnityEngine.InputSystem;

public class BotaoPerde : MonoBehaviour
{
    [Header("Configura��es")]
    [SerializeField] private float alteracaoInteresse = -10f;
    [SerializeField] private bool ehPositivo = true;

    private Camera mainCamera;
    private Mouse mouse;

    private void Start()
    {
        mainCamera = Camera.main;
        mouse = Mouse.current;
    }

    private void Update()
    {
        if (mouse.leftButton.wasPressedThisFrame)
        {
            Ray raio = mainCamera.ScreenPointToRay(mouse.position.ReadValue());
            if (Physics.Raycast(raio, out RaycastHit hit) && hit.transform == transform)
            {
                float quantidade = ehPositivo ? alteracaoInteresse : -alteracaoInteresse;
                FindObjectOfType<Interesse>().ModificarInteresse(quantidade);
                Debug.Log($"Bot�o pressionado! Interesse {(ehPositivo ? "+" : "-")}{alteracaoInteresse}");
            }
        }
    }
}