using UnityEngine;
public class CameraPuzzle : MonoBehaviour
{
    
    public GameObject indicador;

    // Var para teleportar a câmera
    public Transform FocoCamera;
    private Vector3 _originalPositionCamera; 
    private Quaternion _originalRotationCamera;

    // Var para teleportar o player
    private Vector3 _originalPositionPlayer; //local em que o player deve voltar após o puzzle
    [SerializeField] private GameObject _puzzlePosition; //local em que o player deve ir quando começar um puzzle
    [SerializeField] private GameObject _player;


    public bool IniciarPuzzle(Interactor interactor)
    {
        _originalPositionCamera = Camera.main.transform.position;
        _originalRotationCamera = Camera.main.transform.rotation;
        _originalPositionPlayer = _player.transform.position;
        _player.transform.position = _puzzlePosition.transform.position;

        Debug.Log("posição original do player: " + _originalPositionPlayer);

        //Muda a bool que permite o movimnto para falsa
        Debug.Log("Puzzle iniciado");
        var playerController = interactor.GetComponent<FirstPersonController>();
        if (playerController == true)
        {
            playerController.SetControl(false);
        }

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Camera.main.transform.position = FocoCamera.position;
        Camera.main.transform.rotation = FocoCamera.rotation;
        indicador.SetActive(false);
        return true;
    }

    public void ParaPuzzle(Interactor interactor)
    {
        //Muda a bool para permitir o movimento normal
        Debug.Log("Puzzle acabou");
        
        var playerController = interactor.GetComponent<FirstPersonController>();
        _player.transform.position = _originalPositionPlayer;
        playerController.SetControl(true);

        var characterController = _player.GetComponent<CharacterController>();
        if (characterController != null)
        {
        characterController.enabled = false;
        _player.transform.position = _originalPositionPlayer;
        characterController.enabled = true;
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Camera.main.transform.position = _originalPositionCamera;
        Camera.main.transform.rotation = _originalRotationCamera;
        

        indicador.SetActive(true);
    }
    
}
