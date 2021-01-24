using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwordIsWorldServer
{
    static class Data
    {

        private static int maxPeople,timeInterval,threadCount;//服务器最大人数,发送频率,线程数
        private static string password;//白名单
        private static string whiteList;//密码

        public static int MaxPeople { get => maxPeople; set { if(value >  0)maxPeople = value;} }
        public static string WhiteList { get => whiteList; set => whiteList = value; }
        public static string Password { get => password; set => password = value; }
        public static int TimeInterval { get => timeInterval; set => timeInterval = value; }
        public static int ThreadCount { get => threadCount; set => threadCount = value; }

        public static ArrayList ArrayWhiteList = new ArrayList();//声明一个数组用于储存白名单

        static Dictionary<string, int> ClientData = new Dictionary<string, int> { };//用于存储客户的信息，可通过网络节点号查找
    }
}
