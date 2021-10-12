using Clebra.Loopus.Core.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Clebra.Loopus.Model
{
    public class Color : BaseModel
    {
        [MaxLength(128)]
        public string ColorName { get; set; }

        [MaxLength(128)]
        public string ColorCode { get; set; }

        public ICollection<Product> Product { get; set; }
    }
}