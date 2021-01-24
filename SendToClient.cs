using System;
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
