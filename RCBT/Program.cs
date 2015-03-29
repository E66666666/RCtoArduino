using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Threading;

namespace RCBT {
    class Program {

        static void Main(string[] args) {
            string[] ports = ArduinoCommunicator.GetAvailablePorts();
            string comPort = "";
            foreach(String port in ports) {
                Console.WriteLine(port);
            }
            do
            {
                Console.WriteLine("Choose a port.");
                comPort = Console.ReadLine();
            } while (!comPort.StartsWith("COM"));

            ArduinoCommunicator arduinoCommunicator = new ArduinoCommunicator(comPort, 9600);
            arduinoCommunicator.serialPort.Open();
            Console.WriteLine("Prolly connected now.");

            bool running = true;
            

            while (running) {
                GamePadState currentState = GamePad.GetState(PlayerIndex.One);
                float forward = currentState.Triggers.Right;
                float back = currentState.Triggers.Left;
                float steer = currentState.ThumbSticks.Left.X;
                if (forward > 0.01 && back < 0.1)
                {
                    int fwd = (int)(forward * (float)255);
                    string message = messageFormatter("FWD", fwd.ToString());
                    string message2 = messageFormatter("BWD", "0");
                    arduinoCommunicator.SendMessage(message);
                    Console.WriteLine(message);
                }
                if (back > 0.01 && forward < 0.1)
                {
                    int bwd = (int)(back * (float)255);
                    string message = messageFormatter("BWD", bwd.ToString());
                    string message2 = messageFormatter("FWD", "0");
                    arduinoCommunicator.SendMessage(message);
                    Console.WriteLine(message);
                }
                if (steer > 0.01)
                {
                    int kek = (int)(steer * (float)255);
                    string message = messageFormatter("RITE", kek.ToString());
                    arduinoCommunicator.SendMessage(message);
                    Console.WriteLine(message);
                }
                if (steer < -0.01)
                {
                    int kek = (int)(-steer * (float)255);
                    string message = messageFormatter("LEFT", kek.ToString());
                    arduinoCommunicator.SendMessage(message);
                    Console.WriteLine(message);
                }
                if (currentState.Buttons.A == ButtonState.Pressed)
                {
                    string message = messageFormatter("BRK", "pls");
                    arduinoCommunicator.SendMessage(message);
                    Console.WriteLine("Braking");
                }
                Thread.Sleep(50);
            }
        }

        static string messageFormatter(string command, string data)
        {
            string begin = ">";
            string seperator = ":";
            string end = ";";
            return (begin + command + seperator + data + end);
        }
    }
}
