using UnityEngine;

namespace Noko.Player
{
    [RequireComponent(typeof(LootCollector), typeof(TargetDetector))]
    public class PlayerCombatArea : MonoBehaviour
    {
        private TargetDetector _detector;
        private LootCollector _collector;

        public TargetDetector Detector => _detector ? _detector : (_detector = GetComponent<TargetDetector>());
        public LootCollector Collector => _collector ? _collector : (_collector = GetComponent<LootCollector>());
    }
}