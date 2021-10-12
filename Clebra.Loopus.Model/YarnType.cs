using Clebra.Loopus.Core.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Clebra.Loopus.Model
{
    public class YarnType : BaseModel
    {
        [MaxLength(128)]
        public string YarnCode { get; set; }
        [MaxLength(128)]
        public string YarnName { get; set; }

        public ICollection<ClothType> ClothTypes { get; set; }
    }
}