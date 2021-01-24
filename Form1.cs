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
        static Form1 form;//当前静态窗体
        static Socket SIWServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        //创建socket//定义一个套接字用于监听客户端发来的消息，包含三个参数（IP4寻址协议，流式连接，Tcp协议）

        private static Dictionary<string, Socket> clientConnectionItems = new Dictionary<string, Socket> { };//Dictionary (词典)用于储存键和值
        //实例化的键为string类型，值为Socket套接字,//定义一个集合，存储客户端信息。下

        private static Dictionary<string, string> clientUserName = new Dictionary<string, string> { };//用于储存客户在的用户名.通过网络节点号RemoteEndPoint查找
        private static Dictionary<string, string> clientData = new Dictionary<string, string> { };//用于储存用户数据
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


        private static string serverIP,serverPort;//服务器IP地址，端口号
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


        public Form1()
        {
            InitializeComponent();
            ClickTime = 0;//时钟计时
            if (Data.TimeInterval == 0) Data.TimeInterval = 50;//默认信息发送频率
            Data.MaxPeople = 100;//服务器最大链入人数默认为100
            MaxSendData = 10;//最大待发送数据
            Data.ThreadCount = 10;//线程数，控制线程
            ClientConnectionItems.Clear();//移除所有键与值，初始化
            ClientUserName.Clear();
            ClientData.Clear();
            SendMData.Add("XyKs");//基础位，防止被清除
            SendTMData.Add("XyKs");
        }

        /// <summary>
        /// 当窗体加载完成后执行该函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            form = this;//为窗体类型赋值为当前窗体
            TextBox.CheckForIllegalCrossThreadCalls = false;//关闭文本框的非法线程操作检查
        }

        /// <summary>
        /// 启动服务器与关闭服务器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOpenServer_Click(object sender, EventArgs e)
        {
            textBoxMain.Text = "以按下按钮“服务器”";
            if (buttonOpenServer.Text == "打开服务器")//判断服务器是否开启（当前按钮状态）
            {
                if (OpenServer())//调用函数,打开服务器
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
                if (CloseServer())//调用关闭服务器函数，关闭成功时
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
            try//尝试打开服务器，返回true
            {
                IPAddress address = IPAddress.Parse(form.ServerIP);//将string类型的IP地址转为IPAddress储存,调用ServerIP属性的值
                int port = int.Parse(form.ServerPort);//将端口号由string转为int类型
                MessageBox.Show(port.ToString());
                EndPoint point = new IPEndPoint(address, port);//将IP和端口号绑定到节点上
                SIWServer.Bind(point);//监听绑定的端口节点.使用上面已经声明好的套接字
                SIWServer.Listen(100);//限制监听队列的长度
                Thread threadWatchConnecting = new Thread(WatchConnecting);//创建一个新的线程，调用监听客户端的连接请求
                threadWatchConnecting.IsBackground = true;//将该线程设置为后台运行，线程与主程序同步运行，受主程序影响
                threadWatchConnecting.Start();//启动该线程，监听客户端的连接请求
                
                //Thread threadSendMessageToAllClient = new Thread(SendMessageToClient);//创建一个新的线程，用于向所有客户端发送信息
                //threadSendMessageToAllClient.IsBackground = true;//将该线程设置为后台运行，线程与主程序同步运行，受主程序影响
                //threadSendMessageToAllClient.Start();//启动该线程

                //Thread threadSendMessageBuding = new Thread(SendMessageBuding);//创建一个新的线程，用于向所有客户端发送信息
                //threadSendMessageBuding.IsBackground = true;//将该线程设置为后台运行，线程与主程序同步运行，受主程序影响
                //threadSendMessageBuding.Start();//启动该线程

                return true;
            }
            catch(Exception e)//报错时返回false
            {
                MessageBox.Show("打开服务器环节" + e.Message.ToString());//打开消息窗口返回错误信息
                return false;
            }
        }

        /// <summary>
        /// 监听客户端发送的连接请求，查看连接
        /// </summary>
        static void WatchConnecting()
        {
            Socket connection = null;//新建一个套接字，用于查看客户链接请求
            while (true)//不断监听客户请求
            {
                    try//尝试进行链接的等待
                    {
                        form.textBoxMain.AppendText("\r\n服务器已打开");
                        connection = SIWServer.Accept();//等待先前以创建好的套接字有链接进入,好像会暂停以等待链接进入来着
                    }
                    catch (Exception e)//当套接字等待出现异常时
                    {
                        MessageBox.Show("监听客户端连接请求环节" + e.Message.ToString());//打开消息窗口返回错误信息
                        break;//并直接跳出while语句,结束当前循环
                }//尝试进行链接的等待
                if (ClientConnectionItems.Count < Data.MaxPeople)
                {
                    IPAddress clientIP = (connection.RemoteEndPoint as IPEndPoint).Address;//获取链接进入客户的端点,将网络点表示为IP和端口，获取链入客户端的IP地址与端口号
                    int clientPort = (connection.RemoteEndPoint as IPEndPoint).Port;//获取链接进入客户的端口号
                    InformationVerification iv = new InformationVerification();
                    if (iv.IPV(clientIP.ToString()))//传入客户IP信息如果被白名单认可的话(不在黑名单)
                    {
                        SendToClient stc = new SendToClient();//实例化向客户发送消息的类
                        string sendMsg = stc.SendTC("连接服务端成功");
                        //string sendMsg = "连接服务端成功！\r\n" + "本地IP:" + clientIP + "，本地端口" + clientPort.ToString();//新建一个string类型,里面存入之后要发送给客户的信息
                        byte[] arrSendMsg = Encoding.UTF8.GetBytes(sendMsg);//将需要发送的信息转为byte[]的形式，以便于发送
                        connection.Send(arrSendMsg);//通过客户连接上的套接字,发送信息至链接成功的客户处

                        string remoteEndPoint = connection.RemoteEndPoint.ToString();//将客户的网络节点号转为string类型
                        form.textBoxMain.AppendText("与" + remoteEndPoint + "客户端建立连接！\t\n");//显示连接状况.使用AppendText方法向文本框中追加内容

                        IPEndPoint netPoint = connection.RemoteEndPoint as IPEndPoint;//将客户的IP与端口储存到新建的变量netpoint中
                        //IPEndPoint netPoint = new IPEndPoint(clientIP,clientPort);//除了上面的储存外也可以用该储存方法

                        ParameterizedThreadStart PTS = new ParameterizedThreadStart(recv);//将方法装入pts变量中，以便于下面打开一个新的线程
                        Thread thread = new Thread(PTS);//创建新的线程，实例化
                        thread.IsBackground = true;//设置线程为后台程序
                        thread.Start(connection);//打开创建好的线程，参数传入为当前客户的套接字
                    }
                    else
                    {
                        SendToClient stc = new SendToClient();//实例化向客户发送消息的类
                        string sendMsg = stc.SendTC("该IP地址不允许访问");//需要发送的信息赋值给新建的变量
                        form.textBoxMain.AppendText("客户链入请求——该IP地址不允许访问*1");
                        byte[] arrSendMsg = Encoding.UTF8.GetBytes(sendMsg);//将需要发送的信息转为byte[]的形式，以便于发送
                        connection.Send(arrSendMsg);//向客户端发送信息
                    }
                }//如果没有超出最大链入人数限制
                else
                {
                    SendToClient stc = new SendToClient();//实例化向客户发送消息的类
                    string sendMsg = stc.SendTC("超出人数上限");//需要发送的信息赋值给新建的变量
                    form.textBoxMain.AppendText("客户链入请求——超出人数上限*1");
                    byte[] arrSendMsg = Encoding.UTF8.GetBytes(sendMsg);//将需要发送的信息转为byte[]的形式，以便于发送
                    connection.Send(arrSendMsg);//向客户端发送人数超过限制的信息
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
            Socket socketServer = socketclientpara as Socket;//将传入的已链入客户套接字参数存入该新建的Socket类型的变量socketServer中
            try
            {
                socketServer.Close();
            }
            catch
            {
                form.textBoxMain.AppendText("关闭客户链接时失败");
            }
        }

        /// <summary>
        /// 与客户交流环节
        /// </summary>
        /// <param name="socketclientpara"></param>
        static void recv(object socketclientpara)
        {
            Socket socketServer = socketclientpara as Socket;//将传入的已链入客户套接字参数存入该新建的Socket类型的变量socketServer中
            string SSREP = socketServer.RemoteEndPoint.ToString();//先将客户的IP和端口号储存起来，格式如127.0.0.1:10629
            //获取接收到客户最后一次发来消息的时间。之后判断时间超出则断开链接
            SendTMData.Add("TF" + Data.TimeInterval);//频率设置
            while (true)//不断循环
            {
                StringBuilder sb = new StringBuilder();             //这个是用来保存：接收到了的，但是还没有结束的消息
                byte[] arrServerRecMsg = new byte[128];//创建一个大小为1M的内存缓冲区 (1024*1024字节)
                try
                {
                    int length = socketServer.Receive(arrServerRecMsg);//接收套接字变量socketServer的数据并赋给int类型变量length
                    string strRevMsg = Encoding.UTF8.GetString(arrServerRecMsg, 0, length);//将接受到的数据转化为string类型，UTF8格式，从第0位开始，在内存缓冲区中转化
                    //form.textBoxMainBack.AppendText(strRevMsg);
                    lock(strRevMsg){
                        for (int i = 0; i < strRevMsg.Length;)
                        {
                            if (strRevMsg.Substring(i, 1) != "\r")
                            {
                                if (i > 1024) { break; }//如果溢出
                                sb.Append(strRevMsg.Substring(i, 1));
                                i++;
                            }//是否截取到数据包尾部字符
                            else
                            {
                                if (sb.Length > 128) break;//如果数值溢出
                                string RMessage = sb.ToString();
                                sb.Clear();
                                if (ClientConnectionItems.ContainsKey(SSREP))
                                {
                                    //form.textBoxMain.AppendText(RMessage);
                                    if (RMessage.Substring(0, 2) == "Hi")
                                    {
                                        form.textBoxMainBack.AppendText("\r\n客户端:" + socketServer.RemoteEndPoint + ",time:" + GetCurrentTime() + "\r\n" + RMessage + "\r\n\n");//将接收到的信息转化并显示到窗体中
                                                                                                                                                                               //SendToClient STC = new SendToClient();//实例化对象
                                                                                                                                                                               //socketServer.Send(Encoding.UTF8.GetBytes(STC.SendTC("以收到消息")));//并且发送数据给客户，发送码
                                        string clientUN;
                                        ClientUserName.TryGetValue(SSREP, out clientUN);//获取该线程用户的用户名


                                        SendTMData.Add("Hi" + clientUN + ":" + RMessage.Substring(2));//向需要发送的消息中添加
                                    }//如果客户传来消息的开头为"Hi"的话,后面接用户想要在聊天框发送的消息
                                    else if (RMessage.Substring(0, 2) == "PS")
                                    {
                                        string s = "";
                                        int mixLength = 0;//最小
                                        for (int x = 0; x < RMessage.Length; x++)
                                        {
                                            if (RMessage.Substring(x, 1) == "E")
                                            {
                                                s = RMessage.Substring(mixLength, x + 1 - mixLength);//截取不同玩家的信息，保存
                                                mixLength = x + 1;
                                                if (ClientData.ContainsKey(SSREP))
                                                {
                                                    ClientData[SSREP] = s.Substring(2);//覆盖之前已有信息
                                                }//确认是否包含指定键
                                                else
                                                {
                                                    ClientData.Add(SSREP, s.Substring(2));//将用户信息储存起来,用户名和信息
                                                }//如果没有用户信息
                                            }//如果截取到结尾字符"E"
                                        }//遍历整个信息

                                        //form.textBoxMainBack.AppendText("\r\n客户端:" + socketServer.RemoteEndPoint + ",time:" + GetCurrentTime() + "\r\n" + strSRecMsg + "\r\n\n");
                                        //form.textBoxMain.AppendText(clientData[SSREP]);
                                    }//用户的位置与动作等
                                    else if(RMessage.Substring(0, 2) == "EN")
                                    {
                                        sb.Clear();
                                        sb.Append("EN");
                                        SendTMData.Add("EN" + SSREP);//添加用户终结信息
                                        throw new ArgumentOutOfRangeException("用户请求断开连接");//抛出异常
                                        break;
                                    }//结束当前线程,与用户连接

                                }//检测客户是否已经被保存到服务器信息中
                                else
                                {
                                    InformationVerification iv = new InformationVerification();
                                    if (iv.Verification(RMessage))
                                    {
                                        string remoteEndPoint = socketServer.RemoteEndPoint.ToString();//将客户的网络节点号转为string类型
                                        form.textBoxMain.AppendText("\r\n成功与" + remoteEndPoint + "客户端建立连接！\r\n");//显示连接状况.使用AppendText方法向文本框中追加内容
                                        ClientConnectionItems.Add(remoteEndPoint, socketServer);//添加客户端信息到一开始创建好了的Dictionary集合中,第一个参数为string类型，第二个参数为套接字Socket类型
                                        string UN = RMessage.Substring(5, 13);//暂时保存当前用户名
                                        string UNT = null;//记录去掉了尾部的字符串
                                        for (int UNi = 0; UNi < UN.Length;)
                                        {
                                            if (UNi == UN.Length - 1)
                                            {
                                                UNT = UN;
                                                break;
                                            }//如果该字符串没有截取到结尾字符，则直接赋值
                                            if (UN.Substring(UNi, 1) == "\t")
                                            {
                                                UNT = UN.Substring(0, UNi + 1);
                                                break;
                                            }//如果截取到表示结尾字符"\0"
                                            UNi++;
                                        }//遍历出真正的用户名并记录

                                        form.textBoxMainBack.AppendText(
                                        "\r\n登录方式:" + RMessage.Substring(0, 4) +//检测登录方式,0010为剑与世界
                                        "\r\n用户名:" + UNT +//检测用户名,
                                        "\r\n用户密码:" + RMessage.Substring(20, 18) +//检测用户密码
                                        "\r\n用户账号:" + RMessage.Substring(40, 10));//检测用户账号);

                                        string a = "";
                                        Dictionary<string, string>.KeyCollection Keys = ClientUserName.Keys;//得到字典中用户名值的集合
                                        foreach (string str in Keys)
                                        {
                                            ClientUserName.TryGetValue(str, out a);//获取每一次遍历的用户名
                                            if (UNT == a)
                                            {
                                                string sendMsg = "MS用户名重复!!!将断开连接!\r\n";//需要发送的信息赋值给新建的变量
                                                byte[] arrSendMsg = Encoding.UTF8.GetBytes(sendMsg);//将需要发送的信息转为byte[]的形式，以便于发送
                                                socketServer.Send(arrSendMsg);//向客户端发送人数超过限制的信息
                                                form.textBoxMainBack.AppendText(UNT + ":" + str);

                                                form.textBoxMain.AppendText("\r\nClient Count:" + ClientConnectionItems.Count);//将信息显示到窗体界面
                                                form.textBoxMain.AppendText("客户端" + SSREP + "因用户名重复:连接已主动中断" + "\r\n" + "\r\n");//在窗体中显示消息,提示客户已断开连接
                                                try
                                                {
                                                    socketServer.Close();//并且关闭该异常客户的连接.
                                                    RecordModify.DeleteClientDictionary(SSREP);//将出现异常的客户从储存的客户信息中移除到

                                                    form.textBoxMain.AppendText("Client Count:" + ClientConnectionItems.Count);//将信息显示到窗体界面

                                                    Thread threadUserNameRepeat = Thread.CurrentThread;//获取该客户连接进入时分配的线程
                                                    threadUserNameRepeat.Abort();//终止当前线程
                                                }
                                                catch (Exception ex)
                                                {
                                                    form.textBoxMain.AppendText("关闭Socket连接异常:" + ex.ToString());
                                                }//关闭当前链接
                                                break;
                                            }//当用户名重复时
                                        }//遍历用户信息,判断用户名是否重复

                                        ClientUserName.Add(remoteEndPoint, UNT);//添加用户的用户名到字典集合中
                                    }//需要调用账号密码检测函数,当客户发送登录的消息请求时,如果发送信息正确
                                    else
                                    {
                                        string sendMsg = "Hi账户、密码、用户名是否正确";//需要发送的信息赋值给新建的变量
                                        byte[] arrSendMsg = Encoding.UTF8.GetBytes(sendMsg + "\r\n");//将需要发送的信息转为byte[]的形式，以便于发送
                                        socketServer.Send(arrSendMsg);//向客户端发送人数超过限制的信息


                                        form.textBoxMain.AppendText("\r\nClient Count:" + ClientConnectionItems.Count);//将信息显示到窗体界面
                                        form.textBoxMain.AppendText("客户端" + SSREP + "连接已主动中断" + "\r\n" + "\r\n");//在窗体中显示消息,提示客户已断开连接
                                        try
                                        {
                                            socketServer.Close();//并且关闭该异常客户的连接.
                                        }
                                        catch (Exception ex)
                                        {
                                            form.textBoxMain.AppendText("关闭Socket连接异常:" + ex.ToString());
                                        }
                                        break;//跳出循环
                                    }//如果检测错误，则断开连接
                                }//如果客户当前套接字还未被保存则判断传入客户信息是否为登录信息
                                i++;
                                i++;
                            }
                        }//遍历收到的信息,截取每一个数据包
                    }
                    if (sb.Length > 128 || sb.ToString() == "EN") break;
                }
                catch (Exception e)//当发生错误时
                {
                    //MessageBox.Show("服务端与客户端交流环节(已连接成功)" + e.Message);
                    RecordModify.DeleteClientDictionary(SSREP);//删除已链接用户信息

                    form.textBoxMain.AppendText("Client Count:" + ClientConnectionItems.Count);//将信息显示到窗体界面
                    form.textBoxMain.AppendText("客户端" + SSREP + "已经中断连接" + "\r\n" + e.Message + "\r\n" + e.StackTrace + "\r\n");//在窗体中显示消息,提示客户已断开连接
                    try
                    {
                        socketServer.Send(Encoding.UTF8.GetBytes("MS连接错误，将断开连接!\r\n"));
                        socketServer.Close();//并且关闭该异常客户的连接.
                    }
                    catch (Exception ex)
                    {
                        form.textBoxMain.AppendText("关闭Socket连接异常:" + ex.ToString());
                    }
                    break;//跳出循环
                }//判断接收到的客户信息
            }//循环接收客户信息

            try
            {
                RecordModify.DeleteClientDictionary(SSREP);//将出现异常的客户从储存的客户信息中移除到
            }
            catch
            {

            }//移除当前客户信息
            Thread thread = Thread.CurrentThread;//获取该客户连接进入时分配的线程
            thread.Abort();//终止当前线程
        }

        /// <summary>
        /// 与客户交流环节
        /// </summary>
        /// <param name="socketclientpara"></param>
        static void recv1(object socketclientpara)
        {
            Socket socketServer = socketclientpara as Socket;//将传入的已链入客户套接字参数存入该新建的Socket类型的变量socketServer中
            string SSREP = socketServer.RemoteEndPoint.ToString();//先将客户的IP和端口号储存起来，格式如127.0.0.1:10629
            Thread thread = Thread.CurrentThread;//获取该客户连接进入时分配的线程
            thread.Abort();//终止当前线程
        }

        /// <summary>
        /// 向所有客户端发送SendMData数组中的信息
        /// </summary>
        static void SendMessageToClient()
        {
            do
            {
                string a = null;
                if (true)
                {
                    if (ClickTime == 0)//当时钟频率正确时
                    {
                        if (SendMData.Count > 1)//当有需要发送的消息时
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
                                SendMData.RemoveRange(1, 1);//删除掉第2位
                                if (ClientConnectionItems.Count > 0)
                                {
                                    Dictionary<string, Socket>.KeyCollection Keys = ClientConnectionItems.Keys;//得到字典中值的集合
                                    foreach (string SS in Keys)//遍历所有的客户套接字
                                    {
                                        try
                                        {
                                            ClientConnectionItems[SS].Send(Encoding.UTF8.GetBytes(a + "\r\n"));//发送需要发送的消息给客户
                                                                                                               //form.textBoxMainBack.AppendText("\r\n" + "发送信息:" + a + "\r\n");
                                        }
                                        catch (Exception e)
                                        {
                                            form.textBoxMainBack.AppendText("\r\n" + "发送信息遍历时报错:" + e + "\r\n");
                                        }
                                    }
                                }
                            }
                            catch (Exception e)
                            {
                                form.textBoxMainBack.AppendText("\r\n发送消息时出错:" + e.ToString());
                            }
                        }//有待发送消息时
                    }//当时钟频率正确时
                }
                if (Data.ThreadCount > 5)
                {
                    _mreMS.Reset();
                    _mreMS.WaitOne();
                }
            } while (Data.ThreadCount > 5);//当处于多线程模式时继续循环
            if (Data.ThreadCount > 5)
            {
                Thread thread = Thread.CurrentThread;//获取该客户连接进入时分配的线程
                thread.Abort();//终止当前线程
            }//当处于多线程模式时
        }

        /// <summary>
        /// 向所有客户端发送SendTMData数组中的信息
        /// </summary>
        static void SendTMessageToClient()
        {
            do
            {
                string a = null;
                if (true)
                {
                    if (ClickTime == 0)//当时钟频率正确时
                    {
                        if (SendTMData.Count > 1)//当有需要发送的消息时
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
                                SendTMData.RemoveRange(1, 1);//删除掉第2位
                                if (ClientConnectionItems.Count > 0)
                                {
                                    Dictionary<string, Socket>.KeyCollection Keys = ClientConnectionItems.Keys;//得到字典中值的集合
                                    foreach (string SS in Keys)//遍历所有的客户套接字
                                    {
                                        try
                                        {
                                            ClientConnectionItems[SS].Send(Encoding.UTF8.GetBytes(a + "\r\n"));//发送需要发送的消息给客户
                                                                                                               //form.textBoxMainBack.AppendText("\r\n" + "发送信息:" + a + "\r\n");
                                        }
                                        catch (Exception e)
                                        {
                                            form.textBoxMainBack.AppendText("\r\n" + "发送信息遍历时报错:" + e + "\r\n");
                                        }
                                    }
                                }
                            }
                            catch (Exception e)
                            {
                                form.textBoxMainBack.AppendText("\r\n发送消息时出错:" + e.ToString());
                            }
                        }
                    }//当时钟频率正确时
                }
                if (Data.ThreadCount > 5)
                {
                    _mreTMS.Reset();
                    _mreTMS.WaitOne();
                }
            } while (Data.ThreadCount > 5);//当处于多线程模式时继续循环
            if (Data.ThreadCount > 5)
            {
                Thread thread = Thread.CurrentThread;//获取该客户连接进入时分配的线程
                thread.Abort();//终止当前线程
            }//当处于多线程模式时
        }

        /// <summary>
        /// 所有客户端发送SendMData的信息组合到一起
        /// </summary>
        static void SendMessageBuilding()
        {
            do
            {
                if (ClientData.Count > 0)//当有用户消息时
                {
                    try
                    {
                        Dictionary<string, string> CData = ClientData;
                        Dictionary<string, string>.KeyCollection Keys = CData.Keys;//得到字典中键的集合
                                                                                   //form.textBoxMain.AppendText(CData.Count.ToString());
                        if (SendMData.Count <= 5)
                        {
                            foreach (string DKey in Keys)//遍历所有的键
                            {
                                string USValue, DValue;
                                USValue = null; DValue = null;
                                CData.TryGetValue(DKey, out DValue);
                                ClientUserName.TryGetValue(DKey, out USValue);
                                try
                                {
                                    SendMData.Add("PS" + (USValue + "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t").Substring(0, 13) + "V" + DValue);//添加数据到待发送区
                                }
                                catch
                                {
                                    //不用报错;
                                }
                            }
                        }//判断待发送数据是否超标
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
            } while (Data.ThreadCount > 5);//当处于多线程模式时继续循环
            if (Data.ThreadCount > 5)
            {
                Thread thread = Thread.CurrentThread;//获取该客户连接进入时分配的线程
                thread.Abort();//终止当前线程
            }//当处于多线程模式时
        }

        /// <summary>
        /// 获取当前的系统时间
        /// </summary>
        /// <returns></returns>
        static DateTime GetCurrentTime()
        {
            DateTime currentTime = new DateTime();//实例化DateTime
            currentTime = DateTime.Now;//获取当前时间
            return currentTime;//返回
        }

        /// <summary>
        /// 获取当前程序占用的内存大小
        /// </summary>
        /// <returns></returns>
        public double GetProcessUsedMemory()
        {
            double usedMemory = 0;
            usedMemory = Process.GetCurrentProcess().WorkingSet64 / 1024.0 / 1024.0;//获取当前程序，获取占用内存大小，除两次1024，将单位转为MB
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
            labelMemoryFootprintR.Text = Math.Round(GetProcessUsedMemory(),2) + "MB";//将内存占用显示到窗体控件中
            labelNumberOfPeopleR.Text = ClientConnectionItems.Count.ToString() + "人";//获取连接人数
            timerBack.Interval = Data.TimeInterval;//设置发送频率
        }

        static Thread threadSendMessageBuding = new Thread(SendMessageBuilding);//创建一个新的线程
        static Thread threadSendMessageToClient = new Thread(SendMessageToClient);//创建一个新的线程，用于向所有客户端发送信息
        static Thread threadSendTMessageToClient = new Thread(SendTMessageToClient);//创建一个新的线程
        static ManualResetEvent _mreMB = new ManualResetEvent(false);//用于暂停线程,MessageBuilding
        static ManualResetEvent _mreMS = new ManualResetEvent(false);//MessageSend
        static ManualResetEvent _mreTMS = new ManualResetEvent(false);//TextMessageSend
        /// <summary>
        /// 控制发送频率
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerBack_Tick(object sender, EventArgs e)
        {
            if (Data.ThreadCount > 5)
            {
                _mreMB.Set();//设置线程为开启状态
                _mreMS.Set();
                _mreTMS.Set();
                if (!threadSendMessageBuding.IsAlive)
                {
                    {
                        threadSendMessageBuding.IsBackground = true;//将该线程设置为后台运行，线程与主程序同步运行，受主程序影响
                        threadSendMessageBuding.Start();//启动该线程

                        if (!threadSendMessageToClient.IsAlive)
                        {
                            threadSendMessageToClient.IsBackground = true;//将该线程设置为后台运行，线程与主程序同步运行，受主程序影响
                            threadSendMessageToClient.Start();//启动该线程
                                                              //SendMessageToClient();
                        }
                        if (!threadSendTMessageToClient.IsAlive)
                        {
                            threadSendTMessageToClient.IsBackground = true;//将该线程设置为后台运行，线程与主程序同步运行，受主程序影响
                            threadSendTMessageToClient.Start();//启动该线程
                                                            //SendTMessageToClient();
                        }
                    }                                     
                }//判断线程是否存在
            }//处于多线程模式时
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
            string a = "\r\nSendMData数量:" + SendMData.Count + "\r\nSendTMData:" + SendTMData.Count + "\r\nClientConnectionItems:" + ClientConnectionItems.Count + "\r\nClientData:" + ClientData.Count + "\r\nClientUserName:" + ClientUserName.Count;
            form.textBoxMain.AppendText(a);
        }

        /// <summary>
        /// 设置按钮，用于打开设置窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSet_Click(object sender, EventArgs e)
        {
            FormSet formSet = new FormSet();
            formSet.Show();//打开设置界面
        }

    }
}
