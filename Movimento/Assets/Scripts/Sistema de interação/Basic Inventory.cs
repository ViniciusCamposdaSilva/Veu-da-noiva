using UnityEngine;
using UnityEngine.InputSystem;

public class BasicInventory : MonoBehaviour
{
    public bool HasKey = false;
    private void Update()
    {
        if (Keyboard.current.qKey.wasPressedThisFrame) HasKey = !HasKey;
    }
}
