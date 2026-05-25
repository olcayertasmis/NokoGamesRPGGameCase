using System;
using UnityEngine;
using Noko.Interfaces;
using Noko.Systems;

namespace Noko.Combat
{
    public class HealthSystem : MonoBehaviour, IDamageable
    {
        private float _currentHealth;
        private float _maxHealth;

        private MaterialFlash _materialFlash;

        public bool IsAlive { get; private set; }

        public float CurrentHealth => _currentHealth;
        public float MaxHealth => _maxHealth;

        public event Action<float, float> OnHealthChanged;
        public event Action<float, GameObject> OnDamageTakenWithInfo;
        public event Action OnDamageTaken;
        public event Action OnDeath;

        private void Awake()
        {
            _materialFlash = GetComponent<MaterialFlash>();
        }

        public void InitializeHealth(float maxHealth)
        {
            _maxHealth = maxHealth;
            _currentHealth = _maxHealth;
            IsAlive = true;
            OnHealthChanged?.Invoke(_currentHealth, _maxHealth);
        }

        public void TakeDamage(float amount, GameObject source = null)
        {
            if (!IsAlive) return;

            _currentHealth -= amount;
            ClampHealth();

            OnDamageTaken?.Invoke();
            OnDamageTakenWithInfo?.Invoke(amount, source);
            OnHealthChanged?.Invoke(_currentHealth, _maxHealth);

            EventBus.TriggerDamageTaken(gameObject, amount);
            if (source) EventBus.TriggerDamageDealt(gameObject, amount, source);

            if (_materialFlash) _materialFlash.TriggerFlash();

            if (_currentHealth <= 0f)
            {
                Die();
            }
        }

        public void Heal(float amount)
        {
            if (!IsAlive) return;

            _currentHealth += amount;
            ClampHealth();

            OnHealthChanged?.Invoke(_currentHealth, _maxHealth);
        }

        private void Die()
        {
            IsAlive = false;
            OnDeath?.Invoke();
            EventBus.TriggerEntityDied(gameObject);
        }

        public void IncreaseMaxHealth(float amount)
        {
            _maxHealth += amount;
            _currentHealth += amount;
            _currentHealth = Mathf.Min(_currentHealth, _maxHealth);
            OnHealthChanged?.Invoke(_currentHealth, _maxHealth);
        }

        private void ClampHealth() => _currentHealth = Mathf.Clamp(_currentHealth, 0f, _maxHealth);

        public void AddTemporaryMaxHealth(float bonusAmount, float duration)
        {
            StartCoroutine(TemporaryMaxHealthRoutine(bonusAmount, duration));
        }

        private System.Collections.IEnumerator TemporaryMaxHealthRoutine(float bonusAmount, float duration)
        {
            _maxHealth += bonusAmount;
            Heal(bonusAmount);

            yield return new WaitForSeconds(duration);

            _maxHealth -= bonusAmount;
            if (_currentHealth > _maxHealth) _currentHealth = _maxHealth;
            OnHealthChanged?.Invoke(_currentHealth, _maxHealth);
        }
    }
}