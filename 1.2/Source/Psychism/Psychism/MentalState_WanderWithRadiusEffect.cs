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

            int tickInterval = def.GetModExtension<DefModExtension_WanderWithRadiusEffect>().tickInterval;

            if (!Gen.IsHashIntervalTick(pawn, tickInterval))
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

        private void AffectPawns(Pawn source, List<Pawn> pawns)
        {
            Hediff_ImplantWithLevel psylink = source.health.hediffSet.GetFirstHediffOfDef(HediffDefOf.PsychicAmplifier) as Hediff_ImplantWithLevel;
            float radius = def.GetModExtension<DefModExtension_WanderWithRadiusEffect>().radius;
            ThoughtDef thoughtDef = def.GetModExtension<DefModExtension_WanderWithRadiusEffect>().thoughtDef;
            HediffDef hediffDef = def.GetModExtension<DefModExtension_WanderWithRadiusEffect>().hediffDef;

            if (psylink == null)
                return;

            //Log.Message(string.Format("radius={0}", radius));
            //Log.Message(string.Format("target pawn count={0}", pawns.Count));
            //Log.Message(string.Format("thoughtDef={0}", (thoughtDef == null) ? "null" : thoughtDef.defName));

            for (int i = 0; i < pawns.Count; i++)
            {
                Pawn target = pawns[i];
                if (source == target ||
                    !source.RaceProps.Humanlike ||
                    (
                        source.Spawned &&
                        target.Spawned &&
                        target.Position.DistanceTo(source.Position) > radius
                    )
                )
                    continue;

                if(
                    (
                        target.RaceProps.Humanlike && 
                        def.GetModExtension<DefModExtension_WanderWithRadiusEffect>().affectsHumans
                    ) ||
                    (
                        target.RaceProps.Animal && 
                        target.Faction != null && 
                        def.GetModExtension<DefModExtension_WanderWithRadiusEffect>().affectsTameAnimals
                    )
                )
                {
                    if (thoughtDef != null)
                        TryApplyThought(target, thoughtDef, psylink, radius);
                    if (hediffDef != null)
                        TryApplyHediff(target, hediffDef, psylink, radius);
                }

            }

        }


        private void TryApplyThought(Pawn target, ThoughtDef thoughtDef, Hediff_ImplantWithLevel psylink, float radius)
        {
            if (target.needs == null ||
                target.needs.mood == null ||
                target.needs.mood.thoughts == null) return;

            bool flag = false;
            using (List<Thought_Memory>.Enumerator enumerator = target.needs.mood.thoughts.memories.Memories.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    Thought_PsychicRadiusEffect oldThought = enumerator.Current as Thought_PsychicRadiusEffect;
                    if (oldThought != null && oldThought.psylink.pawn == psylink.pawn)
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
                target.needs.mood.thoughts.memories.TryGainMemory(thought, null);
            }
        }

        private void TryApplyHediff(Pawn target, HediffDef hediffDef, Hediff_ImplantWithLevel psylink, float radius)
        {
            bool flag = false;

            foreach (HediffComp comp in target.health.hediffSet.GetAllComps())
            {
                HediffComp_PsychicRadiusEffect oldHediffComp = comp as HediffComp_PsychicRadiusEffect;
                if (oldHediffComp != null && oldHediffComp.psylink.pawn == psylink.pawn)
                {
                    flag = true;
                    break;
                }
            }

            if (!flag)
            {
                List<Hediff> added = new List<Hediff>();
                HediffGiverUtility.TryApply(target, hediffDef, new[] { BodyPartDefOf.Brain }, outAddedHediffs:added);
                foreach (Hediff addedHediff in added)
                {
                    HediffComp_PsychicRadiusEffect comp = addedHediff.TryGetComp<HediffComp_PsychicRadiusEffect>();
                    if (comp == null) continue;
                    comp.psylink = psylink;
                    comp.radius = radius;
                    addedHediff.Severity = psylink.level * psylink.pawn.GetStatValue(StatDefOf.PsychicSensitivity);
                }
            }
        }
    }
}
