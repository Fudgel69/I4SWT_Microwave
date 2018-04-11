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
    [TestFixture]
    class IT7UIToLight
    {

        private ILight _light;
        private IOutput _output;

        [SetUp]
        public void SetUp()
        {
            //Output
            _output = new Output();

            //Light
            _light = new Light(_output);
        }

        //LightOn
        //Tænd lyset, og se om "on" bliver vist
        [Test]
        public void Light_OffToOn()
        {
            _light.TurnOn();
            Assert.That(_light.LightISON, Is.EqualTo(true));
        }

        //LightOff
        //Tænd lyset, og se om "off" bliver vist
        [Test]
        public void Light_OnToOff()
        {
            _light.TurnOff();
            Assert.That(_light.LightISON, Is.EqualTo(false));
        }
    }
}
