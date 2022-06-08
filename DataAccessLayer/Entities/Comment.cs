using System;

namespace DataAccessLayer.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public string CommentBody { get; set; }
        public DateTime CreationTime { get; set; }
        public int ProblemId { get; set; }
        public Problem Problem { get; set; }
    }
}