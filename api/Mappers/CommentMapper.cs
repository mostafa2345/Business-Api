using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Comment;
using api.Models;

namespace api.Mappers
{
    public static class CommentMapper
    {
        public static CommentDto ToCommentDto(this Comment comment)
        {
            return new CommentDto
            {
                Id = comment.Id,
                Content = comment.Content,
                CreatedOn = comment.CreatedOn,
                StockId = comment.StockId,
                Title = comment.Title,
                CreatedBy=comment.appUser.UserName
            };
        }
        public static Comment ToCreateCommentDto(this CreateCommentDto comment, int stock_id)
        {
            return new Comment
            {

                Content = comment.Content,

                Title = comment.Title,
                StockId = stock_id,
            };
        }
        public static Comment ToUpdateCommentDto(this UpdateCommentDto commentDto)
        {
            return new Comment
            {
                Content = commentDto.Content,

                Title = commentDto.Title,

            };
        }
    }
}