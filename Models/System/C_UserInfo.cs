using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.System
{
    public enum IsOnUseEnum {
        启用=0,
        停用=1
    }
    public class C_UserInfo:BaseModel
    {
        public string UserInfoName { get; set; }
        public string UserInfoCode { get; set; }
        public string LoginName { get; set; }
        public string Password { get; set; }
        public IsOnUseEnum? IsOnUse { get; set; }
    }
}
