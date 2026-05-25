using UnityEngine;
using Noko.Interfaces;
using Noko.Data;
using Noko.Enemy;

namespace Noko.Skills.Mage
{
    public class FreezeSkillBehaviour : SkillBaseBehaviour
    {
        private FreezeSkillData FreezeData => skillData as FreezeSkillData;
        private Collider[] _hitResults = new Collider[30];
        private bool _isCharging;

        public override bool CanExecute(Vector3 casterPosition, Transform target)
        {
            return currentCooldown <= 0 && target != null;
        }

        public override void Execute(Transform caster, Transform target)
        {
            if (_isCharging) return;
            _isCharging = true;

            TriggerCooldown();
            ApplyVFX(caster.position);
            PlaySFX(caster.position);

            int hitCount = Physics.OverlapSphereNonAlloc(caster.position, FreezeData.areaOfEffectRadius, _hitResults);

            for (int i = 0; i < hitCount; i++)
            {
                if (_hitResults[i].TryGetComponent(out EnemyAIBase enemy) && enemy.IsAlive)
                {
                    enemy.ApplySlow(FreezeData.slowMultiplier, FreezeData.slowDuration);
                    if (enemy.TryGetComponent(out Systems.MaterialFlash flash)) flash.TriggerFlash();
                }
            }
            
            _isCharging = false;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, FreezeData.areaOfEffectRadius);
        }
    }
}