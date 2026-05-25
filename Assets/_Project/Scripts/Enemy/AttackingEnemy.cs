using UnityEngine;
using Noko.Data;
using Noko.Interfaces;

namespace Noko.Enemy.EnemyTypes
{
    public class AttackingEnemy : EnemyAIBase
    {
        private AttackingEnemyData AttackData => (AttackingEnemyData)characterData;
        private float _nextAttackTime;

        protected override void HandleTriggeredBehavior()
        {
            if (!attackerTarget) return;

            float distance = Vector3.Distance(characterRigidbody.position, attackerTarget.position);

            if (distance > AttackData.attackRange)
            {
                Vector3 moveDirection = (attackerTarget.position - characterRigidbody.position).normalized;
                moveDirection.y = 0f;
                Move(moveDirection, AttackData.moveSpeed);
                Rotate(moveDirection);
                UpdateAnimation(1f);
            }
            else
            {
                UpdateAnimation(0f);
                TryAttack();
            }
        }

        private void TryAttack()
        {
            if (Time.time < _nextAttackTime) return;

            _nextAttackTime = Time.time + AttackData.attackCooldown;

            if (attackerTarget.TryGetComponent<IDamageable>(out var damageable))
            {
                damageable.TakeDamage(AttackData.attackDamage);
            }
        }
    }
}