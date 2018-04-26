using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository_基础结构层.Models
{
    public class EntityBase
    {
        [Key]
        public int ID { get; set; }

    }
}
