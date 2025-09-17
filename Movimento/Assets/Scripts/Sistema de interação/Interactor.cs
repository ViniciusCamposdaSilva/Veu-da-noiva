using UnityEngine;

using UnityEngine.InputSystem;

  

public class Interactor : MonoBehaviour

{

    // Variaveis para o texto com a indicação do controle

[SerializeField] private DialogueSystem _dialogueSystem;

  

[SerializeField] private Transform _interactionPoint;

[SerializeField] private float _interactionPointRadius;

[SerializeField] private LayerMask _interactableMask;

[SerializeField] private InteractionPromptUI _interactionPromptUI;

private readonly Collider[] _colliders = new Collider[3];

[SerializeField] private int _numFound;

private INterfaceInteractor _interactable;

private void Update()
{
_numFound = Physics.OverlapSphereNonAlloc(_interactionPoint.position, _interactionPointRadius, _colliders, _interactableMask);

if (_numFound > 0)
{
 _interactable = _colliders[0].GetComponent<INterfaceInteractor>();
if (_interactable != null)
{
if (_dialogueSystem.indicadorControleAtivo == false) _dialogueSystem.IndicadorControleTrue(_interactable.InteractionPrompt);
if (Keyboard.current.eKey.wasPressedThisFrame) _interactable.Interact(this);
}
}
else
{
if (_interactable != null) _interactable = null;
if (_dialogueSystem.indicadorControleAtivo == true) _dialogueSystem.IndicadorControleFalse();
}
}

private void OnDrawGizmos()

{

Gizmos.color = Color.red;

Gizmos.DrawWireSphere(_interactionPoint.position, _interactionPointRadius);
}
}
