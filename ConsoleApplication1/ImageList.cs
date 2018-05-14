using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageList
{
    public class Rootobject
    {
        public List[] list { get; set; }//表情包主题集合
        public bool rt { get; set; }//返回是否成功
        public int last_id { get; set; }//分页标记
    }

    public class List
    {
        public string desc { get; set; }//描述，默认空
        public string[] thumbs { get; set; }//缩略图集合
        public User user { get; set; }//创建表情用户信息
        public Folder folder { get; set; }//文件夹信息
    }

    public class User
    {
        public string avatar { get; set; }//用户头像
        public int id { get; set; }//用户id
        public string name { get; set; }//用户姓名
    }

    public class Folder
    {
        public int id { get; set; }//文件夹id
        public string name { get; set; }//文件夹名称
    }
}
