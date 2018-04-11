using System;
using System.Runtime.InteropServices;
using System.Threading;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NSubstitute.Core;
using NSubstitute.ExceptionExtensions;
using NUnit.Framework;
using Timer = MicrowaveOvenClasses.Boundary.Timer;

namespace Microwave.Test.Integration
{
    [TestFixture]
    public class IT6CookControllerToDisplay
    {

        private IOutput _output;
        private IDisplay _display;
        private IPowerTube _powerTube;
        private ITimer _timer;
        private ICookController _cookController;

        [SetUp]
        public void SetUp()
        {
            _output = new Output();
            _powerTube = new PowerTube(_output);
            _timer = new Timer();

            _display = new Display(_output);


            _cookController = new CookController(_timer, _display, _powerTube);
        }

        //Efter et sekund, vil et sekund mindre en forrige blive vist som output
        [TestCase(50)]
        [TestCase(37)]
        public void StartCooking_OneSecondPasses_DisplaySecondLess(int sec)
        {
            _cookController.StartCooking(99, sec);
            Thread.Sleep(1000);
            _output.OutputLine(Arg.Is<string>(str => str.Contains($"00:{sec - 1}")));
        }


        //StartCooking outputter wattprocenten
        [TestCase(100)]
        [TestCase(50)]
        public void StartCooking_OutputsPower(int powerPercent)
        {
            _cookController.StartCooking(powerPercent, 5);
            _output.OutputLine(Arg.Is<string>(str => str.Contains($"{powerPercent} %")));
        }
    }
}