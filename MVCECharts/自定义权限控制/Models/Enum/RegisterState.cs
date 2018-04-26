using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace 自定义权限控制.Models
{
    public enum RegisterState
    {
        Success = 1,
        Defeated = -1,
        abnormal = 0
    }
}