namespace Domain.Entities
{
    public class BlogPost
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime PublishDate { get; set; }
        public List<PostComment> Comments { get; set; }

        public BlogPost()
        {
            Comments = new List<PostComment>(); 
        }
    }
}
