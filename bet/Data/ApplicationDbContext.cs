using Microsoft.EntityFrameworkCore;
using FutebolSimplesBetsHub.Models;

namespace FutebolSimplesBetsHub.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            // Ensure database is created
            Database.EnsureCreated();
        }

        public DbSet<Match> Matches { get; set; }
        public DbSet<Bet> Bets { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Match entity
            modelBuilder.Entity<Match>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Participant1).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Participant2).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Competition).IsRequired().HasMaxLength(100);
                entity.Property(e => e.CompetitionId).IsRequired().HasMaxLength(50);
                entity.Property(e => e.OddsParticipant1).HasPrecision(5, 2);
                entity.Property(e => e.OddsDraw).HasPrecision(5, 2);
                entity.Property(e => e.OddsParticipant2).HasPrecision(5, 2);
            });

            // Configure Bet entity
            modelBuilder.Entity<Bet>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.BetType).IsRequired().HasMaxLength(20);
                entity.Property(e => e.Amount).HasPrecision(10, 2);
                entity.Property(e => e.Odds).HasPrecision(5, 2);
                entity.Property(e => e.PotentialWin).HasPrecision(10, 2);
                entity.Property(e => e.Status).HasMaxLength(20);
                
                entity.HasOne(e => e.Match)
                    .WithMany(e => e.Bets)
                    .HasForeignKey(e => e.MatchId)
                    .OnDelete(DeleteBehavior.Restrict);
                    
                entity.HasOne(e => e.User)
                    .WithMany(e => e.Bets)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Configure User entity
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(150);
                entity.Property(e => e.Username).IsRequired().HasMaxLength(100);
                entity.Property(e => e.PasswordHash).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Balance).HasPrecision(10, 2);
                entity.Property(e => e.Role).HasMaxLength(20);
                
                entity.HasIndex(e => e.Email).IsUnique();
                entity.HasIndex(e => e.Username).IsUnique();
            });

            // Seed data
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Seed Matches
            modelBuilder.Entity<Match>().HasData(
                new Match
                {
                    Id = 1,
                    Participant1 = "Bob Esponja",
                    Participant2 = "Patrick",
                    Competition = "Corrida de Cavalo Marinho",
                    CompetitionId = "seahorse-race",
                    MatchDate = DateTime.Today.AddHours(16).AddMinutes(30),
                    Date = "Hoje",
                    Time = "16:30",
                    OddsParticipant1 = 2.10m,
                    OddsDraw = 3.20m,
                    OddsParticipant2 = 3.40m,
                    IsLive = true,
                    CreatedAt = DateTime.Now
                },
                new Match
                {
                    Id = 2,
                    Participant1 = "Lula Molusco",
                    Participant2 = "Sandy",
                    Competition = "Quem Caça Mais Águas Vivas",
                    CompetitionId = "jellyfish-catching",
                    MatchDate = DateTime.Today.AddHours(18),
                    Date = "Hoje",
                    Time = "18:00",
                    OddsParticipant1 = 2.80m,
                    OddsDraw = 3.10m,
                    OddsParticipant2 = 2.50m,
                    IsLive = true,
                    CreatedAt = DateTime.Now
                },
                new Match
                {
                    Id = 3,
                    Participant1 = "Sr. Siriguejo",
                    Participant2 = "Plankton",
                    Competition = "Quem Come Mais Hambúrgueres de Siri",
                    CompetitionId = "krabby-patty-eating",
                    MatchDate = DateTime.Today.AddDays(1).AddHours(16),
                    Date = "Amanhã",
                    Time = "16:00",
                    OddsParticipant1 = 1.90m,
                    OddsDraw = 3.40m,
                    OddsParticipant2 = 4.20m,
                    IsLive = false,
                    CreatedAt = DateTime.Now
                },
                new Match
                {
                    Id = 4,
                    Participant1 = "Bob Esponja",
                    Participant2 = "Lula Molusco",
                    Competition = "Corrida de Cavalo Marinho",
                    CompetitionId = "seahorse-race",
                    MatchDate = DateTime.Today.AddDays(2).AddHours(20),
                    Date = "Domingo",
                    Time = "20:00",
                    OddsParticipant1 = 2.60m,
                    OddsDraw = 3.00m,
                    OddsParticipant2 = 2.90m,
                    IsLive = false,
                    CreatedAt = DateTime.Now
                }
            );

            // Seed Users
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Name = "Usuário Demo",
                    Email = "demo@fendadobiquini.com",
                    Username = "demo",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456"),
                    Balance = 1000.00m,
                    Role = "user",
                    IsActive = true,
                    CreatedAt = DateTime.Now
                }
            );
        }
    }
} 