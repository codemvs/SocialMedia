using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Api.Responses;
using SocialMedia.Core.DTOs;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialMedia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly IMapper _mapper;

        public PostController(IPostService postServoce,
            IMapper mapper)
        {
            _postService = postServoce;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
            var posts = await _postService.GetPosts();
            var postsDTO = _mapper.Map<IEnumerable<PostDTO>>(posts);

            var response = new ApiResponse<IEnumerable<PostDTO>>(postsDTO);

            return Ok(response);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPost(int id)
        {
            var post = await _postService.GetPostById(id);
            var postDTO = _mapper.Map<PostDTO>(post);
            var response = new ApiResponse<PostDTO>(postDTO);
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> Post(PostDTO postDTO)
        {
            var post = _mapper.Map<Post>(postDTO);
            await _postService.InsertPost(post);

            postDTO =_mapper.Map<PostDTO>(post);

            var response = new ApiResponse<PostDTO>(postDTO);
            return Ok(response);
        }
       [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, PostDTO postDTO)
        {
            var post = _mapper.Map<Post>(postDTO);
            post.PostId = id;
            var result = await _postService.UpdatePost(post);
            var response = new ApiResponse<bool>(result);

            return Ok(response);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            
            var result = await _postService.DeletePost(id);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }
    }
}
