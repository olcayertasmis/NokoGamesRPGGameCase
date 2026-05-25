using UnityEngine;
using Noko.Data;

namespace Noko.Interfaces
{
    public interface ISkill
    {
        SkillData SkillData { get; }
        bool CanExecute(Vector3 casterPosition, Transform target);
        void Execute(Transform caster, Transform target);
        void ResetCooldown();
        void UpdateCooldown(float deltaTime);
    }
}