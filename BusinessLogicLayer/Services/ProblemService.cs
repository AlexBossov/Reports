using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Resources;
using BusinessLogicLayer.Resources.ProblemResources;
using BusinessLogicLayer.Services.Communication;
using DataAccessLayer.Entities;
using DataAccessLayer.EStates;
using DataAccessLayer.Interfaces;

namespace BusinessLogicLayer.Services
{
    public class ProblemService : IProblemService
    {
        private readonly IProblemRepository _problemRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProblemService(IProblemRepository problemRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _problemRepository = problemRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProblemResource>> ListAsync()
        {
            IEnumerable<Problem> problems = await _problemRepository.ListAsync();
            IEnumerable<ProblemResource> resource = 
                _mapper.Map<IEnumerable<Problem>, IEnumerable<ProblemResource>>(problems);
            return resource;
        }
        
        public async Task<ProblemResponse> SaveAsync(SaveProblemResource saveProblemResource)
        {
            Problem problem = _mapper.Map<SaveProblemResource, Problem>(saveProblemResource);
            try
            {
                problem.EProblemState = EProblemState.OpenTask;
                await _problemRepository.AddAsync(problem);
                await _unitOfWork.CompleteAsync();
                ProblemResource resource = _mapper.Map<Problem, ProblemResource>(problem);
                return new ProblemResponse(resource);
            }
            catch (Exception ex)
            {
                return new ProblemResponse($"An error occurred when saving the problem: {ex.Message}");
            }
        }

        public async Task<ProblemResponse> UpdateAsync(int id, SaveProblemResource saveProblemResource)
        {
            
            Problem existingProblem = await _problemRepository.FindByIdAsync(id);

            if (existingProblem == null)
                return new ProblemResponse("Problem not found.");
            Problem problem = _mapper.Map<SaveProblemResource, Problem>(saveProblemResource);

            existingProblem.Description = problem.Description;
            existingProblem.Comments = problem.Comments;
            existingProblem.EmployeeId = problem.EmployeeId;
            existingProblem.ChangeTime = DateTime.Now;
            existingProblem.EProblemState = problem.EProblemState;

            try
            {
                _problemRepository.Update(existingProblem);
                await _unitOfWork.CompleteAsync();
                ProblemResource resource = _mapper.Map<Problem, ProblemResource>(problem);
                return new ProblemResponse(resource);
            }
            catch (Exception ex)
            {
                return new ProblemResponse($"An error occurred when updating the problem: {ex.Message}");
            }
        }

        public async Task<ProblemResponse> DeleteAsync(int id)
        {
            Problem existingProblem = await _problemRepository.FindByIdAsync(id);

            if (existingProblem == null)
                return new ProblemResponse("Problem not found.");

            try
            {
                _problemRepository.Remove(existingProblem);
                await _unitOfWork.CompleteAsync();
                ProblemResource resource = _mapper.Map<Problem, ProblemResource>(existingProblem);
                return new ProblemResponse(resource);
            }
            catch (Exception ex)
            {
                return new ProblemResponse($"An error occurred when deleting the problem: {ex.Message}");
            }
        }

        public async Task<ProblemResource> FindByIdAsync(int id)
        {
            Problem problem = await _problemRepository.FindByIdAsync(id);
            ProblemResource resource = 
                _mapper.Map<Problem, ProblemResource>(problem);
            return resource;
        }

        public IEnumerable<ProblemResource> FindByCreationTime(DateTime creationTime)
        {
            IEnumerable<Problem> problems = _problemRepository.FindByCreationTime(creationTime);
            IEnumerable<ProblemResource> resource = 
                _mapper.Map<IEnumerable<Problem>, IEnumerable<ProblemResource>>(problems);
            return resource;
        }
        
        public IEnumerable<ProblemResource> FindByChangeTime(DateTime changeTime)
        {
            IEnumerable<Problem> problems = _problemRepository.FindByChangeTime(changeTime);
            IEnumerable<ProblemResource> resource = 
                _mapper.Map<IEnumerable<Problem>, IEnumerable<ProblemResource>>(problems);
            return resource;
        }

        public IEnumerable<ProblemResource> FindByEmployeeId(int employeeId)
        {
            IEnumerable<Problem> problems = _problemRepository.FindByEmployeeId(employeeId);
            IEnumerable<ProblemResource> resource = 
                _mapper.Map<IEnumerable<Problem>, IEnumerable<ProblemResource>>(problems);
            return resource;
        }

        public async Task<CommentResponse> SaveCommentAsync(CommentResource commentResource)
        {
            try
            {                
                Comment comment = _mapper.Map<CommentResource, Comment>(commentResource);

                await _problemRepository.AddCommentAsync(comment);
                await _unitOfWork.CompleteAsync();

                return new CommentResponse(commentResource);
            }
            catch (Exception ex)
            {
                return new CommentResponse($"An error occurred when saving the comment: {ex.Message}");
            }
        }
        
        public async Task<IEnumerable<CommentResource>> CommentsListAsync()
        {
            IEnumerable<Comment> comments = await _problemRepository.CommentsListAsync();
            IEnumerable<CommentResource> resources = 
                _mapper.Map<IEnumerable<Comment>, IEnumerable<CommentResource>>(comments);
            return resources;
        }
    }
}