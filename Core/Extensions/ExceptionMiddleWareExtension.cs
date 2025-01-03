﻿using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public static class ExceptionMiddleWareExtension
    {
        public static void ConfigureCustomExceptionMiddleWare(this IApplicationBuilder app) 
        {
            app.UseMiddleware<ExceptionMiddleWare>();
        }
    }
}
