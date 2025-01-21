﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Films.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Films.Infrastructure.Migrations
{
    [DbContext(typeof(FilmDbContext))]
    [Migration("20250120072703_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Films.Core.FilmManagement.Film", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.ComplexProperty<Dictionary<string, object>>("Description", "Films.Core.FilmManagement.Film.Description#Description", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("film_description");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Director", "Films.Core.FilmManagement.Film.Director#Director", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("film_director");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Genre", "Films.Core.FilmManagement.Film.Genre#Genre", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("film_genre");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Rating", "Films.Core.FilmManagement.Film.Rating#Rating", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<int>("Value")
                                .HasColumnType("integer")
                                .HasColumnName("film_rating");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("ReleaseYear", "Films.Core.FilmManagement.Film.ReleaseYear#ReleaseYear", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<int>("Value")
                                .HasColumnType("integer")
                                .HasColumnName("film_release_year");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Title", "Films.Core.FilmManagement.Film.Title#Title", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(60)
                                .HasColumnType("character varying(60)")
                                .HasColumnName("film_name");
                        });

                    b.HasKey("Id")
                        .HasName("pk_films");

                    b.ToTable("films", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
