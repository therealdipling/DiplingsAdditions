using System;
using System.Linq;
using System.Collections.Generic;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;

namespace DiplingsAdditions
{
    [BepInPlugin(modGUID, modName, modVersion)]
    [BepInDependency(LethalLib.Plugin.ModGUID)]
    public class DiplingsAdditionsBase : BaseUnityPlugin
    {
        private const string modGUID = "DiplingsAdditions";
        private const string modName = "Dipling's Additions";
        private const string modVersion = "1.0.4";

        private readonly Harmony harmony = new Harmony(modGUID);

        private static DiplingsAdditionsBase Instance;

        internal static ManualLogSource mls;

        internal static List<AudioClip> Musics;
        internal static AssetBundle MusicBundle;

        internal static AssetBundle SpeakAndSpellBundle;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            mls = BepInEx.Logging.Logger.CreateLogSource(modGUID);
            mls.LogInfo("The logger has been created!");

            mls.LogInfo("The mod is about to patch...");
            harmony.PatchAll();
            mls.LogInfo("The mod has patched successfully!");

            string folderLocation = Instance.Info.Location;
            folderLocation = folderLocation.TrimEnd("DiplingsAdditions.dll".ToCharArray());

            mls.LogInfo("The music asset bundle is about to load...");
            Musics = new List<AudioClip>();
            MusicBundle = AssetBundle.LoadFromFile(folderLocation + "levelmusics");
            if (MusicBundle != null)
            {
                Musics = MusicBundle.LoadAllAssets<AudioClip>().ToList();
                mls.LogInfo("Music asset bundle loaded successfully!");
            }
            else
            {
                mls.LogError("Failed to load music asset bundle.");
            }

            mls.LogInfo("The speak and spell asset bundle is about to load...");
            SpeakAndSpellBundle = AssetBundle.LoadFromFile(folderLocation + "speakandspell");
            if (SpeakAndSpellBundle != null)
            {
                int iRarity = 30;
                Item MyCustomItem = SpeakAndSpellBundle.LoadAsset<Item>("directory/to/itemdataasset.asset");
                LethalLib.Modules.NetworkPrefabs.RegisterNetworkPrefab(MyCustomItem.spawnPrefab);
                LethalLib.Modules.Items.RegisterScrap(MyCustomItem, iRarity, LethalLib.Modules.Levels.LevelTypes.All);
                mls.LogInfo("Speak and spell asset bundle loaded successfully!");
            }
            else
            {
                mls.LogError("Failed to load speak and spell asset bundle");
            }
        }
    }
}
