using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SwordIsWorldServer
{
    class RecordModify
    {
        /// <summary>
        /// 删除客户词典信息
        /// </summary>
        /// <param name="i">IP:端口</param>
        public static void DeleteClientDictionary(string i)
        {
            try
            {
                if (Form1.ClientConnectionItems.ContainsKey(i))
                {
                    Form1.ClientConnectionItems.Remove(i);
                }
                if (Form1.ClientUserName.ContainsKey(i))
                {
                    Form1.ClientUserName.Remove(i);
                }
                if (Form1.ClientData.ContainsKey(i))
                {
                    Form1.ClientData.Remove(i);
                }
            }
            catch(Exception e)
            {
                MessageBox.Show("移除客户时信息失败" + e);
            }
        }
    }
}
