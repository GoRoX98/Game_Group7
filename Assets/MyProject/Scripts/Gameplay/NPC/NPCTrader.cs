using System;
using UnityEngine;

public class NPCTrader : MonoBehaviour
{
    [SerializeField] private GameObject _tradeWindow;
    [SerializeField] private float _tradeRadius = 5f;

    public static event Action<int> SellLoot;

    private void OnEnable()
    {
        TradeSlot.SlotSale += Trade;
    }

    private void OnDisable()
    {
        TradeSlot.SlotSale -= Trade;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
            if(Vector3.Distance(transform.position, SceneController.PlayerPos) < _tradeRadius)
                TradeWindow();
    }

    private void TradeWindow() => _tradeWindow.SetActive(!_tradeWindow.activeSelf);

    private void Trade()
    {
        if (SceneController.Player.Loot.Count <= 0)
            return;

        print("Sell sucess");
        int gold = 0;

        foreach (LootSO loot in SceneController.Player.Loot)
        {
            gold += loot.Cost;
        }

        SellLoot?.Invoke(gold);
    }

    private void Trade(LootSO loot)
    {
        SceneController.Player.Loot.Remove(loot);
        SellLoot?.Invoke(loot.Cost);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _tradeRadius);
    }
}
