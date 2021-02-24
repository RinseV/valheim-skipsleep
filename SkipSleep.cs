using System.Collections.Generic;
using System.Linq;
using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using System.IO;

namespace ModSkipSleepValheim
{
    [BepInPlugin("com.rinsev.skipsleep", "ModSkipSleepValheim", "1.0.0.0")]
    [BepInProcess("valheim.exe")]
    [BepInProcess("valheim_server.exe")]
    [HarmonyPatch]
    public class Mod : BaseUnityPlugin
    {
        private static readonly Harmony harmony = new(typeof(Mod).GetCustomAttributes(typeof(BepInPlugin), false)
            .Cast<BepInPlugin>()
            .First()
            .GUID);

        private static ConfigFile configFile = new ConfigFile(Path.Combine(Paths.ConfigPath, "SkipSleep.cfg"), true);
        private static ConfigEntry<double> ratio = configFile.Bind("General", "ratio", 0.5, "Threshold of ratio of players that need to be sleeping, must be > 0");

        private void Awake()
        {
            harmony.PatchAll();
        }

        private void OnDestroy()
        {
            harmony.UnpatchAll();
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(Game), "EverybodyIsTryingToSleep")]
        static bool SkipSleep(ref Game __instance, ref bool __result)
        {
            // Amount of players in bed
            int count = 0;

            // Get all players
            List<ZDO> allCharacterZdos = ZNet.instance.GetAllCharacterZDOS();
            // Return false if none
            if (allCharacterZdos.Count == 0)
            {
                __result = false;
                return false;
            }

            // Count number of players in bed
            foreach (ZDO zdo in allCharacterZdos)
            {
                if (zdo.GetBool("inBed"))
                    count++;
            }

            double sleepRatio = (double) count / allCharacterZdos.Count;

            //FOR DEBUGGING ONLY
            //UnityEngine.Debug.Log($"Players sleeping: {count}");
            //UnityEngine.Debug.Log($"Ratio of players sleeping: {sleepRatio}");
            //UnityEngine.Debug.Log($"Threshold needed: {ratio.Value}");

            // If the ratio of the amount of players sleeping vs awake reaches the threshold, return true to sleep
            if (sleepRatio >= ratio.Value)
            {
                UnityEngine.Debug.Log($"SkipSleep: Threshold of {ratio.Value} reached, sleeping...");
                __result = true;
                return false;
            }

            // Otherwise, result false
            __result = false;

            return false;
        }
    }
}
