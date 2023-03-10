// <auto-generated />
using System;
using Fire.DataAccess.DbConnection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Fire.DataAccess.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230112163707_ı-updated-PaymentCont")]
    partial class ıupdatedPaymentCont
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("Fire.Entity.Concrete.Branch", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ModifyDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("branch")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("İsDelete")
                        .HasColumnType("bit");

                    b.HasKey("id");

                    b.ToTable("Branch");
                });

            modelBuilder.Entity("Fire.Entity.Concrete.Check", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ModifyDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("bankid")
                        .HasColumnType("int");

                    b.Property<int>("branchid")
                        .HasColumnType("int");

                    b.Property<DateTime>("checkDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("checkNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("toWhoWasGiven")
                        .HasColumnType("int");

                    b.Property<int>("whoFromGetted")
                        .HasColumnType("int");

                    b.Property<bool>("İsDelete")
                        .HasColumnType("bit");

                    b.HasKey("id");

                    b.ToTable("CheckCont");
                });

            modelBuilder.Entity("Fire.Entity.Concrete.Customer", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ModifyDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("branchid")
                        .HasColumnType("int");

                    b.Property<bool>("İsDelete")
                        .HasColumnType("bit");

                    b.HasKey("id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("Fire.Entity.Concrete.Earnings", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ModifyDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("İsDelete")
                        .HasColumnType("bit");

                    b.HasKey("id");

                    b.ToTable("earnings");
                });

            modelBuilder.Entity("Fire.Entity.Concrete.Employees", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ModifyDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Salary")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Surname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("branchid")
                        .HasColumnType("int");

                    b.Property<bool>("İsDelete")
                        .HasColumnType("bit");

                    b.HasKey("id");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("Fire.Entity.Concrete.Expenses", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("ExpensesPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("ModifyDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("branchid")
                        .HasColumnType("int");

                    b.Property<bool>("personSalaryControll")
                        .HasColumnType("bit");

                    b.Property<bool>("İsDelete")
                        .HasColumnType("bit");

                    b.HasKey("id");

                    b.ToTable("Expenses");
                });

            modelBuilder.Entity("Fire.Entity.Concrete.Factory", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ModifyDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("İsDelete")
                        .HasColumnType("bit");

                    b.HasKey("id");

                    b.ToTable("factories");
                });

            modelBuilder.Entity("Fire.Entity.Concrete.FactoryProductType", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("factoryid")
                        .HasColumnType("int");

                    b.Property<decimal>("kgPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("id");

                    b.HasIndex("factoryid");

                    b.ToTable("FactoryProductType");
                });

            modelBuilder.Entity("Fire.Entity.Concrete.FactoryQuantity", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Kg")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("ModifyDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Totalkg")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Totalprice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("alacak")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("branchid")
                        .HasColumnType("int");

                    b.Property<int>("dailyTakeCount")
                        .HasColumnType("int");

                    b.Property<int>("factoryid")
                        .HasColumnType("int");

                    b.Property<int>("factoryproducttypeid")
                        .HasColumnType("int");

                    b.Property<bool>("paid")
                        .HasColumnType("bit");

                    b.Property<bool>("İsDelete")
                        .HasColumnType("bit");

                    b.HasKey("id");

                    b.ToTable("factoryQuantities");
                });

            modelBuilder.Entity("Fire.Entity.Concrete.Payment", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ModifyDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("PaymentContId")
                        .HasColumnType("int");

                    b.Property<decimal>("debtprice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("giveprice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("İsDelete")
                        .HasColumnType("bit");

                    b.HasKey("id");

                    b.ToTable("Payment");
                });

            modelBuilder.Entity("Fire.Entity.Concrete.PaymentCont", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsPaid")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ModifyDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ReceiptId")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("İsDelete")
                        .HasColumnType("bit");

                    b.HasKey("id");

                    b.ToTable("PaymentCont");
                });

            modelBuilder.Entity("Fire.Entity.Concrete.ProductQuantity", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Kg")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("ModifyDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("ProductPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ReceiptId")
                        .HasColumnType("int");

                    b.Property<decimal>("Totalkg")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Totalprice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("alacak")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("paid")
                        .HasColumnType("bit");

                    b.Property<int>("productTypeid")
                        .HasColumnType("int");

                    b.Property<bool>("İsDelete")
                        .HasColumnType("bit");

                    b.HasKey("id");

                    b.HasIndex("productTypeid");

                    b.ToTable("productQuantity");
                });

            modelBuilder.Entity("Fire.Entity.Concrete.ProductType", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ModifyDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("kgPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("İsDelete")
                        .HasColumnType("bit");

                    b.HasKey("id");

                    b.ToTable("ProductTypes");
                });

            modelBuilder.Entity("Fire.Entity.Concrete.Receipt", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("BranchId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<int>("DailyTakeCount")
                        .HasColumnType("int");

                    b.Property<DateTime>("ModifyDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ReceiptDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("İsDelete")
                        .HasColumnType("bit");

                    b.HasKey("id");

                    b.ToTable("Receipt");
                });

            modelBuilder.Entity("Fire.Entity.Concrete.StockTracking", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ModifyDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("branchid")
                        .HasColumnType("int");

                    b.Property<int>("productid")
                        .HasColumnType("int");

                    b.Property<decimal>("totalkg")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("İsDelete")
                        .HasColumnType("bit");

                    b.HasKey("id");

                    b.ToTable("StockTracking");
                });

            modelBuilder.Entity("Fire.Entity.Concrete.User", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ModifyDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("branchid")
                        .HasColumnType("int");

                    b.Property<string>("email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("userroleid")
                        .HasColumnType("int");

                    b.Property<bool>("İsDelete")
                        .HasColumnType("bit");

                    b.HasKey("id");

                    b.HasIndex("userroleid");

                    b.ToTable("users");
                });

            modelBuilder.Entity("Fire.Entity.Concrete.UserRoles", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ModifyDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("İsDelete")
                        .HasColumnType("bit");

                    b.HasKey("id");

                    b.ToTable("userRoles");
                });

            modelBuilder.Entity("Fire.Entity.Concrete.bank", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ModifyDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("İsDelete")
                        .HasColumnType("bit");

                    b.HasKey("id");

                    b.ToTable("Bank");
                });

            modelBuilder.Entity("Fire.Entity.Concrete.FactoryProductType", b =>
                {
                    b.HasOne("Fire.Entity.Concrete.Factory", "factory")
                        .WithMany("factoryProductTypes")
                        .HasForeignKey("factoryid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("factory");
                });

            modelBuilder.Entity("Fire.Entity.Concrete.ProductQuantity", b =>
                {
                    b.HasOne("Fire.Entity.Concrete.ProductType", "productType")
                        .WithMany("ProductQuantities")
                        .HasForeignKey("productTypeid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("productType");
                });

            modelBuilder.Entity("Fire.Entity.Concrete.User", b =>
                {
                    b.HasOne("Fire.Entity.Concrete.UserRoles", "userRoles")
                        .WithMany("Users")
                        .HasForeignKey("userroleid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("userRoles");
                });

            modelBuilder.Entity("Fire.Entity.Concrete.Factory", b =>
                {
                    b.Navigation("factoryProductTypes");
                });

            modelBuilder.Entity("Fire.Entity.Concrete.ProductType", b =>
                {
                    b.Navigation("ProductQuantities");
                });

            modelBuilder.Entity("Fire.Entity.Concrete.UserRoles", b =>
                {
                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
