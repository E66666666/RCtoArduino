using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Ports;

namespace RCBT
{
    public class ArduinoCommunicator
    {
        private string markerBegin;
        private string markerSeparate;
        private string markerEnd;
        private MessageBuilder messageBuilder;

        public SerialPort serialPort { get; private set; }
        public string CommandBuffer { private get; set; }
        public string ReturnBuffer { get; private set; }

        public ArduinoCommunicator(string portName, int baudRate)
        {
            serialPort = new SerialPort();
            serialPort.PortName = portName;
            serialPort.BaudRate = baudRate;
            markerBegin = "$";
            markerSeparate = ":";
            markerEnd = "%";
            messageBuilder = new MessageBuilder(markerBegin, markerEnd);
        }

        public void SendMessage(string message)
        {
            if (serialPort.IsOpen)
            {
                serialPort.Write(message);
            }
            else
            {
                throw new IOException("serialPort");
            }
        }

        private void buildMessage()
        {
            if (serialPort.IsOpen && serialPort.BytesToRead > 0)
            {
                String dataFromSocket = serialPort.ReadExisting();
                messageBuilder.Append(dataFromSocket);
            }
        }

        public string ReadMessage()
        {
            buildMessage();
            return (messageBuilder.FindAndRemoveNextMessage());
        }

        public static string[] GetAvailablePorts()
        {
            String[] ports = SerialPort.GetPortNames();
            Array.Sort(ports);
            return (ports);
        }

        public void ClosePort()
        {
            if (serialPort.IsOpen)
            {
                serialPort.Close();
            }
        }
    }
}
