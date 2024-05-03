using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Scrollbar _playerHealth;
    [SerializeField] private Scrollbar _playerExp;
    [SerializeField] private TextMeshProUGUI _goldTMP;

    private void OnEnable()
    {
        Player.PlayerHealthChanged += OnPlayerHealthChanged;
        _player.Wallet.GoldChanged += OnGoldChanged;
    }

    private void OnDisable()
    {
        Player.PlayerHealthChanged -= OnPlayerHealthChanged;
        _player.Wallet.GoldChanged -= OnGoldChanged;
    }

    private void OnPlayerHealthChanged(int health, int maxHealth)
    {
        _playerHealth.size = (float)health / (float)maxHealth;
    }

    private void OnGoldChanged(int gold)
    {
        _goldTMP.text = gold.ToString();
    }
}
