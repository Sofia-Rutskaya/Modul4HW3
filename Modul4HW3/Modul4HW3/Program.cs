using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Text;
using System.Data.Common;
using System.Data.SqlTypes;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Modul4HW3.Data;
using Modul4HW3.Data.Entity;
using Modul4HW3.Data.EntityConfigurations;

namespace Modul4HW3
{
    public class Program
    {
        public static void Main(string[] args)
        {
            LINQ(args).GetAwaiter().GetResult();
            Console.WriteLine("Done!");
            Console.Read();
        }

        public static async Task LINQ(string[] args)
        {
             var query = new Query();
             await query.First(args);
             await query.Second(args);
             await query.Third(args);
             await query.Fourth(args);
             await query.Fifth(args);
             await query.Sixth(args);
        }
    }
}
