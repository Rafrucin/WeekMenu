﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WeekMenu.Client.Data;

namespace WeekMenu.Client.Migrations
{
    [DbContext(typeof(ModelsDbContext))]
    [Migration("20200906193424_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.7");

            modelBuilder.Entity("WeekMenu.Client.Models.DayMenuModel", b =>
                {
                    b.Property<int>("DayMenuModelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("AfternoonTeaRecipeModelID")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("BreakfastRecipeModelID")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("DinnerRecipeModelID")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("LunchRecipeModelID")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("SecondBreakfastRecipeModelID")
                        .HasColumnType("INTEGER");

                    b.HasKey("DayMenuModelId");

                    b.HasIndex("AfternoonTeaRecipeModelID");

                    b.HasIndex("BreakfastRecipeModelID");

                    b.HasIndex("DinnerRecipeModelID");

                    b.HasIndex("LunchRecipeModelID");

                    b.HasIndex("SecondBreakfastRecipeModelID");

                    b.ToTable("DaysDBSet");
                });

            modelBuilder.Entity("WeekMenu.Client.Models.RecipeModel", b =>
                {
                    b.Property<int>("RecipeModelID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("How")
                        .HasColumnType("TEXT");

                    b.Property<string>("Ingredients")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsAfternoonTea")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsBreakfast")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsDinner")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsLunch")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsSecondBreakfast")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsVegan")
                        .HasColumnType("INTEGER");

                    b.Property<string>("RecipeImage")
                        .HasColumnType("TEXT");

                    b.Property<string>("RecipeName")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(200);

                    b.HasKey("RecipeModelID");

                    b.ToTable("RecipesDBSet");
                });

            modelBuilder.Entity("WeekMenu.Client.Models.DayMenuModel", b =>
                {
                    b.HasOne("WeekMenu.Client.Models.RecipeModel", "AfternoonTea")
                        .WithMany()
                        .HasForeignKey("AfternoonTeaRecipeModelID");

                    b.HasOne("WeekMenu.Client.Models.RecipeModel", "Breakfast")
                        .WithMany()
                        .HasForeignKey("BreakfastRecipeModelID");

                    b.HasOne("WeekMenu.Client.Models.RecipeModel", "Dinner")
                        .WithMany()
                        .HasForeignKey("DinnerRecipeModelID");

                    b.HasOne("WeekMenu.Client.Models.RecipeModel", "Lunch")
                        .WithMany()
                        .HasForeignKey("LunchRecipeModelID");

                    b.HasOne("WeekMenu.Client.Models.RecipeModel", "SecondBreakfast")
                        .WithMany()
                        .HasForeignKey("SecondBreakfastRecipeModelID");
                });
#pragma warning restore 612, 618
        }
    }
}
