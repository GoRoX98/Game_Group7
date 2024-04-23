using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "ScriptableObjects/Create Char Type")]
public class CharProgressSO : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private List<CharCharacteristics> _char—haracteristicsData;

    public string Name => _name;
    public List<CharCharacteristics> Char—haracteristics => _char—haracteristicsData;
    
    public CharCharacteristics CurrentLevelData(int level)
    {
        Debug.Log("Take char data");
        int index = level - 1;

        if (level < 1)
            return _char—haracteristicsData[0];
        else if (level >= _char—haracteristicsData.Count)
            return _char—haracteristicsData[_char—haracteristicsData.Count - 1];

        return _char—haracteristicsData[index];
    }
}
