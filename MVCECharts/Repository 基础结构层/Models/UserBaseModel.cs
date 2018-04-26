using System;
using System.ComponentModel.DataAnnotations;

namespace Repository_基础结构层.Models
{
    public class UserBaseModel : EntityBase
    {

        public String UserName { get; set; }

        public string Uid { get; set; }

        public string Pwd { get; set; }

        public string ReadirectUrl { get; set; }
    }
}