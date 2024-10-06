using System;
using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class PostComment
    {
        public Guid Id { get; set; }
        public Guid BlogPostId { get; set; }
        public string UserFullName { get; set; }
        public string Comment { get; set; }


        [JsonIgnore]
        public BlogPost BlogPost { get; set; }
    }
}
