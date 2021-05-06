using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SwordIsWorldServer
{
    /// <summary>
    /// 用于在硬盘中储存服务端数据
    /// </summary>
    class Data_Save
    {
        /// <summary>
        /// 保存设置
        /// </summary>
        /// <param name="data">需要传入的数据</param>
        public void SaveSet(string data = null)
        {
            Save("SetData.txt", data);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public static void Initialization()
        {
            //创建文件夹Data
            Create("Data");
            Create("Data/Account");
            Create("Data/UserName");
        }

        /// <summary>
        /// 生成一个txt文本
        /// </summary>
        /// <param name="path"></param>
        /// <param name="information"></param>
        public static void Save(string information, string path)
        {
            try
            {
                MessageBox.Show(AppDomain.CurrentDomain.BaseDirectory);
                //FileStream aFile = new FileStream(@"" + Path, FileMode.OpenOrCreate);
                FileStream aFile = new FileStream(getPath() + path, FileMode.OpenOrCreate);
                StreamWriter sw = new StreamWriter(aFile);
                sw.Write(information);
                sw.Close();
                sw.Dispose();
            }
            catch (FileNotFoundException e)
            {
                Form1.textboxmain_Append("SaveFile:" + e);
            }
        }

        /// <summary>
        /// 读取一个txt文本;
        /// 返回值："00000"找不到相应文本，"00001"读取异常
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string Read(string path)
        {
            try
            {
                if (File.Exists(getPath() + path))
                {
                    FileStream aFile = new FileStream(getPath() + path, FileMode.Open);
                    StreamReader sw = new StreamReader(aFile);
                    string a = sw.ReadLine();
                    sw.Close();
                    sw.Dispose();
                    return a;
                }
                else
                {
                    return "00000";
                }
            }
            catch (FileNotFoundException e)
            {
                Form1.textboxmain_Append("ReadFile:" + e);
                return "00001";
            }
        }

        /// <summary>
        /// 读取回车符后的字符
        /// </summary>
        /// <param name="s">需要截取的字符串</param>
        /// <param name="i">读取的回车符个数（作为起始点）</param>
        /// <param name="length">需要截取的长度;为0时,则截取到下一个回车符前</param>
        /// <returns></returns>
        public static string Read_enter(string s, int i, int length)
        {
            //计数
            int count_i = 0, count_length = 0;
            //存储截取后的字符
            string return_s = null;
            //尝试读取信息
            try
            {
                //遍历检测
                foreach (char ss in s)
                {
                    //找到相应回车符
                    if (count_i == i)
                    {
                        //找到结尾，跳出循环
                        if (ss == Data.getEnter_First())
                        {
                            break;
                        }
                        return_s += ss;
                        count_length++;
                        //达到截取标准,跳出循环
                        if (length != 0 && count_length >= length)
                        {
                            break;
                        }
                    }
                    //每次找到回车符
                    if (ss == Data.getEnter_First())
                    {
                        //回车符计数加一
                        count_i++;
                        //已跳过相应回车符时
                        if(count_i > i)
                        {
                            break;
                        }
                    }
                }
                if (return_s.Length > 0)
                {
                    //当第一位字符为换行符时
                    if (return_s.Substring(0, 1) == "\n")
                    {
                        return return_s.Substring(1);
                    }
                }
                else
                {

                }
            }
            catch
            {

            }
            return return_s;
        }

        /// <summary>
        /// 创建一个文件夹
        /// </summary>
        /// <param name="path">文件夹路径</param>
        /// <returns></returns>
        public static string Create(string path)
        {
            try
            {
                if (Directory.Exists(getPath() + path))
                {
                    return ("文件夹已存在");
                }
                else
                {
                    Directory.CreateDirectory(getPath() + path);
                    return ("创建成功");
                }
            }
            catch (DirectoryNotFoundException e)
            {
                Form1.textboxmain_Append("CreateDirectory:" + e);
                return ("异常");
            }
        }

        /// <summary>
        /// 存入注册后的初始化数据
        /// </summary>
        /// <param name="u"></param>
        /// <param name="a"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public static string Register(string u,string a,string p)
        {
            //去除空格及空字符
            {
                u = u.Replace(" ", "");
                a = a.Replace(" ", "");
                p = p.Replace(" ", "");
                u = u.Replace("\t", "");
                a = a.Replace("\t", "");
                p = p.Replace("\t", "");
            }
            //创建以用户名为关键字的文件夹
            {
                try
                {
                    //用于存储路径值
                    string z = "Data/UserName/";
                    foreach (char s in u)
                    {
                        z = z + "/" + s;
                        Create(z);
                    }
                    //保存文件
                    Save(a, z);
                }
                catch
                {
                    return "false";
                }
            }
            //创建以账号为关键字的文件夹
            {
                try
                {
                    //用于存储路径值
                    string z = "Data/Account/";
                    foreach (char s in u)
                    {
                        z = z + "/" + s;
                        Create(z);
                    }
                    //保存文件
                    Save(u + Data.getEnter() + p, z);
                }
                catch
                {
                    return "false";
                }
            }
            return "true";
        }

        /// <summary>
        /// 获取当前应用程序路径
        /// </summary>
        /// <returns></returns>
        private static string getPath()
        {
            return AppDomain.CurrentDomain.BaseDirectory + "/";
        }
    }
}
