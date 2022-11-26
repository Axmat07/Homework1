﻿// <auto-generated />
using System;
using HomeworkDb1.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HomeworkDb1.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20221126213440_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CourseStudent", b =>
                {
                    b.Property<Guid>("CoursesId")
                        .HasColumnType("uuid")
                        .HasColumnName("courses_id");

                    b.Property<Guid>("StudentsId")
                        .HasColumnType("uuid")
                        .HasColumnName("students_id");

                    b.HasKey("CoursesId", "StudentsId")
                        .HasName("pk_course_student");

                    b.HasIndex("StudentsId")
                        .HasDatabaseName("ix_course_student_students_id");

                    b.ToTable("course_student", (string)null);
                });

            modelBuilder.Entity("HomeworkDb1.Domain.Entity.Course", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("start_date");

                    b.HasKey("Id")
                        .HasName("pk_courses");

                    b.ToTable("courses", (string)null);
                });

            modelBuilder.Entity("HomeworkDb1.Domain.Entity.Lector", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid?>("CourseId")
                        .HasColumnType("uuid")
                        .HasColumnName("course_id");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("last_name");

                    b.Property<int>("Salary")
                        .HasColumnType("integer")
                        .HasColumnName("salary");

                    b.HasKey("Id")
                        .HasName("pk_lectors");

                    b.HasIndex("CourseId")
                        .HasDatabaseName("ix_lectors_course_id");

                    b.ToTable("lectors", (string)null);
                });

            modelBuilder.Entity("HomeworkDb1.Domain.Entity.Student", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("last_name");

                    b.HasKey("Id")
                        .HasName("pk_students");

                    b.ToTable("students", (string)null);
                });

            modelBuilder.Entity("CourseStudent", b =>
                {
                    b.HasOne("HomeworkDb1.Domain.Entity.Course", null)
                        .WithMany()
                        .HasForeignKey("CoursesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_course_student_courses_courses_id");

                    b.HasOne("HomeworkDb1.Domain.Entity.Student", null)
                        .WithMany()
                        .HasForeignKey("StudentsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_course_student_students_students_id");
                });

            modelBuilder.Entity("HomeworkDb1.Domain.Entity.Lector", b =>
                {
                    b.HasOne("HomeworkDb1.Domain.Entity.Course", null)
                        .WithMany("Lectors")
                        .HasForeignKey("CourseId")
                        .HasConstraintName("fk_lectors_courses_course_id");
                });

            modelBuilder.Entity("HomeworkDb1.Domain.Entity.Course", b =>
                {
                    b.Navigation("Lectors");
                });
#pragma warning restore 612, 618
        }
    }
}