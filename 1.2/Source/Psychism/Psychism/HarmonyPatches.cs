using HarmonyLib;
using Verse;
using RimWorld;

namespace Psychism
{
    [StaticConstructorOnStartup]
    public class HarmonyPatches
    {
        static HarmonyPatches()
        {
            Harmony harmony = new Harmony("com.dogproblems.rimworldmods.psychism");
            harmony.PatchAll();
        }
    }

    [HarmonyPatch(typeof(CompPowerTrader))]
    [HarmonyPatch("get_PowerOutput")]
    class HarmonyPatch_CompPowerTrader_get_PowerOutput
    { 
        static void Postfix(CompPowerTrader __instance, ref float __result)
        {
            CompBuilding_Psychism comp = __instance.parent.GetComp<CompBuilding_Psychism>();

            if (comp != null && __result < 0f)
                __result *= 1f + (.1f * comp.TotalStrength);
        }
    }
}
