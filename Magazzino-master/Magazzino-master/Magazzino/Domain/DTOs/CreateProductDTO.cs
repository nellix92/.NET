using AutoMapper;
using Magazzino.Domain.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Magazzino.Domain.DTOs;

public class CreateProductDTO
{
    public string Name { get; set;}
    public decimal Price { get; set;}
    public string Sku { get; set;}
    public string Description { get; set;}
    public int Quantities { get; set;}

    public class CreateProductDTOProfile : Profile
    {
        public CreateProductDTOProfile()
        {
            CreateMap<CreateProductDTO, Product>();
        }
    }
}
