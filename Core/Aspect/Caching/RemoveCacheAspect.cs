﻿using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Aspect.Caching
{
    public class RemoveCacheAspect:MethodInterception
    {
        private string _pattern;
        private ICacheManager _cacheManager;
        public RemoveCacheAspect(string pattern)
        {
            _pattern = pattern;
            _cacheManager=ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }

        protected override void OnSuccess(IInvocation invocation)
        {
            _cacheManager.RemoveByPattern(_pattern);
        }

    }
}
