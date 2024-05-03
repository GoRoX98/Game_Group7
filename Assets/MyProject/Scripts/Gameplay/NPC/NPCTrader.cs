using System;
using UnityEngine;

public class NPCTrader : MonoBehaviour
{
    [SerializeField] private float _tradeRadius = 5f;

    public static event Action<int> SellLoot;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T) && Vector3.Distance(transform.position, SceneController.PlayerPos) < _tradeRadius)
            Trade();
    }

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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _tradeRadius);
    }
}