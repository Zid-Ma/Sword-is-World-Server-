using SwordIsWorldServer.Script;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SwordIsWorldServer
{
    class Verification_Information
    {
        public void DataStorage(string c)
        {

        }


        /// <summary>
        /// IP检测Verification
        /// </summary>
        /// <param name="IP"></param>
        /// <returns></returns>
        public bool IPV(string IP)
        {
            if(Data.WhiteList_Array.Count != 0)//如果白名单不为零
            {
                foreach(string str in Data.WhiteList_Array)//遍历白名单数组
                {
                    if(str == IP)
                    {
                        return true;
                    }
                }
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 参数验证
        /// </summary>
        /// <param name="M"></param>
        /// <returns></returns>
        public bool V_initial_information(string M)
        {
            string final = "true";
            //0010X1234567890123XYXYKS00000000000000KS0000000001HH
            //0099X1234567890123456XYabcdefghijklmnopqrstuvwxyz123456KS123456789012HH
            //登录方式，用户名，密码，账号 ,验证码
            string A = null, B = null, C = null, D = null, E = null;
            try
            {
                A = M.Substring(0, 4);//检测登录方式,0010为剑与世界
                switch (A)
                {
                    case "0010":
                        B = M.Substring(5, 13);//检测用户名,
                        C = M.Substring(20, 18);//检测用户密码
                        D = M.Substring(40, 10);//检测用户账号
                                                //string E = M.Substring(52);//检测用户发送的信息
                        break;
                    case "0099":
                        //B = M.Substring(5, 16);
                        C = M.Substring(23, 32);
                        D = M.Substring(57, 12);
                        break;
                    default:
                        B = M.Substring(5, 16);
                        C = M.Substring(23, 32);
                        D = M.Substring(57, 12);
                        E = M.Substring(71);
                        break;
                }
            }
            catch(Exception e)
            {
                final = "登录参数验证时错误：截取字符串错误" + e;
            }
            //去除空格及空字符
            {
                A = Data.removeChar(A,' ');
                B = Data.removeChar(B, ' ');
                C = Data.removeChar(C, ' ');
                D = Data.removeChar(D, ' ');
                E = Data.removeChar(E, ' ');
                A = Data.removeChar(A, '\t');
                B = Data.removeChar(B, '\t');
                C = Data.removeChar(C, '\t');
                D = Data.removeChar(D, '\t');
                E = Data.removeChar(E, '\t');
            }
            //验证
            {
                //当链入方式为剑与世界时
                if (A == "0010")
                {
                    if (PasswordV(int.Parse(D), C))//判断账号、密码检测是否通过
                    {
                        return true;
                    }
                    else
                    {
                        final = "登录参数验证时错误：链入方式为剑与世界：密码验证错误";
                    }
                }
                //当链入方式为群聊时
                else if (A == "0099")
                {

                    //登录验证
                    if (Verification.Login(D, C))
                    {
                        //MessageBox.Show("登录验证：" + D + C);
                        return true;
                    }
                    else
                    {

                    }
                }
                //当链入方式为注册账号时
                else if (A == "Join")
                {
                    //MessageBox.Show("注册验证");
                    //注册验证
                    if (Verification.Register(B, D, C , E))
                    {
                        return true;
                    }
                }
                else if(A == "Code")
                {
                    Code_Verification code_Verification = new Code_Verification();
                    //发送验证码
                    code_Verification.Send(D);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 密码验证
        /// </summary>
        /// <param name="a">账号</param>
        /// <param name="p">密码</param>
        /// <returns></returns>
        public bool PasswordV(int a,string p)//账号和密码检测,a为账号，p为密码
        {
            //判断是否符合连接规则
            switch (a)
            {
                case 1:
                    if (p == "XYKS00000000000000")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case 2:
                    if (p == "XYKS")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
            }
            //最大人数与，密码
            if (a < Data.MaxPeople && p == Data.Password)
            {
                return true;
            }
            return false;
        }
    }
}
