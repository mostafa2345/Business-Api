using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Comment;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly AppDbContext dbContext;

        public CommentRepository(AppDbContext DbContext)
        {
            dbContext = DbContext;
        }

        public async Task<Comment> CreateCommentAsync(Comment commentModel)
        {
            await dbContext.comments.AddAsync(commentModel);
            await dbContext.SaveChangesAsync();
            return commentModel;
        }

        public async Task<Comment> DeleteCommentAsync(int id)
        {
            var Comment = await dbContext.comments.FindAsync(id);
            if (Comment == null)
            {
                return null;

            }
            dbContext.Remove(Comment);
            await dbContext.SaveChangesAsync();
            return Comment;
        }

        public async Task<List<Comment>> GetAllAsync()
        {
            return await dbContext.comments.Include(x=>x.appUser).ToListAsync();
        }

        public async Task<Comment?> GetByIdAsync(int id)
        {
            return await dbContext.comments.Include(x=>x.appUser).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Comment?> UpdateCommentAsync(int id, Comment commentModel)
        {
            var comment = await dbContext.comments.FindAsync(id);
            if (comment == null)
            {
                return null;
            }

            comment.Title = commentModel.Title;
            comment.Content = commentModel.Content;
            await dbContext.SaveChangesAsync();
            return comment;

        }




    }
}