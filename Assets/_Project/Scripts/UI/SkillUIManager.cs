using UnityEngine;
using UnityEngine.UI;
using Noko.Skills;

namespace Noko.UI
{
    public class SkillUIManager : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private Image[] cooldownOverlays;

        private SkillBaseBehaviour[] _playerSkills;

        private void Start()
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player) _playerSkills = player.GetComponentsInChildren<SkillBaseBehaviour>();
        }

        private void Update()
        {
            if (_playerSkills == null || cooldownOverlays == null) return;

            for (int i = 0; i < cooldownOverlays.Length && i < _playerSkills.Length; i++)
            {
                if (_playerSkills[i].SkillData)
                {
                    float fillRatio = _playerSkills[i].CurrentCooldown / _playerSkills[i].SkillData.cooldown;
                    cooldownOverlays[i].fillAmount = fillRatio;
                }
            }
        }
    }
}