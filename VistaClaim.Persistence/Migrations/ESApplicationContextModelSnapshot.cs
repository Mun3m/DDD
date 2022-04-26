﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VistaClaim.Persistence.EventStore;

namespace VistaClaim.Persistence.Migrations
{
    [DbContext(typeof(ESApplicationContext))]
    partial class ESApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("VistaClaim.Application.Interfaces.ReadModels+Assignment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ClaimNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ClientId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ClientName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Assignments");
                });

            modelBuilder.Entity("VistaClaim.Application.Interfaces.ReadModels+Client", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("VistaClaim.Application.Interfaces.ReadModels+Dashboard", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Approved")
                        .HasColumnType("int");

                    b.Property<int>("Completed")
                        .HasColumnType("int");

                    b.Property<int>("New")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Dashboards");
                });

            modelBuilder.Entity("VistaClaim.Persistence.EventStore.Checkpoint", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.ToTable("Checkpoints");
                });

            modelBuilder.Entity("VistaClaim.Persistence.EventStore.CurrentPosition", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CheckpointId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<long>("CommitPosition")
                        .HasColumnType("bigint");

                    b.Property<long>("PreparePosition")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("CheckpointId")
                        .IsUnique()
                        .HasFilter("[CheckpointId] IS NOT NULL");

                    b.ToTable("CurrentPosition");
                });

            modelBuilder.Entity("VistaClaim.Persistence.EventStore.CurrentPosition", b =>
                {
                    b.HasOne("VistaClaim.Persistence.EventStore.Checkpoint", "Checkpoint")
                        .WithOne("Position")
                        .HasForeignKey("VistaClaim.Persistence.EventStore.CurrentPosition", "CheckpointId");

                    b.Navigation("Checkpoint");
                });

            modelBuilder.Entity("VistaClaim.Persistence.EventStore.Checkpoint", b =>
                {
                    b.Navigation("Position");
                });
#pragma warning restore 612, 618
        }
    }
}
