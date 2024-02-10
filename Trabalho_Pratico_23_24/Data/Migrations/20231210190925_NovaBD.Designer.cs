﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Trabalho_Pratico_23_24.Data;

#nullable disable

namespace Trabalho_Pratico_23_24.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231210190925_NovaBD")]
    partial class NovaBD
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.23")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("HabitacaoLocador", b =>
                {
                    b.Property<int>("LocadorId")
                        .HasColumnType("int");

                    b.Property<int>("LocadoresId")
                        .HasColumnType("int");

                    b.HasKey("LocadorId", "LocadoresId");

                    b.HasIndex("LocadoresId");

                    b.ToTable("HabitacaoLocador");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Trabalho_Pratico_23_24.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DataNascimento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<int?>("NIF")
                        .HasColumnType("int");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("PrimeiroNome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UltimoNome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Trabalho_Pratico_23_24.Models.Arrendamento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("Aceite")
                        .HasColumnType("bit");

                    b.Property<int?>("CheckInId")
                        .HasColumnType("int");

                    b.Property<int?>("CheckOutId")
                        .HasColumnType("int");

                    b.Property<string>("ClienteId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("DatraEntrada")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DatraSaida")
                        .HasColumnType("datetime2");

                    b.Property<bool>("EntreguePorCliente")
                        .HasColumnType("bit");

                    b.Property<int?>("ImovelId")
                        .HasColumnType("int");

                    b.Property<int?>("LocadorId")
                        .HasColumnType("int");

                    b.Property<bool>("PorConfirmar")
                        .HasColumnType("bit");

                    b.Property<bool>("RecebidoPeloLocador")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("CheckInId");

                    b.HasIndex("CheckOutId");

                    b.HasIndex("ClienteId");

                    b.HasIndex("ImovelId");

                    b.HasIndex("LocadorId");

                    b.ToTable("Arrendamentos");
                });

            modelBuilder.Entity("Trabalho_Pratico_23_24.Models.Categorias", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categorias");
                });

            modelBuilder.Entity("Trabalho_Pratico_23_24.Models.CheckIn", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Danos")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EntregadorId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("EquipamentoOpcionais")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("HoraEntrada")
                        .HasColumnType("datetime2");

                    b.Property<string>("Observacoes")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TemDanos")
                        .HasColumnType("bit");

                    b.Property<bool>("TemEquipamentosOpcionais")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("EntregadorId");

                    b.ToTable("CheckIns");
                });

            modelBuilder.Entity("Trabalho_Pratico_23_24.Models.CheckOut", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Danos")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EquipamentoOpcionais")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("HoraSaida")
                        .HasColumnType("datetime2");

                    b.Property<string>("Observacoes")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RecetorId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("TemDanos")
                        .HasColumnType("bit");

                    b.Property<bool>("TemEquipamentosOpcionais")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("RecetorId");

                    b.ToTable("CheckOuts");
                });

            modelBuilder.Entity("Trabalho_Pratico_23_24.Models.Funcionario", b =>
                {
                    b.Property<int>("FuncionarioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FuncionarioId"), 1L, 1);

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("LocadorId")
                        .HasColumnType("int");

                    b.Property<int>("N_telefone")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UtilizadorId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("FuncionarioId");

                    b.HasIndex("LocadorId");

                    b.HasIndex("UtilizadorId");

                    b.ToTable("Funcionarios");
                });

            modelBuilder.Entity("Trabalho_Pratico_23_24.Models.Gestor", b =>
                {
                    b.Property<int>("gestorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("gestorId"), 1L, 1);

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("LocadorId")
                        .HasColumnType("int");

                    b.Property<int>("N_telefone")
                        .HasColumnType("int");

                    b.Property<string>("PrimeiroNome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UltimoNome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UtilizadorId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("gestorId");

                    b.HasIndex("LocadorId");

                    b.HasIndex("UtilizadorId");

                    b.ToTable("Gestores");
                });

            modelBuilder.Entity("Trabalho_Pratico_23_24.Models.Habitacao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<decimal>("AvaliacaoLocador")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("CategoriaId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<decimal>("CustoArrendamento")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("DataFimContrato")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataInicioContrato")
                        .HasColumnType("datetime2");

                    b.Property<string>("Localizacao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PeriodoMaximoArrendamento")
                        .HasColumnType("int");

                    b.Property<int>("PeriodoMinimoArrendamento")
                        .HasColumnType("int");

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CategoriaId");

                    b.ToTable("Habitacao");
                });

            modelBuilder.Entity("Trabalho_Pratico_23_24.Models.Locador", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("N_Telefone")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Locadores");
                });

            modelBuilder.Entity("HabitacaoLocador", b =>
                {
                    b.HasOne("Trabalho_Pratico_23_24.Models.Habitacao", null)
                        .WithMany()
                        .HasForeignKey("LocadorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Trabalho_Pratico_23_24.Models.Locador", null)
                        .WithMany()
                        .HasForeignKey("LocadoresId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Trabalho_Pratico_23_24.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Trabalho_Pratico_23_24.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Trabalho_Pratico_23_24.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Trabalho_Pratico_23_24.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Trabalho_Pratico_23_24.Models.Arrendamento", b =>
                {
                    b.HasOne("Trabalho_Pratico_23_24.Models.CheckIn", "CheckIn")
                        .WithMany()
                        .HasForeignKey("CheckInId");

                    b.HasOne("Trabalho_Pratico_23_24.Models.CheckOut", "CheckOut")
                        .WithMany()
                        .HasForeignKey("CheckOutId");

                    b.HasOne("Trabalho_Pratico_23_24.Models.ApplicationUser", "Cliente")
                        .WithMany()
                        .HasForeignKey("ClienteId");

                    b.HasOne("Trabalho_Pratico_23_24.Models.Habitacao", "Imovel")
                        .WithMany()
                        .HasForeignKey("ImovelId");

                    b.HasOne("Trabalho_Pratico_23_24.Models.Locador", "Locador")
                        .WithMany()
                        .HasForeignKey("LocadorId");

                    b.Navigation("CheckIn");

                    b.Navigation("CheckOut");

                    b.Navigation("Cliente");

                    b.Navigation("Imovel");

                    b.Navigation("Locador");
                });

            modelBuilder.Entity("Trabalho_Pratico_23_24.Models.CheckIn", b =>
                {
                    b.HasOne("Trabalho_Pratico_23_24.Models.ApplicationUser", "Entregador")
                        .WithMany()
                        .HasForeignKey("EntregadorId");

                    b.Navigation("Entregador");
                });

            modelBuilder.Entity("Trabalho_Pratico_23_24.Models.CheckOut", b =>
                {
                    b.HasOne("Trabalho_Pratico_23_24.Models.ApplicationUser", "Recetor")
                        .WithMany()
                        .HasForeignKey("RecetorId");

                    b.Navigation("Recetor");
                });

            modelBuilder.Entity("Trabalho_Pratico_23_24.Models.Funcionario", b =>
                {
                    b.HasOne("Trabalho_Pratico_23_24.Models.Locador", "Locador")
                        .WithMany()
                        .HasForeignKey("LocadorId");

                    b.HasOne("Trabalho_Pratico_23_24.Models.ApplicationUser", "ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UtilizadorId");

                    b.Navigation("ApplicationUser");

                    b.Navigation("Locador");
                });

            modelBuilder.Entity("Trabalho_Pratico_23_24.Models.Gestor", b =>
                {
                    b.HasOne("Trabalho_Pratico_23_24.Models.Locador", "Locador")
                        .WithMany()
                        .HasForeignKey("LocadorId");

                    b.HasOne("Trabalho_Pratico_23_24.Models.ApplicationUser", "ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UtilizadorId");

                    b.Navigation("ApplicationUser");

                    b.Navigation("Locador");
                });

            modelBuilder.Entity("Trabalho_Pratico_23_24.Models.Habitacao", b =>
                {
                    b.HasOne("Trabalho_Pratico_23_24.Models.Categorias", "Categoria")
                        .WithMany("Habitacoes")
                        .HasForeignKey("CategoriaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Categoria");
                });

            modelBuilder.Entity("Trabalho_Pratico_23_24.Models.Categorias", b =>
                {
                    b.Navigation("Habitacoes");
                });
#pragma warning restore 612, 618
        }
    }
}
