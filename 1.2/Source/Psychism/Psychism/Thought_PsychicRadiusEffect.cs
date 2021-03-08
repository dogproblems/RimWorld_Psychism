using RimWorld;
using RimWorld.Planet;
using Verse;

namespace Psychism
{
    class Thought_PsychicRadiusEffect : Thought_Memory
    {
        public Hediff_ImplantWithLevel psylink;
        public float radius;

        public override string LabelCap
        {
            get
            {
                return base.CurStage.label.Formatted(this.psylink.pawn.Named("PSYCHISMSOURCE")).CapitalizeFirst();
            }
        }

        public override bool ShouldDiscard
        {
            get
            {
                if (this.psylink == null)
                    return true;
                Pawn pawn = this.psylink.pawn;
                return pawn.health.Dead ||
                        pawn.needs == null ||
                        pawn.needs.mood == null ||
                        !pawn.InMentalState ||
                        (
                            (pawn.Spawned || this.pawn.Spawned || pawn.GetCaravan() != this.pawn.GetCaravan()) &&  
                            (
                                !pawn.Spawned || 
                                !this.pawn.Spawned || 
                                pawn.Map != this.pawn.Map || 
                                pawn.Position.DistanceTo(this.pawn.Position) > radius
                            )
                        );
            }
        }
        
        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_References.Look<Hediff_ImplantWithLevel>(ref this.psylink, "psylink", false);
            Scribe_Values.Look<float>(ref this.radius, "radius");
        }

        public override float MoodOffset()
        {
            if (ThoughtUtility.ThoughtNullified(this.pawn, this.def))
            {
                return 0f;
            }
            if (ShouldDiscard)
            {
                return 0f;
            }
            float b = base.MoodOffset();
            float ps = psylink.pawn.GetStatValue(StatDefOf.PsychicSensitivity, true);
            float pl = psylink.level;
            return b * ps * pl;
        }

        public override bool TryMergeWithExistingMemory(out bool showBubble)
        {
            showBubble = false;
            return false;
        }

        public override bool GroupsWith(Thought other)
        {
            Thought_PsychicRadiusEffect thought = other as Thought_PsychicRadiusEffect;
            return thought != null && base.GroupsWith(other) && thought.psylink == this.psylink;
        }

    }
}
