using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormsApp1.entity;

namespace WinFormsApp1.DataX
{
    public class appDbcontext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Integrated Security=true;Initial Catalog=ListAccMetamask;MultipleActiveResultSets=True;encrypt=true;trustservercertificate=true");
        }
        public DbSet<ListAcc> listAccs { get; set; }
    }
}
