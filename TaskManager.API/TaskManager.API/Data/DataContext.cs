﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.API.Models;

namespace TaskManager.API.Data
{
    public class DataContext : DbContext	
    {
        public DataContext(DbContextOptions<DataContext> options) : base
            (options) { }

        public DbSet<Project> Projects { get; set; }

    }
}
