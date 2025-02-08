using Application.Common.Mapping;
using Application.Dtos.Common;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Products
{
    public class ProductDto : CommandDto, IMapForm<Product>
    {
        public int id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        public int ProductTypeId { get; set; }
        public int ProductBeandId { get; set; }
        public string ProductBrand { get; set; }// Title
        public string ProductType { get; set; }//Title


        public void Mapping(Profile profile)
        {
            profile.CreateMap<Product, ProductDto>()
                .ForMember(x => x.ProductBrand, c => c.MapFrom(v => v.ProductBrand.Title))
                .ForMember(x => x.ProductType, c => c.MapFrom(v => v.ProductType.Title));
        }
    }
}
