﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Mapping
{
    public interface IMapForm<T>
    {
       virtual void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType());
    }
}
