using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Infrastructure
{
    public interface ISaveImage
    {
        public Task<List<string>> Save(IFormFileCollection files);
    }
}
