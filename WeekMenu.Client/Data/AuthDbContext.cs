using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WeekMenu.Client.Data
{
    public class AuthDbContex : IdentityDbContext
    {
        public AuthDbContex(DbContextOptions<AuthDbContex> options)
            : base(options)
        {
        }
    }
}
