using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Data.SqlClient;

namespace Comp2001Referral.Models
{
    public class access :DbContext
    {
        public virtual DbSet<customers> Customers { get; set; }
        public virtual DbSet<billingAddresses> BillingAddresses { get; set; }
        public virtual DbSet<deliveryAddresses> DeliveryAddresses { get; set; }
        public virtual DbSet<orders> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=socem1.uopnet.plymouth.ac.uk; databse=COMP2001_PScutter-cairns; User ID=PScutter-cairns; Password=XtoQ494+");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            //Sets up customer table
            modelBuilder.Entity<customers>(entity =>
            {
                entity.HasKey(e => e.IDNumber).HasName("PK__CUSTOMERS_TABLE__B9BF33078DB946F8");
                entity.ToTable("CUSTOMERS_TABLE");
                entity.Property(e => e.IDNumber).ValueGeneratedNever().HasColumnName("IDNumber");
                entity.Property(e => e.title).IsRequired().HasMaxLength(4).IsUnicode(false);
                entity.Property(e => e.firstName).IsRequired().HasMaxLength(10).IsUnicode(false);
                entity.Property(e => e.lastName).IsRequired().HasMaxLength(14).IsUnicode(false);
                entity.Property(e => e.phoneNumber).IsRequired().HasMaxLength(13).IsUnicode(false);
                //entity.Property(e => e.deliveryAddressID).IsRequired().IsUnicode(false);
                //entity.Property(e => e.billingAddressID).IsRequired().IsUnicode();
            });

            //sets up orders table
            modelBuilder.Entity<orders>(entity =>
            {
                entity.HasKey(e => e.orderNumber).HasName("PK__ORDERS_TABLE__B9BF33078DB946F8");
                entity.ToTable("ORDERS_TABLE");
                entity.Property(e => e.orderNumber).ValueGeneratedNever().HasColumnName("OrderNumber");
                entity.Property(e => e.customerID).IsRequired().IsUnicode(false);
                entity.Property(e => e.totalAmount).IsRequired().IsUnicode(false);
                entity.Property(e => e.date).IsRequired().IsUnicode(false);
                entity.Property(e => e.dispatched).IsRequired().IsUnicode(false);
            });

            //sets up billing table
            modelBuilder.Entity<billingAddresses>(entity =>
            {
                entity.HasKey(e => e.billingID).HasName("PK__BILLING_ADDRESSES_TABLE__B9BF33078DB946F8");
                entity.ToTable("BILLING_ADDRESSES_TABLE");
                entity.Property(e => e.billingID).ValueGeneratedNever().HasColumnName("BillingID");
                entity.Property(e => e.houseName).IsRequired().IsUnicode(false);
                entity.Property(e => e.streetName).IsRequired().IsUnicode(false);
                entity.Property(e => e.town).IsRequired().IsUnicode(false);
                entity.Property(e => e.county).IsRequired().IsUnicode(false);
                entity.Property(e => e.country).IsRequired().IsUnicode(false);
                entity.Property(e => e.postcode).IsRequired().IsUnicode(false);
            });
            
            //sets up delivery table
            modelBuilder.Entity<deliveryAddresses>(entity =>
            {
                entity.HasKey(e => e.deliveryID).HasName("PK__DELIVERY_ADDRESSES_TABLE__B9BF33078DB946F8");
                entity.ToTable("DELIVERY_ADDRESSES_TABLE");
                entity.Property(e => e.deliveryID).ValueGeneratedNever().HasColumnName("BillingID");
                entity.Property(e => e.houseName).IsRequired().IsUnicode(false);
                entity.Property(e => e.streetName).IsRequired().IsUnicode(false);
                entity.Property(e => e.town).IsRequired().IsUnicode(false);
                entity.Property(e => e.county).IsRequired().IsUnicode(false);
                entity.Property(e => e.country).IsRequired().IsUnicode(false);
                entity.Property(e => e.postcode).IsRequired().IsUnicode(false);
            });
        }
        
        //register a new customer
        public void Register(customers Customers, out string output)
        {
            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@RespondMessage";
            parameter.IsNullable = true;
            parameter.SqlDbType = System.Data.SqlDbType.VarChar;
            parameter.Direction = System.Data.ParameterDirection.Output;
            parameter.Size = 50;
            Database.ExecuteSqlRaw("EXEC Register @IDNumber, @Title, @FirstName, @LastName, @PhoneNumber",
            new SqlParameter("@IDNumber", Customers.IDNumber),
            new SqlParameter("@Title", Customers.title),
            new SqlParameter("@FirstName", Customers.firstName),
            new SqlParameter("@LastName", Customers.lastName),
            new SqlParameter("@PhoneNumber", Customers.phoneNumber),
            parameter );
            output = parameter.Value.ToString();
        }

        //check if the new customer exists
        public bool CustomerValidation(customers Customers)
        {
            SqlParameter parameter = new SqlParameter("Return", System.Data.SqlDbType.Int, 50);
            parameter.Direction = System.Data.ParameterDirection.Output;
            Database.ExecuteSqlRaw("EXEC CustomerValidation @FirstName, @LastName, @PhoneNumber",
                new SqlParameter("@FirstName", Customers.firstName),
                new SqlParameter("@LastName", Customers.lastName),
                new SqlParameter("@PhoneNumber", Customers.phoneNumber),                
                parameter );

            if(Convert.ToInt32(parameter.Value) == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //modify customer details

        public void UpdateCustomer(customers Customers)
        {
            Database.ExecuteSqlRaw("EXEC UpdateCustomer @Title, @FirstName, @LastName, @PhoneNumber",
            new SqlParameter("@Title", Customers.title),
            new SqlParameter("@FirstName", Customers.firstName),
            new SqlParameter("@LastName", Customers.lastName),
            new SqlParameter("@PhoneNumber", Customers.phoneNumber) );
                
        }

        //delete a customer
        public void DeleteCustomer(int IDNumber)
        {
            Database.ExecuteSqlRaw("EXEC DeleteCustomer @IDNumber",
            new SqlParameter("@IDNumber", IDNumber) );

        }
    }
}
