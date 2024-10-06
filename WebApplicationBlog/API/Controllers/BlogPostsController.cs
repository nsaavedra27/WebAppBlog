using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Application.Dtos;
using WebApplicationBlog.Application.Dtos;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class BlogPostsController : ControllerBase
    {
        private readonly BlogPostService _blogPostService;
        private readonly PostCommentService _postCommentService;

        public BlogPostsController(BlogPostService blogPostService, PostCommentService postCommentService)
        {
            _blogPostService = blogPostService;
            _postCommentService = postCommentService;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BlogPost>>> GetAllBlogPostsAndComments(string title = null, int pageNumber = 1, int pageSize = 10)
        {
            var blogPosts = await _blogPostService.GetBlogPostsAsync(title, pageNumber, pageSize);
            if (blogPosts == null || !blogPosts.Any())
            {
                return NotFound("No existen post.");
            }
            return Ok(blogPosts);
        }
              
        [HttpGet("{BlogPostId}")]
        public async Task<ActionResult<BlogPost>> GetBlogPostAndCommentsByBlogPostId(Guid BlogPostId)
        {
            var blogPost = await _blogPostService.GetBlogPostByIdAsync(BlogPostId);
            if (blogPost == null) 
            {
                return Ok(new { message = "No existe el post." });
            }
            return Ok(blogPost);
        }

        [HttpGet("{BlogPostId}/comments")]
        public async Task<ActionResult<IEnumerable<PostComment>>> GetCommentsByBlogPostId(Guid BlogPostId)
        {
            var comments = await _postCommentService.GetCommentsByBlogPostIdAsync(BlogPostId);
            if (comments == null || !comments.Any())
            {
                return Ok(new { message = "Este post aún no tiene comentarios." }); 
            }
            return Ok(comments);
        }
         
        [HttpPost("{BlogPostId}/comments")]
        public async Task<IActionResult> AddCommentByPostId(Guid BlogPostId, [FromBody] PostCommentDto commentDto)
        {            
            if (commentDto == null || string.IsNullOrWhiteSpace(commentDto.UserFullName) || string.IsNullOrWhiteSpace(commentDto.Comment))
            {
                return BadRequest("El comentario debe contener el nombre del usuario y el comentario.");
            }

            var comment = new PostComment
            {
                Id = Guid.NewGuid(), 
                BlogPostId = BlogPostId, 
                UserFullName = commentDto.UserFullName,
                Comment = commentDto.Comment
            };
            await _postCommentService.AddPostCommentAsync(comment);            
            return Ok(new { message = "El comentario fue agregado correctamente.", comment });
        }

        [HttpPost]
        public async Task<ActionResult<BlogPost>> AddNewBlogPost([FromBody] BlogPostDto blogPostDto)
        {
            if (blogPostDto == null || string.IsNullOrWhiteSpace(blogPostDto.Title))
            {
                return BadRequest("El título del post es obligatorio.");
            }

            var blogPost = new BlogPost
            {
                Title = blogPostDto.Title,
                PublishDate = DateTime.UtcNow,
                Comments = new List<PostComment>()
            };

            await _blogPostService.AddBlogPostAsync(blogPost);
           
            return CreatedAtAction(nameof(GetAllBlogPostsAndComments), new { id = blogPost.Id },
            new { message = "El post fue creado correctamente.", post = blogPost });
        }
    }
}
