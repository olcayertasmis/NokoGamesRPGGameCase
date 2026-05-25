using UnityEngine;
using Noko.Core;
using Noko.Combat;
using Noko.Systems;
using Noko.Data;

namespace Noko.Enemy
{
    public abstract class EnemyAIBase : CharacterBase
    {
        [Header("Enemy Components")]
        [SerializeField] private HealthSystem healthSystem;
        [SerializeField] private EnemyMovement movement;
        [SerializeField] private EnemyStateMachine stateMachine;
        [SerializeField] private EnemyEffectHandler effectHandler;

        [Header("VFX/SFX")]
        [SerializeField] private GameObject deathVfxPrefab;
        [SerializeField] private AudioClip deathSfxClip;

        protected AreaSpawner mySpawner;
        protected Transform attackerTarget;

        private Vector3 _spawnCenter;
        private float _spawnRadius;
        private Vector3 _roamTargetPosition;
        private float _roamTimer;

        public override bool IsAlive => healthSystem && healthSystem.IsAlive;
        private EnemyData EnemyData => (EnemyData)characterData;

        private static readonly int Hit = Animator.StringToHash("Hit");
        private static readonly int Dead = Animator.StringToHash("Dead");

        protected virtual void OnEnable() => SubscribeEvents();
        protected virtual void OnDisable() => UnsubscribeEvents();

        private void SubscribeEvents()
        {
            if (healthSystem)
            {
                healthSystem.OnDamageTaken += HandleDamageTaken;
                healthSystem.OnDeath += HandleDeath;
            }

            EventBus.OnPlayerSpawned += OnPlayerSpawned;
        }

        private void UnsubscribeEvents()
        {
            if (healthSystem)
            {
                healthSystem.OnDamageTaken -= HandleDamageTaken;
                healthSystem.OnDeath -= HandleDeath;
            }

            EventBus.OnPlayerSpawned -= OnPlayerSpawned;
        }

        private void Update()
        {
            if (!IsAlive) return;

            //if (stateMachine.CurrentState == EnemyState.Triggered) HandleTriggeredBehavior();
            //else HandleRoaming();
            HandleRoaming();
        }

        private void HandleRoaming()
        {
            _roamTimer += Time.deltaTime;

            if (_roamTimer >= EnemyData.roamInterval)
            {
                _roamTimer = 0f;
                SetNewRoamTarget();
            }

            Vector3 direction = (_roamTargetPosition - characterRigidbody.position);
            direction.y = 0f;

            if (direction.sqrMagnitude > 0.1f)
            {
                Vector3 normalizedDir = direction.normalized;
                Move(normalizedDir);
                Rotate(normalizedDir);
                UpdateAnimation(1f);
            }
            else
            {
                UpdateAnimation(0f);
            }
        }

        private void HandleDamageTaken()
        {
            stateMachine.SetTriggered(true);
            effectHandler.PlayHitFlash();

            if (characterAnimator) characterAnimator.SetTrigger(Hit);
        }

        private void OnPlayerSpawned(GameObject player)
        {
            if (player && !attackerTarget) attackerTarget = player.transform;
        }

        private void HandleDeath()
        {
            UpdateAnimation(0f);
            EventBus.TriggerDeathEffect(transform.position);

            if (characterAnimator) characterAnimator.SetTrigger(Dead);

            StartCoroutine(DeathRoutine(2f));
        }

        private System.Collections.IEnumerator DeathRoutine(float delay)
        {
            if (TryGetComponent<Collider>(out var col)) col.enabled = false;

            yield return new WaitForSeconds(delay);

            mySpawner?.ReportEnemyDeath(this);

            if (col) col.enabled = true;
        }

        public void InitializeSpawner(AreaSpawner spawner) => mySpawner = spawner;

        public virtual void ResetState(Vector3 center, float radius)
        {
            stateMachine.SetTriggered(false);
            attackerTarget = null;

            _spawnCenter = center;
            _spawnRadius = radius;
            _roamTimer = EnemyData.roamInterval;

            if (healthSystem) healthSystem.InitializeHealth(EnemyData.maxHealth);

            if (effectHandler && TryGetComponent<MaterialFlash>(out var flash)) flash.ResetFlash();

            SetNewRoamTarget();
        }

        private void SetNewRoamTarget()
        {
            Vector2 randomCircle = Random.insideUnitCircle * _spawnRadius;
            _roamTargetPosition = _spawnCenter + new Vector3(randomCircle.x, 0f, randomCircle.y);
        }

        public void ApplySlow(float multiplier, float duration) => effectHandler.ApplySlow(multiplier, duration, movement);

        protected abstract void HandleTriggeredBehavior();
    }
}