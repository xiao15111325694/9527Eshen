using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace 自定义权限控制.Models.Enum
{
    public enum AuthorizeState
    {
        RoleErro = -1,
        TokenErro = -2,
        UserValidateErro = -3,
        ValidateSuucss = 1
    }
}