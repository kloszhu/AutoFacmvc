using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Models.EasyUI
{
    public  class Student:BaseModel
    {
        public string Name { get; set; }
        public int? Age { get; set; }
        public IEnumerable<Books> books { get; set; }
        public int ID { get; set; }
    }

    public class Books
    {
        public string BookName { get; set; }
        public string BookVersion { get; set; }
    }
}
