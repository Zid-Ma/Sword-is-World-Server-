using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwordIsWorldServer
{
    class InformationVerification
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
            if(Data.ArrayWhiteList.Count != 0)//如果白名单不为零
            {
                foreach(string str in Data.ArrayWhiteList)//遍历白名单数组
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

        public bool Verification(string M)//参数检测
        {
            //0010X1234567890123XYXYKS00000000000000KS0000000001HH
            string A, B, C, D;// E;
            try
            {
                A = M.Substring(0, 4);//检测登录方式,0010为剑与世界
                B = M.Substring(5, 13);//检测用户名,
                C = M.Substring(20, 18);//检测用户密码
                D = M.Substring(40, 10);//检测用户账号
                //string E = M.Substring(52);//检测用户发送的信息
            }
            catch//(Exception e)
            {
                return false;
            }
            if(PasswordV(int.Parse(D), C))//判断账号、密码检测是否通过
            {
                return true;
            }
            return false;
        }

        public bool PasswordV(int a,string b)//账号和密码检测,a为账号，b为密码
        {
            switch(a)
            {
                case 1:
                    if (b == "XYKS00000000000000")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case 2:
                    if (b == "XYKS")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
            }//判断是否符合连接规则
            if(a < Data.MaxPeople && b == Data.Password)//最大人数与，密码
            {
                return true;
            }
            return false;
        }
    }
}
