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
    public class Query
    {
        public async Task First(string[] args)
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

        public async Task Second(string[] args)
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

        public async Task Third(string[] args)
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

        public async Task Fourth(string[] args)
        {
            await using (var context = new SampleContextFactory().CreateDbContext(args))
            {
                using (var transaction = await context.Database.BeginTransactionAsync())
                {
                    try
                    {
                        Console.WriteLine("Запрос, который добавляет сущность Employee с Title и Project");

                        var title = new Title
                        {
                            Name = "Tittle1"
                        };

                        var office = new Office
                        {
                            Location = "Centre Galoha",
                            Title = "Maneger"
                        };

                        var employee = new Employee
                        {
                            FirstName = "Mia",
                            LastName = "Dias",
                            HiredDate = new DateTime(2021, 12, 12),
                            DateOfBirth = new DateTime(2000, 10, 12),
                            OfficeId = 1,
                            TitleId = 1,
                            Office = office,
                            Title = title
                        };

                        await context.AddAsync<Employee>(employee);
                        await context.AddAsync<Title>(title);
                        await context.AddAsync<Office>(office);

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

        public async Task Fifth(string[] args)
        {
            await using (var context = new SampleContextFactory().CreateDbContext(args))
            {
                using (var transaction = await context.Database.BeginTransactionAsync())
                {
                    try
                    {
                        Console.WriteLine("Запрос, который удаляет сущность Employee ");
                        var data = context.Employees.FirstOrDefault();

                        context.Remove(data);

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

        public async Task Sixth(string[] args)
        {
            await using (var context = new SampleContextFactory().CreateDbContext(args))
            {
                try
                {
                    Console.WriteLine("Запрос, который группирует сотрудников по ролям и возвращает название роли (Title) если оно не содержит ‘a’");
                    var data = await context.Employees
                    .AsNoTracking()
                    .Include(x => x.Title)
                    .GroupBy(x => x.Title.Name)
                    .Select(g => g.Key)
                    .Where(g => !g.Contains("a"))
                    .ToListAsync();

                    foreach (var item in data)
                    {
                        Console.WriteLine($"{item}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }
    }
}
