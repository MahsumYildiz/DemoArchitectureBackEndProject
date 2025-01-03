﻿using Castle.DynamicProxy;
using Core.Extensions;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Business.Aspect.Secured
{
    public class SecuredAspect:MethodInterception
    {
        private string[] _roles;
        private IHttpContextAccessor _httpContextAccessor;
        public SecuredAspect(string roles)
        {
            _roles = roles.Split(",");
            _httpContextAccessor=ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }
        public SecuredAspect()
        {
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }

        protected override void OnBefore(IInvocation invocation)
        {
            if(_roles!=null)
            {
                var roleClaims = _httpContextAccessor.HttpContext.User.ClaimsRole();
                  foreach (var role in _roles) 
                      {
                         if (roleClaims.Contains(role))
                           {
                              return;
                           }     
                      }
                throw new Exception("Yetkiniz yok");
            }
            else
            {
                var claims=_httpContextAccessor.HttpContext.User.Claims;
                if(claims.Count()>0) 
                {
                    return;
                }
                throw new Exception("İşlem için yetkiniz yok");
            }
            
        }
    }
}
