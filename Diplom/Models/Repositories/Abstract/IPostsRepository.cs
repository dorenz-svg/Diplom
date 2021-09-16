using Diplom.Models.Query;
using Diplom.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Models.Repositories.Abstract
{
    public interface IPostsRepository
    {
        public Task Create(PostWithPhotoQuery query);
        public Task Update(PostWithPhotoQuery query);
        public Task<IEnumerable<PostsResponse>> Get(string id,int count);
        public Task Delete(long id);
    }
}
