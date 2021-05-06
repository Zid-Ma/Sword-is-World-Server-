using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SwordIsWorldServer
{
    static class Data
    {
        private static int maxPeople,timeInterval,threadCount;//服务器最大人数,发送频率,线程数
        private static string password;//密码
        private static string whiteList;//白名单
        private static ArrayList whiteList_Array = new ArrayList();//声明一个数组用于储存白名单
        private static ArrayList codeList_Array = new ArrayList();//已发送客户验证码的名单
        private static Dictionary<string, int> verification_Code = new Dictionary<string, int> { };//用于存储客户验证码,账号，验证码
        public static int MaxPeople { get => maxPeople; set { if(value >  0)maxPeople = value;} }
        public static string WhiteList { get => whiteList; set => whiteList = value; }
        public static string Password { get => password; set => password = value; }
        public static int TimeInterval { get => timeInterval; set => timeInterval = value; }
        public static int ThreadCount { get => threadCount; set => threadCount = value; }
        public static ArrayList WhiteList_Array { get => whiteList_Array; set => whiteList_Array = value; }
        public static ArrayList CodeList_Array { get => codeList_Array; set => codeList_Array = value; }
        public static Dictionary<string, int> Verification_Code { get => verification_Code; set => verification_Code = value; }

        /// <summary>
        /// 获得换行
        /// </summary>
        /// <returns></returns>
        public static string getEnter()
        {
            return System.Environment.NewLine;
            return "\r";
        }

        /// <summary>
        /// 获得第一个换行符号
        /// </summary>
        /// <returns></returns>
        public static char getEnter_First()
        {
            return '\r';
            return System.Environment.NewLine.Substring(0, 1).ToCharArray()[0];
        }

        /// <summary>
        /// 去掉相应字符
        /// </summary>
        /// <returns></returns>
        public static string removeChar(string s = " ", char c = ' ')
        {
            string z = null;
            try
            {
                foreach (char a in s)
                {
                    if (a != c)
                    {
                        z += a;
                    }
                }
            }
            catch
            {

            }
            return z;
        }

        /// <summary>
        /// 获取用户名
        /// </summary>
        /// <param name="a">账号</param>
        /// <returns></returns>
        public static string getUserName(string a)
        {
            return Script.Verification.File_account(a, "UserName.txt");
        }

    }
}
