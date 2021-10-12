using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clebra.Loopus.Core.Model;

namespace Clebra.Loopus.Model
{
    public class ProductFile : BaseModel
    {
        public string ImageName { get; set; }
        public ICollection<Product> Product { get; set; }

    }
}
