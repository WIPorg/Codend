﻿// <auto-generated />
using System;
using Codend.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Codend.Persistence.Migrations
{
    [DbContext(typeof(CodendApplicationDbContext))]
    [Migration("20230829125125_ProjectTask-ProjectTaskStatus-Properties")]
    partial class ProjectTaskProjectTaskStatusProperties
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Codend.Domain.Core.Events.DomainEvent", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ProjectId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ProjectTaskId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.HasIndex("ProjectTaskId");

                    b.ToTable("DomainEvent");
                });

            modelBuilder.Entity("Codend.Domain.Entities.Project", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Deleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.HasKey("Id");

                    b.ToTable("Project");
                });

            modelBuilder.Entity("Codend.Domain.Entities.ProjectTask", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AssigneeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Deleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DueDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("DueDate");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Priority")
                        .HasColumnType("int")
                        .HasColumnName("ProjectTaskPriority");

                    b.Property<Guid>("ProjectId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("SprintId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("StatusId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.HasIndex("SprintId");

                    b.HasIndex("StatusId");

                    b.ToTable("ProjectTask");

                    b.HasDiscriminator<string>("Discriminator").HasValue("ProjectTask");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Codend.Domain.Entities.ProjectTaskStatus", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProjectId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("ProjectTaskStatus");
                });

            modelBuilder.Entity("Codend.Domain.Entities.ProjectVersion", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Deleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<Guid>("ProjectId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("ProjectVersion");
                });

            modelBuilder.Entity("Codend.Domain.Entities.Sprint", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Deleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<Guid>("ProjectId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("Sprint");
                });

            modelBuilder.Entity("Codend.Domain.Entities.BugFixProjectTask", b =>
                {
                    b.HasBaseType("Codend.Domain.Entities.ProjectTask");

                    b.HasDiscriminator().HasValue("BugFixProjectTask");
                });

            modelBuilder.Entity("Codend.Domain.Core.Events.DomainEvent", b =>
                {
                    b.HasOne("Codend.Domain.Entities.Project", null)
                        .WithMany("DomainEvents")
                        .HasForeignKey("ProjectId");

                    b.HasOne("Codend.Domain.Entities.ProjectTask", null)
                        .WithMany("DomainEvents")
                        .HasForeignKey("ProjectTaskId");
                });

            modelBuilder.Entity("Codend.Domain.Entities.Project", b =>
                {
                    b.OwnsOne("Codend.Domain.ValueObjects.ProjectDescription", "ProjectDescription", b1 =>
                        {
                            b1.Property<Guid>("ProjectId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Description")
                                .IsRequired()
                                .HasMaxLength(2000)
                                .HasColumnType("nvarchar(2000)")
                                .HasColumnName("ProjectDescription");

                            b1.HasKey("ProjectId");

                            b1.ToTable("Project");

                            b1.WithOwner()
                                .HasForeignKey("ProjectId");
                        });

                    b.OwnsOne("Codend.Domain.ValueObjects.ProjectName", "ProjectName", b1 =>
                        {
                            b1.Property<Guid>("ProjectId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)")
                                .HasColumnName("ProjectName");

                            b1.HasKey("ProjectId");

                            b1.ToTable("Project");

                            b1.WithOwner()
                                .HasForeignKey("ProjectId");
                        });

                    b.Navigation("ProjectDescription");

                    b.Navigation("ProjectName")
                        .IsRequired();
                });

            modelBuilder.Entity("Codend.Domain.Entities.ProjectTask", b =>
                {
                    b.HasOne("Codend.Domain.Entities.Project", null)
                        .WithMany("ProjectTasks")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Codend.Domain.Entities.Sprint", null)
                        .WithMany("SprintProjectTasks")
                        .HasForeignKey("SprintId");

                    b.HasOne("Codend.Domain.Entities.ProjectTaskStatus", null)
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.OwnsOne("Codend.Domain.ValueObjects.ProjectTaskDescription", "Description", b1 =>
                        {
                            b1.Property<Guid>("ProjectTaskId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Description")
                                .IsRequired()
                                .HasMaxLength(2000)
                                .HasColumnType("nvarchar(2000)")
                                .HasColumnName("Description");

                            b1.HasKey("ProjectTaskId");

                            b1.ToTable("ProjectTask");

                            b1.WithOwner()
                                .HasForeignKey("ProjectTaskId");
                        });

                    b.OwnsOne("Codend.Domain.ValueObjects.ProjectTaskName", "Name", b1 =>
                        {
                            b1.Property<Guid>("ProjectTaskId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasMaxLength(150)
                                .HasColumnType("nvarchar(150)")
                                .HasColumnName("Name");

                            b1.HasKey("ProjectTaskId");

                            b1.ToTable("ProjectTask");

                            b1.WithOwner()
                                .HasForeignKey("ProjectTaskId");
                        });

                    b.Navigation("Description");

                    b.Navigation("Name")
                        .IsRequired();
                });

            modelBuilder.Entity("Codend.Domain.Entities.ProjectTaskStatus", b =>
                {
                    b.HasOne("Codend.Domain.Entities.Project", null)
                        .WithMany()
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.OwnsOne("Codend.Domain.ValueObjects.ProjectTaskStatusName", "Name", b1 =>
                        {
                            b1.Property<Guid>("ProjectTaskStatusId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasMaxLength(150)
                                .HasColumnType("nvarchar(150)")
                                .HasColumnName("Name");

                            b1.HasKey("ProjectTaskStatusId");

                            b1.ToTable("ProjectTaskStatus");

                            b1.WithOwner()
                                .HasForeignKey("ProjectTaskStatusId");
                        });

                    b.Navigation("Name")
                        .IsRequired();
                });

            modelBuilder.Entity("Codend.Domain.Entities.ProjectVersion", b =>
                {
                    b.HasOne("Codend.Domain.Entities.Project", null)
                        .WithMany("ProjectVersions")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("Codend.Domain.Entities.Sprint", b =>
                {
                    b.HasOne("Codend.Domain.Entities.Project", null)
                        .WithMany("Sprints")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("Codend.Domain.Entities.Project", b =>
                {
                    b.Navigation("DomainEvents");

                    b.Navigation("ProjectTasks");

                    b.Navigation("ProjectVersions");

                    b.Navigation("Sprints");
                });

            modelBuilder.Entity("Codend.Domain.Entities.ProjectTask", b =>
                {
                    b.Navigation("DomainEvents");
                });

            modelBuilder.Entity("Codend.Domain.Entities.Sprint", b =>
                {
                    b.Navigation("SprintProjectTasks");
                });
#pragma warning restore 612, 618
        }
    }
}
