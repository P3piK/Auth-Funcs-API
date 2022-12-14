// <auto-generated />
using System;
using AuthFuncsRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AuthFuncsRepository.Migrations
{
    [DbContext(typeof(AFContext))]
    [Migration("20220823082409_AddUserStatusUserRole")]
    partial class AddUserStatusUserRole
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("AuthFuncsRepository.Entity.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ModifierId")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<int>("StatusId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ModifierId");

                    b.HasIndex("RoleId");

                    b.HasIndex("StatusId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Login = "admin",
                            Modified = new DateTime(2022, 8, 23, 10, 24, 9, 548, DateTimeKind.Local).AddTicks(4148),
                            Password = "pass123",
                            RoleId = 1,
                            StatusId = 1
                        });
                });

            modelBuilder.Entity("AuthFuncsRepository.Entity.UserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("UserRoles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Created = new DateTime(2022, 8, 23, 10, 24, 9, 548, DateTimeKind.Local).AddTicks(4128),
                            Name = "User"
                        },
                        new
                        {
                            Id = 2,
                            Created = new DateTime(2022, 8, 23, 10, 24, 9, 548, DateTimeKind.Local).AddTicks(4131),
                            Name = "Superuser"
                        },
                        new
                        {
                            Id = 3,
                            Created = new DateTime(2022, 8, 23, 10, 24, 9, 548, DateTimeKind.Local).AddTicks(4133),
                            Name = "Admin"
                        });
                });

            modelBuilder.Entity("AuthFuncsRepository.Entity.UserStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("UserStatuses");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Created = new DateTime(2022, 8, 23, 10, 24, 9, 548, DateTimeKind.Local).AddTicks(4017),
                            Name = "Active"
                        },
                        new
                        {
                            Id = 2,
                            Created = new DateTime(2022, 8, 23, 10, 24, 9, 548, DateTimeKind.Local).AddTicks(4049),
                            Name = "Inactive"
                        },
                        new
                        {
                            Id = 3,
                            Created = new DateTime(2022, 8, 23, 10, 24, 9, 548, DateTimeKind.Local).AddTicks(4051),
                            Name = "PasswordReset"
                        },
                        new
                        {
                            Id = 4,
                            Created = new DateTime(2022, 8, 23, 10, 24, 9, 548, DateTimeKind.Local).AddTicks(4053),
                            Name = "NotConfirmed"
                        });
                });

            modelBuilder.Entity("AuthFuncsRepository.Entity.User", b =>
                {
                    b.HasOne("AuthFuncsRepository.Entity.User", "Modifier")
                        .WithMany()
                        .HasForeignKey("ModifierId");

                    b.HasOne("AuthFuncsRepository.Entity.UserRole", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AuthFuncsRepository.Entity.UserStatus", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Modifier");

                    b.Navigation("Role");

                    b.Navigation("Status");
                });
#pragma warning restore 612, 618
        }
    }
}
