using System.Collections.Generic;
using Verse;
using Verse.AI;
using RimWorld;
using RimWorld.Planet;

namespace Psychism
{
    class MentalState_PsychismMelancholy : MentalState
    {
        public override void MentalStateTick()
        {
            base.MentalStateTick();

            if (Gen.IsHashIntervalTick(pawn, 150))
                return;

            if (pawn.needs == null || pawn.needs.mood == null || pawn.Faction == null)
                return;

            if (pawn.Spawned)
            {
                this.AffectPawns(pawn, pawn.Map.mapPawns.AllPawnsSpawned);
                return;
            }
            Caravan caravan = pawn.GetCaravan();
            if (caravan != null)
            {
                this.AffectPawns(pawn, caravan.pawns.InnerListForReading);
            }
        }

        private void AffectPawns(Pawn p, List<Pawn> pawns)
        {
            Hediff_ImplantWithLevel psylink = p.health.hediffSet.GetFirstHediffOfDef(HediffDefOf.PsychicAmplifier) as Hediff_ImplantWithLevel;
            if(psylink is null)
                return;

            for (int i = 0; i < pawns.Count; i++)
            {
                Pawn pawn = pawns[i];
                if (p != pawn && 
                    p.RaceProps.Humanlike && 
                    pawn.needs != null && 
                    pawn.needs.mood != null && 
                    pawn.needs.mood.thoughts != null && 
                    (!p.Spawned || !pawn.Spawned || pawn.Position.DistanceTo(p.Position) <= 9))
                {
                    bool flag = false;
                    using (List<Thought_Memory>.Enumerator enumerator = pawn.needs.mood.thoughts.memories.Memories.GetEnumerator())
                    {
                        while (enumerator.MoveNext())
                        {
                            Thought_PsychicMelancholy oldThought = enumerator.Current as Thought_PsychicMelancholy;
                            if (oldThought != null && oldThought.psylink.pawn == p)
                            {
                                flag = true;
                                break;
                            }
                        }
                    }
                    if (!flag)
                    {
                        Thought_PsychicMelancholy thought = (Thought_PsychicMelancholy)ThoughtMaker.MakeThought(ThoughtDef.Named("PsychicMelancholy"));
                        thought.psylink = p.health.hediffSet.GetFirstHediffOfDef(HediffDefOf.PsychicAmplifier) as Hediff_ImplantWithLevel;
                        pawn.needs.mood.thoughts.memories.TryGainMemory(thought, null);
                    }
                }
            }

        }
    }
}
