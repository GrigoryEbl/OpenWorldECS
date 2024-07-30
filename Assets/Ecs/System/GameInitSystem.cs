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
            var unitActor = Object.Instantiate(_playerInitData.UnitPrefab, _spawnPoint.position, Quaternion.identity);

            var player = _world.NewEntity();
            player.Get<PlayerComponent>();
            player.Get<InputEventComponent>();

            ref var movableComponent = ref player.Get<MovableComponent>();
            movableComponent.Speed = _playerInitData.DefaultSpeed;
            movableComponent.Transform = unitActor.transform;

            ref var animationsComponent = ref player.Get<AnimatedCharacterComponent>();
            animationsComponent.Animator = unitActor.Animator;

            var enemySpawnPosition = _spawnPoint.position + Vector3.one * Random.Range(-2f, 2f);
            enemySpawnPosition.y = 0;

            CreateEnemy(enemySpawnPosition, unitActor.transform);
        }

        private void CreateEnemy(Vector3 atPosition, Transform target)
        {
            var unitActor = Object.Instantiate(_enemyInitData.UnitPrefab, atPosition, Quaternion.identity);

            var enemy = _world.NewEntity();

            ref var enemyMovableComponent = ref enemy.Get<MovableComponent>();
            enemyMovableComponent.Speed = _enemyInitData.DefaultSpeed;
            enemyMovableComponent.Transform = unitActor.transform;

            ref var enemyAnimationsComponent = ref enemy.Get<AnimatedCharacterComponent>();
            enemyAnimationsComponent.Animator = unitActor.Animator;

            ref var followComponent = ref enemy.Get<FollowComponent>();
            followComponent.Target = target;
        }
    }
}
