using SecuringApps.Data;
using SecuringApps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecuringApps.Repository
{
    public class CommentRepository : ICommentRepository
    {
        SecuringAppDbContext context;

        public CommentRepository(SecuringAppDbContext _context)
        {
            context = _context;
        }

        public Guid AddComment(Comment comments)
        {
            context.Comments.Add(comments);
            context.SaveChanges();
            return comments.Id;
        }

        public IQueryable<Comment> GetAllComments()
        {
            return context.Comments;
        }

        public Comment GetComment(Guid Id)
        {
            return context.Comments.SingleOrDefault(x => x.Id == Id);
        }
    }
}
