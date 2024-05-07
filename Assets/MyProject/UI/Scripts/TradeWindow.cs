using UnityEngine;

public class TradeWindow : MonoBehaviour
{
    [SerializeField] private GameObject _slotPrefab;
    [SerializeField] private Transform _playerTransformInventory;
    [SerializeField] private Transform _traderTransformInventory;

    private void Start()
    {
        foreach(LootSO loot in SceneController.Player.Loot)
        {
            TradeSlot slot = Instantiate(_slotPrefab, _playerTransformInventory).GetComponent<TradeSlot>();
            slot.Init(loot, LootOwner.Player);
        }
    }

    private void OnEnable()
    {
        TradeSlot.SlotSale += AddTraderObj;
    }

    private void OnDisable()
    {
        TradeSlot.SlotSale -= AddTraderObj;
    }

    private void AddTraderObj(LootSO data)
    {
        TradeSlot slot = Instantiate(_slotPrefab, _traderTransformInventory).GetComponent<TradeSlot>();
        slot.Init(data, LootOwner.Trader);
    }
}
