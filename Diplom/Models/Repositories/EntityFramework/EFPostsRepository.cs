using Diplom.Models.Query;
using Diplom.Models.Repositories.Abstract;
using Diplom.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Models.Repositories.EntityFramework
{
    public class EFPostsRepository : IPostsRepository
    {
        public Task Create(PostQuery query)
        {
            throw new NotImplementedException();
        }

        public Task Delete(PostQuery query)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PostsResponse>> Get(string id)
        {
            throw new NotImplementedException();
        }

        public Task Update(PostQuery query)
        {
            throw new NotImplementedException();
        }
    }
}
