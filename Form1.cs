using SwordIsWorldServer.Script;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Configuration;
using System.Net.Sockets;//引用命名空间,套接字
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;//引用命名空间，线程
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SwordIsWorldServer
{
    public partial class Form1 : Form
    {
        //当前静态窗体
        static Form1 form;
        //创建socket//定义一个套接字用于监听客户端发来的消息，包含三个参数（IP4寻址协议，流式连接，Tcp协议）
        static Socket SIWServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        //实例化的键为string类型，值为Socket套接字,//定义一个集合，存储客户端信息。下
        private static Dictionary<string, Socket> clientConnectionItems = new Dictionary<string, Socket> { };//Dictionary (词典)用于储存键和值
        //用于储存客户在的用户名.通过网络节点号RemoteEndPoint查找
        private static Dictionary<string, string> clientUserName = new Dictionary<string, string> { };
        //用于储存用户数据
        private static Dictionary<string, string> clientData = new Dictionary<string, string> { };
        /// <summary>
        /// 当前窗体对象
        /// </summary>
        public static Form1 Form_this { get => form; set => form = value; }
        public static Dictionary<string, Socket> ClientConnectionItems { get => clientConnectionItems; set => clientConnectionItems = value; }
        public static Dictionary<string, string> ClientUserName { get => clientUserName; set => clientUserName = value; }
        public static Dictionary<string, string> ClientData { get => clientData; set => clientData = value; }

        private static ArrayList sendMData = new ArrayList();
        /// <summary>
        /// Count以获取里面包含的元素个数,待发送数据
        /// </summary>
        public static ArrayList SendMData { get => sendMData; set => sendMData = value; }

        private static ArrayList sendTMData = new ArrayList();
        /// <summary>
        /// 待发送文本数据
        /// </summary>
        public static ArrayList SendTMData { get => sendTMData; set { if (sendTMData.Count < MaxSendData * 3 + 10) sendTMData = value; } }

        private static int maxSendData;
        /// <summary>
        /// 最大待发送数据
        /// </summary>
        public static int MaxSendData { get => maxSendData; set => maxSendData = value; }

        //服务器IP地址，端口号
        private static string serverIP,serverPort;
        /// <summary>
        /// 服务器IP地址
        /// </summary>
        string ServerIP
        {
            get
            {
                if (textBoxIP.Text != "" && checkBoxIP.Checked == true) { return textBoxIP.Text; }//当IP填写并选择使用时
                else { return "127.0.0.1"; }
            }
            set
            {
                serverIP = value;
            }
        }
        /// <summary>
        /// 服务器端口号
        /// </summary>
        string ServerPort
        {
            get
            {
                if (textBoxPort.Text != "" && checkBoxPort.Checked == true) { return textBoxPort.Text; }//当端口号填写并选择使用时
                else { return "10629"; }
            }
            set
            {
                serverPort = value;
            }
        }

        static int clickTime;
        /// <summary>
        /// 定义一个时钟计时
        /// </summary>
        public static int ClickTime { get => clickTime; set => clickTime = value; }

        /// <summary>
        /// 起始执行
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            //外部文件初始化
            {
                Data_Save.Initialization();
            }
            //数值初始化
            {
                //时钟计时
                ClickTime = 0;
                //默认信息发送频率
                if (Data.TimeInterval == 0) Data.TimeInterval = 50;
                //服务器最大链入人数默认为100
                Data.MaxPeople = 100;
                //最大待发送数据
                MaxSendData = 10;
                //线程数，控制线程
                Data.ThreadCount = 10;
                //移除所有键与值，初始化
                ClientConnectionItems.Clear();
                ClientUserName.Clear();
                ClientData.Clear();
                //基础位，防止被清除
                SendMData.Add("XyKs");
                SendTMData.Add("XyKs");
                Data.CodeList_Array.Add("XyKs");
            }
            //Socket初始化
            {
                Ipv6Element ipv6Element = new Ipv6Element();
                if (ipv6Element.Enabled)
                {
                    //使用IPv6地址协议
                    SIWServer = new Socket(AddressFamily.InterNetworkV6, SocketType.Stream, ProtocolType.Tcp);
                }
            }
        }

        /// <summary>
        /// 当窗体加载完成后执行该函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            //为窗体类型赋值为当前窗体
            Form_this = this;
            //关闭文本框的非法线程操作检查
            TextBox.CheckForIllegalCrossThreadCalls = false;
        }

        /// <summary>
        /// 启动服务器与关闭服务器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOpenServer_Click(object sender, EventArgs e)
        {
            textBoxMain.Text = "以按下按钮“服务器”";
            //判断服务器是否开启（当前按钮状态）
            if (buttonOpenServer.Text == "打开服务器")
            {
                //调用函数,打开服务器
                if (OpenServer())
                {
                    buttonOpenServer.Text = "关闭服务器";
                    textBoxMain.Text = "正在打开监听程序";
                }
                else//打开失败时
                {
                    
                }
            }
            else//服务器处于开启状态时
            {
                //调用关闭服务器函数，关闭成功时
                if (CloseServer())
                {
                    buttonOpenServer.Text = "打开服务器";
                }
                else//关闭失败时
                {

                }
            }
        }

        /// <summary>
        /// 打开服务器
        /// </summary>
        /// <returns></returns>
        static bool OpenServer()
        {
            //尝试打开服务器，返回true
            try
            {
                //套接字启用
                {
                    if (Form_this.ServerIP.Length > 16)
                    {
                        //认定IP地址为IPv6
                        SIWServer = new Socket(AddressFamily.InterNetworkV6, SocketType.Stream, ProtocolType.Tcp);
                    }
                    else
                    {
                        //认定IP地址为IPv4
                        SIWServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    }
                    //将string类型的IP地址转为IPAddress储存,调用ServerIP属性的值
                    IPAddress address = IPAddress.Parse(Form_this.ServerIP);
                    //将端口号由string转为int类型
                    int port = int.Parse(Form_this.ServerPort);
                    MessageBox.Show(port.ToString());
                    //将IP和端口号绑定到节点上
                    EndPoint point = new IPEndPoint(address, port);
                    //监听绑定的端口节点.使用上面已经声明好的套接字
                    SIWServer.Bind(point);
                    //限制监听队列的长度
                    SIWServer.Listen(100);
                }
                //打开新线程
                {
                    //创建一个新的线程，调用监听客户端的连接请求
                    Thread threadWatchConnecting = new Thread(WatchConnecting);
                    //将该线程设置为后台运行，线程与主程序同步运行，受主程序影响
                    threadWatchConnecting.IsBackground = true;
                    //启动该线程，监听客户端的连接请求
                    threadWatchConnecting.Start();

                    //Thread threadSendMessageToAllClient = new Thread(SendMessageToClient);//创建一个新的线程，用于向所有客户端发送信息
                    //threadSendMessageToAllClient.IsBackground = true;//将该线程设置为后台运行，线程与主程序同步运行，受主程序影响
                    //threadSendMessageToAllClient.Start();//启动该线程

                    //Thread threadSendMessageBuding = new Thread(SendMessageBuding);//创建一个新的线程，用于向所有客户端发送信息
                    //threadSendMessageBuding.IsBackground = true;//将该线程设置为后台运行，线程与主程序同步运行，受主程序影响
                    //threadSendMessageBuding.Start();//启动该线程
                }
                return true;
            }
            catch(Exception e)//报错时返回false
            {
                //打开消息窗口返回错误信息
                MessageBox.Show("打开服务器环节" + e.Message.ToString());
                return false;
            }
        }

        /// <summary>
        /// 监听客户端发送的连接请求，查看连接
        /// </summary>
        static void WatchConnecting()
        {
            //新建一个套接字，用于查看客户链接请求
            Socket connection = null;
            //不断监听客户请求
            while (true)
            {
                //尝试进行链接的等待
                try
                {
                    Form_this.textBoxMain.AppendText(Data.getEnter() + "服务器已打开");
                    //等待先前以创建好的套接字有链接进入,好像会暂停以等待链接进入来着
                    connection = SIWServer.Accept();
                }
                catch (Exception e)//当套接字等待出现异常时
                {
                    //打开消息窗口返回错误信息
                    MessageBox.Show("监听客户端连接请求环节" + e.Message.ToString());
                    //并直接跳出while语句,结束当前循环
                    break;
                }
                //如果没有超出最大链入人数限制
                if (ClientConnectionItems.Count < Data.MaxPeople)
                {
                    //获取链接进入客户的端点,将网络点表示为IP和端口，获取链入客户端的IP地址与端口号
                    IPAddress clientIP = (connection.RemoteEndPoint as IPEndPoint).Address;
                    //获取链接进入客户的端口号
                    int clientPort = (connection.RemoteEndPoint as IPEndPoint).Port;
                    Verification_Information iv = new Verification_Information();
                    //传入客户IP信息如果被白名单认可的话(不在黑名单)
                    if (iv.IPV(clientIP.ToString()))
                    {
                        //实例化向客户发送消息的类
                        SendToClient stc = new SendToClient();
                        string sendMsg = stc.SendTC("连接服务端成功");
                        //string sendMsg = "连接服务端成功！" + Data.getEnter_First() + "本地IP:" + clientIP + "，本地端口" + clientPort.ToString();//新建一个string类型,里面存入之后要发送给客户的信息
                        //将需要发送的信息转为byte[]的形式，以便于发送
                        byte[] arrSendMsg = Encoding.UTF8.GetBytes(sendMsg);
                        //通过客户连接上的套接字,发送信息至链接成功的客户处
                        connection.Send(arrSendMsg);

                        //将客户的网络节点号转为string类型
                        string remoteEndPoint = connection.RemoteEndPoint.ToString();
                        //显示连接状况.使用AppendText方法向文本框中追加内容
                        Form_this.textBoxMain.AppendText("与" + remoteEndPoint + "客户端建立连接！" + Data.getEnter());

                        //将客户的IP与端口储存到新建的变量netpoint中
                        IPEndPoint netPoint = connection.RemoteEndPoint as IPEndPoint;
                        //除了上面的储存外也可以用该储存方法
                        //IPEndPoint netPoint = new IPEndPoint(clientIP,clientPort);

                        //将方法装入pts变量中，以便于下面打开一个新的线程
                        ParameterizedThreadStart PTS = new ParameterizedThreadStart(recv);
                        //创建新的线程，实例化
                        Thread thread = new Thread(PTS);
                        //设置线程为后台程序
                        thread.IsBackground = true;
                        //打开创建好的线程，参数传入为当前客户的套接字
                        thread.Start(connection);
                    }
                    else
                    {
                        //实例化向客户发送消息的类
                        SendToClient stc = new SendToClient();
                        //需要发送的信息赋值给新建的变量
                        string sendMsg = stc.SendTC("该IP地址不允许访问");
                        Form_this.textBoxMain.AppendText("客户链入请求——该IP地址不允许访问*1");
                        //将需要发送的信息转为byte[]的形式，以便于发送
                        byte[] arrSendMsg = Encoding.UTF8.GetBytes(sendMsg);
                        //向客户端发送信息
                        connection.Send(arrSendMsg);
                    }
                }
                else
                {
                    //实例化向客户发送消息的类
                    SendToClient stc = new SendToClient();
                    //需要发送的信息赋值给新建的变量
                    string sendMsg = stc.SendTC("超出人数上限");
                    Form_this.textBoxMain.AppendText("客户链入请求——超出人数上限*1");
                    //将需要发送的信息转为byte[]的形式，以便于发送
                    byte[] arrSendMsg = Encoding.UTF8.GetBytes(sendMsg);
                    //向客户端发送人数超过限制的信息
                    connection.Send(arrSendMsg);
                    CloseSC(connection);
                }
            }
        }

        /// <summary>
        /// 关闭客户链接
        /// </summary>
        /// <param name="socketclientpara">套接字</param>
        static void CloseSC(object socketclientpara)
        {
            //将传入的已链入客户套接字参数存入该新建的Socket类型的变量socketServer中
            Socket socketServer = socketclientpara as Socket;
            try
            {
                socketServer.Close();
            }
            catch
            {
                Form_this.textBoxMain.AppendText("关闭客户链接时失败");
            }
        }

        /// <summary>
        /// 与客户交流环节
        /// </summary>
        /// <param name="socketclientpara"></param>
        static void recv(object socketclientpara)
        {
            //将传入的已链入客户套接字参数存入该新建的Socket类型的变量socketServer中
            Socket socketServer = socketclientpara as Socket;
            //先将客户的IP和端口号储存起来，格式如127.0.0.1:10629
            string SSREP = socketServer.RemoteEndPoint.ToString();
            //获取接收到客户最后一次发来消息的时间。之后判断时间超出则断开链接
            SendTMData.Add("TF" + Data.TimeInterval);//频率设置
            //不断循环接收客户信息
            while (true)
            {
                //这个是用来保存：接收到了的，但是还没有结束的消息
                StringBuilder sb = new StringBuilder();
                //创建一个大小为1M的内存缓冲区 (1024*1024字节)
                byte[] arrServerRecMsg = new byte[1024];
                //判断接收到的客户信息
                try
                {
                    //接收套接字变量socketServer的数据并赋给int类型变量length
                    int length = socketServer.Receive(arrServerRecMsg);
                    //将接受到的数据转化为string类型，UTF8格式，从第0位开始，在内存缓冲区中转化
                    string strRevMsg = Encoding.UTF8.GetString(arrServerRecMsg, 0, length);
                    form.textBoxMainBack.AppendText(strRevMsg);
                    lock(strRevMsg){
                        //遍历收到的信息,截取每一个数据包
                        for (int i = 0; i < strRevMsg.Length;)
                        {
                            //是否截取到数据包尾部字符
                            if (strRevMsg.Substring(i, 1) != Data.getEnter_First().ToString())
                            {
                                //如果溢出
                                if (i > 1024) { break; }
                                sb.Append(strRevMsg.Substring(i, 1));
                                i++;
                            }
                            else
                            {
                                //如果数值溢出
                                if (sb.Length > 256) break;
                                //接收到的信息
                                string RMessage = sb.ToString();
                                sb.Clear();
                                //检测客户是否已经被保存到服务器信息中
                                if (ClientConnectionItems.ContainsKey(SSREP))
                                {
                                    //form.textBoxMain.AppendText(RMessage);
                                    //如果客户传来消息的开头为"Hi"的话,后面接用户想要在聊天框发送的消息
                                    if (RMessage.Substring(0, 2) == "Hi")
                                    {
                                        Form_this.textBoxMainBack.AppendText(Data.getEnter() + "客户端:" + socketServer.RemoteEndPoint + ",time:" + GetCurrentTime() + "\r\n" + RMessage + "\r\n\n");//将接收到的信息转化并显示到窗体中
                                                                                                                                                                               //SendToClient STC = new SendToClient();//实例化对象
                                                                                                                                                                               //socketServer.Send(Encoding.UTF8.GetBytes(STC.SendTC("以收到消息")));//并且发送数据给客户，发送码
                                        string clientUN;
                                        //获取该线程用户的用户名
                                        ClientUserName.TryGetValue(SSREP, out clientUN);

                                        //向需要发送的消息中添加
                                        SendTMData.Add("Hi" + clientUN + ":" + RMessage.Substring(2));
                                    }
                                    //用户的位置与动作等
                                    else if (RMessage.Substring(0, 2) == "PS")
                                    {
                                        string s = "";
                                        //最小
                                        int mixLength = 0;
                                        //遍历整个信息
                                        for (int x = 0; x < RMessage.Length; x++)
                                        {
                                            if (RMessage.Substring(x, 1) == "E")
                                            {
                                                //截取不同玩家的信息，保存
                                                s = RMessage.Substring(mixLength, x + 1 - mixLength);
                                                mixLength = x + 1;
                                                //确认是否包含指定键
                                                if (ClientData.ContainsKey(SSREP))
                                                {
                                                    //覆盖之前已有信息
                                                    ClientData[SSREP] = s.Substring(2);
                                                }
                                                //如果没有用户信息
                                                else
                                                {
                                                    //将用户信息储存起来,用户名和信息
                                                    ClientData.Add(SSREP, s.Substring(2));
                                                }
                                            }//如果截取到结尾字符"E"
                                        }

                                        //form.textBoxMainBack.AppendText(Data.getEnter_First() + "客户端:" + socketServer.RemoteEndPoint + ",time:" + GetCurrentTime() + "\r\n" + strSRecMsg + "\r\n\n");
                                        //form.textBoxMain.AppendText(clientData[SSREP]);
                                    }
                                    //结束当前线程,与用户连接
                                    else if (RMessage.Substring(0, 2) == "EN")
                                    {
                                        sb.Clear();
                                        sb.Append("EN");
                                        //添加用户终结信息
                                        SendTMData.Add("EN" + SSREP);
                                        //抛出异常
                                        throw new ArgumentOutOfRangeException("用户请求断开连接");
                                        break;
                                    }

                                }
                                //如果客户当前套接字还未被保存,则判断传入客户信息是否为登录信息
                                else
                                {
                                    Verification_Information iv = new Verification_Information();
                                    if (RMessage != null)
                                    {
                                        //需要调用账号密码检测函数,当客户发送登录的消息请求时,如果发送信息正确
                                        if (iv.V_initial_information(RMessage))
                                        {
                                            //当发送的信息为注册或获取验证码时
                                            if (RMessage.Substring(0, 4) == "Join" || RMessage.Substring(0, 4) == "Code")
                                            {
                                                //将消息显示到主文本框：
                                                {
                                                    //请求+:+用户IP地址与端口
                                                    Form_this.textBoxMain.AppendText(
                                                        Data.getEnter()
                                                        + "收到请求:"
                                                        + RMessage.Substring(0, 4).Replace("\t", "")
                                                        + ":"
                                                        + socketServer.RemoteEndPoint.ToString()
                                                        + Data.getEnter()
                                                        );
                                                    Form_this.textBoxMainBack.AppendText(
                                                        "用户请求已成功:"
                                                        + RMessage.Substring(0, 4).Replace("\t", "")
                                                        + Data.getEnter()
                                                        + socketServer.RemoteEndPoint.ToString()
                                                        + ":"
                                                        + RMessage
                                                        + Data.getEnter()
                                                        );
                                                }
                                                switch (RMessage.Substring(0, 4))
                                                {
                                                    case "Join":
                                                        //向客户发送消息：已成功注册
                                                        socketServer.Send(Encoding.UTF8.GetBytes("JoinTrue"));
                                                        break;
                                                    case "Code":
                                                        //向客户发送消息：已发送验证码
                                                        socketServer.Send(Encoding.UTF8.GetBytes("CodeTrue"));
                                                        break;
                                                }
                                            }
                                            else
                                            {
                                                //存入信息到内存
                                                {
                                                    //将客户的网络节点号转为string类型
                                                    string remoteEndPoint = socketServer.RemoteEndPoint.ToString();
                                                    //显示连接状况.使用AppendText方法向文本框中追加内容
                                                    textboxmain_Append(Data.getEnter() + "成功与" + remoteEndPoint + "客户端建立连接！" + Data.getEnter());
                                                    //添加客户端信息到一开始创建好了的Dictionary集合中,第一个参数为string类型，第二个参数为套接字Socket类型
                                                    ClientConnectionItems.Add(remoteEndPoint, socketServer);
                                                    //记录去掉了尾部的字符串
                                                    string UNT = null;
                                                    switch (RMessage.Substring(0, 4))
                                                    {
                                                        case "0099":
                                                            //群聊
                                                            {
                                                                //暂时保存当前用户名
                                                                string UN = RMessage.Substring(5, 16);
                                                                //遍历出真正的用户名并记录
                                                                for (int UNi = 0; UNi < UN.Length;)
                                                                {
                                                                    //如果该字符串没有截取到结尾字符，则直接赋值
                                                                    if (UNi == UN.Length - 1)
                                                                    {
                                                                        UNT = UN;
                                                                        break;
                                                                    }
                                                                    //如果截取到表示结尾字符"\0"
                                                                    if (UN.Substring(UNi, 1) == "\t")
                                                                    {
                                                                        UNT = UN.Substring(0, UNi + 1);
                                                                        break;
                                                                    }
                                                                    UNi++;
                                                                }

                                                                //由文件目录中找到真正的用户名
                                                                UNT = Data.getUserName(RMessage.Substring(57, 12));

                                                                Form_this.textBoxMainBack.AppendText(Data.getEnter() +
                                                                //检测登录方式,0099为群聊
                                                                "登录方式:" + RMessage.Substring(0, 4) + Data.getEnter() +
                                                                //检测用户名,
                                                                "用户名:" + UNT + Data.getEnter() +
                                                                //检测用户密码
                                                                "用户密码:" + RMessage.Substring(23, 32) + Data.getEnter() +
                                                                //检测用户账号);
                                                                "用户账号:" + RMessage.Substring(57, 12));
                                                            }
                                                            UNT = (UNT + "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t").Substring(0, 16);
                                                            break;
                                                        default:
                                                            //剑与世界
                                                            {
                                                                //暂时保存当前用户名
                                                                string UN = RMessage.Substring(5, 13);
                                                                //遍历出真正的用户名并记录
                                                                for (int UNi = 0; UNi < UN.Length;)
                                                                {
                                                                    //如果该字符串没有截取到结尾字符，则直接赋值
                                                                    if (UNi == UN.Length - 1)
                                                                    {
                                                                        UNT = UN;
                                                                        break;
                                                                    }
                                                                    //如果截取到表示结尾字符"\0"
                                                                    if (UN.Substring(UNi, 1) == "\t")
                                                                    {
                                                                        UNT = UN.Substring(0, UNi + 1);
                                                                        break;
                                                                    }
                                                                    UNi++;
                                                                }

                                                                Form_this.textBoxMainBack.AppendText(Data.getEnter() +
                                                                //检测登录方式,0010为剑与世界
                                                                "登录方式:" + RMessage.Substring(0, 4) + Data.getEnter() +
                                                                //检测用户名,
                                                                "用户名:" + UNT + Data.getEnter() +
                                                                //检测用户密码
                                                                "用户密码:" + RMessage.Substring(20, 18) + Data.getEnter() +
                                                                //检测用户账号);
                                                                "用户账号:" + RMessage.Substring(40, 10));
                                                            }
                                                            break;
                                                    }

                                                    string a = "";
                                                    //得到字典中用户名值的集合
                                                    Dictionary<string, string>.KeyCollection Keys = ClientUserName.Keys;
                                                    foreach (string str in Keys)
                                                    {
                                                        //获取每一次遍历的用户名
                                                        ClientUserName.TryGetValue(str, out a);
                                                        //当用户名重复时
                                                        if (UNT == a)
                                                        {
                                                            //需要发送的信息赋值给新建的变量
                                                            string sendMsg = "MS用户名重复!!!将断开连接!" + Data.getEnter();
                                                            //将需要发送的信息转为byte[]的形式，以便于发送
                                                            byte[] arrSendMsg = Encoding.UTF8.GetBytes(sendMsg);
                                                            //向客户端发送人数超过限制的信息
                                                            socketServer.Send(arrSendMsg);
                                                            Form_this.textBoxMainBack.AppendText(UNT + ":" + str);

                                                            //将信息显示到窗体界面
                                                            Form_this.textBoxMain.AppendText(Data.getEnter() + "Client Count:" + ClientConnectionItems.Count);
                                                            //在窗体中显示消息,提示客户已断开连接
                                                            Form_this.textBoxMain.AppendText("客户端" + SSREP + "因用户名重复:连接已主动中断" + Data.getEnter() + Data.getEnter());
                                                            //关闭当前链接
                                                            try
                                                            {
                                                                //并且关闭该异常客户的连接.
                                                                socketServer.Close();
                                                                //将出现异常的客户从储存的客户信息中移除到
                                                                RecordModify.DeleteClientDictionary(SSREP);

                                                                //将信息显示到窗体界面
                                                                Form_this.textBoxMain.AppendText("Client Count:" + ClientConnectionItems.Count);

                                                                //获取该客户连接进入时分配的线程
                                                                Thread threadUserNameRepeat = Thread.CurrentThread;
                                                                //终止当前线程
                                                                threadUserNameRepeat.Abort();
                                                            }
                                                            catch (Exception ex)
                                                            {
                                                                Form_this.textBoxMain.AppendText("关闭Socket连接异常:" + ex.ToString());
                                                            }
                                                            break;
                                                        }
                                                    }//遍历用户信息,判断用户名是否重复
                                                     //添加用户的用户名到字典集合中
                                                    ClientUserName.Add(remoteEndPoint, UNT);

                                                    if (RMessage.Substring(0, 4) == "0099")
                                                    {
                                                        textboxmain_Append(Data.getEnter() + "向客户发送消息：登录成功" + Data.getEnter());
                                                        //向用户发送消息：已登录成功
                                                        SendToSocket(socketServer, "0099" + "True");
                                                    }
                                                }
                                            }
                                        }
                                        //如果检测错误，则断开连接
                                        else
                                        {
                                            //发送错误信息到客户端
                                            {
                                                //需要发送的信息赋值给新建的变量
                                                string sendMsg = "MS账户、密码、用户名是否正确";
                                                //将需要发送的信息转为byte[]的形式，以便于发送
                                                byte[] arrSendMsg = Encoding.UTF8.GetBytes(sendMsg + Data.getEnter());
                                                //向客户端发送人数超过限制的信息
                                                SendToSocket(socketServer, arrSendMsg.ToString());
                                            }

                                            //将信息显示到窗体界面
                                            Form_this.textBoxMain.AppendText(Data.getEnter() + "Client Count:" + ClientConnectionItems.Count + Data.getEnter());
                                            //在窗体中显示消息,提示客户已断开连接
                                            Form_this.textBoxMain.AppendText(Data.getEnter() + "已主动中断：" + SSREP + "客户端的连接" + Data.getEnter());

                                            try
                                            {
                                                //并且关闭该异常客户的连接.
                                                socketServer.Close();
                                            }
                                            catch (Exception ex)
                                            {
                                                Form_this.textBoxMain.AppendText("关闭Socket连接异常:" + ex.ToString());
                                            }
                                            //跳出循环
                                            break;
                                        }
                                    }
                                }
                                i++;
                                i++;
                            }
                        }
                    }
                    if (sb.Length > 128 || sb.ToString() == "EN") break;
                }
                catch (Exception e)//当发生错误时
                {
                    //MessageBox.Show("服务端与客户端交流环节(已连接成功)" + e.Message);
                    //删除已链接用户信息
                    RecordModify.DeleteClientDictionary(SSREP);

                    //将信息显示到窗体界面
                    textboxmain_Append("Client Count:" + ClientConnectionItems.Count + Data.getEnter());
                    //在窗体中显示消息,提示客户已断开连接
                    textboxmain_Append("客户端" + SSREP + "已经中断连接" + Data.getEnter() + e.Message + Data.getEnter() + e.StackTrace + Data.getEnter());
                    try
                    {
                        SendToSocket(socketServer,"MS来自服务端：连接错误，将断开连接!");
                        //并且关闭该异常客户的连接.
                        socketServer.Close();
                    }
                    catch (Exception ex)
                    {
                        Form_this.textBoxMain.AppendText("关闭Socket连接异常:" + ex.ToString() + Data.getEnter());
                    }
                    //跳出循环
                    break;
                }
            }
            //移除当前客户信息
            try
            {
                //将出现异常的客户从储存的客户信息中移除到
                RecordModify.DeleteClientDictionary(SSREP);
            }
            catch
            {

            }
            //获取该客户连接进入时分配的线程
            Thread thread = Thread.CurrentThread;
            //终止当前线程
            thread.Abort();
        }

        /// <summary>
        /// 与客户交流环节
        /// </summary>
        /// <param name="socketclientpara"></param>
        static void recv1(object socketclientpara)
        {
            //将传入的已链入客户套接字参数存入该新建的Socket类型的变量socketServer中
            Socket socketServer = socketclientpara as Socket;
            //先将客户的IP和端口号储存起来，格式如127.0.0.1:10629
            string SSREP = socketServer.RemoteEndPoint.ToString();
            //获取该客户连接进入时分配的线程
            Thread thread = Thread.CurrentThread;
            //终止当前线程
            thread.Abort();
        }

        /// <summary>
        /// 向所有客户端发送SendMData数组中的信息
        /// </summary>
        static void SendMessageToClient()
        {
            //当处于多线程模式时继续循环
            do
            {
                string a = null;
                if (true)
                {
                    //当时钟频率正确时
                    if (ClickTime == 0)
                    {
                        //当有需要发送的消息时
                        if (SendMData.Count > 1)
                        {
                            try
                            {
                                a = SendMData[1].ToString();
                            }
                            catch
                            {
                                //不用报错;
                            }
                            try
                            {
                                //删除掉第2位
                                SendMData.RemoveRange(1, 1);
                                if (ClientConnectionItems.Count > 0)
                                {
                                    //得到字典中值的集合
                                    Dictionary<string, Socket>.KeyCollection Keys = ClientConnectionItems.Keys;
                                    //遍历所有的客户套接字
                                    foreach (string SS in Keys)
                                    {
                                        try
                                        {
                                            //发送需要发送的消息给客户
                                            ClientConnectionItems[SS].Send(Encoding.UTF8.GetBytes(a + Data.getEnter()));
                                                                                                               //form.textBoxMainBack.AppendText("\r\n" + "发送信息:" + a + "\r\n");
                                        }
                                        catch (Exception e)
                                        {
                                            Form_this.textBoxMainBack.AppendText(Data.getEnter() + "发送信息遍历时报错:" + e + Data.getEnter());
                                        }
                                    }
                                }
                            }
                            catch (Exception e)
                            {
                                Form_this.textBoxMainBack.AppendText("\r\n发送消息时出错:" + e.ToString());
                            }
                        }
                    }
                }
                if (Data.ThreadCount > 5)
                {
                    _mreMS.Reset();
                    _mreMS.WaitOne();
                }
            } while (Data.ThreadCount > 5);
            //当处于多线程模式时
            if (Data.ThreadCount > 5)
            {
                //获取该客户连接进入时分配的线程
                Thread thread = Thread.CurrentThread;
                //终止当前线程
                thread.Abort();
            }
        }

        /// <summary>
        /// 向所有客户端发送SendTMData数组中的信息
        /// </summary>
        static void SendTMessageToClient()
        {
            //当处于多线程模式时继续循环
            do
            {
                string a = null;
                if (true)
                {
                    //当时钟频率正确时
                    if (ClickTime == 0)
                    {
                        //当有需要发送的消息时
                        if (SendTMData.Count > 1)
                        {
                            try
                            {
                                a = SendTMData[1].ToString();
                            }
                            catch
                            {
                                //不用报错;
                            }
                            try
                            {
                                //删除掉第2位  
                                SendTMData.RemoveRange(1, 1);     
                                if (ClientConnectionItems.Count > 0)
                                {
                                    //得到字典中值的集合
                                    Dictionary<string, Socket>.KeyCollection Keys = ClientConnectionItems.Keys;
                                    //遍历所有的客户套接字
                                    foreach (string SS in Keys)
                                    {
                                        try
                                        {
                                            //发送需要发送的消息给客户
                                            ClientConnectionItems[SS].Send(Encoding.UTF8.GetBytes(a + Data.getEnter()));
                                                                                                               //form.textBoxMainBack.AppendText("\r\n" + "发送信息:" + a + "\r\n");
                                        }
                                        catch (Exception e)
                                        {
                                            Form_this.textBoxMainBack.AppendText(Data.getEnter() + "发送信息遍历时报错:" + e + Data.getEnter());
                                        }
                                    }
                                }
                            }
                            catch (Exception e)
                            {
                                Form_this.textBoxMainBack.AppendText(Data.getEnter() + "发送消息时出错:" + e.ToString());
                            }
                        }
                    }
                }
                if (Data.ThreadCount > 5)
                {
                    _mreTMS.Reset();
                    _mreTMS.WaitOne();
                }
            } while (Data.ThreadCount > 5);
            //当处于多线程模式时
            if (Data.ThreadCount > 5)
            {
                //获取该客户连接进入时分配的线程
                Thread thread = Thread.CurrentThread;
                //终止当前线程
                thread.Abort();
            }
        }

        /// <summary>
        /// 所有客户端发送SendMData的信息组合到一起
        /// </summary>
        static void SendMessageBuilding()
        {
            //当处于多线程模式时继续循环
            do
            {
                //当有用户消息时
                if (ClientData.Count > 0)
                {
                    try
                    {
                        Dictionary<string, string> CData = ClientData;
                        //得到字典中键的集合
                        Dictionary<string, string>.KeyCollection Keys = CData.Keys;
                        //form.textBoxMain.AppendText(CData.Count.ToString());

                        //判断待发送数据是否超标
                        if (SendMData.Count <= 5)
                        {
                            //遍历所有的键
                            foreach (string DKey in Keys)
                            {
                                string USValue, DValue;
                                USValue = null; DValue = null;
                                CData.TryGetValue(DKey, out DValue);
                                ClientUserName.TryGetValue(DKey, out USValue);
                                try
                                {
                                    //添加数据到待发送区
                                    SendMData.Add("PS" + (USValue + "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t").Substring(0, 13) + "V" + DValue);
                                }
                                catch
                                {
                                    //不用报错;
                                }
                            }
                        }
                    }
                    catch //(Exception e)
                    {
                        //form.textBoxMainBack.AppendText("整合待发送消息时出错" + e);
                    }
                }
                if (Data.ThreadCount > 5)
                {
                    _mreMB.Reset();
                    _mreMB.WaitOne();
                }
            } while (Data.ThreadCount > 5);
            //当处于多线程模式时
            if (Data.ThreadCount > 5)
            {
                //获取该客户连接进入时分配的线程
                Thread thread = Thread.CurrentThread;
                //终止当前线程
                thread.Abort();
            }
        }

        /// <summary>
        /// 获取当前的系统时间
        /// </summary>
        /// <returns></returns>
        static DateTime GetCurrentTime()
        {
            //实例化DateTime
            DateTime currentTime = new DateTime();
            //获取当前时间
            currentTime = DateTime.Now;
            //返回
            return currentTime;
        }

        /// <summary>
        /// 获取当前程序占用的内存大小
        /// </summary>
        /// <returns></returns>
        public double GetProcessUsedMemory()
        {
            double usedMemory = 0;
            //获取当前程序，获取占用内存大小，除两次1024，将单位转为MB
            usedMemory = Process.GetCurrentProcess().WorkingSet64 / 1024.0 / 1024.0;
            return usedMemory;
        }

        /// <summary>
        /// 关闭服务器
        /// </summary>
        /// <returns></returns>
        static bool CloseServer()
        {
            MessageBox.Show("点击右上角的'X',以达到关闭效果");
            return true;
        }

        /// <summary>
        /// timer控件，每隔一段时间运行一次
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerMain_Tick(object sender, EventArgs e)
        {
            //将内存占用显示到窗体控件中
            labelMemoryFootprintR.Text = Math.Round(GetProcessUsedMemory(),2) + "MB";
            //获取连接人数
            labelNumberOfPeopleR.Text = ClientConnectionItems.Count.ToString() + "人";
            //设置发送频率
            timerBack.Interval = Data.TimeInterval;
            //调用更新函数
        }
        //创建一个新的线程
        static Thread threadSendMessageBuding = new Thread(SendMessageBuilding);
        //创建一个新的线程，用于向所有客户端发送信息
        static Thread threadSendMessageToClient = new Thread(SendMessageToClient);
        //创建一个新的线程
        static Thread threadSendTMessageToClient = new Thread(SendTMessageToClient);
        //用于暂停线程,MessageBuilding
        static ManualResetEvent _mreMB = new ManualResetEvent(false);
        //MessageSend
        static ManualResetEvent _mreMS = new ManualResetEvent(false);
        //TextMessageSend
        static ManualResetEvent _mreTMS = new ManualResetEvent(false);
        /// <summary>
        /// 控制发送频率
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerBack_Tick(object sender, EventArgs e)
        {
            //处于多线程模式时
            if (Data.ThreadCount > 5)
            {
                //设置线程为开启状态
                _mreMB.Set();
                _mreMS.Set();
                _mreTMS.Set();
                //判断线程是否存在
                if (!threadSendMessageBuding.IsAlive)
                {
                    {
                        //将该线程设置为后台运行，线程与主程序同步运行，受主程序影响
                        threadSendMessageBuding.IsBackground = true;
                        //启动该线程
                        threadSendMessageBuding.Start();

                        if (!threadSendMessageToClient.IsAlive)
                        {
                            //将该线程设置为后台运行，线程与主程序同步运行，受主程序影响
                            threadSendMessageToClient.IsBackground = true;
                            //启动该线程
                            threadSendMessageToClient.Start();
                                                              //SendMessageToClient();
                        }
                        if (!threadSendTMessageToClient.IsAlive)
                        {
                            //将该线程设置为后台运行，线程与主程序同步运行，受主程序影响
                            threadSendTMessageToClient.IsBackground = true;
                            //启动该线程
                            threadSendTMessageToClient.Start();
                                                            //SendTMessageToClient();
                        }
                    }                                     
                }
            }
            else
            {
                SendMessageBuilding();
                SendMessageToClient();
                SendTMessageToClient();
            }
        }

        /// <summary>
        /// 离线检测计时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerOffLineDetection_Tick(object sender, EventArgs e)
        {
            string a = Data.getEnter() + "SendMData数量:" + SendMData.Count + Data.getEnter() + "SendTMData:" + SendTMData.Count + Data.getEnter() + "ClientConnectionItems:" + ClientConnectionItems.Count + Data.getEnter() + "ClientData:" + ClientData.Count + Data.getEnter() + "ClientUserName:" + ClientUserName.Count;
            Form_this.textBoxMain.AppendText(a);
            //SaveLog();//保存日志
        }

        /// <summary>
        /// 保存日志文件
        /// </summary>
        private void SaveLog()
        {
            //保存日志文件
            Data_Save.Save("Log.txt", textBoxMain.Text);
            //保存日志文件
            Data_Save.Save("AccountLog.txt", textBoxMainBack.Text);
            textBoxMain.Text = textBoxMain.Text.Substring(textBoxMain.TextLength / 2);
            textBoxMainBack.Text = textBoxMainBack.Text.Substring(textBoxMainBack.TextLength / 2);
        }

        /// <summary>
        /// 在主要消息盒子中追加文本
        /// </summary>
        /// <param name="s">追加信息</param>
        public static void textboxmain_Append(string s)
        {
            Form_this.textBoxMain.AppendText(s);
        }

        /// <summary>
        /// 在主要背部消息盒子中追加文本
        /// </summary>
        /// <param name="s"></param>
        public static void textboxmainback_Append(string s)
        {
            Form_this.textBoxMainBack.AppendText(s);
        }

        /// <summary>
        /// 主文本框改变时调用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxMain_TextChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 设置按钮，用于打开设置窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSet_Click(object sender, EventArgs e)
        {
            FormSet formSet = new FormSet();
            //打开设置界面
            formSet.Show();
            
        }

        /// <summary>
        /// 发送消息到指定套接字
        /// </summary>
        /// <param name="socket"></param>
        /// <param name="s"></param>
        private static void SendToSocket(Socket socket,string s)
        {
            socket.Send(Encoding.UTF8.GetBytes(s + "\r\n"));
        }
    }
}
