using Verse;
using RimWorld;

namespace Psychism
{
    class HediffComp_PsychicDistraction : HediffComp_PsychicRadiusEffect
    {
        public HediffCompProperties_TickIntervalEffect Props => (HediffCompProperties_TickIntervalEffect)props;

        public override void CompPostTick(ref float severityAdjustment)
        {
            if (!Gen.IsHashIntervalTick(parent.pawn, Props.tickInterval))
                return;

            if (parent.pawn.needs == null || parent.pawn.needs.joy == null)
                return;

            float amount = Props.baseAmount * 
                parent.pawn.GetStatValue(StatDefOf.PsychicSensitivity);

            parent.pawn.needs.joy.CurLevel -= amount;

        }
    }
}
