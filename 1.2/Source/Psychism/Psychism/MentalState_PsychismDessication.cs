using System.Collections.Generic;
using Verse;
using RimWorld;

namespace Psychism
{
    class MentalState_PsychismDessication : MentalState_WanderWithRadiusEffect
    {
        protected override void TryApplyCustom(Hediff_Psylink psylink, float radius)
        {
            float strength = psylink.level * psylink.pawn.GetStatValue(StatDefOf.PsychicSensitivity);
            float baseChance = def.GetModExtension<DefModExtension_WanderWithRadiusEffect>().baseChance;
            float baseAmount = def.GetModExtension<DefModExtension_WanderWithRadiusEffect>().baseAmount;

            List<ThingWithComps> targets = new List<ThingWithComps>();

            foreach (ThingWithComps plant in pawn.Map.listerThings.ThingsInGroup(ThingRequestGroup.Plant))
            {
                if (pawn.Position.DistanceTo(plant.Position) <= radius &&
                        Verse.Rand.Chance(strength * baseChance))
                    targets.Add(plant);
            }

            if (targets.Any())
            {
                DamageInfo damage = new DamageInfo(DamageDefOf.Rotting, baseAmount * strength);
                for (int i = targets.Count - 1; i >= 0; i--)
                    targets[i].TakeDamage(damage);
            }

        }
    }
}
