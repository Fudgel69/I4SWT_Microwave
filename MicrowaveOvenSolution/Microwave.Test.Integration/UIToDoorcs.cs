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
    class UIToDoorcs
    {
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
            //Door
            _door = new Door();

            //Substitutes
            _powerTube = Substitute.For<IPowerTube>();
            _powerButton = Substitute.For<IButton>();
            _timeButton = Substitute.For<IButton>();
            _startCancelButton = Substitute.For<IButton>();
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
