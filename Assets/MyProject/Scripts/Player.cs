using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _speed;

    private Vector3 _input;
    private Camera _camera;

    private void Start()
    {
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

}
