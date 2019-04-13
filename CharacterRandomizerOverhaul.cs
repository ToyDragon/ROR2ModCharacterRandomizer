using BepInEx;
using RoR2;

namespace Frogtown
{
    [BepInDependency("com.frogtown.shared")]
    [BepInDependency("com.frogtown.healinghelper", BepInDependency.DependencyFlags.SoftDependency)] //Make sure this respawn postfix runs after the healing helper postfix
    [BepInPlugin("com.frogtown.characterrandomizer", "Character Randomizer", "1.0")]
    public class CharacterRandomizerOverhaul : BaseUnityPlugin
    {
        public ModDetails modDetails;

        public void Awake()
        {
            modDetails = new ModDetails("com.frogtown.characterrandomizer");

            On.RoR2.Stage.RespawnCharacter += (orig, instance, characterMaster) =>
            {
                StageRespawnCharacterPrefix(characterMaster);
                orig(instance, characterMaster);
            };
        }

        private void StageRespawnCharacterPrefix(CharacterMaster characterMaster)
        {
            if (!modDetails.enabled)
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