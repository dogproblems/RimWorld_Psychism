using System.Collections.Generic;
using System.Linq;
using Verse;
using RimWorld;



namespace Psychism
{
    class CompBuilding_Psychism : ThingComp
    {
        private List<Hediff_Psylink> psylinks = new List<Hediff_Psylink>();

        public override void PostExposeData()
        {
            base.PostExposeData();
            Scribe_Collections.Look<Hediff_Psylink>(ref this.psylinks, "psylinks", LookMode.Reference);
        }


        public float GetTotalStrength(MentalStateDef def)
        {
            return psylinks.Where(x => x.pawn.MentalStateDef == def)
                .Select(x => x.level * x.pawn.GetStatValue(StatDefOf.PsychicSensitivity))
                .Sum();
        }

        public void TryAddPsylink(Hediff_Psylink psylink, float radius)
        {
            if (!psylinks.Contains(psylink))
            {
                psylinks.Add(psylink);
                Log.Message(string.Format("adding {0} to {1}", psylink.pawn.LabelShort, parent.LabelShort));
            }
        }

        public void TryRemovePsylink(Hediff_Psylink psylink) 
        {
            if (psylinks.Contains(psylink))
            {
                psylinks.Remove(psylink);
                Log.Message(string.Format("removing {0} from {1}", psylink.pawn.LabelShort, parent.LabelShort));
            }
        }
    }
}
