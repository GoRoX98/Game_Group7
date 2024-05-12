using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class PlayerCamera : MonoBehaviour
{
    private CinemachineVirtualCamera _camera;

    private void Awake()
    {
        _camera = GetComponent<CinemachineVirtualCamera>();
    }

    private void Start()
    {
        _camera.Follow = SceneController.PlayerTransform;
    }
}
