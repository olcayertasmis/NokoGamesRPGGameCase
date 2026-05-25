using System.Collections.Generic;
using UnityEngine;
using Noko.Interfaces;
using Noko.Data;
using Noko.Skills;
using Noko.Systems;

namespace Noko.Combat
{
    public class SkillManager : MonoBehaviour
    {
        private ITargetProvider _targetProvider;

        private List<ISkill> _skills = new();
        private IInputHandler _inputHandler;
        private Transform _playerTransform;

        private Dictionary<ISkill, int> _skillIndexMap;

        private Animator _animator;
        private static readonly int AttackTrigger = Animator.StringToHash("Attack");

        private void Awake()
        {
            _targetProvider = GetComponentInChildren<ITargetProvider>();

            _playerTransform = transform;
            _inputHandler = GetComponent<IInputHandler>();

            _animator = GetComponentInChildren<Animator>();

            _skillIndexMap = new Dictionary<ISkill, int>();
            var skillBehaviours = GetComponents<SkillBaseBehaviour>();
            for (int i = 0; i < skillBehaviours.Length; i++)
            {
                _skills.Add(skillBehaviours[i]);
                _skillIndexMap[skillBehaviours[i]] = i;
            }
        }

        private void Update()
        {
            ProcessSkills();
        }

        private void ProcessSkills()
        {
            Transform closestTarget = _targetProvider?.GetClosestTarget(_playerTransform.position);

            foreach (var skill in _skills)
            {
                skill.UpdateCooldown(Time.deltaTime);
                bool canExecute = skill.CanExecute(_playerTransform.position, closestTarget);

                if (canExecute)
                {
                    if (skill.SkillData.activationType == SkillActivationType.Manual && _inputHandler != null)
                    {
                        int index = _skillIndexMap[skill];
                        if (_inputHandler.GetSkillTrigger(index))
                        {
                            ExecuteSkill(skill, closestTarget, index);
                        }
                    }
                    else
                    {
                        ExecuteSkill(skill, closestTarget, _skills.IndexOf(skill));
                    }
                }
            }
        }

        private void ExecuteSkill(ISkill skill, Transform target, int index)
        {
            if (_animator) _animator.SetTrigger(AttackTrigger);

            skill.Execute(_playerTransform, target);
            EventBus.TriggerSkillExecuted(index);
        }
    }
}