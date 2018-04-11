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
        class IT9UIToOther
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

                //Substitudes
                _powerButton = Substitute.For<IButton>();
                _timeButton = Substitute.For<IButton>();
                _startCancelButton = Substitute.For<IButton>();
                _door = Substitute.For<IDoor>();

                //CookControler
                _powerTube = new PowerTube(_output);
                _timer = new Timer();

                //Userinterface
                _display = new Display(_output);
                _light = new Light(_output);
                _cooker = new CookController(_timer, _display, _powerTube);
                _userInterface = new UserInterface(_powerButton, _timeButton, _startCancelButton, _door, _display, _light, _cooker);
            }

            //Same Tests as IT7 and IT8, but without CookControler as Substitude

            #region Light

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

            #endregion


            #region Display

            //Tester at microwave er i ready state, og viser power på displayet når der trykkes på powerbutton
            [Test]
            public void OnPowerPressedReadyStateShowPower()
            {
                _powerButton.Press();
                _output.OutputLine(Arg.Is<string>(str => str.Contains("50 W")));
            }

            //Tester at microwave er i SetPower state, og viser tid på displayet når der trykkes på timebutton
            [Test]
            public void OnTimePressedSetPowerStateDisplayTime()
            {
                _powerButton.Press();
                //SetTime state
                _timeButton.Press();
                _output.OutputLine(Arg.Is<string>(str => str.Contains("01:00")));
            }

            //Tester at microwave clearer display når der trykkes start/cancel i SetTime state
            [Test]
            public void OnStartCancelPressedSetPowerStateClearDisplay()
            {
                _powerButton.Press();
                _startCancelButton.Press();
                _output.OutputLine(Arg.Is<string>(str => str.Contains("Display cleared")));
            }

            #endregion
        }
}
