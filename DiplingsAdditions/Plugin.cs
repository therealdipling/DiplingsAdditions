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
    public class DiplingsAdditionsBase : BaseUnityPlugin
    {
        private const string modGUID = "DiplingsAdditions";
        private const string modName = "Dipling's Additions";
        private const string modVersion = "1.0.4";

        private readonly Harmony harmony = new Harmony(modGUID);

        private static DiplingsAdditionsBase Instance;

        internal static ManualLogSource mls;

        internal static List<AudioClip> SoundFX;
        internal static AssetBundle Bundle;

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

            mls.LogInfo("The asset bundle is about to load...");
            SoundFX = new List<AudioClip>();
            string folderLocation = Instance.Info.Location;
            folderLocation = folderLocation.TrimEnd("DiplingsAdditions.dll".ToCharArray());
            Bundle = AssetBundle.LoadFromFile(folderLocation + "levelmusics");
            if (Bundle != null)
            {
                SoundFX = Bundle.LoadAllAssets<AudioClip>().ToList();
                mls.LogInfo("Asset bundle loaded successfully!");
            }
            else
            {
                mls.LogError("Failed to load asset bundle.");
            }
        }
    }
}
