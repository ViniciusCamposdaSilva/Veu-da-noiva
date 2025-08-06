using UnityEngine;
using System.Collections;
using Unity.Collections;
public class CameraPuzzle : MonoBehaviour
{

    public Transform FocoCamera;
    private Vector3 _origemCamera;
    private Quaternion _origemRotacaoCamera;
    public GameObject indicador;
    [SerializeField] private GameObject _player; 
    [SerializeField] private GameObject _playerPositionForPuzzle; 
    private Vector3 _posicaoOriginalPlayer;
    

    public bool IniciarPuzzle(Interactor interactor)
    {
        _origemCamera = Camera.main.transform.position;
        _origemRotacaoCamera = Camera.main.transform.rotation;
        _posicaoOriginalPlayer = _player.transform.position;
        Debug.Log("Posição original: " + _posicaoOriginalPlayer);
        
        _player.transform.position = _playerPositionForPuzzle.transform.position;

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


    public bool ParaPuzzle(Interactor interactor)
    {
        //Muda a bool para permitir o movimento normal
        Debug.Log("Puzzle acabou");
        
        var playerController = interactor.GetComponent<FirstPersonController>();
        _player.transform.position = _posicaoOriginalPlayer;
        playerController.SetControl(true);
        Cursor.lockState = CursorLockMode.Locked; 
        Cursor.visible = false;

        Camera.main.transform.position = _origemCamera;
        Camera.main.transform.rotation = _origemRotacaoCamera;
        indicador.SetActive(true);
        Debug.Log("Posição atual do player: " + _player.transform.position);

        var controller = _player.GetComponent<CharacterController>();

        if (controller != null)
        {
            controller.enabled = false;
            _player.transform.position = _posicaoOriginalPlayer;
            controller.enabled = true;
        }
        else
        {
            _player.transform.position = _posicaoOriginalPlayer;
        }

        return true;
    }
    
}
