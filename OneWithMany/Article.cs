﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneWithMany
{
    class Article
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public List<Comment> Comments { get; set; } = new List<Comment>();

    }
}
