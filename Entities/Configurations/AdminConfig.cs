using Entities.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Configurations
{
    public class AdminConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData(
               new User
         {
         Email = "AhmedAdel@yahoo.com",
         Role = "Admin",
         PasswordHash = "12345678",
         Id = new Guid().ToString(),
         Password = "12345678",
         PhoneNumber = "01032882094",
         UserName = "Admin",
         NormalizedEmail = "AhmedAdel@yahoo.com",
         NormalizedUserName = "Admin",
         Name = "Ahmed Adel"
          });
        }
    }
}
