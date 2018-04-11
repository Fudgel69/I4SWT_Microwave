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
        class UIToOther
        {

            //Button
            private IButton _powerButton;
            private IButton _timeButton;
            private IButton _startCancelButton;
            private IUserInterface _userInterface;

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
                //Button
                _output = new Output();
                _powerButton = new Button();
                _timeButton = new Button();
                _startCancelButton = new Button();

                //CookControler
                _powerTube = new PowerTube(_output);
                _timer = new Timer();

                //Userinterface
                _door = new Door();
                _display = new Display(_output);
                _light = new Light(_output);
                _cooker = new CookController(_timer, _display, _powerTube);
                _userInterface = new UserInterface(_powerButton, _timeButton, _startCancelButton, _door, _display, _light, _cooker);
            }


            #region Buttons

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
            #endregion


            #region Door

            //Door
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
            #endregion


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
