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
    public class CookControllerToOther
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

        #region PowerTube

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

        #endregion

        #region Timer

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



        #endregion

        //Efter et sekund, vil et sekund mindre en forrige blive vist som output
        [TestCase(50)]
        [TestCase(37)]
        public void StartCooking_OneSecondPasses_DisplaySecondLess(int sec)
        {
            _cookController.StartCooking(99, sec);
            Thread.Sleep(1000);
            _output.OutputLine(Arg.Is<string>(str => str.Contains($"00:{sec - 1}")));
        }

        #region Display


        //StartCooking outputter wattprocenten
        [TestCase(100)]
        [TestCase(50)]
        public void StartCooking_OutputsPower(int powerPercent)
        {
            _cookController.StartCooking(powerPercent, 5);
            _output.OutputLine(Arg.Is<string>(str => str.Contains($"{powerPercent} %")));
        }

        #endregion


    }
}