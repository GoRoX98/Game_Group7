using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TradeSlot : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _nameTMP;
    [SerializeField] private TextMeshProUGUI _costTMP;
    private LootOwner _owner;
    private LootSO _lootData;

    public LootSO LootData => _lootData;
    public static Action<LootSO> SlotSale;

    public void Init(LootSO data, LootOwner owner)
    {
        _lootData = data;
        _icon.sprite = data.Icon;
        _nameTMP.text = data.Name;
        _costTMP.text = data.Cost.ToString();
        _owner = owner;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_owner == LootOwner.Player)
        {
            SlotSale?.Invoke(_lootData);
            Destroy(gameObject);
        }
    }
}

public enum LootOwner
{
    Player,
    Trader,
    None
}
