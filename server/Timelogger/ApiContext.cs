﻿using Microsoft.EntityFrameworkCore;
using Timelogger.Domain.Entities;

namespace Timelogger
{
	public class ApiContext : DbContext
	{
		public ApiContext(DbContextOptions<ApiContext> options)
			: base(options)
		{
		}

		public DbSet<Project> Projects { get; set; }

		public DbSet<TimeRegister> TimeRegisters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApiContext).Assembly);
		}
    }
}
