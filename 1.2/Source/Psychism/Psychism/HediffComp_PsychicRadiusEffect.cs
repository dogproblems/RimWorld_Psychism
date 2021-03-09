using RimWorld;
using RimWorld.Planet;
using Verse;

namespace Psychism
{
    class HediffComp_PsychicRadiusEffect : HediffComp
    {
        public Hediff_ImplantWithLevel psylink;
        public float radius;

        public override void CompExposeData()
        {
            base.CompExposeData();
            Scribe_References.Look<Hediff_ImplantWithLevel>(ref this.psylink, "psylink", false);
            Scribe_Values.Look<float>(ref this.radius, "radius");

            
        }

        public override bool CompShouldRemove
        {
            get
            {
                if (psylink == null || base.CompShouldRemove)
                    return true;

                Pawn source = psylink.pawn;

                return source.health.Dead ||
                        source.needs == null ||
                        source.needs.mood == null ||
                        !source.InMentalState ||
                        parent.pawn.GetStatValue(StatDefOf.PsychicSensitivity) <= 0f ||
                        (
                            (
                                source.Spawned || 
                                parent.pawn.Spawned || 
                                source.GetCaravan() != parent.pawn.GetCaravan()
                            ) &&
                            (
                                !source.Spawned ||
                                !parent.pawn.Spawned ||
                                source.Map != parent.pawn.Map ||
                                source.Position.DistanceTo(parent.pawn.Position) > radius
                            )
                        );

            }
        }
    }
}
