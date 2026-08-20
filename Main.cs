using System;
using BepInEx;
using UnityEngine;
using WhichMap.Patches;

namespace WhichMap;

[BepInPlugin(Constants.Guid, Constants.Name, Constants.Version)]
public class Main : BaseUnityPlugin
{
    public static Main Instance;
    float lastCheck;

    public Main()
    {
        Instance = this;
        HarmonyPatches.Patch();
    }

    void Update()
    {
        if (Time.time - lastCheck >= 1f)
        {
            lastCheck = Time.time;
            PatchScoreBoard.UpdateZones();
        }
    }
}