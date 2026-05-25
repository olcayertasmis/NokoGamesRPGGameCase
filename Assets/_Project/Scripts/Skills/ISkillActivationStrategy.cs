using UnityEngine;
using Noko.Data;

namespace Noko.Skills
{
    public interface ISkillActivationStrategy
    {
        bool CanExecute(SkillData data, float currentCooldown, Vector3 casterPosition, Transform target);
    }

    public class AutoTimerStrategy : ISkillActivationStrategy
    {
        public bool CanExecute(SkillData data, float currentCooldown, Vector3 casterPosition, Transform target) => currentCooldown <= 0 && target;
    }

    public class ProximityStrategy : ISkillActivationStrategy
    {
        public bool CanExecute(SkillData data, float currentCooldown, Vector3 casterPosition, Transform target)
        {
            if (currentCooldown > 0 || !target) return false;
            return Vector3.Distance(casterPosition, target.position) <= data.range;
        }
    }

    public class PassiveStrategy : ISkillActivationStrategy
    {
        public bool CanExecute(SkillData data, float currentCooldown, Vector3 casterPosition, Transform target) => currentCooldown <= 0;
    }

    public class ManualStrategy : ISkillActivationStrategy
    {
        public bool CanExecute(SkillData data, float currentCooldown, Vector3 casterPosition, Transform target) => currentCooldown <= 0 && target;
    }

    public static class SkillActivationStrategyFactory
    {
        public static ISkillActivationStrategy Create(SkillActivationType type)
        {
            return type switch
            {
                SkillActivationType.AutoTimer => new AutoTimerStrategy(),
                SkillActivationType.Proximity => new ProximityStrategy(),
                SkillActivationType.Passive => new PassiveStrategy(),
                SkillActivationType.Manual => new ManualStrategy(),
                _ => new ManualStrategy()
            };
        }
    }
}