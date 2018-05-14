using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmoticonCommon;
using Newtonsoft.Json;
using ImageList;
using System.Net;
using System.Drawing;
namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            // SaveImage();
            Test();
        }
        public static void  Test()
        {
            //Send(url, "GET", null, null);

            string str = HttpHelper.Send("https://hz.5i5j.com/map/ajax/location/sale?onMove=false&locationId=2&locationLevel=2&bounds={'e':120.477135,'w':119.857376,'s':30.136711,'n':30.355351}&boundsLevel=3&pageSize=20&page=1", "GET", null, null);
            Console.Write(str);
            Console.ReadLine();
        }
        public static void SaveImage(string last_id = "")
        {
            string repsonsestr = HttpHelper.SendGet("http://mobile.shenmeiguan.cn/folder/cherrypick/");
            if (last_id != "")
            {
                repsonsestr = HttpHelper.SendGet("http://mobile.shenmeiguan.cn/folder/cherrypick/?last_id=" + last_id);
            }
            Rootobject rb = JsonConvert.DeserializeObject<Rootobject>(repsonsestr);
            if (rb != null && rb.rt == true)
            {
                //遍历集合创建文件夹
                foreach (List lis in rb.list)
                {
                    string dertory = string.Format(@"C:\PickEmoticon\{0}", lis.folder.name);
                    if (!System.IO.Directory.Exists(dertory))
                    {
                        try
                        {
                            System.IO.Directory.CreateDirectory(dertory);
                        }
                        catch (Exception)
                        {
                            dertory = string.Format(@"C:\PickEmoticon\{0}", DateTime.Now.ToString("yyyyMMddhh24mmss"));

                        }

                    }
                    string detailurl = "http://mobile.shenmeiguan.cn/user/social/" + lis.user.id + "/" + "folder/" + lis.folder.id + "?pagesize=200";
                    repsonsestr = HttpHelper.SendGet(detailurl);
                    Imagedetail imgdetail = JsonConvert.DeserializeObject<Imagedetail>(repsonsestr);
                    if (imgdetail != null && imgdetail.rt == true)
                    {
                        foreach (Emotion em in imgdetail.emotions.ToList())
                        {
                            //根据路径创建图片
                            getimages(dertory, em.url, em.online_id.ToString());
                        }
                    }

                }
                SaveImage(rb.last_id.ToString());
            }
        }
        public static void getimages(string dertory, string url, string online_id)
        {
            System.Drawing.Image downImage = null;
            //创建一个request 同时可以配置requst其余属性
            try
            {
                System.Net.WebRequest imgRequst = System.Net.WebRequest.Create(url);

                //在这里我是以流的方式保存图片

                downImage = System.Drawing.Image.FromStream(imgRequst.GetResponse().GetResponseStream());

                //string dertory = string.Format(@"C:\PickEmoticon\{0}", DateTime.Now.ToString("yyyy-MM-dd"));

                string fileName = string.Format("{0}.jpg", online_id);

                //if (!System.IO.Directory.Exists(dertory))
                //{

                //    System.IO.Directory.CreateDirectory(dertory);
                //}

                downImage.Save(dertory + "\\" + fileName);
                downImage.Dispose();

            }
            catch (Exception)
            {
                //downImage.Dispose();

            }


            //用完一定要释放

        }
        public static void SaveDetailImage(string usrid, string folderid, string dertory, string last_id = "")
        {
            string detailurl = "";
            if (last_id != "")
                detailurl = "http://mobile.shenmeiguan.cn/user/social/" + usrid + "/" + "folder/" + folderid + "?pagesize=30&last_id=" + last_id;
            else
                detailurl = "http://mobile.shenmeiguan.cn/user/social/" + usrid + "/" + "folder/" + folderid + "?pagesize=30";
            string repsonsestr = HttpHelper.SendGet(detailurl);
            Imagedetail imgdetail = JsonConvert.DeserializeObject<Imagedetail>(repsonsestr);
            if (imgdetail != null && imgdetail.rt == true)
            {
                foreach (Emotion em in imgdetail.emotions.ToList())
                {
                    //根据路径创建图片
                    getimages(dertory, em.url, em.online_id.ToString());
                }
            }

        }
    }

}
