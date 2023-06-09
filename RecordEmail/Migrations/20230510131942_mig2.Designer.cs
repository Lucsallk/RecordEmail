﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RecordEmail.Context;

#nullable disable

namespace RecordEmail.Migrations
{
    [DbContext(typeof(EmailCadastroBdContext))]
    [Migration("20230510131942_mig2")]
    partial class mig2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("RecordEmail.Models.Email", b =>
                {
                    b.Property<int>("IdEmail")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("Ativo")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("EnderecoEmail")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("ModificadoEm")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("RegistradoEm")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("TokenDeVerificacao")
                        .HasColumnType("longtext");

                    b.Property<bool>("Verificado")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("IdEmail");

                    b.ToTable("Emails");
                });
#pragma warning restore 612, 618
        }
    }
}
