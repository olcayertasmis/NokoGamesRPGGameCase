using System.Collections;
using UnityEngine;
using Noko.Interfaces;
using Noko.Data;
using Noko.Systems;

namespace Noko.Skills.Mage
{
    public class FireballSkillBehaviour : SkillBaseBehaviour
    {
        private FireballSkillData FireballData => skillData as FireballSkillData;
        private Collider[] _hitResults = new Collider[30];
        private bool _isCharging;

        private IUpgradeProvider _upgradeProvider;

        protected override void Awake()
        {
            base.Awake();
            _upgradeProvider = FindFirstObjectByType<UpgradeManager>();
        }

        public override bool CanExecute(Vector3 casterPosition, Transform target)
        {
            return currentCooldown <= 0 && target && !_isCharging;
        }

        public override void Execute(Transform caster, Transform target)
        {
            StartCoroutine(DelayedFireballRoutine(caster));
        }

        private IEnumerator DelayedFireballRoutine(Transform caster)
        {
            _isCharging = true;
            TriggerCooldown();
            yield return new WaitForSeconds(0.4f);

            float damageBonus = _upgradeProvider != null ? _upgradeProvider.GetUpgradeValue(UpgradeType.Damage) : 0f;
            float finalDamage = FireballData.baseDamage + damageBonus;

            int hitCount = Physics.OverlapSphereNonAlloc(caster.position, FireballData.range, _hitResults);
            if (hitCount <= 0)
            {
                _isCharging = false;
                yield break;
            }

            PlaySFX(caster.position);
            for (int i = 0; i < hitCount; i++)
            {
                if (_hitResults[i].TryGetComponent<IDamageable>(out var damageable) && damageable.IsAlive)
                {
                    Vector3 enemyPos = _hitResults[i].transform.position;
                    Vector3 skyPosition = enemyPos + Vector3.up * 15f;
                    Quaternion lookDown = Quaternion.Euler(90f, 0f, 0f);

                    if (FireballData.vfxPrefab) Instantiate(FireballData.vfxPrefab, skyPosition, lookDown);

                    StartCoroutine(ApplyDamageAfterFall(damageable, caster.gameObject, finalDamage, 0.9f));
                }
            }

            _isCharging = false;
        }

        private IEnumerator ApplyDamageAfterFall(IDamageable target, GameObject caster, float damage, float fallDuration)
        {
            yield return new WaitForSeconds(fallDuration);
            if (target != null && target.IsAlive)
            {
                target.TakeDamage(damage, caster);
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, FireballData.range);
        }
    }
}