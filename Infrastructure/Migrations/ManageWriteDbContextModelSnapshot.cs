﻿// <auto-generated />
using System;
using Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(ManageWriteDbContext))]
    partial class ManageWriteDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.Attendance", b =>
                {
                    b.Property<Guid>("AttendanceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("Checkin")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("Checkout")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("EmployeesEmployeeId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Note")
                        .HasColumnType("longtext");

                    b.Property<float?>("OvertimeHours")
                        .HasColumnType("float");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<float?>("WorkHours")
                        .HasColumnType("float");

                    b.HasKey("AttendanceId");

                    b.HasIndex("EmployeesEmployeeId");

                    b.ToTable("Attendance");
                });

            modelBuilder.Entity("Domain.Entities.Department", b =>
                {
                    b.Property<Guid>("DepartmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("DepartmentName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("EstablishDate")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("DepartmentId");

                    b.ToTable("Department");

                    b.HasData(
                        new
                        {
                            DepartmentId = new Guid("ad75d6f4-7362-4731-8775-bf1a2adeaa0a"),
                            Address = "Default",
                            DepartmentName = "Default",
                            EstablishDate = new DateTime(2025, 3, 28, 16, 18, 41, 656, DateTimeKind.Local).AddTicks(6152),
                            IsDeleted = false
                        });
                });

            modelBuilder.Entity("Domain.Entities.EmployeeRoles", b =>
                {
                    b.Property<Guid>("EmployeesEmployeeId")
                        .HasColumnType("char(36)");

                    b.Property<int>("RolesId")
                        .HasColumnType("int");

                    b.HasKey("EmployeesEmployeeId", "RolesId");

                    b.HasIndex("RolesId");

                    b.ToTable("EmployeeRoles");

                    b.HasData(
                        new
                        {
                            EmployeesEmployeeId = new Guid("56a958cb-2b69-435a-a356-7511152b4233"),
                            RolesId = 1
                        });
                });

            modelBuilder.Entity("Domain.Entities.Employees", b =>
                {
                    b.Property<Guid>("EmployeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Address")
                        .HasColumnType("longtext");

                    b.Property<string>("AvatarUrl")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("DepartmentId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Email")
                        .HasColumnType("longtext");

                    b.Property<string>("EmployeeName")
                        .HasColumnType("longtext");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("JobTitle")
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<float?>("SalaryBase")
                        .HasColumnType("float");

                    b.HasKey("EmployeeId");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Employees");

                    b.HasData(
                        new
                        {
                            EmployeeId = new Guid("56a958cb-2b69-435a-a356-7511152b4233"),
                            Address = "admin",
                            DateOfBirth = new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DepartmentId = new Guid("ad75d6f4-7362-4731-8775-bf1a2adeaa0a"),
                            Email = "admin@gmail.com",
                            EmployeeName = "admin",
                            Gender = 0,
                            IsActive = true,
                            IsDeleted = false,
                            Password = "$2a$11$2X3wHXf6JA2AM3EyYB3wSOnNbIZKWGzn4eCTpfQVWId4KYO6q/zG6",
                            PhoneNumber = "1234567890"
                        });
                });

            modelBuilder.Entity("Domain.Entities.Permissions", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Permissions", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "ReadMember"
                        },
                        new
                        {
                            Id = 2,
                            Name = "UpdateMember"
                        },
                        new
                        {
                            Id = 3,
                            Name = "DeleteMember"
                        },
                        new
                        {
                            Id = 4,
                            Name = "CreateMember"
                        });
                });

            modelBuilder.Entity("Domain.Entities.RolePermission", b =>
                {
                    b.Property<int>("RolesId")
                        .HasColumnType("int");

                    b.Property<int>("PermissionsId")
                        .HasColumnType("int");

                    b.HasKey("RolesId", "PermissionsId");

                    b.HasIndex("PermissionsId");

                    b.ToTable("RolePermissions");

                    b.HasData(
                        new
                        {
                            RolesId = 3,
                            PermissionsId = 1
                        },
                        new
                        {
                            RolesId = 4,
                            PermissionsId = 2
                        },
                        new
                        {
                            RolesId = 2,
                            PermissionsId = 1
                        },
                        new
                        {
                            RolesId = 2,
                            PermissionsId = 2
                        },
                        new
                        {
                            RolesId = 1,
                            PermissionsId = 1
                        },
                        new
                        {
                            RolesId = 1,
                            PermissionsId = 2
                        },
                        new
                        {
                            RolesId = 1,
                            PermissionsId = 4
                        },
                        new
                        {
                            RolesId = 1,
                            PermissionsId = 3
                        });
                });

            modelBuilder.Entity("Domain.Entities.Roles", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Roles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Admin"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Manager"
                        },
                        new
                        {
                            Id = 3,
                            Name = "UserView"
                        },
                        new
                        {
                            Id = 4,
                            Name = "UserEdit"
                        });
                });

            modelBuilder.Entity("Domain.Entities.Salaries", b =>
                {
                    b.Property<Guid>("SalaryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<float?>("Allowance")
                        .HasColumnType("float");

                    b.Property<float?>("Deduction")
                        .HasColumnType("float");

                    b.Property<Guid>("EmployeesEmployeeId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("Month")
                        .HasColumnType("datetime(6)");

                    b.Property<float?>("OvertimeHours")
                        .HasColumnType("float");

                    b.Property<float>("SalaryBase")
                        .HasColumnType("float");

                    b.Property<float?>("TotalSalary")
                        .HasColumnType("float");

                    b.Property<float>("WorkHours")
                        .HasColumnType("float");

                    b.HasKey("SalaryId");

                    b.HasIndex("EmployeesEmployeeId");

                    b.ToTable("Salaries");
                });

            modelBuilder.Entity("Domain.Entities.Attendance", b =>
                {
                    b.HasOne("Domain.Entities.Employees", "Employees")
                        .WithMany()
                        .HasForeignKey("EmployeesEmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employees");
                });

            modelBuilder.Entity("Domain.Entities.EmployeeRoles", b =>
                {
                    b.HasOne("Domain.Entities.Employees", "Employees")
                        .WithMany()
                        .HasForeignKey("EmployeesEmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Roles", "Roles")
                        .WithMany()
                        .HasForeignKey("RolesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employees");

                    b.Navigation("Roles");
                });

            modelBuilder.Entity("Domain.Entities.Employees", b =>
                {
                    b.HasOne("Domain.Entities.Department", "Department")
                        .WithMany("Employees")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");
                });

            modelBuilder.Entity("Domain.Entities.RolePermission", b =>
                {
                    b.HasOne("Domain.Entities.Permissions", null)
                        .WithMany()
                        .HasForeignKey("PermissionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Roles", null)
                        .WithMany()
                        .HasForeignKey("RolesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.Salaries", b =>
                {
                    b.HasOne("Domain.Entities.Employees", "Employees")
                        .WithMany()
                        .HasForeignKey("EmployeesEmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employees");
                });

            modelBuilder.Entity("Domain.Entities.Department", b =>
                {
                    b.Navigation("Employees");
                });
#pragma warning restore 612, 618
        }
    }
}
