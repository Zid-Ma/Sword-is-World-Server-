using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwordIsWorldServer.Script
{
    /// <summary>
    /// 验证码
    /// </summary>
    class Code_Verification
    {
        /// <summary>
        /// 计数指针
        /// </summary>
        static int i_count = 0;
        /// <summary>
        /// 每秒的验证码计数
        /// </summary>
        static int[] count_Code = new int[60];

        /// <summary>
        /// 固定更新调用
        /// </summary>
        public static void FixedUpdata()
        {
            //每秒一次
            if (i_count < count_Code.Length)
            {
                //验证码计数归零
                count_Code[i_count] = 0;
                i_count++;
            }
            else
            {
                count_Code[i_count] = 0;
                i_count = 0;
            }
            Destroy_auto();
        }

        /// <summary>
        /// 发送
        /// </summary>
        /// <param name="a">账号/手机号</param>
        public void Send(string a)
        {
            Data.CodeList_Array.Add(a);
            Random random = new Random();
            //获得一个随机数
            int r = random.Next(1000,9999);
            Data.Verification_Code.Add(a,r);
            //发送短信至客户处
            Form1.textboxmainback_Append(r.ToString());

            //当前秒内，验证码计数加一
            count_Code[i_count] += 1;
        }

        /// <summary>
        /// 销毁
        /// </summary>
        /// <param name="a">账号/手机号</param>
        public void Destroy(string a)
        {
            try
            {
                Data.Verification_Code.Remove(a);
            }
            catch
            {

            }
        }

        /// <summary>
        /// 自动销毁
        /// </summary>
        private static void Destroy_auto()
        {
            //销毁多个
            if (Data.CodeList_Array.Count > count_Code[i_count])
            {
                //遍历数组中记录的次数
                for (int i = 0; i < count_Code[i_count]; i++)
                {
                    //删除词典中的信息
                    try
                    {
                        Data.Verification_Code.Remove(Data.CodeList_Array[1].ToString());
                    }
                    catch
                    {

                    }
                    //删除arraylist列表
                    try
                    {
                        Data.CodeList_Array.RemoveAt(1);
                    }
                    catch
                    {
                        Data.CodeList_Array.RemoveRange(1, 1);
                    }
                }
            }
            //销毁单个
            else if (Data.CodeList_Array.Count > 1)
            {
                //删除词典中的信息
                try
                {
                    Data.Verification_Code.Remove(Data.CodeList_Array[1].ToString());
                }
                catch
                {

                }
                //删除arraylist列表
                try
                {
                    Data.CodeList_Array.RemoveAt(1);
                }
                catch
                {
                    Data.CodeList_Array.RemoveRange(1, 1);
                }
            }
        }
    }
}
