using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static Transform _playerTransform;
    public static Vector3 PlayerPos => _playerTransform.position;
    public static Transform PlayerTransform => _playerTransform;

    void Awake()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }
}
