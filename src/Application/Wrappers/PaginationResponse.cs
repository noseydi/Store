using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Wrappers
{
    public class PaginationResponse<T> where T : class
        {
        public PaginationResponse(int pageindex , int pagesize , int count , IEnumerable<T> result)
        {
            PageIndex = pageindex; 
            PageSize=pagesize;
            Count = count;
            Result = result;
        }
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; }= 12;
        public int Count { get; set; }  
        //ireadonly
        public IEnumerable<T> Result { get; set; }

    }
}
