using System.Collections.Generic;
using UnityEngine;

public class UI_Inventory : MonoBehaviour
{
    [SerializeField] private GameObject _slotPrefab;
    [SerializeField] private Transform _backpack;

    private List<UI_Slot> _backpackSlots = new List<UI_Slot>();

    private void FixedUpdate()
    {
        if (SceneController.Player.Loot.Count > _backpackSlots.Count)
            AddSlot(_backpackSlots.Count);
    }

    private void AddSlot(int index)
    {
        UI_Slot slot = Instantiate(_slotPrefab, _backpack).GetComponent<UI_Slot>();
        slot.Init(SceneController.Player.Loot[index]);
        _backpackSlots.Add(slot);
    }
}
