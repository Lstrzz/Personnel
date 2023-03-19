﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personnel
{
    internal class AppContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public AppContext() : base("DefaultConnection") { }
    }
}