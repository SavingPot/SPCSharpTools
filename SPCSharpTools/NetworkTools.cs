using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace SP.Tools
{
    public static class NetworkTools
    {
        /// <summary>
        /// 返回一个 (从开始端口到结束端口中) 未被占用端口.
        /// return an unoccupiedPort (from beginPort to endPort).
        /// </summary>
        /// <param name="beginPort"></param>
        /// <param name="endPort"></param>
        /// <returns></returns>
        public static bool GetUnoccupiedPort(ushort beginPort, out ushort port, ushort endPort = ushort.MaxValue)
        {
            port = beginPort;

            if (endPort < beginPort)
            {
                return false;
            }

            //如果端口被占用则切换下一个
            while (IsPortOccupied(beginPort))
            {
                port++;
            }

            if (port > endPort)
            {
                port = endPort;
                return false;
            }

            return true;
        }

        /// <summary>
        /// 如果指定端口被占用, 返回 true, 否则返回 false.
        /// If the point port is occupied, return true, or return false.
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        public static bool IsPortOccupied(ushort port)
        {
            return IPGlobalProperties.GetIPGlobalProperties().GetActiveTcpListeners().Any(p => p.Port == port);
        }

        /// <summary>
        /// 无论网络通不通都能获取到Ip
        /// </summary>
        /// <returns></returns>
        public static string GetLocalIPv4()
        {
            string localIP = string.Empty;
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
            {
                socket.Connect("8.8.8.8", 65530);
                IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
                localIP = endPoint.Address.ToString();
            }

            return localIP;
        }

        public static void PingComputers(Action<object, PingCompletedEventArgs> action, int timeout = 1000)
        {
            try
            {
                for (int i = 1; i < 255; i++)
                {
                    InternalPingComputers(action, timeout, i);
                }
            }
            catch
            {

            }
        }

        public static void ThreadPingComputers(Action<object, PingCompletedEventArgs> action, int timeout = 1000)
        {
            try
            {
                for (int i = 1; i < byte.MaxValue; i++)
                {
                    Thread thread = new Thread(() => InternalPingComputers(action, timeout, i));
                    thread.IsBackground = true;
                    thread.Start();
                }
            }
            catch
            {

            }
        }

        static void InternalPingComputers(Action<object, PingCompletedEventArgs> action, int timeout, int i)
        {
            Ping ping = new Ping();
            string pingIP = "192.168.1." + i.ToString();

            ping.PingCompleted += new PingCompletedEventHandler(action);

            ping.SendAsync(pingIP, timeout, null);
        }
    }
}