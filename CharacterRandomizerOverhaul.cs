using Harmony;
using RoR2;
using System;
using System.Reflection;
using UnityModManagerNet;

namespace Frogtown
{
    public class CharacterRandomizerOverhaul
    {
        public static bool enabled;
        public static UnityModManager.ModEntry modEntry;

        static bool Load(UnityModManager.ModEntry modEntry)
        {
            var harmony = HarmonyInstance.Create("com.frog.characterrandomizer");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
            CharacterRandomizerOverhaul.modEntry = modEntry;
            enabled = true;
            modEntry.OnToggle = OnToggle;
            return true;
        }

        static bool OnToggle(UnityModManager.ModEntry modEntry, bool value)
        {
            enabled = value;
            FrogtownShared.ModToggled(value);
            return true;
        }
    }

    /// <summary>
    /// Restores the original prefab on stage change.
    /// </summary>
    [HarmonyPatch(typeof(RoR2.Stage))]
    [HarmonyPatch("RespawnCharacter")]
    [HarmonyPatch(new Type[] { typeof(CharacterMaster) })]
    class StagePatch
    {
        static void Prefix(CharacterMaster characterMaster)
        {
            if (!CharacterRandomizerOverhaul.enabled)
            {
                return;
            }

            string[] prefabNames = new string[]{
                "CommandoBody",
                "ToolbotBody",
                "HuntressBody",
                "EngiBody",
                "MageBody",
                "MercBody"
            };

            string prefabName = prefabNames[UnityEngine.Random.Range(0, prefabNames.Length)];
            characterMaster.bodyPrefab = BodyCatalog.FindBodyPrefab(prefabName);
        }
    }
}