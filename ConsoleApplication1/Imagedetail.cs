using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageList
{
    public class Imagedetail
    {
        public int ufid { get; set; }
        public string user_avatar { get; set; }
        public Emotion[] emotions { get; set; }
        public string user_name { get; set; }
        public bool comment_on { get; set; }
        public int view_count { get; set; }
        public bool rt { get; set; }
        public int comment_count { get; set; }
        public string[] thumbs { get; set; }
        public bool is_followed { get; set; }
        public int share_count { get; set; }
        public int last_id { get; set; }
        public int follow_count { get; set; }
        public int user_id { get; set; }
        public int total { get; set; }
    }

    public class Emotion
    {
        public string url { get; set; }
        public int online_id { get; set; }
        public string thumb { get; set; }
    }

}
