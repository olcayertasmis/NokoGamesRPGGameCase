using UnityEngine;

namespace Noko.Interfaces
{
    public interface IInputHandler
    {
        Vector3 GetMovementInput();
        bool GetSkillTrigger(int skillIndex);
        bool GetUpgradeToggle();
    }
}