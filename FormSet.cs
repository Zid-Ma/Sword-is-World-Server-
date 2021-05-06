using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SwordIsWorldServer
{
    public partial class FormSet : Form
    {
        public FormSet()
        {
            InitializeComponent();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if(checkBoxMaximumPeople.Checked == true)//当最大人数被选中时
            {
                if (textBoxMaximumPeople != null)//如果值不为空
                {
                    Data.MaxPeople = int.Parse(textBoxMaximumPeople.Text);
                }
            }
            if(checkBoxLoginPassword.Checked == true)//登录密码被选中时
            {
                if (textBoxLoginPassword != null)
                {
                    Data.Password = textBoxLoginPassword.Text;
                }
            }
            if (checkBoxWhiteList.Checked == true)//当白名单项被选中时
            {
                Data.WhiteList = textBoxWhiteList.Text;//储存数据
            }
            if (checkBoxTimeInterval.Checked)//发送频率选中时
            {
                Data.TimeInterval = int.Parse(textBoxTimeInterval.Text);
            }
            if (checkBoxThreadCountLeft.Checked)//线程数选中
            {
                Data.ThreadCount = 1;
            }
            else
            {
                Data.ThreadCount = 10;
            }
            int mixLength;
            mixLength = 0;
            string wl = null;
            wl = Data.WhiteList;
            Data.WhiteList_Array.Clear();//先清空数组信息
            if (wl != null)
            {
                for (int i = 0; i < wl.Length; i++)//遍历整个白名单
                {
                    if (wl.Substring(i, 1) == "\r" && (wl.Substring(i + 1, 1)) == "\n")//判断是否有换行
                    {
                        string d = wl.Substring(mixLength, i + 1 - mixLength);
                        Data.WhiteList_Array.Add(d);//剪下一整行的信息添加进白名单数组
                        mixLength = i + 2;//进入下一行
                    }
                    else if (wl.Length == i + 1)//如果到达了最后一行
                    {
                        string d = wl.Substring(mixLength, i + 1 - mixLength);
                        Data.WhiteList_Array.Add(d);
                    }
                }
            }
            foreach(string str in Data.WhiteList_Array)//遍历信息
            {
                MessageBox.Show(str);
            }
            MessageBox.Show("保存成功");
        }
    }
}
