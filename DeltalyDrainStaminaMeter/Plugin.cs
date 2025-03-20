using BepInEx;
using BepInEx.Bootstrap;
using BepInEx.Logging;
using System;
using System.Reflection;
using HarmonyLib;


namespace DeltalyDrainStaminaMeter
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    [BepInProcess("ULTRAKILL.exe")]
    [BepInDependency(PluginDependencies.PLUGINCONFIGURATOR_GUID, BepInDependency.DependencyFlags.SoftDependency)]
    public class Plugin : BaseUnityPlugin
    {
        private Harmony harmony;
        internal static ManualLogSource Log;
        private void Awake()
        {
            Log = base.Logger;
            LoadMainModule();
            LoadOptionalModule();

            // If you want to modify this mod or focusing on create a new one, just check the Harmony patches only 
            // Other parts is only used for PluginConfigurator support
            PatchHarmony(); 
            Log.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
        }

        private void LoadMainModule(){
            InstanceConfig.Initialize(this);
        }
        private void LoadOptionalModule(){
            CheckPluginLoaded(PluginDependencies.PLUGINCONFIGURATOR_GUID, "DeltalyDrainStaminaMeter.IPluginConfigurator");
        }
        private void PatchHarmony(){
            harmony = new Harmony(PluginInfo.PLUGIN_GUID+".harmony");
            harmony.PatchAll();
        }

        private void CheckPluginLoaded(string GUID, string assemblyName){
            if (!Chainloader.PluginInfos.ContainsKey(GUID)){
                Plugin.Log.LogWarning($"Plugin {GUID} not loaded, stopping loading {assemblyName}"); 
                return;
            }
            try 
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                Type configuratorType = assembly.GetType(assemblyName);
                MethodInfo initialize = configuratorType.GetMethod("Initialize", BindingFlags.Public | BindingFlags.Static);
                initialize?.Invoke(null, null);
            }
            catch (Exception ex)
            {
                Logger.LogError($"Failed to load module: {ex}");
            }
        }
    }
}
