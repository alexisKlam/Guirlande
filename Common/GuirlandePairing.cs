using System.Collections.Generic;
using System.Linq;
using Guirlande.Server;

namespace Common
{
    public class GuirlandePairing : GuirlandeBase
    {
        public override string GetNextStep(int step)
        {
            var entries = new List<GuirlandeEntry>();
            for (int i = 0; i < Constante.TotalLight; i++)
            {
                entries.Add(new GuirlandeEntry
                {
                    Color = ((i+ step )% 2)==0? "#f44242" : "#12bc15"
                });
            }

            string result = "";

            return entries.Aggregate<GuirlandeEntry, string>(result, (entry, guirlandeEntry) => result += entry + ";" + guirlandeEntry.Color);
        }

        public override string Name => "2 Couleurs";
    }
}