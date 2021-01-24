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
    class SaveData
    {
        /// <summary>
        /// 保存设置
        /// </summary>
        /// <param name="data">需要传入的数据</param>
        public void SaveSet(string data)//保存设置
        {
            
            
        }


        /// <summary>
        /// 保存文件,储存地址为应用程序下
        /// </summary>
        /// <param name="Path">相对路径</param>
        /// <param name="information">需要存储的信息</param>
        public void Save(string Path, string information)
        {
            try
            {
                //FileStream aFile = new FileStream(@"" + Path, FileMode.OpenOrCreate);
                FileStream aFile = new FileStream(Application.StartupPath + "/" + Path, FileMode.OpenOrCreate);//创建文件到当前运行应用程序的路径下
                StreamWriter sw = new StreamWriter(aFile);
                sw.Write(information);
                sw.Close();
                sw.Dispose();
            }
            catch //(FileNotFoundException e)
            {
                
            }
        }//生成一个txt文本


        /// <summary>
        /// 读取文件,读取路径为当前应用程序文件夹下
        /// </summary>
        /// <param name="Path">相对路径</param>
        /// <returns></returns>
        public string Read(string Path)
        {
            if (File.Exists(Application.StartupPath + "/" + Path))
            {
                FileStream aFile = new FileStream(Application.StartupPath + "/" + Path, FileMode.Open);
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
        }//读取一个txt文本
    }
}
