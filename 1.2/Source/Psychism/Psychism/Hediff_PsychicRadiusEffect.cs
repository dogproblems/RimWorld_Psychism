using RimWorld;
using RimWorld.Planet;
using Verse;

namespace Psychism
{
    class Hediff_PsychicRadiusEffect : HediffWithComps
    {
        public override string LabelBase
        {
            get
            {
                HediffComp_PsychicRadiusEffect comp = this.TryGetComp<HediffComp_PsychicRadiusEffect>();
                string label = base.LabelBase;

                if (comp == null)
                    return label;

                return label + ": " + comp.psylink.pawn.LabelShort;
            }
        }
    }
}
