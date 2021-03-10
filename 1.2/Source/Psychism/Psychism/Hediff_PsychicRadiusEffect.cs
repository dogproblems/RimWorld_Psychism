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

        public override bool TryMergeWith(Hediff other)
        {
            if (other == null || other.def != def || other.Part != Part)
                return false;

            Hediff_PsychicRadiusEffect otherPRE = other as Hediff_PsychicRadiusEffect;
            if (otherPRE == null)
                return false;

            HediffComp_PsychicRadiusEffect comp = this.TryGetComp<HediffComp_PsychicRadiusEffect>();
            HediffComp_PsychicRadiusEffect otherComp = otherPRE.TryGetComp<HediffComp_PsychicRadiusEffect>();

            if (comp == null || otherComp == null)
                return false;

            if (comp.psylink == null || otherComp.psylink == null || comp.psylink != otherComp.psylink)
                return false;

            return true;
        }
    }
}
