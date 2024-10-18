#if UNITY_IOS
using DeadMosquito.IosGoodies;
#else
using DeadMosquito.AndroidGoodies;
#endif

using UnityEngine;

public static class Vibration
{
    public static bool Enabled = true;

    private const int MillisecondsInSeconds = 1000;
    private const float DefaultDuration = 0.09f;

    public static void Vibrate()
    {
        if (!Enabled) return;

#if UNITY_IOS
        if (!IGHapticFeedback.IsHapticFeedbackSupported) return;
        IGHapticFeedback.SelectionChanged();
#else
        if (!AGVibrator.HasVibrator()) return;
        AGVibrator.Vibrate((int)(DefaultDuration * MillisecondsInSeconds));
#endif
    }

    private static void ManifestBuild()
    {
        Handheld.Vibrate();
    }
}