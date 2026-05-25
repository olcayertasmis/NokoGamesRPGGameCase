using UnityEngine;
using System.Collections;
using Noko.Combat;
using Noko.Data;
using Noko.Systems;

namespace Noko.Skills.Mage
{
    public class ArcaneShieldSkillBehaviour : SkillBaseBehaviour
    {
        private ArcaneShieldSkillData ShieldData => skillData as ArcaneShieldSkillData;
        private HealthSystem _healthSystem;
        private GameObject _activeShieldVfx;

        protected override void Start()
        {
            base.Start();
            _healthSystem = GetComponent<HealthSystem>();
        }

        public override bool CanExecute(Vector3 casterPosition, Transform target)
        {
            if (!ShieldData) return false;
            if (currentCooldown > 0) return false;
            if (!_healthSystem) return false;

            return currentCooldown <= 0 && target && _healthSystem;
        }

        public override void Execute(Transform caster, Transform target)
        {
            if (!_healthSystem || !ShieldData) return;

            PlaySFX(caster.position);
            HapticFeedback.Medium();

            //_healthSystem.Heal(ShieldData.healAmount);

            if (ShieldData.vfxPrefab)
            {
                _activeShieldVfx = Instantiate(ShieldData.vfxPrefab, caster.position, Quaternion.identity, caster);

                StartCoroutine(DestroyShieldRoutine(ShieldData.cooldown * 0.3f));
            }

            _healthSystem.AddTemporaryMaxHealth(ShieldData.healAmount, 5f);

            TriggerCooldown();
        }

        private IEnumerator DestroyShieldRoutine(float duration)
        {
            yield return new WaitForSeconds(duration);
            if (_activeShieldVfx)
            {
                Destroy(_activeShieldVfx);
                _activeShieldVfx = null;
            }
        }
    }
}