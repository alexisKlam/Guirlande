using System.Collections.Generic;
using System.Linq;
using Guirlande.Server;

namespace Common
{
    public class Guirlande3Ligne: GuirlandeBase{

        private List<string> _colors = new List<string>
        {
            "#50f442","#973fba","#bcab3e"

        };

        public override string GetNextStep(int step)
        {
            var entries = new List<GuirlandeEntry>();
            for (int i = 0; i < Constante.TotalLight; i++)
            {
                entries.Add(new GuirlandeEntry
                {
                    Color = _colors[(step+i) % _colors.Count]
                });
            }
          
            string result = "";

            return entries.Aggregate<GuirlandeEntry, string>(result, (entry, guirlandeEntry) => result += entry + ";" + guirlandeEntry.Color);

        }

        public override string Name => "Trois couleurs ";

  
    }
}