using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrowaveOvenClasses.Interfaces
{
    public interface IPowerTube
    {
        void TurnOn(int power);
        bool ISON { get; }
        void TurnOff();
    }
}
