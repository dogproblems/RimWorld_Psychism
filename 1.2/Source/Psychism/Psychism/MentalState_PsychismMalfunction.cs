using Verse;
using RimWorld;

namespace Psychism
{
    class MentalState_PsychismMalfunction : MentalState_WanderWithRadiusEffect
    {
        protected override void TryApplyCustom(Hediff_Psylink psylink, float radius)
        {
            float strength = psylink.level * psylink.pawn.GetStatValue(StatDefOf.PsychicSensitivity);
            float baseChance = def.GetModExtension<DefModExtension_WanderWithRadiusEffect>().baseChance;

            foreach (ThingWithComps building in pawn.Map.listerThings.ThingsInGroup(ThingRequestGroup.BuildingArtificial))
            {
                CompBreakdownable comp = building.TryGetComp<CompBreakdownable>();

                if (comp != null)
                {
                    if (pawn.Position.DistanceTo(building.Position) <= radius &&
                        Verse.Rand.Chance(strength * baseChance))
                    {
                        comp.DoBreakdown();
                        break;
                    }
                }
            }

        }
    }
}
