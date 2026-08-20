using System;
using HarmonyLib;
using UnityEngine;

namespace WhichMap.Patches
{
    [HarmonyPatch(typeof(GorillaPlayerScoreboardLine), nameof(GorillaPlayerScoreboardLine.UpdatePlayerText))]
    public class PatchPlayerLine
    {
        static void Postfix(GorillaPlayerScoreboardLine __instance)
        {
            if (!__instance || !__instance.playerName || string.IsNullOrEmpty(__instance.playerNameVisible)) 
                return;

            __instance.playerName.text = __instance.playerNameVisible + MapNames.GetFinalMapTag(PatchScoreBoard.FindRig(__instance));
        }
    }
}