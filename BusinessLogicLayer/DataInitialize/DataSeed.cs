using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.EF;
using DataAccessLayer.Entities;
using DataAccessLayer.EStates;

namespace BusinessLogicLayer.DataInitialize
{
    public class DatabaseSeed
    {
        public static void Seed(ReportsDbContext context, IPasswordHasher passwordHasher)
        {
            context.Database.EnsureCreated();
            
            if (!context.Roles.Any())
            {

                var roles = new List<Role>
                {
                    new Role { Name = ERoles.CommonEmployee.ToString() },
                    new Role { Name = ERoles.TeamLeader.ToString() }
                };

                context.Roles.AddRange(roles);
                context.SaveChanges();
            }

            if (!context.Employees.Any())
            {
                var employees = new List<Employee>
                {
                    new Employee
                    {
                        Name = "Ivan",
                        Email = "lol1@teamLeader.com",
                        Password = passwordHasher.HashPassword("12345678")
                    },
                    new Employee { 
                        Name = "Igor",
                        Email = "lol@Head1.com",
                        Password = passwordHasher.HashPassword("12345678"),
                        HeadId = 1
                    },
                    new Employee { 
                        Name = "Oleg",
                        Email = "lol@commonEmployee2.com",
                        Password = passwordHasher.HashPassword("12345678"),
                        HeadId = 1,
                    },
                    new Employee { 
                        Name = "Igor",
                        Email = "lol@commonEmployee3.com",
                        Password = passwordHasher.HashPassword("1234"),
                        HeadId = 2
                    },
                    new Employee { 
                        Name = "Igor",
                        Email = "lol@commonEmployee4.com",
                        Password = passwordHasher.HashPassword("12345"),
                        HeadId = 2
                    }
                };

                employees[0].EmployeeRoles.Add(
                    new EmployeeRole
                {
                    RoleId = context.Roles.First(r => r.Name == ERoles.TeamLeader.ToString()).Id,
                });
                
                employees[0].EmployeeRoles.Add(
                    new EmployeeRole
                    {
                        RoleId = context.Roles.First(r => r.Name == ERoles.CommonEmployee.ToString()).Id,
                    });

                employees[1].EmployeeRoles.Add(new EmployeeRole
                {
                    RoleId = context.Roles.First(r => r.Name == ERoles.CommonEmployee.ToString()).Id,
                });
                
                employees[2].EmployeeRoles.Add(new EmployeeRole
                {
                    RoleId = context.Roles.First(r => r.Name == ERoles.CommonEmployee.ToString()).Id
                });
                
                employees[3].EmployeeRoles.Add(new EmployeeRole
                {
                    RoleId = context.Roles.First(r => r.Name == ERoles.CommonEmployee.ToString()).Id
                });
                
                employees[4].EmployeeRoles.Add(new EmployeeRole
                {
                    RoleId = context.Roles.First(r => r.Name == ERoles.CommonEmployee.ToString()).Id
                });

                context.Employees.AddRange(employees);
                context.SaveChanges();
            }

            if (!context.Reports.Any())
            {
                var reports = new List<Report>();
                var report1 = new Report
                {
                    EmployeeId = 1,
                    Description = "report1",
                    EReportState = EReportState.Active
                };
                var report2 = new Report
                {
                    EmployeeId = 2,
                    Description = "report2",
                    EReportState = EReportState.Active
                };
                reports.Add(report1);
                reports.Add(report2);
                context.Reports.AddRange(reports);
                context.SaveChanges();
            }
            
            if (!context.Problems.Any())
            {
                var problem1 = new Problem
                {
                    Description = "Problem 1",
                    CreationTime = DateTime.Now,
                    EProblemState = EProblemState.OpenTask,
                    EmployeeId = 1,
                    ReportId = 1
                };
                var problem2 = new Problem
                {
                    Description = "Problem 2",
                    CreationTime = DateTime.Now,
                    EProblemState = EProblemState.OpenTask,
                    EmployeeId = 2,
                    ReportId = 2
                };
                var problem3 = new Problem
                {
                    Description = "Problem 3",
                    CreationTime = DateTime.Now,
                    EProblemState = EProblemState.OpenTask,
                    EmployeeId = 2,
                    ReportId = 1
                };
                var problem4 = new Problem
                {
                    Description = "Problem 4",
                    CreationTime = DateTime.Now,
                    EProblemState = EProblemState.OpenTask,
                    EmployeeId = 3,
                    ReportId = 2
                };
                
                var problem5 = new Problem
                {
                    Description = "Problem 5",
                    CreationTime = DateTime.Now,
                    EProblemState = EProblemState.OpenTask,
                    EmployeeId = 4,
                    ReportId = 1
                };
                
                var problems = new List<Problem>{ problem1, problem2 };
                context.Problems.AddRange(problems);
                context.SaveChanges();
            }

            if (!context.Comments.Any())
            {
                var comments = new List<Comment>();
                var comment1 = new Comment
                {
                    CommentBody = "first comment",
                    ProblemId = 1
                };
                
                var comment2 = new Comment
                {
                    CommentBody = "second comment",
                    ProblemId = 1
                };
                comments.Add(comment1);
                comments.Add(comment2);
                context.Comments.AddRange(comments);
                context.SaveChanges();
            }
        }
    }
}