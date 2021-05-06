using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwordIsWorldServer
{
    /// <summary>
    /// 向客户端发送消息时，可用于返回需要发送的信息
    /// </summary>
    class SendToClient
    {
        private static ArrayList BBAC = new ArrayList();//用于存储每个包

        private static ArrayList B = new ArrayList();//用于存储每个包
        void AS()
        {
            int x = 0;//计数
            byte[] a = null;//如果a数组有N个元素
            byte[] b = null;//用于存放100个元素的值
            for (int i = 0; i < a.Length; i++)//遍历N次
            {
                b[100%i] = a[i];                            //每遍历100次，重复存入数据
                if (100 %  (i + 1) == 0)//当存入100个元素时
                {
                    if(x != 0)B.Add(b);//向数组中添加一个包b
                    x++;
                }
            }
        }

        void BS()
        {
            int x = 0;//计数，判断当前存入list元素位置
            byte[][] BB = null;
            List<byte[]> s = new List<byte[]> { };
            for(int i = 0; i < BB.Length; i++)
            {
                byte[] a = BB[i];
                for (int ii = 0; ii < a.Length; ii++)
                {
                    s[x][ii] = BB[i][ii];
                    if (s[x].Length == 100)//当存入数据达到100时
                    {
                        x++;
                    }
                }
            }
        }

        /// <summary>
        /// 需要向客户端发送的消息
        /// </summary>
        /// <param name="STC">输入当前操作环节</param>
        /// <returns></returns>
        public string SendTC(string STC)
        {
            switch (STC)
            {
                case "超出人数上限":
                    return "Hi超出人数上限\r\n";
                case "该IP地址不允许访问":
                    return "Hi该IP地址不允许访问\r\n";
                case "连接服务端成功":
                    return "Hi连接服务端成功\r\n";
                case "以收到消息":
                    return "Hi以收到消息\r\n";
                case "游戏数据":
                    return "Hi游戏数据\r\n";
            }
            return "";
        }
    }
}
