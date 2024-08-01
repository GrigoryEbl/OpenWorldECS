using UnityEngine;

namespace Ecs.Data
{
    [CreateAssetMenu]
    public class UnitInitData : ScriptableObject
    {
        [field: SerializeField] public UnitActor UnitPrefab {  get; private set; }
        [field: SerializeField] public float DefaultSpeed { get; private set; } = 1.5f;
        [field: SerializeField] public int DefaultHealth { get; private set; } = 10;
        [field: SerializeField] public float ScanRadius { get; private set; } = 10;

        [field: SerializeField] public int Damage { get; private set; } = 2;
        [field: SerializeField] public float AttackDistance { get; private set; } = 2.1f;
        [field: SerializeField] public float AttackDelay { get; private set; } = 1f;
    }
}
