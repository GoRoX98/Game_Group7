using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Slot : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _nameTMP;
    [SerializeField] private TextMeshProUGUI _costTMP;

    public void Init(LootSO data)
    {
        _icon.sprite = data.Icon;
        _nameTMP.text = data.Name;
        _costTMP.text = data.Cost.ToString();
    }
}
