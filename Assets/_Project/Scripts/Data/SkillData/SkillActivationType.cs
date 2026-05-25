namespace Noko.Data
{
    public enum SkillActivationType
    {
        AutoTimer,      // Automatically triggered every cooldown when conditions met
        Proximity,      // Triggered when enemy enters a certain range
        Passive,        // Always active or triggers on specific events
        Manual          // Requires player input (button press)
    }
}