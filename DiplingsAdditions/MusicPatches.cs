using System;
using HarmonyLib;
using DiplingsAdditions;
using UnityEngine;
using BepInEx;
using System.Collections;
using System.Reflection;
using System.Collections.Generic;

[HarmonyPatch]
internal class MusicPatches
{
    static internal bool inside = false;
    static internal AudioClip intro = null;
    static internal AudioClip music = null;
    static internal List<AudioClip> Musics = DiplingsAdditionsBase.SoundFX;

    [HarmonyPatch(typeof(EntranceTeleport), "TeleportPlayer")]
    [HarmonyPrefix]
    private static void EntranceTeleportPreTeleportPlayer(EntranceTeleport __instance)
    {
        if (!inside)
        {
            DiplingsAdditionsBase.mls.LogInfo("About to enter dungeon...");
            inside = true;
            if (!Music.playing)
            {
                switch (RoundManager.Instance.currentDungeonType)
                {
                    case 0:
                        DiplingsAdditionsBase.mls.LogInfo("Dungeon type is Factory.");
                        break;
                    case 1:
                        DiplingsAdditionsBase.mls.LogInfo("Dungeon type is Manor.");
                        intro = Musics[0];
                        music = Musics[1];
                        break;
                    case 2:
                        DiplingsAdditionsBase.mls.LogInfo("Dungeon type is Mineshaft.");
                        break;
                    default:
                        DiplingsAdditionsBase.mls.LogError("Unknown dungeon type. No music can play.");
                        break;
                }
                if (music != null)
                {
                    if (intro != null)
                    {
                        ((MonoBehaviour)(object)__instance).StartCoroutine(Music.PlayLevelMusicWithIntro(intro, music));
                    }
                }
            }
        }
        else
        {
            DiplingsAdditionsBase.mls.LogInfo("About to exit dungeon...");
            inside = false;
            intro = null;
            music = null;
        }
    }

    [HarmonyPatch(typeof(StartOfRound), "SetShipReadyToLand")]
    [HarmonyPostfix]
    private static void StartOfRoundPostSetShipReadyToLand()
    {
        DiplingsAdditionsBase.mls.LogInfo("Reset inside bool.");
        inside = false;
    }

    [HarmonyPatch(typeof(StartOfRound), "Awake")]
    [HarmonyPostfix]
    private static void StartOfRoundPostAwake()
    {
        DiplingsAdditionsBase.mls.LogInfo("Reset inside bool.");
        inside = false;
    }
}