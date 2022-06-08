using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLogicLayer.Resources;
using BusinessLogicLayer.Resources.ProblemResources;
using BusinessLogicLayer.Services.Communication;
using DataAccessLayer.Entities;

namespace BusinessLogicLayer.Interfaces
{
    public interface IProblemService
    {
        Task<IEnumerable<ProblemResource>> ListAsync();
        Task<ProblemResponse> SaveAsync(SaveProblemResource saveProblemResource);
        Task<ProblemResponse> UpdateAsync(int id, SaveProblemResource saveProblemResource);
        Task<ProblemResponse> DeleteAsync(int id);
        Task<ProblemResource> FindByIdAsync(int id);
        IEnumerable<ProblemResource> FindByCreationTime(DateTime creationTime);
        IEnumerable<ProblemResource> FindByChangeTime(DateTime changeTime);
        IEnumerable<ProblemResource> FindByEmployeeId(int employeeId);
        Task<CommentResponse> SaveCommentAsync(CommentResource commentResource);
        Task<IEnumerable<CommentResource>> CommentsListAsync();
    }
}