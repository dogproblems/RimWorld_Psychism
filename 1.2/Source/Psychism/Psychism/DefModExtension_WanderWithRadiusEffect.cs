using Verse;
using RimWorld;

namespace Psychism
{
    class DefModExtension_WanderWithRadiusEffect : DefModExtension
    {
        public float radius = 0f;
        public int tickInterval = -1;
        public bool affectsHumans = false;
        public bool affectsTameAnimals = false;
        public bool affectsWildAnimals = false;
        public ThoughtDef thoughtDef = null;
        public HediffDef hediffDef = null;
    }
}
