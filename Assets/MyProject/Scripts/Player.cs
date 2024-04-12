using UnityEngine;

public class Player : MonoBehaviour, IDamageble
{
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private Animator _animator;
    [SerializeField] private int _maxHealth;
    [SerializeField] private float _speed;

    private Vector3 _input;
    private Camera _camera;

    private int _health;

    public int Health => _health;

    #region UnityMethods

    private void Start()
    {
        _health = _maxHealth;
        _camera = Camera.main;
    }

    private void Update()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        _input = new Vector3(horizontal, 0, vertical);

        Vector3 movementVector = _camera.transform.TransformDirection(_input);
        movementVector.y = 0;
        movementVector.Normalize();

        transform.forward = movementVector;

        _characterController.Move(movementVector * (_speed * Time.deltaTime));
        _animator.SetFloat("Speed", _characterController.velocity.magnitude);
    }

    #endregion

    #region IDamagble

    public void Die()
    {
        print("Dead");
    }

    public void Heal(int heal)
    {
        if (_health < _maxHealth)
            _health += heal;

        if (_health > _maxHealth)
            _health = _maxHealth;
    }

    public void TakeDmg(int damage)
    {
        if (_health > damage)
            _health -= damage;
        else
        {
            _health = 0;
            Die();
        }
    }

    #endregion

}
