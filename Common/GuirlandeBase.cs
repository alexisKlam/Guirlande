using System.ComponentModel;
using System.Runtime.CompilerServices;
using Common.Annotations;

namespace Guirlande.Server
{
    public abstract class GuirlandeBase
    {

        public GuirlandeBase()
        {
        }

        public abstract string GetNextStep(int step);

        public  abstract  string Name { get; }
        
    }
}