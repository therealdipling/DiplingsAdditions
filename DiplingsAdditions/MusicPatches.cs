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

    [HarmonyPatch(typeof(EntranceTeleport), "TeleportPlayer")]
    [HarmonyPrefix]
    private static void EntranceTeleportPreTeleportPlayer(EntranceTeleport __instance)
    {
        AudioClip intro;
        AudioClip music;
        List<AudioClip> Musics = DiplingsAdditionsBase.SoundFX;
        if (!inside)
        {
            if (!Music.playing)
            {
                switch (RoundManager.Instance.currentDungeonType)
                {
                    case 1:
                        inside = true;
                        intro = Musics[0];
                        music = Musics[1];
                        ((MonoBehaviour)(object)__instance).StartCoroutine(Music.PlayLevelMusic(intro, music));
                        break;
                }
            }
        }
        else
        {
            inside = false;
            HUDManager.Instance.UIAudio.Stop();
        }
    }

    [HarmonyPatch(typeof(StartOfRound), "SetShipReadyToLand")]
    [HarmonyPostfix]
    private static void StartOfRoundPostSetShipReadyToLand()
    {
        inside = false;
    }

    [HarmonyPatch(typeof(StartOfRound), "Awake")]
    [HarmonyPostfix]
    private static void StartOfRoundPostAwake()
    {
        inside = false;
    }
}