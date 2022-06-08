using BusinessLogicLayer.Resources.ProblemResources;
using DataAccessLayer.Entities;

namespace BusinessLogicLayer.Services.Communication
{
    public class ProblemResponse : BaseResponse
    {
        public ProblemResource ProblemResource { get; private set; }

        private ProblemResponse(bool success, string message, ProblemResource problemResource) 
            : base(success, message)
        {
            ProblemResource = problemResource;
        }

        public ProblemResponse(ProblemResource problemResource)
            : this(true, string.Empty, problemResource)
        {
        }

        public ProblemResponse(string message) : this(false, message, null)
        {
        }
    }
}