using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour, IDamageble, IMoveble
{
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private Animator _animator;
    [SerializeField] private ParticleSystem _bloodParticles;

    [Header("Player Settings")]
    [SerializeField] private float _lootingRadius = 1f;
    [SerializeField] private int _level = 1;
    [SerializeField] private CharProgressSO _progressSO;
    private CharCharacteristics _charData => _progressSO.CurrentLevelData(_level);
    private Inventory _inventory = new Inventory();
    private Wallet _wallet = new();

    private Collider[] _loot = new Collider[5];
    private float _currentSpeed = 1f;
    private PlayerInput _input;
    private InputAction _moveAction;
    private Attacker _attacker;
    private Vector3 _move;
    private Camera _camera;

    private int _currentHealth;
    private bool _alive = true;

    public List<LootSO> Loot => _inventory.Backpack;
    public Wallet Wallet => _wallet;
    public float Speed => _currentSpeed;
    public float MaxSpeed => _charData.MaxSpeed;
    public float SpeedIncrase => _charData.SpeedIncrase;
    public int Health => _currentHealth;
    public int MaxHealth => _charData.MaxHP;

    public EventHandler<int> TakeDamage => OnTakeDmg;
    public EventHandler<int> TakeHeal => OnHeal;

    public static event Action<int, int> PlayerHealthChanged;

    #region UnityMethods


    private void Awake()
    {
        _attacker = GetComponent<Attacker>();
        _input = GetComponent<PlayerInput>();
        _moveAction = _input.actions["Movement"];
    }

    private void OnEnable()
    {
        NPCTrader.SellLoot += OnTrade;
        //_moveAction.performed += Move;
    }

    private void OnDisable()
    {
        NPCTrader.SellLoot -= OnTrade;
        //_moveAction.performed -= Move;
    }

    private void Start()
    {
        _currentHealth = MaxHealth;
        _alive = _currentHealth > 0;
        _camera = Camera.main;
        PlayerHealthChanged?.Invoke(_currentHealth, MaxHealth);
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

        if (movementVector.x != 0 && movementVector.z != 0 && _currentSpeed < MaxSpeed)
            _currentSpeed += SpeedIncrase * Time.deltaTime;
        else if (movementVector.x == 0 && movementVector.z == 0)
            _currentSpeed = _charData.MinSpeed;

        _characterController.Move(movementVector * (_currentSpeed * Time.deltaTime));
        _animator.SetFloat("Speed", _characterController.velocity.magnitude);
    }

    private void LateUpdate()
    {
        if (Input.GetMouseButtonDown(0))
            _attacker.Attack();

        if (Input.GetKeyDown(KeyCode.E))
            PickUpLoot();
    }

    #endregion

    private void Move(InputAction.CallbackContext obj)
    {
        _move = obj.ReadValue<Vector2>();

    }

    private void PickUpLoot()
    {
        print("Try pickup");
        int count = Physics.OverlapSphereNonAlloc(transform.position, _lootingRadius, _loot, 1);

        for (int i = 0; i < count; i++)
        {
            if (_loot[i].TryGetComponent<Loot>(out var loot))
            {
                print("Add loot");
                _inventory.AddLoot(loot.LootData);
                loot.OnPickUp();
            }
        }
    }

    private void Die()
    {
        print("Dead");
    }

    private void OnHeal(object sender, int heal)
    {
        if (_currentHealth < MaxHealth)
            _currentHealth += heal;

        if (_currentHealth > MaxHealth)
            _currentHealth = MaxHealth;

        PlayerHealthChanged?.Invoke(_currentHealth, MaxHealth);
    }

    private void OnTakeDmg(object sender, int damage)
    {

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

        print($"Player Health: {_currentHealth} | Dmg: {damage}");
        PlayerHealthChanged?.Invoke(_currentHealth, MaxHealth);
        _bloodParticles.Play();
    }

    private void OnTrade(int gold)
    {
        _inventory.SellLoot();
        _wallet.AddGold(gold);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, _lootingRadius);
    }

}
