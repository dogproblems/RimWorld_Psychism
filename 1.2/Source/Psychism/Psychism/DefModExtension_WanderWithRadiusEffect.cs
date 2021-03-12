using Verse;
using RimWorld;

namespace Psychism
{
    class DefModExtension_WanderWithRadiusEffect : DefModExtension
    {
        public float radius = 0f;
        public int tickInterval = -1;
        public float baseAmount = 0f;
        public float baseChance = 0f;
        public bool affectsHumans = false;
        public bool affectsTameAnimals = false;
        public bool affectsWildAnimals = false;
        public ThoughtDef thoughtDef = null;
        public HediffDef hediffDef = null;
        public ThingDef filthDef = null;
    }
}
