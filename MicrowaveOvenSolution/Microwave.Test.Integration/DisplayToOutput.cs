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
    public class DisplayToOutput
    {

        private IOutput _output;
        private IDisplay _display;

        [SetUp]
        public void SetUp()
        {
            _output = new Output();
            _display = new Display(_output);

        }

        //Sendes minut- og sekund værdier, vil disse blive vist
        [TestCase(0, 5)]
        [TestCase(1, 5)]
        [TestCase(12, 33)]
        public void ShowTime_DisplaysTime(int min, int sec)
        {
            _display.ShowTime(min, sec);
            _output.OutputLine(Arg.Is<string>(str => str.Contains($"{min}:{sec}")));

        }

        //Sendes watt-værdier vil disse blive vist
        [TestCase(500)]
        [TestCase(650)]
        public void ShowPower_DisplaysPower(int watt)
        {
            _display.ShowPower(watt);
            _output.OutputLine(Arg.Is<string>(str => str.Contains($"{watt} W")));
        }

        //Clear vil vise en besked bestående af "cleared"
        [Test]
        public void Clear_ClearsDisplay()
        {
            _display.Clear();
            _output.OutputLine(Arg.Is<string>(str => str.Contains("cleared")));

        }

    }
}
