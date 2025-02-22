using System;
using System.Collections;
using HarmonyLib;
using DiplingsAdditions;
using UnityEngine;
using System.Xml.Linq;

internal class Music
{
    internal static bool playing = false;
    internal static IEnumerator PlayLevelMusic(AudioClip music)
    {
        DiplingsAdditionsBase.mls.LogInfo("About to play music...");
        playing = true;
        yield return (object)new WaitForSeconds(0.2f);
        while (MusicPatches.inside)
        {
            HUDManager.Instance.UIAudio.PlayOneShot(music);
            yield return (object)new WaitForSeconds(music.length);
        }
        playing = false;
    }
    internal static IEnumerator PlayLevelMusicWithIntro(AudioClip intro, AudioClip music)
    {
        DiplingsAdditionsBase.mls.LogInfo("About to play music with intro...");
        playing = true;
        yield return (object)new WaitForSeconds(0.2f);
        HUDManager.Instance.UIAudio.PlayOneShot(intro);
        yield return (object)new WaitForSeconds(intro.length);
        while (MusicPatches.inside)
        {
            HUDManager.Instance.UIAudio.PlayOneShot(music);
            yield return (object)new WaitForSeconds(music.length);
        }
        playing = false;
    }
}