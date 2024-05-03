using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _lifetime = 1f;
    private float _timer = 0f;
    private bool _live = true;
    
    void Update()
    {
        _timer += Time.deltaTime;

        if (_timer > _lifetime && _live)
        {
            _live = false;
            Destroy(gameObject);
        }
    }
}
