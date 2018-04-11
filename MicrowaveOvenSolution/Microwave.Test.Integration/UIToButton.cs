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
    class UIToButton
    {
        //Button
        private IButton _powerButton;
        private IButton _timeButton;
        private IButton _startCancelButton;

        private IDisplay _display;
        private IOutput _output;
        private IPowerTube _powerTube;

        [SetUp]
        public void SetUp()
        {
            //Button
            _powerButton = new Button();
            _timeButton = new Button();
            _startCancelButton = new Button();

            //Substitutes
            _output = Substitute.For<IOutput>();
            _powerTube = Substitute.For<IPowerTube>();
            _display = Substitute.For<IDisplay>(_output);
        }


        //PowerButton
        //tryk på power, og se om display viser det rigtige
        [Test]
        public void PowerButton_PowerIs50()
        {
            _powerButton.Press();
            _display.ShowPower(Arg.Is<int>(50));
        }


        //TimerButton
        //tryk på power, så timer, og se om display viser den rigtige timer
        [Test]
        public void SetPower_TimerButton_TimerIs1()
        {
            _powerButton.Press();
            _timeButton.Press();
            _display.ShowTime(Arg.Is<int>(1), Arg.Is<int>(0));
        }


        //StartCancelButton
        //tænd for program, tjek om StartCooking modtages
        [Test]
        public void SetPowerSetTimer_MicrowaveIsOn()
        {
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();
            Assert.That(_powerTube.ISON, Is.EqualTo(true));
        }

        //StartCancelButton
        //tænd for program, tryk på cancel, og se om programmet stopper
        [Test]
        public void Cooking_StopCookingCancelButtonIsPressed()
        {
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();
            _startCancelButton.Press();
            Assert.That(_powerTube.ISON, Is.EqualTo(false));
        }
    }
}
