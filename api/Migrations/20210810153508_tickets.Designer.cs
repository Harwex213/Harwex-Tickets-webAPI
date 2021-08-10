﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using api.Models;

namespace api.Migrations
{
    [DbContext(typeof(HarwexTicketsApiContext))]
    [Migration("20210810153508_tickets")]
    partial class tickets
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.8")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("api.Models.Cinema", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CityId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Cinemas");
                });

            modelBuilder.Entity("api.Models.CinemaMovie", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CinemaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("MovieId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CinemaId");

                    b.HasIndex("MovieId");

                    b.ToTable("CinemaMovies");
                });

            modelBuilder.Entity("api.Models.Hall", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CinemaId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CinemaId");

                    b.ToTable("Halls");
                });

            modelBuilder.Entity("api.Models.Movie", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<TimeSpan>("Duration")
                        .HasColumnType("time");

                    b.Property<string>("Genre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("api.Models.RefreshToken", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("api.Models.Role", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Name");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("api.Models.Seat", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("HallId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Position")
                        .HasColumnType("int");

                    b.Property<int>("Row")
                        .HasColumnType("int");

                    b.Property<string>("SeatType")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("HallId");

                    b.HasIndex("SeatType");

                    b.ToTable("Seats");
                });

            modelBuilder.Entity("api.Models.SeatType", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Name");

                    b.ToTable("SeatTypes");
                });

            modelBuilder.Entity("api.Models.Service", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Name");

                    b.ToTable("Services");
                });

            modelBuilder.Entity("api.Models.Session", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CinemaMovieId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("HallId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CinemaMovieId");

                    b.HasIndex("HallId");

                    b.ToTable("Sessions");
                });

            modelBuilder.Entity("api.Models.SessionSeatPrice", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Price")
                        .HasColumnType("money");

                    b.Property<string>("SeatType")
                        .HasColumnType("nvarchar(450)");

                    b.Property<Guid>("SessionId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("SeatType");

                    b.HasIndex("SessionId");

                    b.ToTable("SessionSeatPrices");
                });

            modelBuilder.Entity("api.Models.SessionService", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Price")
                        .HasColumnType("money");

                    b.Property<string>("ServiceName")
                        .HasColumnType("nvarchar(450)");

                    b.Property<Guid>("SessionId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ServiceName");

                    b.HasIndex("SessionId");

                    b.ToTable("SessionServices");
                });

            modelBuilder.Entity("api.Models.Ticket", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("SeatId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SessionSeatPriceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("SeatId");

                    b.HasIndex("SessionSeatPriceId");

                    b.HasIndex("UserId");

                    b.ToTable("Ticket");
                });

            modelBuilder.Entity("api.Models.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleName")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("RoleName");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("api.Models.CinemaMovie", b =>
                {
                    b.HasOne("api.Models.Cinema", "Cinema")
                        .WithMany("CinemaMovies")
                        .HasForeignKey("CinemaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("api.Models.Movie", "Movie")
                        .WithMany("CinemaMovies")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cinema");

                    b.Navigation("Movie");
                });

            modelBuilder.Entity("api.Models.Hall", b =>
                {
                    b.HasOne("api.Models.Cinema", "Cinema")
                        .WithMany("Halls")
                        .HasForeignKey("CinemaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cinema");
                });

            modelBuilder.Entity("api.Models.Seat", b =>
                {
                    b.HasOne("api.Models.Hall", "Hall")
                        .WithMany("Seats")
                        .HasForeignKey("HallId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("api.Models.SeatType", "SeatTypeNavigation")
                        .WithMany("Seats")
                        .HasForeignKey("SeatType");

                    b.Navigation("Hall");

                    b.Navigation("SeatTypeNavigation");
                });

            modelBuilder.Entity("api.Models.Session", b =>
                {
                    b.HasOne("api.Models.CinemaMovie", null)
                        .WithMany("Sessions")
                        .HasForeignKey("CinemaMovieId");

                    b.HasOne("api.Models.Hall", "Hall")
                        .WithMany("Sessions")
                        .HasForeignKey("HallId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hall");
                });

            modelBuilder.Entity("api.Models.SessionSeatPrice", b =>
                {
                    b.HasOne("api.Models.SeatType", "SeatTypeNavigation")
                        .WithMany("SessionSeatPrices")
                        .HasForeignKey("SeatType");

                    b.HasOne("api.Models.Session", "Session")
                        .WithMany("SessionSeatPrices")
                        .HasForeignKey("SessionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SeatTypeNavigation");

                    b.Navigation("Session");
                });

            modelBuilder.Entity("api.Models.SessionService", b =>
                {
                    b.HasOne("api.Models.Service", "Service")
                        .WithMany()
                        .HasForeignKey("ServiceName");

                    b.HasOne("api.Models.Session", "Session")
                        .WithMany()
                        .HasForeignKey("SessionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Service");

                    b.Navigation("Session");
                });

            modelBuilder.Entity("api.Models.Ticket", b =>
                {
                    b.HasOne("api.Models.Seat", null)
                        .WithMany("Tickets")
                        .HasForeignKey("SeatId");

                    b.HasOne("api.Models.SessionSeatPrice", "SessionSeatPrice")
                        .WithMany("Tickets")
                        .HasForeignKey("SessionSeatPriceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("api.Models.User", "User")
                        .WithMany("Tickets")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SessionSeatPrice");

                    b.Navigation("User");
                });

            modelBuilder.Entity("api.Models.User", b =>
                {
                    b.HasOne("api.Models.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleName");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("api.Models.Cinema", b =>
                {
                    b.Navigation("CinemaMovies");

                    b.Navigation("Halls");
                });

            modelBuilder.Entity("api.Models.CinemaMovie", b =>
                {
                    b.Navigation("Sessions");
                });

            modelBuilder.Entity("api.Models.Hall", b =>
                {
                    b.Navigation("Seats");

                    b.Navigation("Sessions");
                });

            modelBuilder.Entity("api.Models.Movie", b =>
                {
                    b.Navigation("CinemaMovies");
                });

            modelBuilder.Entity("api.Models.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("api.Models.Seat", b =>
                {
                    b.Navigation("Tickets");
                });

            modelBuilder.Entity("api.Models.SeatType", b =>
                {
                    b.Navigation("Seats");

                    b.Navigation("SessionSeatPrices");
                });

            modelBuilder.Entity("api.Models.Session", b =>
                {
                    b.Navigation("SessionSeatPrices");
                });

            modelBuilder.Entity("api.Models.SessionSeatPrice", b =>
                {
                    b.Navigation("Tickets");
                });

            modelBuilder.Entity("api.Models.User", b =>
                {
                    b.Navigation("Tickets");
                });
#pragma warning restore 612, 618
        }
    }
}
