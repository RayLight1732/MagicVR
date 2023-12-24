using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bluetooth;
using System.Runtime.InteropServices;
using System.Text;
using System.IO.Ports;

namespace Bluetooth
{
    public class WinBluetooth : IBluetooth
    {
        [DllImport("NativeBluetoothPlugin", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        private static extern bool GetNumber(string input, StringBuilder builder, int bufferSize);

        private SerialPort serialPort;
        public bool Connect(string deviceName)
        {
            int bufferSize = 256;
            StringBuilder builder = new StringBuilder(bufferSize);

            if (GetNumber("ESP32Test2", builder, bufferSize))
            {
                serialPort = new SerialPort();
                serialPort.BaudRate = 115200;
                serialPort.Parity = Parity.None;
                serialPort.DataBits = 8;
                serialPort.StopBits = StopBits.One;
                serialPort.Handshake = Handshake.None;
                serialPort.PortName = builder.ToString();
                serialPort.Open();
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Write(string str)
        {
            serialPort.Write(str);
        }

        public void Close()
        {
            serialPort?.Close();
        }
    }
}
