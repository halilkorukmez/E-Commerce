﻿using Clebra.Loopus.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clebra.Loopus.Model
{
    public class SubImage : BaseModel
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImgPath { get; set; }
    }
}
