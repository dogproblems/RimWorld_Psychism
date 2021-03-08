using RimWorld;
using RimWorld.Planet;
using Verse;

namespace Psychism
{
    class Hediff_PsychicRadiusEffect : HediffWithComps
    {
        public override string Label
        {
            get
            {
                HediffComp_PsychicRadiusEffect comp = this.TryGetComp<HediffComp_PsychicRadiusEffect>();
                if (comp == null)
                    return def.label;
                return def.label.Formatted(comp.psylink.pawn.Named("PSYCHISMSOURCE")).CapitalizeFirst();
            }
        }

    }
}
