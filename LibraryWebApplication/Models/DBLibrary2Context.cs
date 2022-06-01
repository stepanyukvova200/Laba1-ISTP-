using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LibraryWebApplication.Models
{
    public partial class DBLibrary2Context : DbContext
    {
        public DBLibrary2Context()
        {
        }

        public DBLibrary2Context(DbContextOptions<DBLibrary2Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Bootcamp> Bootcamps { get; set; } = null!;
        public virtual DbSet<Club> Clubs { get; set; } = null!;
        public virtual DbSet<ClubsSponsor> ClubsSponsors { get; set; } = null!;
        public virtual DbSet<ClubsTournament> ClubsTournaments { get; set; } = null!;
        public virtual DbSet<CountriesDirectory> CountriesDirectories { get; set; } = null!;
        public virtual DbSet<Player> Players { get; set; } = null!;
        public virtual DbSet<RoleDirectory> RoleDirectories { get; set; } = null!;
        public virtual DbSet<Sponsor> Sponsors { get; set; } = null!;
        public virtual DbSet<Tournament> Tournaments { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server = INANITY\\SQLEXPRESS02; Database = DBLibrary2; Trusted_Connection = True; ");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bootcamp>(entity =>
            {
                entity.Property(e => e.BootcampId).HasColumnName("BootcampID");

                entity.Property(e => e.ClubId).HasColumnName("ClubID");

                entity.Property(e => e.ConstructionType).HasMaxLength(50);

                entity.Property(e => e.Location).HasMaxLength(50);

                entity.HasOne(d => d.Club)
                    .WithMany(p => p.Bootcamps)
                    .HasForeignKey(d => d.ClubId)
                    .HasConstraintName("FK_Bootcamps_Clubs");
            });

            modelBuilder.Entity<Club>(entity =>
            {
                entity.Property(e => e.ClubId).HasColumnName("ClubID");

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.NameClub).HasMaxLength(50);

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Clubs)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK_Clubs_CountriesDirectory");
            });

            modelBuilder.Entity<ClubsSponsor>(entity =>
            {
                entity.HasKey(e => e.ClubSponsorId);

                entity.Property(e => e.ClubSponsorId).HasColumnName("ClubSponsorID");

                entity.Property(e => e.ClubId).HasColumnName("ClubID");

                entity.Property(e => e.Edrpou).HasColumnName("EDRPOU");

                entity.HasOne(d => d.Club)
                    .WithMany(p => p.ClubsSponsors)
                    .HasForeignKey(d => d.ClubId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClubsSponsors_Clubs");

                entity.HasOne(d => d.EdrpouNavigation)
                    .WithMany(p => p.ClubsSponsors)
                    .HasForeignKey(d => d.Edrpou)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClubsSponsors_Sponsors");
            });

            modelBuilder.Entity<ClubsTournament>(entity =>
            {
                entity.HasKey(e => e.ClubTournamentId);

                entity.Property(e => e.ClubTournamentId).HasColumnName("ClubTournamentID");

                entity.Property(e => e.ClubId).HasColumnName("ClubID");

                entity.Property(e => e.TournamentId).HasColumnName("TournamentID");

                entity.HasOne(d => d.Club)
                    .WithMany(p => p.ClubsTournaments)
                    .HasForeignKey(d => d.ClubId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClubsTournaments_Clubs");

                entity.HasOne(d => d.Tournament)
                    .WithMany(p => p.ClubsTournaments)
                    .HasForeignKey(d => d.TournamentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClubsTournaments_Tournaments");
            });

            modelBuilder.Entity<CountriesDirectory>(entity =>
            {
                entity.ToTable("CountriesDirectory");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Country).HasMaxLength(50);
            });

            modelBuilder.Entity<Player>(entity =>
            {
                entity.Property(e => e.PlayerId).HasColumnName("PlayerID");

                entity.Property(e => e.Awards).HasMaxLength(50);

                entity.Property(e => e.ClubId).HasColumnName("ClubID");

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.Nickname).HasMaxLength(50);

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.HasOne(d => d.Club)
                    .WithMany(p => p.Players)
                    .HasForeignKey(d => d.ClubId)
                    .HasConstraintName("FK_Players_Clubs");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Players)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Players_CountriesDirectory");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Players)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Players_RoleDirectory");
            });

            modelBuilder.Entity<RoleDirectory>(entity =>
            {
                entity.ToTable("RoleDirectory");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Role).HasMaxLength(50);
            });

            modelBuilder.Entity<Sponsor>(entity =>
            {
                entity.HasKey(e => e.Edrpou);

                entity.Property(e => e.Edrpou)
                    .ValueGeneratedNever()
                    .HasColumnName("EDRPOU");

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.NameSponsor).HasMaxLength(50);

                entity.Property(e => e.SphereOfActivity).HasMaxLength(50);

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Sponsors)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK_Sponsors_CountriesDirectory");
            });

            modelBuilder.Entity<Tournament>(entity =>
            {
                entity.Property(e => e.TournamentId).HasColumnName("TournamentID");

                entity.Property(e => e.Awards).HasMaxLength(50);

                entity.Property(e => e.Location).HasMaxLength(50);

                entity.Property(e => e.Regulations).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
