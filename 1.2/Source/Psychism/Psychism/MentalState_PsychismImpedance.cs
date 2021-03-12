using Verse;
using RimWorld;

namespace Psychism
{
    class MentalState_PsychismImpedance : MentalState_WanderWithRadiusEffect
    {
        protected override void TryApplyCustom(Hediff_Psylink psylink, float radius)
        {
            float strength = psylink.level * psylink.pawn.GetStatValue(StatDefOf.PsychicSensitivity);
            
            foreach (ThingWithComps building in pawn.Map.listerThings.ThingsInGroup(ThingRequestGroup.BuildingArtificial))
            {
                CompBuilding_Psychism comp = building.TryGetComp<CompBuilding_Psychism>();

                if (comp != null)
                {
                    if (pawn.Position.DistanceTo(building.Position) > radius)
                        comp.TryRemovePsylink(psylink);
                    else
                        comp.TryAddPsylink(psylink, radius);
                }
            }
            
        }

        public override void PostEnd()
        {
            base.PostEnd();
            Log.Message(string.Format("PostEnd on {0}", pawn.LabelShort));

            Hediff_Psylink psylink = pawn.health.hediffSet.GetFirstHediffOfDef(HediffDefOf.PsychicAmplifier) as Hediff_Psylink;

            if (psylink == null || !pawn.Spawned)
                return;

            foreach (ThingWithComps building in pawn.Map.listerThings.ThingsInGroup(ThingRequestGroup.BuildingArtificial))
            {
                CompBuilding_Psychism comp = building.TryGetComp<CompBuilding_Psychism>();
                if (comp != null)
                    comp.TryRemovePsylink(psylink);
            }
        }
    }
}
