using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour, IDamageble, IMoveble
{
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private Animator _animator;

    [Header("Player Settings")]
    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private float _speed;
    [SerializeField] private float _maxSpeed = 5f;
    [SerializeField] private float _speedIncrase = 1f;

    private PlayerInput _input;
    private InputAction _moveAction;
    private Attacker _attacker;
    private Vector3 _move;
    private Camera _camera;

    private int _currentHealth;
    private bool _alive = true;

    public float Speed => _speed;
    public float MaxSpeed => _maxSpeed;
    public float SpeedIncrase => _speedIncrase;
    public int Health => _currentHealth;

    public EventHandler<int> TakeDamage => OnTakeDmg;
    public EventHandler<int> TakeHeal => OnHeal;

    #region UnityMethods

    private void Awake()
    {
        _attacker = GetComponent<Attacker>();
        _input = GetComponent<PlayerInput>();
        _moveAction = _input.actions["Movement"];
    }

    private void OnEnable()
    {
        //_moveAction.performed += Move;
    }

    private void OnDisable()
    {
        //_moveAction.performed -= Move;
    }

    private void Start()
    {
        _currentHealth = _maxHealth;
        _alive = _currentHealth > 0;
        _camera = Camera.main;
    }

    private void Update()
    {
        //Old movement
        /*var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        _move = new Vector3(horizontal, 0, vertical);*/

        _move = _moveAction.ReadValue<Vector2>();

        Vector3 movementVector = _camera.transform.TransformDirection(_move);
        movementVector.y = 0;
        movementVector.Normalize();

        transform.forward = movementVector;

        _characterController.Move(movementVector * (_speed * Time.deltaTime));
        _animator.SetFloat("Speed", _characterController.velocity.magnitude);
    }

    private void LateUpdate()
    {
        if (Input.GetMouseButtonDown(0))
            _attacker.Attack();
    }

    #endregion

    private void Move(InputAction.CallbackContext obj)
    {
        _move = obj.ReadValue<Vector2>();

    }

    private void Die()
    {
        print("Dead");
    }

    private void OnHeal(object sender, int heal)
    {
        if (_currentHealth < _maxHealth)
            _currentHealth += heal;

        if (_currentHealth > _maxHealth)
            _currentHealth = _maxHealth;
    }

    private void OnTakeDmg(object sender, int damage)
    {
        print($"Player Health: {_currentHealth} | Dmg: {damage}");

        if (sender is not Attacker)
            return;

        if (_currentHealth > damage)
            _currentHealth -= damage;
        else if (_alive)
        {
            _alive = false;
            _currentHealth = 0;
            Die();
        }
    }

}
