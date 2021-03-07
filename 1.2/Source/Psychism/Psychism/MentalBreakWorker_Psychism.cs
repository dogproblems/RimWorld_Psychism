using Verse;
using Verse.AI;

namespace Psychism
{
    public class MentalBreakWorker_Psychism : MentalBreakWorker
    {
        public override bool BreakCanOccur(Pawn pawn)
        {
            if (!pawn.HasPsylink) return false;
            return base.BreakCanOccur(pawn);
        }
    }
} 
