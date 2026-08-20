using System.Collections.Generic;
using HarmonyLib;
using UnityEngine;

namespace WhichMap.Patches;

[HarmonyPatch(typeof(GorillaScoreBoard), nameof(GorillaScoreBoard.RedrawPlayerLines))]
public class PatchScoreBoard
{
    static void Postfix(GorillaScoreBoard __instance)
    {
        if (__instance.boardText == null)
        {
            return;
        }

        string fullText = __instance.boardText.text;
        string[] lines = fullText.Split('\n');

        for (int i = 0; i < __instance.lines.Count; i++)
        {
            GorillaPlayerScoreboardLine line = __instance.lines[i];

            if (line == null || line.IsLineActive() == false || string.IsNullOrEmpty(line.playerNameVisible))
            {
                continue;
            }

            int lineIndex = i + 2;

            if (lineIndex >= lines.Length)
            {
                break;
            }

            string playerName = line.playerNameVisible;
            VRRig rig = FindRig(line);
            string mapTag = MapNames.GetFinalMapTag(rig);
            string oldLine = lines[lineIndex];
            string newLine = oldLine.Replace(playerName, playerName + mapTag);
            lines[lineIndex] = newLine;
        }

        string finalText = string.Join("\n", lines);
        __instance.boardText.text = finalText;
    }

    public static void UpdateZones()
    {
        GorillaScoreBoard[] boards = Object.FindObjectsByType<GorillaScoreBoard>(FindObjectsSortMode.None);

        for (int i = 0; i < boards.Length; i++)
        {
            GorillaScoreBoard lb = boards[i];
            if (lb != null)
            {
                lb.RedrawPlayerLines();
            }
        }
    }

    public static VRRig FindRig(GorillaPlayerScoreboardLine line)
    {
        if (line.playerVRRig != null)
        {
            return line.playerVRRig;
        }

        if (line.linePlayer != null && VRRigCache.Instance != null)
        {
            RigContainer container;
            bool found = VRRigCache.Instance.TryGetVrrig(line.linePlayer, out container);

            if (found && container != null)
            {
                return container.Rig;
            }
        }
        return null;
    }
}