using Mono.Cecil;
using UnityEngine;
using UnityEngine.InputSystem;
public class Cofre : MonoBehaviour, INterfaceInteractor
{
    [SerializeField] private string _prompt;
    public string InteractionPrompt => _prompt;

    // Vaviareis para o controle do confre
    private float rotationValue = 36f;  //Isso vai defini o quanto q ele vai rotacionar

    private float currentRotation;
    PlayerControls controls;
    [SerializeField] GameObject ponteiroCofre;

    private bool _cofreAtivo = false;

    //Interação com o cofre
    public bool Interact(Interactor interactor)
    {


        if (!_cofreAtivo)
        {
            controls = new PlayerControls();
            controls.Cofre.RotationRight.performed += _ => Rotate(rotationValue);
            controls.Cofre.RotationLeft.performed += _ => Rotate(-rotationValue);
            controls.Cofre.CheckNumber.performed += _ => ChecarNumeroAtual();

            controls.Cofre.Enable();

            CameraPuzzle cameraPuzzle = GetComponent<CameraPuzzle>();
            cameraPuzzle.IniciarPuzzle(interactor);
            _cofreAtivo = true;
        }
        else
        {
            CameraPuzzle cameraPuzzle = GetComponent<CameraPuzzle>();
            cameraPuzzle.ParaPuzzle(interactor);
            _cofreAtivo = false;

            controls.Cofre.Disable();

            Debug.Log("Era para sair do puzzle");
        }

        return true;
    }

    void Rotate(float angle)
    {
        ponteiroCofre.transform.Rotate(Vector3.up, -angle);
        currentRotation += angle;
        currentRotation = (currentRotation + 360f) % 360f;
    }


    // leitura dos nº + confirmação da senha

    private int _password1 = 1;
    private int _password2 = 4;
    private int _password3 = 3;
    private int _numeroTentado;
    private int _managerPassword = 1;

void ChecarNumeroAtual()
{
    int numeroAtual = Mathf.RoundToInt(currentRotation / 36f) % 10;
    float zRotation = ponteiroCofre.transform.localEulerAngles.z;
    int numero = Mathf.RoundToInt((360f - currentRotation) / 36f) % 10;

    _numeroTentado = numero; // <- Aqui atualiza com o número atual

    Debug.Log("Número atual do cofre: " + numero);

    switch (_managerPassword)
    {
        case 1:
            if (_numeroTentado == _password1)
            {
                _managerPassword++;
            }
            else
            {
                _managerPassword = 1;
            }
            break;
        case 2:
            if (_numeroTentado == _password2)
            {
                _managerPassword++;
            }
            else
            {
                _managerPassword = 1;
            }
            break;
        case 3:
                if (_numeroTentado == _password3)
                {
                    Debug.Log("O cofre abriu");
                    FinalizarDemo final = FindAnyObjectByType<FinalizarDemo>();
                    if (final != null)
                    {
                        final.ShowEndScreen();
                        _cofreAtivo = false;
                        controls.Cofre.Disable();
                    }
                
            }
                else
                {
                    _managerPassword = 1;
                }
            break;
    }
}


}
