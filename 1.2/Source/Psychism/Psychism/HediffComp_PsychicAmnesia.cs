using Verse;
using RimWorld;

namespace Psychism
{
    class HediffComp_PsychicAmnesia : HediffComp_PsychicRadiusEffect
    {
        public HediffCompProperties_TickIntervalEffect Props => (HediffCompProperties_TickIntervalEffect)props;

        public override void CompPostTick(ref float severityAdjustment)
        {
            if (!Gen.IsHashIntervalTick(parent.pawn, Props.tickInterval))
                return;

            if (parent.pawn.skills == null)
                return;

            float amount = Props.baseAmount *
                psylink.level *
                psylink.pawn.GetStatValue(StatDefOf.PsychicSensitivity) *
                parent.pawn.GetStatValue(StatDefOf.PsychicSensitivity) *
                (parent.pawn.story.traits.HasTrait(TraitDefOf.GreatMemory) ? 0.5f : 1f);

            foreach(SkillRecord skill in parent.pawn.skills.skills)
            {
                skill.Learn(GetLevelPenalty(skill.levelInt) * amount);
            }
            parent.pawn.needs.joy.CurLevel -= amount;

        }

        private float GetLevelPenalty(int level)
        {
            if (level == 0) return -0f;
            if (level < 3) return -0.4f;
            if (level < 5) return -0.6f;
            if (level < 7) return -1f;
            if (level < 9) return -1.8f;
            if (level < 11) return -2.8f;
            if (level < 13) return -4f;
            if (level < 15) return -6f;
            if (level < 17) return -8f;
            if (level < 19) return -12f;
            return 20f;
        }
    }
}
