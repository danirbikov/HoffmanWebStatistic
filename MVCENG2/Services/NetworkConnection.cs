using System.Net;
using System.Runtime.InteropServices;

namespace HoffmanWebstatistic.Services
{
    public class NetworkConnection : IDisposable
    {
        private readonly string _networkPath;

        public NetworkConnection(string networkPath, NetworkCredential credentials)
        {
            _networkPath = networkPath;
            ConnectToRemote(networkPath, credentials);
        }

        public void Dispose()
        {
            DisconnectRemote(_networkPath);
        }

        private void ConnectToRemote(string networkPath, NetworkCredential credentials)
        {
            NetResource netResource = new NetResource
            {
                Scope = ResourceScope.GlobalNetwork,
                ResourceType = ResourceType.Disk,
                DisplayType = ResourceDisplaytype.Share,
                RemoteName = networkPath
            };

            int result = WNetAddConnection2(
                netResource,
                credentials.Password,
                credentials.UserName,
                0);

            if (result != 0)
            {
                throw new System.ComponentModel.Win32Exception(result);
            }
        }

        private void DisconnectRemote(string networkPath)
        {
            int result = WNetCancelConnection2(networkPath, 0, true);
            if (result != 0)
            {
                throw new System.ComponentModel.Win32Exception(result);
            }
        }

        [DllImport("mpr.dll")]
        private static extern int WNetAddConnection2(
            NetResource netResource,
            string password,
            string username,
            int flags);

        [DllImport("mpr.dll")]
        private static extern int WNetCancelConnection2(
            string name,
            int flags,
            bool force);
    }

    [StructLayout(LayoutKind.Sequential)]
    public class NetResource
    {
        public ResourceScope Scope;
        public ResourceType ResourceType;
        public ResourceDisplaytype DisplayType;
        public int Usage;
        public string LocalName;
        public string RemoteName;
        public string Comment;
        public string Provider;
    }

    public enum ResourceScope : int
    {
        Connected = 1,
        GlobalNetwork,
        Remembered,
        Recent,
        Context
    }

    public enum ResourceType : int
    {
        Any = 0,
        Disk = 1,
        Print = 2,
        Reserved = 8,
    }

    public enum ResourceDisplaytype : int
    {
        Generic = 0x0,
        Domain = 0x01,
        Server = 0x02,
        Share = 0x03,
        File = 0x04,
        Group = 0x05,
        Network = 0x06,
        Root = 0x07,
        Shareadmin = 0x08,
        Directory = 0x09,
        Tree = 0x0a,
        Ndscontainer = 0x0b
    }
}
