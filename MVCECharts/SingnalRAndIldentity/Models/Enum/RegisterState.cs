using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SingnalRAndIldentity.Models
{
    public enum RegisterState
    {
        Success = 1,
        Defeated = -1,
        abnormal = 0
    }
}