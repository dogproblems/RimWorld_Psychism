using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
