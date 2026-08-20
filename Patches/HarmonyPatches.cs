using HarmonyLib;

namespace WhichMap.Patches;

public static class HarmonyPatches
{
    private static Harmony _harmonyInstance;

    private static Harmony HarmonyInstance
    {
        get
        {
            _harmonyInstance ??= new Harmony(Constants.Guid);
            return _harmonyInstance;
        }
    }

    public static void Patch()
    {
        HarmonyInstance.PatchAll();
    }

    public static void Unpatch()
    {
        HarmonyInstance.UnpatchSelf();
    }
}