using Ecs.Data;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.System
{
    public class GameInitSystem : IEcsInitSystem
    {
        private readonly EcsWorld _world;

        private readonly UnitInitData _playerInitData;
        private readonly UnitInitData _enemyInitData;
        private readonly Transform _spawnPoint;

        public GameInitSystem(UnitInitData playerData, UnitInitData enemyData, Transform spawmPoint)
        {
            _playerInitData = playerData;
            _enemyInitData = enemyData;
            _spawnPoint = spawmPoint;
        }

        public void Init()
        {
            CreatePlayer();

            CreateEnemy();
        }

        private void CreatePlayer()
        {
            var unitActor = Object.Instantiate(_playerInitData.UnitPrefab, _spawnPoint.position, Quaternion.identity);

            var player = _world.NewEntity();
            player.Get<PlayerComponent>();
            player.Get<InputEventComponent>();

            ref var movableComponent = ref player.Get<MovableComponent>();
            movableComponent.Speed = _playerInitData.DefaultSpeed;
            movableComponent.Transform = unitActor.transform;

            ref var animationsComponent = ref player.Get<AnimatedCharacterComponent>();
            animationsComponent.Animator = unitActor.Animator;

            ref var healthComponent = ref player.Get<HealthComponent>();
            healthComponent.Health = _playerInitData.DefaultHealth;
        }

        private void CreateEnemy( )
        {
            var enemySpawnPosition = _spawnPoint.position + Vector3.one * Random.Range(-15f, 15f);
            enemySpawnPosition.y = 0;

            var unitActor = Object.Instantiate(_enemyInitData.UnitPrefab, enemySpawnPosition, Quaternion.identity);

            var enemy = _world.NewEntity();

            ref var enemyMovableComponent = ref enemy.Get<MovableComponent>();
            enemyMovableComponent.Speed = _enemyInitData.DefaultSpeed;
            enemyMovableComponent.Transform = unitActor.transform;

            ref var enemyAnimationsComponent = ref enemy.Get<AnimatedCharacterComponent>();
            enemyAnimationsComponent.Animator = unitActor.Animator;

            ref var findTargetComponent = ref enemy.Get<FindTargetComponent>();
            findTargetComponent.ScanRadius = _enemyInitData.ScanRadius;

            ref var followComponent = ref enemy.Get<FollowComponent>();

            ref var attackComponent = ref enemy.Get<AttackComponent>();
            attackComponent.Damage = _enemyInitData.Damage;
            attackComponent.AttackDistance = _enemyInitData.AttackDistance;
            attackComponent.AttackDelay = _enemyInitData.AttackDelay;
        } 
    }
}
