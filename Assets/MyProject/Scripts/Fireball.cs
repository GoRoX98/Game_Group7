using UnityEngine;

[CreateAssetMenu(fileName = "Fireball", menuName = "Configs/Spell/Fireball", order = 0)]
public class Fireball : Spell
{
    public LayerMask LayerMask;
    public GameObject Prefab;
    public float Speed;
    public float Radius;

    private GameObject _instance;
    private Vector3 _startPos;
    private Vector3 _destination;

    public override void Cast(Vector3 startPos, Vector3 point)
    {
        _startPos = startPos;
        _destination = point;
        _instance = Instantiate(Prefab, _startPos, Quaternion.identity);
        _instance.transform.LookAt(point);
        Completed = false;
    }

    public override void Process()
    {
        if (Completed) return;

        var direction = _destination - _instance.transform.position;

        _instance.transform.Translate(direction * Time.deltaTime * Speed);

        if (Vector3.Distance(_instance.transform.position, _destination) < 0.2f)
        {

            var hits = Physics.OverlapSphere(_instance.transform.position, Radius, LayerMask);
            foreach (var hit in hits)
            {
                if (hit.TryGetComponent<IDamageble>(out var health))
                {
                    health.TakeDamage(this, (int)Damage);
                }
            }

            Completed = true;
            Destroy(_instance);
        }

    }
}
