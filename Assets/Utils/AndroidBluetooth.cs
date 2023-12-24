using UnityEngine;
using UnityEngine.Android;

namespace Bluetooth
{
    public class AndroidBluetooth : IBluetooth
    {
        AndroidJavaObject cls;
        private bool connected = false;
        public bool Connect(string deviceName)
        {
            cls = new AndroidJavaObject("com.jp.ray.mybluetooth.Blue", deviceName);
            Debug.Log("devides:" + cls.Call<string>("getAllDevices"));

            if (!Permission.HasUserAuthorizedPermission("android.permission.BLUETOOTH_CONNECT"))
            {
                Permission.RequestUserPermission("android.permission.BLUETOOTH_CONNEC");
                return false;
            }
            else
            {
                connected = true;
                return cls.Call<bool>("connect");
            }
        }

        public void Write(string str)
        {
            if (connected)
            {
                cls.Call("sendMessage", str);
            }
        }

        public void Close()
        {

        }
    }
}
