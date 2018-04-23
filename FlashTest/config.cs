using System;
using System.Net;
using System.Collections.Generic;
using System.Text;

namespace FlashTest
{
    class config
    {
        public static string DatabaseFile = "";
        public static string DataSource
        {
            get
            {
                return string.Format("data source={0}", DatabaseFile);
            }
        }
        public static string txtIP = "";
        public static IPAddress clientIP
        {
            get
            {
                IPAddress yourAddress = IPAddress.Parse(txtIP);
                return yourAddress;
            }
        }
        public static string txtPort = "";
        public static int clientPort
        {
            get
            {
                int yourPort = int.Parse(txtPort);
                return yourPort;
            }
        }

    }

    [Serializable]  //表示这个类可以被序列化  
    public class User
    {
        string _loginName;
        public string LoginName
        {
            get { return _loginName; }
            set { _loginName = value; }
        }

        string _loingPassword;
        public string LoingPassword
        {
            get
            {
                if (_loingPassword != "")
                    return MyEncrypt.DecryptDES(_loingPassword);
                return _loingPassword;
            }
            set { _loingPassword = value; }
        }

        public User(string loginName, string loginPwd)
        {
            _loginName = loginName;
            _loingPassword = loginPwd;
        }
    }
}
