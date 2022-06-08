using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.EF;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories
{
    public class ProblemRepository : BaseRepository, IProblemRepository
    {
        public ProblemRepository(ReportsDbContext context) 
            : base(context)
        {
        }

        public async Task<IEnumerable<Problem>> ListAsync()
        {
            return await Context.Problems
                .Include(p => p.Employee)
                .ToListAsync();
        }

        public async Task AddAsync(Problem problem)
        {
            await Context.Problems.AddAsync(problem);
        }

        public async Task<Problem> FindByIdAsync(int id)
        {
            return await Context.Problems.FindAsync(id);
        }

        public void Update(Problem problem)
        {
            Context.Problems.Update(problem);
        }

        public void Remove(Problem problem)
        {
            Context.Problems.Remove(problem);
        }

        public IEnumerable<Problem> FindByCreationTime(DateTime creationTime)
        {
            return Context.Problems.Where(p => p.CreationTime == creationTime);
        }
        
        public IEnumerable<Problem> FindByChangeTime(DateTime changeTime)
        {
            return Context.Problems.Where(p => p.ChangeTime == changeTime);
        }

        public IEnumerable<Problem> FindByEmployeeId(int employeeId)
        {
            return Context.Problems.Where(p => p.EmployeeId == employeeId);
        }

        public async Task AddCommentAsync(Comment comment)
        {
            await Context.Comments.AddAsync(comment);
        }
        
        public async Task<IEnumerable<Comment>> CommentsListAsync()
        {
            return await Context.Comments
                .Include(p => p.Problem)
                .ToListAsync();
        }
    }
}