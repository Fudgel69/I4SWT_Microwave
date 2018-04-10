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
    public class LightToOutput
    {

        private IOutput _output;
        private ILight _light;

        [SetUp]
        public void SetUp()
        {
            _output = new Output();
            _light = new Light(_output);

        }

        //Tændes lyset vil output bestå af "on"
        [Test]
        public void TurnLightOn_OutputsOn()
        { 
            _light.TurnOn();
            _output.OutputLine(Arg.Is<string>(str => str.Contains("on")));

        }

        //Slukkes lyset vil output bestå af "off"
        [Test]
        public void TurnLightOff_OutputsOff()
        {
            _light.TurnOff();
            _output.OutputLine(Arg.Is<string>(str => str.Contains("off")));

        }

    }
}
