using UnityEngine;
using Noko.Interfaces;

namespace Noko.Input
{
    public class JoystickPackInputHandler : MonoBehaviour, IInputHandler
    {
        private FloatingJoystick _movementJoystick;

        private void Awake()
        {
            _movementJoystick = FindFirstObjectByType<FloatingJoystick>();

            if (!_movementJoystick) Debug.LogWarning("Movement Joystick null!");
        }

        public Vector3 GetMovementInput()
        {
            if (!_movementJoystick) return Vector3.zero;

            float horizontal = _movementJoystick.Horizontal;
            float vertical = _movementJoystick.Vertical;

            return new Vector3(horizontal, 0f, vertical).normalized;
        }

        public bool GetSkillTrigger(int skillIndex) => false;

        public bool GetUpgradeToggle() => false;
    }
}