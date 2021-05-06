using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SwordIsWorldServer.Script
{
    /// <summary>
    /// 验证
    /// </summary>
    class Verification
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="a">账号</param>
        /// <param name="p">密码</param>
        /// <returns>是否登录成功</returns>
        public static bool Login(string a, string p)
        {
            string final = "true";
            //文件验证
            {
                //存储数据存放位置
                string path = "Data/Account";
                //创建相应文件夹
                Data_Save.Create(path);
                //遍历账号
                foreach(char z in a)
                {
                    //路径值增加
                    path += "/" + z;
                    Data_Save.Create(path);
                }
                //判断数据中信息是否一致
                {
                    string login = null, password = null;
                    login = Data_Save.Read(path + "/Login.txt");
                    //读取文件错误时
                    if (login == "00000")
                    {
                        final = "读取文件错误";
                    }
                    //截取密码部分
                    password = Data_Save.Read_enter(login, 0, 0);
                    if (p != password)
                    {
                        final = "账号或密码错误:" + password + "1001" + login;
                    }
                }
            }
            {
                if (final == "true")
                {
                    //验证通过
                    return true;
                }
                else
                {
                    Form1.textboxmain_Append(final);
                    return false;
                }
            }
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="u">用户名</param>
        /// <param name="a">账号</param>
        /// <param name="p">密码</param>
        /// <param name="v">验证码</param>
        /// <returns></returns>
        public static bool Register(string u,string a,string p,string v)
        {
            //去除空格,以及去除空字符
            {
                u = u.Replace(" ", "");
                a = a.Replace(" ", "");
                p = p.Replace(" ", "");
                v = v.Replace(" ", "");
                u = u.Replace("\t", "");
                a = a.Replace("\t", "");
                p = p.Replace("\t", "");
                v = v.Replace("\t", "");
            }
            //验证码判断
            {
                int i = 0;
                //获得相应的值
                Data.Verification_Code.TryGetValue(a, out i);
                if (v != i.ToString())
                {
                    Form1.textboxmain_Append(Data.getEnter() + "用户注册时，验证码错误:" + u + Data.getEnter());
                    //验证失败
                    return false;
                }
            }
            //最终返回结果
            string final = "true";
            //存储数据存放位置
            string path_a = "Data/Account";
            string path_u = "Data/UserName";
            //密码判断
            {
                //判断数据是否合法
                {
                    //密码长度在8位到32位之间时
                    if (p.Length >= 8 && p.Length <= 32)
                    {
                        //没有非法字符
                        if (ASCII(p, 33, 126))
                        {
                            
                        }
                        else
                        {
                            final = "注册时密码验证失败:非法字符";
                        }
                    }
                    else
                    {
                        final = "注册时密码验证失败:长度错误";
                    }
                }
            }
            //账号判断
            {
                //判断数据是否合法
                {
                    //判断字符数是否符合规则
                    if (a.Length >= 8 && a.Length <= 12)
                    {
                        
                    }
                    else
                    {
                        final = "注册时账号验证失败:长度错误";
                    }
                    //获得字符数组
                    char[] c = a.ToCharArray();
                    //遍历账号
                    for (int i = 0; i < c.Length; i++)
                    {
                        //如果值为数字
                        if (ASCII(c[i], 48, 57))
                        {
                            //路径值增加
                            path_a += "/" + c[i];
                            Data_Save.Create(path_a);
                        }
                        else
                        {
                            final = "注册时账号验证失败:非法字符";
                        }
                    }
                }
                //判断数据中信息是否一致
                {
                    string login = null;
                    login = Data_Save.Read(path_a + "/Login.txt");
                    //读取文件错误时:找不到相应文本
                    if (login == "00000")
                    {
                        //储存登录用基本信息:用户名与密码
                        Data_Save.Save(p, path_a + "/Login.txt");
                        Data_Save.Save(u, path_a + "/UserName.txt");
                    }
                    else
                    {
                        final = "注册时账号验证失败:账号重复";
                    }
                }
            }
            //用户名判断
            {
                //判断数据是否合法
                {
                    //用户名为4位到16位之间时
                    if (u.Length >= 4 && u.Length <= 16) 
                    {
                        //遍历用户名
                        for (int i = 0; i < u.Length; i++)
                        {
                            //路径值增加
                            path_u += "/" + (u.Substring(i, 1));
                            Data_Save.Create(path_u);
                        }
                    }
                    else
                    {
                        final = "注册时用户名验证失败:长度错误";
                    }
                }
                //判断数据中信息是否一致
                {
                    string account = null;
                    account = Data_Save.Read(path_u + "/Account.txt");
                    //读取文件错误时:找不到相应文本
                    if (account == "00000")
                    {
                        //储存验证用户名是否重复的基本信息:账号
                        Data_Save.Save(a, path_u + "/Account.txt");
                    }
                    else
                    {
                        final = "注册时用户名验证失败:用户名重复";
                    }
                }
            }
            //最终结果
            {
                if (final == "true")
                {
                    //注册信息存放到文件中
                    string s = Data_Save.Register(u, a, p);
                    //验证通过                  且                    
                    Form1.textboxmain_Append(Data.getEnter() + "用户注册时，验证通过:" + u + Data.getEnter() + s + Data.getEnter());
                    //判断注册结果
                    if (s == "true")
                        return true;
                    else
                        return false;
                }
                else
                {
                    MessageBox.Show(final);
                    return false;
                }
            }
        }

        /// <summary>
        /// ASCII码验证_Char
        /// </summary>
        /// <param name="c">字符</param>
        /// <param name="o">起始值</param>
        /// <param name="f">最终值</param>
        /// <returns></returns>
        private static bool ASCII(char c, int o, int f)
        {
            if((int)c >= o && (int)c <= f)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// ASCII码验证_String
        /// </summary>
        /// <param name="s">字符串</param>
        /// <param name="o">起始值</param>
        /// <param name="f">最终值</param>
        /// <returns></returns>
        private static bool ASCII(string s, int o, int f)
        {
            //遍历整个字符串
            foreach (char c in s)
            {
                if ((int)c >= o && (int)c <= f)
                {

                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 文件验证-账号
        /// </summary>
        /// <param name="a">账号</param>
        /// <param name="f">文件名</param>
        /// <returns></returns>
        public static string File_account(string a, string f)
        {
            //去除不必要的字符
            a = Data.removeChar(a, ' ');
            a = Data.removeChar(a,'\t');
            //存储数据存放位置
            string path = "Data/Account";
            //创建相应文件夹
            Data_Save.Create(path);
            //遍历账号
            foreach (char z in a)
            {
                //路径值增加
                path += "/" + z;
                Data_Save.Create(path);
            }
            return Data_Save.Read(path + "/" + f);
        }
    }
}
