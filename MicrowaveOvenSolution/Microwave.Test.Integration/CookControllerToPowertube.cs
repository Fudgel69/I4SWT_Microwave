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
    public class CookControllerToPowertube
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
            _display = new Display(_output);
            _powerTube = new PowerTube(_output);
            _timer = new Timer();
            _cookController = new CookController(_timer, _display, _powerTube);
        }

        //StartCooking tænder for PowerTube
        [Test]
        public void StartCooking_PowerTubeTurnsOn()
        {
            _cookController.StartCooking(99, 5);
            Assert.That(_powerTube.ISON, Is.EqualTo(true));
        }

        //stop slukker for PowerTube
        [Test]
        public void StartCookingStopCooking_PowerTuberTurnsOff()
        {
            _cookController.StartCooking(99, 5);
            _cookController.Stop();
            Assert.That(_powerTube.ISON, Is.EqualTo(false));
        }

        //Efter StartCooking har kørt i 5 sekunder vil PowerTube blive slukket
        [Test]
        public void StartCookingFiveSeconds_PowerTubeTurnsOff()
        {
            _cookController.StartCooking(99, 5);
            Thread.Sleep(6000);
            Assert.That(_powerTube.ISON, Is.EqualTo(false));
        }

        //Køres StartCooking med over 100% vil der smides en exception
        [Test]
        public void StartCooking_TooMuchPower_ThrowsException()
        {
            Assert.Throws<System.ArgumentOutOfRangeException>(() => _cookController.StartCooking(101, 5));
        }

        //Køres StartCooking med 0% eller mindre vil en exception blive smidt
        [TestCase(-1)]
        [TestCase(0)]
        public void StartCooking_ZeroOrUnderPower_ThreowsException(int powerPercent)
        {
            Assert.Throws<System.ArgumentOutOfRangeException>(() => _cookController.StartCooking(powerPercent, 5));
        }

    }
}