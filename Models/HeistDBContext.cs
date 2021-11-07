using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agency04.Models
{
    public class HeistDBContext : DbContext
    {
        public DbSet<HeistMember> HeistMember { get; set; }
        public DbSet<HeistSkills> HeistSkills { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HeistSkills>(entity =>
           {
               entity.HasKey(e => e.HeistSkillsId);
               entity.ToTable("HeistSkills");
               entity.Property(e => e.HeistSkillsId).HasColumnName("HeistSkillsID");
               entity.Property(e => e.Name)
               .HasMaxLength(30)
               .IsUnicode(false);
               entity.Property(e => e.Level)
               .HasMaxLength(10)
               .IsUnicode(false);           
           });

            modelBuilder.Entity<HeistMember>(entity =>
            {
                entity.HasKey(e => e.HeistMemberId);
                entity.ToTable("HeistMember");
                entity.Property(e => e.HeistMemberId).HasColumnName("HeistMemberID");
                entity.Property(e => e.Name)
                .HasMaxLength(30)
                .IsUnicode(false);
                entity.Property(e => e.Sex)
                .HasMaxLength(1)
                .IsUnicode(false);
                entity.Property(e => e.Email)
                .IsUnicode(false);
                entity.Property(e => e.Skills)
                .HasMaxLength(20)
                .IsUnicode(false);
                entity.Property(e => e.MainSkill)
                .HasMaxLength(20)
                .IsUnicode(false);
                entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false);
            });
        }
     }
 
}
