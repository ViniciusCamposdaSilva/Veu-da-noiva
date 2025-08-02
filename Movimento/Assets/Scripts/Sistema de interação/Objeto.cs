using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Objeto : MonoBehaviour, INterfaceInteractor
{
    [SerializeField] private string _prompt;
    public string InteractionPrompt => _prompt;

    public bool Interact(Interactor interactor)
    {
        Debug.Log(message: "Pegou o objeto");
        Destroy(gameObject);
        return true;
    }


}


