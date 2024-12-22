using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Aspect.Performance
{
    public class PerformanceAspect:MethodInterception
    {
        private int _interval;
        private Stopwatch _stopwatch;
        public PerformanceAspect(int interval)
        {
            _interval = interval;
            _stopwatch =ServiceTool.ServiceProvider.GetService<Stopwatch>();
        }

        
        public PerformanceAspect()
        {
                _interval = 3;
        }
        protected override void OnBefore(IInvocation invocation)
        {
            _stopwatch.Start();
        }
        protected override void OnAfter(IInvocation invocation)
        {
            double second = _stopwatch.Elapsed.TotalSeconds;
            if (second > _interval)
            {
                //biz consola gönderdik mesjaı ama istersen database veya mail kodlarını yazarak onlara gönderebilirsin.
                Debug.WriteLine($"Performans Raporu: {invocation.Method.DeclaringType.FullName}.{invocation.Method.Name} ==> {_stopwatch.Elapsed.TotalSeconds}");

            }
            _stopwatch.Reset();
        }
    }
}
