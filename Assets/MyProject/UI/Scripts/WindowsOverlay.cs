using UnityEngine;

public class WindowsOverlay : MonoBehaviour
{
    [SerializeField] private GameObject _inventory;

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
            _inventory.SetActive(!_inventory.activeSelf);
    }
}
