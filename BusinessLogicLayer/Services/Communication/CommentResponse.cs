using BusinessLogicLayer.Resources;
using DataAccessLayer.Entities;

namespace BusinessLogicLayer.Services.Communication
{
    public class CommentResponse : BaseResponse
    {
        public CommentResource CommentResource { get; private set; }

        private CommentResponse(bool success, string message, CommentResource commentResource) 
            : base(success, message)
        {
            CommentResource = commentResource;
        }

        public CommentResponse(CommentResource commentResource)
            : this(true, string.Empty, commentResource)
        {
        }

        public CommentResponse(string message)
            : this(false, message, null)
        {
        }
    }
}