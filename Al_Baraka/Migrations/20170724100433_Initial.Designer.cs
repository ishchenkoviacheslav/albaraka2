using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Al_Baraka.Models;

namespace Al_Baraka.Migrations
{
    [DbContext(typeof(ProductContext))]
    [Migration("20170724100433_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Al_Baraka.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Country");

                    b.Property<string>("Desctiption");

                    b.Property<int?>("GroupsId");

                    b.Property<byte[]>("Image");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("Price");

                    b.HasKey("Id");

                    b.HasIndex("GroupsId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Al_Baraka.Models.ProductGroups", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("DriedFruits");

                    b.Property<bool>("EasternMed");

                    b.Property<bool>("Italian");

                    b.Property<bool>("Nuts");

                    b.Property<bool>("Oils");

                    b.Property<bool>("Sauces");

                    b.Property<bool>("Spice");

                    b.Property<bool>("Sweets");

                    b.HasKey("Id");

                    b.ToTable("ProductGroups");
                });

            modelBuilder.Entity("Al_Baraka.Models.Product", b =>
                {
                    b.HasOne("Al_Baraka.Models.ProductGroups", "Groups")
                        .WithMany()
                        .HasForeignKey("GroupsId");
                });
        }
    }
}
