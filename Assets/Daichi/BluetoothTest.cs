using UnityEngine;
using System.IO.Ports;
using System.Runtime.InteropServices;
using System.Text;

public class BluetoothTest : MonoBehaviour
{
    [DllImport("NativeBluetoothPlugin", CharSet = CharSet.Ansi,CallingConvention = CallingConvention.StdCall)]
    private static extern bool GetNumber(string input,StringBuilder builder,int bufferSize);
    // Start is called before the first frame update
    void Start() {
        int bufferSize = 256;
        StringBuilder builder = new StringBuilder(bufferSize);

        if (GetNumber("ESP32Test2", builder, bufferSize)) {
            SerialPort serialPort1= new SerialPort();
            serialPort1.BaudRate = 115200;
            serialPort1.Parity = Parity.None;
            serialPort1.DataBits = 8;
            serialPort1.StopBits = StopBits.One;
            serialPort1.Handshake = Handshake.None;
            serialPort1.PortName = builder.ToString();
            serialPort1.Open();
            serialPort1.WriteLine("On");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
