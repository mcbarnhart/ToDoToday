﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ToDoTodayAPI.Data;

namespace ToDoTodayAPI.Migrations
{
    [DbContext(typeof(ToDoListDBContext))]
    [Migration("20200612130512_AddUtcDateTime")]
    partial class AddUtcDateTime
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ToDoTodayAPI.Models.TaskItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Assignee")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedByUserId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DifficultyRating")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DueTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("EstimatedTimeToComplete")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("StartTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TaskItems");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Assignee = "Matthew",
                            Description = "Get up on that ladder and clean out those filthy gutters!",
                            DifficultyRating = 4,
                            DueTime = new DateTime(2020, 6, 19, 13, 5, 11, 556, DateTimeKind.Utc).AddTicks(6273),
                            EstimatedTimeToComplete = "2 hours",
                            StartTime = new DateTime(2020, 6, 12, 13, 5, 11, 556, DateTimeKind.Utc).AddTicks(2555),
                            Title = "Clean Gutters"
                        },
                        new
                        {
                            Id = 2,
                            Assignee = "Matthew",
                            Description = "Your plants are rootbound-- fix it!",
                            DifficultyRating = 2,
                            DueTime = new DateTime(2020, 6, 19, 13, 5, 11, 556, DateTimeKind.Utc).AddTicks(9414),
                            EstimatedTimeToComplete = "2 hours",
                            StartTime = new DateTime(2020, 6, 12, 13, 5, 11, 556, DateTimeKind.Utc).AddTicks(9398),
                            Title = "Pot Plants"
                        },
                        new
                        {
                            Id = 3,
                            Assignee = "Jessie",
                            Description = "The hard part is done. Now we just have to build and hang the panels.",
                            DifficultyRating = 3,
                            DueTime = new DateTime(2020, 6, 19, 13, 5, 11, 556, DateTimeKind.Utc).AddTicks(9471),
                            EstimatedTimeToComplete = "2 hours",
                            StartTime = new DateTime(2020, 6, 12, 13, 5, 11, 556, DateTimeKind.Utc).AddTicks(9470),
                            Title = "Build the Fence"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
