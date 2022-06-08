using BusinessLogicLayer.Resources.ProblemResources;

namespace BusinessLogicLayer.Resources
{
    public class CommentResource
    {
        public string CommentBody { get; set; }
        public int ProblemId { get; set; }
        public ProblemResource Problem { get; set; }
    }
}