using System.Collections.Generic;
using Verse;
using Verse.AI;
using RimWorld;
using RimWorld.Planet;

namespace Psychism
{
    class MentalState_WanderWithRadiusEffect : MentalState
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
                AffectPawns(pawn, pawn.Map.mapPawns.AllPawnsSpawned);
                return;
            }
            Caravan caravan = pawn.GetCaravan();
            if (caravan != null)
            {
                AffectPawns(pawn, caravan.pawns.InnerListForReading);
            }
        }

        private void AffectPawns(Pawn p, List<Pawn> pawns)
        {
            Hediff_ImplantWithLevel psylink = p.health.hediffSet.GetFirstHediffOfDef(HediffDefOf.PsychicAmplifier) as Hediff_ImplantWithLevel;
            float radius = this.def.GetModExtension<DefModExtension_WanderWithRadiusEffect>().radius;
            ThoughtDef thoughtDef = this.def.GetModExtension<DefModExtension_WanderWithRadiusEffect>().thoughtDef;
            
            if (psylink == null)
                return;

            //Log.Message(string.Format("radius={0}", radius));
            //Log.Message(string.Format("target pawn count={0}", pawns.Count));
            //Log.Message(string.Format("thoughtDef={0}", (thoughtDef == null) ? "null" : thoughtDef.defName));

            for (int i = 0; i < pawns.Count; i++)
            {
                Pawn pawn = pawns[i];
                if (p != pawn && 
                    p.RaceProps.Humanlike && 
                    pawn.needs != null && 
                    pawn.needs.mood != null && 
                    pawn.needs.mood.thoughts != null && 
                    (!p.Spawned || !pawn.Spawned || pawn.Position.DistanceTo(p.Position) <= radius))
                {
                    if (thoughtDef != null)
                    {
                        
                        bool flag = false;
                        using (List<Thought_Memory>.Enumerator enumerator = pawn.needs.mood.thoughts.memories.Memories.GetEnumerator())
                        {
                            while (enumerator.MoveNext())
                            {
                                Thought_PsychicRadiusEffect oldThought = enumerator.Current as Thought_PsychicRadiusEffect;
                                if (oldThought != null && oldThought.psylink.pawn == p)
                                {
                                    flag = true;
                                    break;
                                }
                            }
                        }
                        if (!flag)
                        {
                            Thought_PsychicRadiusEffect thought = (Thought_PsychicRadiusEffect)ThoughtMaker.MakeThought(thoughtDef);

                            thought.psylink = psylink;
                            thought.radius = radius;
                            pawn.needs.mood.thoughts.memories.TryGainMemory(thought, null);
                        }
                    }
                }
            }
        }
    }
}
