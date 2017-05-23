using Documentz.Models;
using Documentz.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Documentz.Utils
{
    public static class AutoMappingConfigurator
    {
        public static void Configure()
        {
            AutoMapper.Mapper.Initialize(config =>
            {
                config.CreateMap<StoredItem, StoredItemViewModel>().ReverseMap();
            });
        }

    }
}
