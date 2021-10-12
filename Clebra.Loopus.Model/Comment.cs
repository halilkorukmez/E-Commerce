using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clebra.Loopus.Core.Model;

namespace Clebra.Loopus.Model
{
   public class Comment : BaseModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string EMail { get; set; }
        public string Telephone { get; set; }
        public string Content { get; set; }


    }
}
