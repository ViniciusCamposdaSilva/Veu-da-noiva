using UnityEngine;
using System.Collections;
public class CameraPuzzle : MonoBehaviour
{

    public Transform FocoCamera;
    private Vector3 _origemCamera;
    private Quaternion _origemRotacaoCamera;
    public GameObject[] objetosParaAparecer;

    public GameObject indicador;
    public float delayEntreObjetos = 0.3f;

    void Start()
{
    foreach (GameObject obj in objetosParaAparecer)
    {
        obj.SetActive(false); // ou só esconder visualmente
    }
}

    public bool IniciarPuzzle(Interactor interactor)
    {
        _origemCamera = Camera.main.transform.position;
        _origemRotacaoCamera = Camera.main.transform.rotation;

        //Muda a bool que permite o movimnto para falsa
        Debug.Log("Puzzle iniciado interagida");
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


        StartCoroutine(AparecerObjetosComDelay());

        return true;
    }

    private IEnumerator AparecerObjetosComDelay()
    {
        foreach (GameObject obj in objetosParaAparecer)
        {
            obj.SetActive(true);
            yield return new WaitForSeconds(delayEntreObjetos);
        }
    }


    public bool ParaPuzzle(Interactor interactor)
    {
        //Muda a bool para permitir o movimento normal
        Debug.Log("Puzzle acabou");
        var playerController = interactor.GetComponent<FirstPersonController>();
        playerController.SetControl(true);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Camera.main.transform.position = _origemCamera;
        Camera.main.transform.rotation = _origemRotacaoCamera;
        indicador.SetActive(true);

        return true;
    }
    
}
