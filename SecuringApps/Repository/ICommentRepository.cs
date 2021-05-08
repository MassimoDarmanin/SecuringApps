using SecuringApps.Data;
using SecuringApps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecuringApps.Repository
{
    public interface ICommentRepository
    {
        Comment GetComment(Guid Id);

        IQueryable<Comment> GetAllComments();

        Guid AddComment(Comment comments);
    }
}
