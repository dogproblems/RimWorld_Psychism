using RimWorld;
using Verse;
namespace Psychism
{
    [DefOf]
    class MentalStateDefOf_Psychism
    {
        public static MentalStateDef PsychismImpedance;

        static MentalStateDefOf_Psychism()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(MentalStateDefOf_Psychism));
        }
    }
}
