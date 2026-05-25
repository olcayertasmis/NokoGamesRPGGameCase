using Noko.Data;

namespace Noko.Interfaces
{
    public interface IUpgradeProvider
    {
        float GetUpgradeValue(UpgradeType type);
        event System.Action<UpgradeData, int> OnUpgradeLeveledUp;
    }
}