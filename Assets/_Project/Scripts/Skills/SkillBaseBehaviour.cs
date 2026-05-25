using UnityEngine;
using Noko.Interfaces;
using Noko.Data;

namespace Noko.Skills
{
    public abstract class SkillBaseBehaviour : MonoBehaviour, ISkill
    {
        [SerializeField] protected SkillData skillData;
        protected float currentCooldown;
        private ISkillActivationStrategy _activationStrategy;

        public SkillData SkillData => skillData;
        public float CurrentCooldown => currentCooldown;

        protected virtual void Awake()
        {
            _activationStrategy = SkillActivationStrategyFactory.Create(skillData.activationType);
        }

        protected virtual void Start()
        {
        }

        public virtual bool CanExecute(Vector3 casterPosition, Transform target)
        {
            if (!skillData) return false;
            return _activationStrategy.CanExecute(skillData, currentCooldown, casterPosition, target);
        }

        public abstract void Execute(Transform caster, Transform target);

        public void ResetCooldown() => currentCooldown = 0f;

        public void UpdateCooldown(float deltaTime)
        {
            if (currentCooldown > 0) currentCooldown -= deltaTime;
        }

        protected virtual void ApplyVFX(Vector3 position)
        {
            if (skillData?.vfxPrefab) Instantiate(skillData.vfxPrefab, position, Quaternion.identity);
        }

        protected virtual void PlaySFX(Vector3 position)
        {
            if (skillData?.sfxClip) AudioSource.PlayClipAtPoint(skillData.sfxClip, position);
        }

        protected void TriggerCooldown()
        {
            if (skillData) currentCooldown = skillData.cooldown;
        }
    }
}