﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OnlineExamination.DataAccess.Context;

#nullable disable

namespace OnlineExamination.DataAccess.Migrations
{
    [DbContext(typeof(onlineExamDbContext))]
    [Migration("20230824001252_first_migration")]
    partial class first_migration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("OnlineExamination.DataAccess.ExamResults", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Answer")
                        .HasColumnType("int");

                    b.Property<int?>("ExamsId")
                        .HasColumnType("int");

                    b.Property<int>("QnAsId")
                        .HasColumnType("int");

                    b.Property<int>("StudentsId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ExamsId");

                    b.HasIndex("QnAsId");

                    b.HasIndex("StudentsId");

                    b.ToTable("ExamResaults");
                });

            modelBuilder.Entity("OnlineExamination.DataAccess.Exams", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<int>("GroupsId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Time")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("GroupsId");

                    b.ToTable("Exams");
                });

            modelBuilder.Entity("OnlineExamination.DataAccess.Groups", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("UsersId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UsersId");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("OnlineExamination.DataAccess.QnAs", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Answer")
                        .HasMaxLength(250)
                        .HasColumnType("int");

                    b.Property<int>("ExamsId")
                        .HasColumnType("int");

                    b.Property<string>("Option1")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Option2")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Option3")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Option4")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Question")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ExamsId");

                    b.ToTable("QnAs");
                });

            modelBuilder.Entity("OnlineExamination.DataAccess.Students", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CVFileName")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Contact")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("GroupsId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PictureFileName")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("GroupsId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("OnlineExamination.DataAccess.Users", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("OnlineExamination.DataAccess.ExamResults", b =>
                {
                    b.HasOne("OnlineExamination.DataAccess.Exams", "Exams")
                        .WithMany("ExamResults")
                        .HasForeignKey("ExamsId");

                    b.HasOne("OnlineExamination.DataAccess.QnAs", "QnAs")
                        .WithMany("ExamResults")
                        .HasForeignKey("QnAsId")
                        .IsRequired();

                    b.HasOne("OnlineExamination.DataAccess.Students", "Students")
                        .WithMany("ExamResults")
                        .HasForeignKey("StudentsId")
                        .IsRequired();

                    b.Navigation("Exams");

                    b.Navigation("QnAs");

                    b.Navigation("Students");
                });

            modelBuilder.Entity("OnlineExamination.DataAccess.Exams", b =>
                {
                    b.HasOne("OnlineExamination.DataAccess.Groups", "Groups")
                        .WithMany("Exams")
                        .HasForeignKey("GroupsId")
                        .IsRequired();

                    b.Navigation("Groups");
                });

            modelBuilder.Entity("OnlineExamination.DataAccess.Groups", b =>
                {
                    b.HasOne("OnlineExamination.DataAccess.Users", "Users")
                        .WithMany("Groups")
                        .HasForeignKey("UsersId")
                        .IsRequired();

                    b.Navigation("Users");
                });

            modelBuilder.Entity("OnlineExamination.DataAccess.QnAs", b =>
                {
                    b.HasOne("OnlineExamination.DataAccess.Exams", "Exams")
                        .WithMany("QnAs")
                        .HasForeignKey("ExamsId")
                        .IsRequired();

                    b.Navigation("Exams");
                });

            modelBuilder.Entity("OnlineExamination.DataAccess.Students", b =>
                {
                    b.HasOne("OnlineExamination.DataAccess.Groups", "Groups")
                        .WithMany("Students")
                        .HasForeignKey("GroupsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Groups");
                });

            modelBuilder.Entity("OnlineExamination.DataAccess.Exams", b =>
                {
                    b.Navigation("ExamResults");

                    b.Navigation("QnAs");
                });

            modelBuilder.Entity("OnlineExamination.DataAccess.Groups", b =>
                {
                    b.Navigation("Exams");

                    b.Navigation("Students");
                });

            modelBuilder.Entity("OnlineExamination.DataAccess.QnAs", b =>
                {
                    b.Navigation("ExamResults");
                });

            modelBuilder.Entity("OnlineExamination.DataAccess.Students", b =>
                {
                    b.Navigation("ExamResults");
                });

            modelBuilder.Entity("OnlineExamination.DataAccess.Users", b =>
                {
                    b.Navigation("Groups");
                });
#pragma warning restore 612, 618
        }
    }
}
