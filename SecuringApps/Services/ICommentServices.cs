using SecuringApps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecuringApps.Services
{
    public interface ICommentServices
    {
        CommentModel GetComment(Guid id);

        IQueryable<CommentModel> GetAllComments();

        void AddComment(CommentModel comment, string userId);
    }
}
