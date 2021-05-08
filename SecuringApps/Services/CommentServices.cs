using SecuringApps.Models;
using SecuringApps.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecuringApps.Services
{
    public class CommentServices : ICommentServices
    {
        private ICommentRepository commentRepo;

        public CommentServices(ICommentRepository commentRepository)
        {
            commentRepo = commentRepository;
        }

        public void AddComment(CommentModel comment, string userId)
        {
            Guid obj = Guid.NewGuid();
            //converting to productviewmodel to product
            Comment newComment = new Comment()
            {
                CommentText = comment.CommentText,
                UserId = obj,
                FileId = comment.FileId
            };

            commentRepo.AddComment(newComment);
        }

        public IQueryable<CommentModel> GetAllComments()
        {
            var list = from c in commentRepo.GetAllComments()
                       select new CommentModel()
                       {
                           Id = c.Id,
                           CommentText = c.CommentText,
                           FileId = c.FileId,
                           UserId = c.UserId
                       };
            return list;
        }

        public CommentModel GetComment(Guid id)
        {
            var myComment = commentRepo.GetComment(id);
            CommentModel model = new CommentModel();

            model.CommentText = myComment.CommentText;
            model.UserId = myComment.UserId;

            model.Files = new FileModel()
            {
                Id = model.Files.Id,
                FileName = model.Files.FileName
            };
            return model;
        }
    }
}
