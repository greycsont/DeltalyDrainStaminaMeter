using BepInEx.Configuration;

namespace DeltalyDrainStaminaMeter{
    public class InstanceConfig{

        public static ConfigEntry<float> TimeInterval;
        public static ConfigEntry<bool> DrainStaminaMeter_Enabled;

        public static void Initialize(Plugin plugin){
            BindConfigEntryValues(plugin);
        }

        private static void BindConfigEntryValues(Plugin plugin){
            TimeInterval = plugin.Config.Bind(
                "General", 
                "Time_interval", 
                0.5f, 
                "The time interval between two drains(in secs)"
            );

            DrainStaminaMeter_Enabled = plugin.Config.Bind(
                "General", 
                "Drain_Stamina_Meter_Enabled", 
                true, 
                "Set it as true to enable the draining"
            );
        }
    }

}