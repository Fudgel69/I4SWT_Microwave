using System;
using System.Runtime.InteropServices;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.Test.Integration
{
    [TestFixture]
    public class PowerTubeToOutput
    {
        private IPowerTube _powertube;
        private IOutput _output;

        [SetUp]
        public void Setup()
        {
            _output = new Output();
            _powertube = new PowerTube(_output);
        }

        //Tænd for powertube outputter wattforbrug i %
        [TestCase(10)]
        [TestCase(50)]
        [TestCase(90)]
        public void TurnOn_DisplaysPower(int pwr)
        {
            _powertube.TurnOn(pwr);
            _output.OutputLine(Arg.Is<string>(str => str.Contains($"{pwr} %")));
        }

        //Slukker man for powertube vil output være off
        [Test]
        public void TurnOff_DisplaysOff()
        {
            _powertube.TurnOn(50);
            _powertube.TurnOff();
            _output.OutputLine(Arg.Is<string>(str => str.Contains("off")));
        }

        //Slukker man uden at have tændt vil intet blive vist
        [Test]
        public void TurnOff_NoOutput()
        {
            _powertube.TurnOff();
            _output.OutputLine(Arg.Any<string>());
        }

        //Tændes der to gange smides en exception
        [Test]
        public void TurnOnTwice_DisplayException()
        {
            _powertube.TurnOn(50);
            Assert.Throws<System.ApplicationException>(() => _powertube.TurnOn(60));
        }

        //Tændes med en negativ værdi smides en exception
        [Test]
        public void TurnOnNegativePower_DisplayException()
        {
            Assert.Throws<System.ArgumentOutOfRangeException>(() => _powertube.TurnOn(-1));
        }

        //Tændes med over 100% smides en exception
        [Test]
        public void TurnOnOverHundred_DisplayException()
        {
            Assert.Throws<System.ArgumentOutOfRangeException>(() => _powertube.TurnOn(101));
        }

        //Tændes med 0% smides en exception
        [Test]
        public void TurnOnZero_DisplayException()
        {
            Assert.Throws<System.ArgumentOutOfRangeException>(() => _powertube.TurnOn(0));
        }
    }
}