using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bluetooth
{
    public class Bluetooth
    {

        IBluetooth instance;

        public Bluetooth() {
#if UNITY_ANDROID && !UNITY_EDITOR
        instance = new AndroidBluetooth();
#elif UNITY_STANDALONE_WIN && !UNITY_EDITOR
        instance = new WinBluetooth();
#else
            instance = new BluetoothStub();
#endif

        }

        public bool Connect(string deviceName)
        {
            return instance.Connect(deviceName);
        }
        public void Write(string str)
        {
            instance.Write(str);
        }

        public void Close()
        {
            instance.Close();
        }

        private class BluetoothStub:IBluetooth
        {
            public bool Connect(string deviceName)
            {
                return false;
            }
            public void Write(string str)
            {

            }

            public void Close()
            {

            }
        }
    }

    public interface IBluetooth
    {
        public bool Connect(string deviceName);
        public void Write(string str);

        public void Close();
    }

}
