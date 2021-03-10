using RimWorld;
using Verse;


namespace Psychism
{
    class Hediff_PsychicPain : Hediff_PsychicRadiusEffect
    {
        public override float PainOffset
        {
            get
            {
                return base.PainOffset * pawn.GetStatValue(StatDefOf.PsychicSensitivity);
            }

        }
    }
}
