using BepInEx;
using RoR2;

namespace Frogtown
{
    [BepInDependency("com.frogtown.shared")]
    [BepInPlugin("com.frogtown.characterrandomizer", "Character Randomizer", "1.0.3")]
    public class CharacterRandomizerOverhaul : BaseUnityPlugin
    {
        public FrogtownModDetails modDetails;

        public void Awake()
        {
            modDetails = new FrogtownModDetails("com.frogtown.characterrandomizer")
            {
                description = "Switches the characters of everyone in the party randomly every stage.",
                githubAuthor = "ToyDragon",
                githubRepo = "ROR2ModCharacterRandomizer",
            };
            FrogtownShared.RegisterMod(modDetails);

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