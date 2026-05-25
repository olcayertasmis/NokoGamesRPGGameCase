using UnityEngine;

namespace Noko.Systems
{
    public static class HapticFeedback
    {
        public static void Light() => DoHaptic(HapticType.Light);
        public static void Medium() => DoHaptic(HapticType.Medium);
        public static void Heavy() => DoHaptic(HapticType.Heavy);

        private enum HapticType
        {
            Light,
            Medium,
            Heavy
        }

        private static void DoHaptic(HapticType type)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            using (var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            using (var currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity"))
            using (var vibrator = currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator"))
            {
                long duration = type == HapticType.Heavy ? 50 : (type == HapticType.Medium ? 30 : 15);
                vibrator.Call("vibrate", duration);
            }
#elif UNITY_IOS && !UNITY_EDITOR
#endif
        }
    }
}