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
        private const string modVersion = "1.0.3";

        private readonly Harmony harmony = new Harmony(modGUID);

        private static DiplingsAdditionsBase Instance;

        internal ManualLogSource mls;

        internal static List<AudioClip> SoundFX;
        internal static AssetBundle Bundle;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            mls = BepInEx.Logging.Logger.CreateLogSource(modGUID);

            mls.LogInfo("Dipling's Additions is about to patch...");

            harmony.PatchAll();

            mls.LogInfo("Dipling's Additions has patched successfully!");


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
