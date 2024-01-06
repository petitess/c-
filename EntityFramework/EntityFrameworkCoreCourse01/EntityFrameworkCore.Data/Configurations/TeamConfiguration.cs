﻿using EntityFrameworkCore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkCore.Data.Configurations
{
    internal class TeamConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder) 
        {
            builder.HasIndex(x => x.Name).IsUnique();

            builder.HasMany(x => x.HomeMatches)
                .WithOne(x => x.HomeTeam)
                .HasForeignKey(x => x.HomeTeamId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.AwayMatches)
                .WithOne(x => x.AwayTeam)
                .HasForeignKey(x => x.AwayTeamId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(
            new Team
            {
                Id = 1,
                Name = "Tivoli Gardens F.C.",
                CreatedDate = new DateTime(2024, 01, 04),
                LeagueId = 1,
                CoachId = 1
           
            },
            new Team
            {
                Id = 2,
                Name = "Waterhouse F.C.",
                CreatedDate = new DateTime(2024, 01, 04),
                LeagueId = 1,
                CoachId = 2
            },
            new Team
            {
                Id = 3,
                Name = "Humble Lions F.C.",
                CreatedDate = new DateTime(2024, 01, 04),
                LeagueId = 1,
                CoachId = 3
            }
            ); ;
        }
    }
}
