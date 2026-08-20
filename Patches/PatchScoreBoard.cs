using System.Collections.Generic;
using HarmonyLib;
using UnityEngine;

namespace WhichMap.Patches;

[HarmonyPatch(typeof(GorillaScoreBoard), nameof(GorillaScoreBoard.RedrawPlayerLines))]
public class PatchScoreBoard
{
    static void Postfix(GorillaScoreBoard __instance)
    {
        if (__instance.boardText == null) return;

        string[] lines = __instance.boardText.text.Split('\n');

        for (int i = 0; i < __instance.lines.Count; i++)
        {
            GorillaPlayerScoreboardLine line = __instance.lines[i];
            if (!line || !line.IsLineActive() || string.IsNullOrEmpty(line.playerNameVisible))
                continue;

            int lineIndex = i + 2;
            if (lineIndex >= lines.Length)
                break;

            string name = line.playerNameVisible;
            VRRig rig = FindRig(line);
            string tag = MapNames.GetFinalMapTag(rig);

            lines[lineIndex] = lines[lineIndex].Replace(name, name + tag);
        }

        __instance.boardText.text = string.Join("\n", lines);
    }

    public static void UpdateZones()
    {
        GorillaScoreBoard[] boards = Object.FindObjectsByType<GorillaScoreBoard>(FindObjectsSortMode.None);
        foreach (var board in boards)
        {
            if (board != null)
                board.RedrawPlayerLines();
        }
    }

    public static VRRig FindRig(GorillaPlayerScoreboardLine line)
    {
        if (line.playerVRRig != null)
            return line.playerVRRig;

        if (line.linePlayer != null && VRRigCache.Instance != null)
        {
            RigContainer container;
            if (VRRigCache.Instance.TryGetVrrig(line.linePlayer, out container) && container != null)
                return container.Rig;
        }

        return null;
    }
}