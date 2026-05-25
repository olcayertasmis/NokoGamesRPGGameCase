using UnityEngine;

namespace Noko.Data
{
    public abstract class SkillData : ScriptableObject
    {
        [Header("Activation")]
        public SkillActivationType activationType;

        [Header("Combat Stats")]
        public float cooldown;
        public float baseDamage;
        public float range;
        public float areaOfEffectRadius;

        [Header("Game Feel")]
        public GameObject vfxPrefab;
        public AudioClip sfxClip;
    }
}