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
    public class IT4CookControllerToTimer
    {

        private IOutput _output;
        private IDisplay _display;
        private IPowerTube _powerTube;
        private ITimer _timer;
        private ICookController _cookController;

        [SetUp]
        public void SetUp()
        {
            _output = Substitute.For<IOutput>(); ;
            _display = Substitute.For<IDisplay>();;
            _powerTube = Substitute.For<IPowerTube>();
            _timer = new Timer();
            _cookController = new CookController(_timer, _display, _powerTube);
        }


        //StartCooking starter en timer
        [Test]
        public void StartCooking_StartsTimer()
        {
            _cookController.StartCooking(50, 50);
            Assert.That(_timer.TIMER.Enabled, Is.EqualTo(true));
        }

        //stop stopper timeren
        [Test]
        public void StopCooking_StopsTimer()
        {
            _cookController.StartCooking(50, 50);
            _cookController.Stop();
            Assert.That(_timer.TIMER.Enabled, Is.EqualTo(false));
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


    }
}