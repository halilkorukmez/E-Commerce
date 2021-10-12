using Clebra.Loopus.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clebra.Loopus.Model
{
    public class About : BaseModel
    {
        public string Content { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }

    }
}
