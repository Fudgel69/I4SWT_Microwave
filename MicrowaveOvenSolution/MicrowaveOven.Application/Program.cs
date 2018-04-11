using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;

namespace MicrowaveOven.Application
{
    class Program
    {

        private static IButton _powerButton;
        private static IButton _startCancelButton;
        private static IButton _timeButton;
        private static IDisplay _display;
        private static IDoor _door;
        private static ILight _light;
        private static IOutput _output;
        private static IPowerTube _powerTube;
        private static ITimer _timer;
        private static ICookController _cooker;
        private static IUserInterface _userInterface;

        static void Main(string[] args)
        {
            _output = new Output();
            _powerButton = new Button();
            _timeButton = new Button();
            _startCancelButton = new Button();
            _powerTube = new PowerTube(_output);
            _timer = new Timer();
            _door = new Door();
            _display = new Display(_output);
            _light = new Light(_output);
            _cooker = new CookController(_timer, _display, _powerTube);
            _userInterface = new UserInterface(_powerButton, _timeButton, _startCancelButton, _door, _display, _light,
                _cooker);

            _cooker.UI = _userInterface;


            // Writeline
            Console.WriteLine("Welcome to MicroWaveOven");
            Console.WriteLine("Press P to increase Power (700 max)");
            Console.WriteLine("Press T to set time");
            Console.WriteLine("Press S for Start/Cancel");
            Console.WriteLine("Press O to Open door");
            Console.WriteLine("Press C to Close door");
            Console.WriteLine("Press E to Exit application");



            // Readline of Choices
            bool working = true;

            while (working)
            {
                var key = Console.ReadKey(true);

                switch (key.KeyChar)
                {
                    case 'p':
                        _powerButton.Press();
                        break;
                    case 't':
                        _timeButton.Press();
                        break;
                    case 's':
                        _startCancelButton.Press();
                        break;
                    case 'o':
                        _door.Open();
                        break;
                    case 'c':
                        _door.Close();
                        break;
                    case 'e':
                        working = false;
                        break;
                }
            }
        }
    }
}