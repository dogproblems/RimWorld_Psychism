using Verse;
using RimWorld;

namespace Psychism
{
    class HediffComp_PsychicFerality : HediffComp_PsychicRadiusEffect
    {
        public HediffCompProperties_TickIntervalEffect Props => (HediffCompProperties_TickIntervalEffect)props;

        public override void CompPostTick(ref float severityAdjustment)
        {
            if (!Gen.IsHashIntervalTick(parent.pawn, Props.tickInterval))
                return;

            if (parent.pawn.training == null)
                return;

            if (Rand.Chance(Props.baseChance * parent.Severity * parent.pawn.GetStatValue(StatDefOf.PsychicSensitivity)))
                ApplyDecay();
        }

        private void ApplyDecay()
        {
            //change this when convenient (ie. when Harmony is needed for something else)
            parent.pawn.training.Debug_MakeDegradeHappenSoon();
        }
    }
}
