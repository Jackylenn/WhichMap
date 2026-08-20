using System;

namespace WhichMap;

public static class MapNames
{
    public static string GetMapName(VRRig rig)
    {
        if (rig == null || rig.zoneEntity == null)
            return "Unknown";

        var zone = rig.zoneEntity.currentZone;
        return GetZoneName(zone);
    }

    public static string GetZoneName(GTZone zone)
    {
        return zone switch
        {
            GTZone.forest => "Forest",
            GTZone.city => "City",
            GTZone.canyon => "Canyons",
            GTZone.beach => "Beach",
            GTZone.mountain => "Mountains",
            GTZone.cave => "Caves",
            GTZone.skyJungle => "Clouds",
            GTZone.basement => "Basement",
            GTZone.attic => "Attic",
            GTZone.arcade => "Arcade",
            GTZone.bayou => "Bayou",
            GTZone.Metropolis => "Metro",
            GTZone.mines => "Mines",
            GTZone.rotating => "Rotating",
            GTZone.tutorial => "Tutorial",
            GTZone.monkeBlocks => "Monke Blocks",
            GTZone.monkeBlocksShared => "Monke Blocks",
            GTZone.critters => "Critters",
            GTZone.ghostReactor => "GR",
            GTZone.customMaps => "Destinations", //future proofing dkfskc
            GTZone.ranked => "Ranked",
            GTZone.spaceMap => "Space",
            GTZone.mall => "Atrium",
            GTZone.arena => "Arena",
            GTZone.hoverboard => "Hoverboard",
            GTZone.GTFC => "VIM",
            GTZone.VIMExperience1 => "Volcano",
            GTZone.VIMExperience2 => "Cave Dig",
            GTZone.VIMExperience3 => "Grav-Dash",
            GTZone.VIMExperience4 => "IHaveNoIdeaButMaybeItWillBeGinoPietermaai4",
            GTZone.drill => "GR Drill",
            GTZone.none => "Stump",
            _ => FormatName(zone.ToString())
        };
    }

    public static string GetFinalMapTag(VRRig rig)
    {
        string mapName = GetMapName(rig);
        return $"<size=50%>{mapName}</size>";
    }

    static string FormatName(string name)
    {
        if (string.IsNullOrEmpty(name)) return "Unknown";
        if (name.Length == 1) return name.ToUpper();
        return char.ToUpper(name[0]) + name.Substring(1);
    }
}