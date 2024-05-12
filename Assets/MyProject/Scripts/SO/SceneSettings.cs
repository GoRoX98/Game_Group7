using UnityEngine;

[CreateAssetMenu(fileName = "New SceneSettings", menuName = "ScriptableObjects/Create Scene Settings")]
public class SceneSettings : ScriptableObject
{
    [SerializeField] protected SceneType _sceneType;
    [SerializeField] protected Vector3 _startPosition;

    public SceneType Type => _sceneType;
    public Vector3 StartPosition => _startPosition;
}

public enum SceneType
{
    SafeZone,
    Dungeon
}