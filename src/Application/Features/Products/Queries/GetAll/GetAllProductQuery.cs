﻿using Application.Contracts;
using Application.Dtos.Products;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Queries.GetAll
{
    public class GetAllProductQuery : IRequest<IEnumerable<ProductDto>> , ICacheQuery
    {
        public int Id { get; set; }
        public int HoursafeData => 1 ;
    }
}
