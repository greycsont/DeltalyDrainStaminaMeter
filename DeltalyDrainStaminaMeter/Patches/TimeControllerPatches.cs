using HarmonyLib;
using UnityEngine;
using System;
namespace DeltalyDrainStaminaMeter
{
    [HarmonyPatch(typeof(TimeController), "Update")]
    public static class TimeControllerUpdatePatch
    {
        private static float timer = 0f;

        static void Prefix()
        {
            if (InstanceConfig.DrainStaminaMeter_Enabled.Value != true) return;
            timer += Time.deltaTime;
            if (timer >= InstanceConfig.TimeInterval.Value)
            {
                timer = 0f;
                DrainStamina();
            }
        }

        private static void DrainStamina()
        {
            NewMovement newMovement = UnityEngine.Object.FindObjectOfType<NewMovement>();
            if (newMovement != null)
            {
                newMovement.EmptyStamina();
            }
            else
            {
                Debug.LogError("NewMovement instance not found!");
            }
        }
    }
}