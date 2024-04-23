using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] private Scrollbar _playerHealth;

    private void OnEnable()
    {
        Player.PlayerHealthChanged += OnPlayerHealthChanged;
    }

    private void OnDisable()
    {
        Player.PlayerHealthChanged -= OnPlayerHealthChanged;
    }

    private void OnPlayerHealthChanged(int health, int maxHealth)
    {
        _playerHealth.size = (float)health / (float)maxHealth;
    }
}
