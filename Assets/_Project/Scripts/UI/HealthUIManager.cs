using UnityEngine;
using UnityEngine.UI;
using Noko.Combat;

namespace Noko.UI
{
    public class HealthUIManager : MonoBehaviour
    {
        [Header("Bar References")]
        [SerializeField] private Image healthBar;
        [SerializeField] private Image shieldBar;

        private HealthSystem _healthSystem;

        private void OnEnable()
        {
            _healthSystem = GetComponentInParent<HealthSystem>();

            if (_healthSystem)
            {
                _healthSystem.OnHealthChanged += UpdateHealthBars;
                UpdateHealthBars(_healthSystem.CurrentHealth, _healthSystem.MaxHealth);
            }
        }

        private void OnDisable()
        {
            if (_healthSystem) _healthSystem.OnHealthChanged -= UpdateHealthBars;
        }

        private void UpdateHealthBars(float currentHealth, float maxHealth)
        {
            float fillRatio = currentHealth / maxHealth;

            if (shieldBar)
            {
                bool hasShield = maxHealth > _healthSystem.MaxHealth;
                if (healthBar) healthBar.fillAmount = fillRatio;
            }
        }
    }
}