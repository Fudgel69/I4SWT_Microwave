using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.Test.Integration
{
   // [TestFixture]
    class UIToOther
    {

        //CookControler
        private IPowerTube _powerTube;
        private ITimer _timer;

        //Userinterface
        private IOutput _output;
        private IDoor _door;
        private IDisplay _display;
        private ILight _light;
        private ICookController _cooker;



    }
}
