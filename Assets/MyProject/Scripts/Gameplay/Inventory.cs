using System.Collections.Generic;

public class Inventory
{
    private List<LootSO> _backpack = new();
    public List<LootSO> Backpack => _backpack;


    public void AddLoot(LootSO item) => _backpack.Add(item);
    public List<LootSO> SellLoot()
    {
        List<LootSO> loot = new(_backpack);
        _backpack.Clear();
        return loot;
    }
}
