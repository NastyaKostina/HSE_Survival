using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDatabase
{
    class Context : DbContext
    {
        public DbSet<Result> resultsDatabase { get; set; }
    }
}
