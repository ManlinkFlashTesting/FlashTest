using System.Net;

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
}
