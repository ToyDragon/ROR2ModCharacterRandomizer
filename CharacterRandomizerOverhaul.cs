using BepInEx;
using RoR2;
using UnityEngine;

namespace Frogtown
{
    [BepInDependency(R2API.R2API.PluginGUID,BepInDependency.DependencyFlags.SoftDependency)]
    [BepInPlugin("com.IFixYourRoR2Mods.CharacterRandomizer", "CharacterRandomizer","1.0.0")]
    public class CharacterRandomizerOverhaul : BaseUnityPlugin
    {

        public void Awake()
        {
            On.RoR2.Stage.RespawnCharacter += (orig, instance, characterMaster) =>
            {
                var newIndex = Random.Range(0, SurvivorCatalog.survivorCount-1);
                characterMaster.bodyPrefab = ((SurvivorDef[]) SurvivorCatalog.allSurvivorDefs)[newIndex].bodyPrefab;

                orig(instance, characterMaster);
            };
        }
}
