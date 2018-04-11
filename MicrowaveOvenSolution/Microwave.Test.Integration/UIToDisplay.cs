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
    class UIToDisplay
    {


        //Userinterface
        private IOutput _output;
     
        //Button
        private IButton _powerButton;
        private IButton _timeButton;
        private IButton _startCancelButton;

        [SetUp]
        public void SetUp()
        {
            //Button
            _powerButton = new Button();
            _timeButton = new Button();
            _startCancelButton = new Button();

            //Substitutes
            _output = Substitute.For<IOutput>();
            _powerButton = Substitute.For<IButton>();
            _timeButton = Substitute.For<IButton>();
            _startCancelButton = Substitute.For<IButton>();

        }

        //Tester at microwave er i ready state, og viser power på displayet når der trykkes på powerbutton
        [Test]
        public void OnPowerPressedReadyStateShowPower()
        {
            _powerButton.Press();
            _output.Received().OutputLine("Display shows: 50 W");
            // _output.Received(1).OutputLine(Arg.Is<string>(str => str.Contains("50 W")));
        }

        //Tester at microwave er i SetPower state, og viser tid på displayet når der trykkes på timebutton
        [Test]
        public void OnTimePressedSetPowerStateDisplayTime()
        {
            _powerButton.Press();
            //SetTime state
            _timeButton.Press();
            // _output.OutputLine(Arg.Is<string>(str => str.Contains("01:00")));
            _output.Received().OutputLine("Display shows: 01:00");
        }

        //Tester at microwave clearer display når der trykkes start/cancel i SetTime state
        [Test]
        public void OnStartCancelPressedSetPowerStateClearDisplay()
        {
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();
            _output.Received().OutputLine("Display cleared nauw");
        }
    }
}
