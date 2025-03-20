using BepInEx.Configuration;
using greycsont.GreyAnnouncer;
using PluginConfig.API;
using PluginConfig.API.Fields;
using PluginConfig.API.Functionals;

namespace DeltalyDrainStaminaMeter{
    public class IPluginConfigurator{
        private static PluginConfigurator config;

        public static void Initialize(){
            config = PluginConfigurator.Create(PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_GUID);
            config.SetIconWithURL(PathManager.GetCurrentPluginPath("icon.png"));
            MainPanel();
        }

        private static void MainPanel(){
            BoolField DrainStaminaMeter = BoolFieldFactory(config.rootPanel, "Drain stamina meter", "drainStaminaMeter",InstanceConfig.DrainStaminaMeter_Enabled, true);
            FloatField TimeInterval = new FloatField(config.rootPanel, "Time interval", "timeInterval", InstanceConfig.TimeInterval.Value);
            TimeInterval.defaultValue = 0.5f;
            TimeInterval.minimumValue = 0f;
            TimeInterval.maximumValue = 1113f;
            TimeInterval.onValueChange += (FloatField.FloatValueChangeEvent e) =>
            {
                InstanceConfig.TimeInterval.Value = e.value;
            };
        }

        private static BoolField BoolFieldFactory(ConfigPanel parentPanel,string name,string GUID,ConfigEntry<bool> configEntry,bool defaultValue){
            BoolField boolField = new BoolField(parentPanel, name, GUID, configEntry.Value);
            boolField.onValueChange += (BoolField.BoolValueChangeEvent e) =>
            {
                configEntry.Value = e.value;
            };
            boolField.defaultValue = defaultValue;
            return boolField;
        }
    }

}