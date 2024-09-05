using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Comment;
using api.Extensions;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository commentRepository;
        private readonly IStockRepository stockRepository;
        private readonly UserManager<AppUser> userManager;

        public CommentController(ICommentRepository commentRepository, IStockRepository stockRepository,UserManager<AppUser> userManager)
        {

            this.commentRepository = commentRepository;
            this.stockRepository = stockRepository;
            this.userManager = userManager;
        }
        [HttpGet]

        public async Task<IActionResult> GetAllComments()
        {
            if(!ModelState.IsValid){
                    return BadRequest(ModelState);
            }
            var Comments = await commentRepository.GetAllAsync();

            var result = Comments.Select(s => s.ToCommentDto());

            return Ok(result);


        }
        [HttpGet("{id:int}")]

        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var comment = await commentRepository.GetByIdAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            return Ok(comment.ToCommentDto());
        }

        [HttpPost("{stock_id:int}")]

        public async Task<IActionResult> Create([FromBody] CreateCommentDto createCommentDto, [FromRoute] int stock_id, IStockRepository stockRepository)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!await stockRepository.StockExist(stock_id))
            {
                return BadRequest("Stock does not exist");
            }
            var username=User.GetUsername();
            var appUser=await userManager.FindByNameAsync(username);

        
            var comment = createCommentDto.ToCreateCommentDto(stock_id);
            comment.AppUserId=appUser.Id;
            
            await commentRepository.CreateCommentAsync(comment);
            return CreatedAtAction(nameof(GetById), new { id = comment.Id }, comment.ToCommentDto());

        }
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromBody] UpdateCommentDto UpdateDto, [FromRoute] int id, IStockRepository stockRepository)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var Comment = await commentRepository.UpdateCommentAsync(id, UpdateDto.ToUpdateCommentDto());
            if (Comment == null)
            {
                return NotFound("Comment not found");
            }

            return Ok(Comment.ToCommentDto());
        }
        [HttpDelete]
        [Route("{id:int}")]

        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var comment = await commentRepository.DeleteCommentAsync(id);
            if (comment == null)
            {
                return NotFound("comment not found");
            }

            return NoContent();

        }
    }
}