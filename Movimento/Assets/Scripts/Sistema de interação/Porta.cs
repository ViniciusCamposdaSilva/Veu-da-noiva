using UnityEngine;

public class Porta : MonoBehaviour, INterfaceInteractor
{
    [SerializeField] private string _prompt;
    public string InteractionPrompt => _prompt;

    public bool Interact(Interactor interactor)
    {
        var inventory = interactor.GetComponent<BasicInventory>();
        
        if (inventory == null) return false;
        
        if (inventory.HasKey)
        {
            Debug.Log(message: "Abriu a porta");
            return true;
        }
        
        Debug.Log(message:"Ainda precisa da chave");
        return false;
    }
}
