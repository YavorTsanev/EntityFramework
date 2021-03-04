﻿using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using CarDealer.Dto.Import;
using CarDealer.Models;

namespace CarDealer
{
    public class CarDealerProfile : Profile
    {
        public CarDealerProfile()
        {
            CreateMap<SupplierDto, Supplier>();
        }
    }
}
