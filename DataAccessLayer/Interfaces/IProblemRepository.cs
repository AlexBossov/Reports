using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccessLayer.Entities;

namespace DataAccessLayer.Interfaces
{
    public interface IProblemRepository
    {
        Task<IEnumerable<Problem>> ListAsync();
        Task AddAsync(Problem problem);
        Task<Problem> FindByIdAsync(int id);
        void Update(Problem problem);
        void Remove(Problem problem);
        IEnumerable<Problem> FindByCreationTime(DateTime creationTime);
        IEnumerable<Problem> FindByChangeTime(DateTime changeTime);
        IEnumerable<Problem> FindByEmployeeId(int employeeId);
        Task AddCommentAsync(Comment comment);
        Task<IEnumerable<Comment>> CommentsListAsync();
    }
}