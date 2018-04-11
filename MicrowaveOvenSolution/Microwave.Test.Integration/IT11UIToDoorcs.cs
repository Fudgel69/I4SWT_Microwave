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
    class IT11UIToDoorcs
    {
        private Output _output;
        
        //Button
        private IButton _powerButton;
        private IButton _timeButton;
        private IButton _startCancelButton;

        //Door
        private IDoor _door;
        private IPowerTube _powerTube;

        [SetUp]
        public void SetUp()
        {
            //UI Entities
            _output = new Output();
            _door = new Door();

            _powerTube = new PowerTube(_output);

            //Buttons
            _powerButton = new Button();
            _timeButton = new Button();
            _startCancelButton = new Button();
        }

        //tænd program, åben dør, og se om programmet stopper
        [Test]
        public void Cooking_StopCookingDoorIsOpened()
        {
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();
            _door.Open();
            Assert.That(_powerTube.ISON, Is.EqualTo(false));
        }
    }
}
