using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Text;
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
             await First(args);
             await Second(args);

            // await Third(args);
        }

        public static async Task First(string[] args)
        {
            await using (var context = new SampleContextFactory().CreateDbContext(args))
            {
                var data = await context.Clients.Join(context.Projects, x => x.ClientId, y => y.ClientId, (x, y) => new { Client = x, Project = y })
                    .Select(s => new
                    {
                        ClientId = s.Client.ClientId,
                        FirstName = s.Client.FirstName,
                        LastName = s.Client.LastName,
                        Email = s.Client.Email,
                        Phone = s.Client.Phone,
                        ProjectId = s.Project.ProjectId,
                        ProjectName = s.Project.Name,
                        Budget = s.Project.Budget,
                        Date = s.Project.StartedDate,
                        Employee = s.Project.EmployeeProjects.Select(e => new
                        {
                            Rate = e.Rate,
                            StartedDate = e.StartedDate
                        })
                    }).ToListAsync();

                Console.WriteLine("Запрос, который объединяет 3 таблицы и обязательно включает LEFT JOIN");

                foreach (var item in data)
                {
                    Console.WriteLine($"Id: {item.ClientId}, Name: {item.FirstName}, Last name: {item.LastName}, Phone: {item.Phone}," +
                         $" Email: {item.Email}, ProjectId: {item.ProjectId}, ProjectName: {item.ProjectName}, Budget: {item.Budget}, EmployeeProjects: {string.Join(";", item.Employee)}  ");
                }
            }
        }

        public static async Task Second(string[] args)
        {
            await using (var context = new SampleContextFactory().CreateDbContext(args))
            {
                var data = await context.Projects
                    .Select(s => new
                    {
                        Date = s.StartedDate,
                        NowTime = DateTime.UtcNow,
                        Diff = $"{Math.Truncate((DateTime.UtcNow - s.StartedDate).TotalDays)} days"
                    }).ToListAsync();

                Console.WriteLine("Запрос, который возвращает разницу между CreatedDate/HiredDate и сегодня. Фильтрация должна быть выполнена на сервере.");

                foreach (var item in data)
                {
                    Console.WriteLine($"Date: {item.Date}, Now: {item.NowTime}, Difference: {item.Diff}");
                }
            }
        }

        public static async Task Third(string[] args)
        {
            await using (var context = new SampleContextFactory().CreateDbContext(args))
            {
                using (var transaction = await context.Database.BeginTransactionAsync())
                {
                    try
                    {
                        Console.WriteLine("Запрос, который обновляет 2 сущности. Сделать в одной  транзакции");
                        var data = await context.Projects.FirstOrDefaultAsync(s => s.ClientId == 4);
                        data.ClientId = 5;

                        data = await context.Projects.FirstOrDefaultAsync(s => s.Budget > 1000);
                        data.Budget = 10;

                        await context.SaveChangesAsync();
                        Console.WriteLine("SaveChangesAsync");
                        await transaction.CommitAsync();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Rollback: {ex}");
                        await transaction.RollbackAsync();
                    }
                }
            }
        }
    }
}
