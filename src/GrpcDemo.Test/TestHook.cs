using AutoMapper;
using GrpcDemo.DomainService.Utilities.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GrpcDemo.Test
{
    public class TestHook
    {
        private static IConfigurationProvider _mapperConfigurationProvider;

        internal static IConfigurationProvider MapperConfigurationProvider
        {
            get
            {
                if (_mapperConfigurationProvider == null)
                {
                    _mapperConfigurationProvider = new MapperConfiguration(options =>
                    {
                        options.AddProfile<DomainServiceProfile>();
                    });
                }
                return _mapperConfigurationProvider;
            }
        }
    }
}