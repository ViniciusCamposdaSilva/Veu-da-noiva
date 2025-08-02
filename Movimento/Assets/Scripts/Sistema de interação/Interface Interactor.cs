using UnityEngine;

public interface INterfaceInteractor
{
    public string InteractionPrompt { get; }

    public bool Interact (Interactor interactor);
}
