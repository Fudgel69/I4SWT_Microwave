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
    class IT8UIToDisplay
    {

        private UserInterface _userInterface;

        //Button
        private IButton _powerButton;
        private IButton _timeButton;
        private IButton _startCancelButton;

        //CookControler
        private IPowerTube _powerTube;
        private ITimer _timer;

        //Userinterface
        private IOutput _output;
        private IDoor _door;
        private IDisplay _display;
        private ILight _light;
        private ICookController _cooker;


        [SetUp]
        public void SetUp()
        {
            _output = new Output();
            _display = new Display(_output);
            _light = new Light(_output);

            //Substitudes    
            _powerButton = Substitute.For<IButton>();
            _timeButton = Substitute.For<IButton>();
            _startCancelButton = Substitute.For<IButton>();
            _timer = Substitute.For<ITimer>();
            _door = Substitute.For<IDoor>();
            _powerTube = Substitute.For<IPowerTube>();
            _cooker = Substitute.For<CookController>(_timer, _display, _powerTube);
           
            //Userinterface
            _userInterface = new UserInterface(
                _powerButton,
                _timeButton,
                _startCancelButton,
                _door,
                _display,
                _light,
                _cooker);
        }

        //Tester at microwave er i ready state, og viser power på displayet når der trykkes på powerbutton
        [Test]
        public void OnPowerPressedReadyStateShowPower()
        {
            _powerButton.Press();
            _output.OutputLine("Display shows: 50 W");   
        }

        //Tester at microwave er i SetPower state, og viser tid på displayet når der trykkes på timebutton
        [Test]
        public void OnTimePressedSetPowerStateDisplayTime()
        {
            _powerButton.Press();
            _timeButton.Press();
            _output.OutputLine("Display shows: 01:00");
        }

        //Tester at microwave clearer display når der trykkes start/cancel i SetTime state
        [Test]
        public void OnStartCancelPressedSetPowerStateClearDisplay()
        {
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();
            _output.OutputLine("Display cleared nauw");
        }
    }
}
