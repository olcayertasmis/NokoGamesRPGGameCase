using UnityEngine;

namespace Noko.Data
{
    [CreateAssetMenu(fileName = "NewFreezeSkill", menuName = "Noko/Data/Skills/Freeze")]
    public class FreezeSkillData : SkillData
    {
        public float slowMultiplier;
        public float slowDuration;
    }
}