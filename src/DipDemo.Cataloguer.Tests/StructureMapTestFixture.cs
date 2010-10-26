using System;
using System.Linq;
using System.Text;
using DipDemo.Cataloguer.Infrastructure.AppController;
using NUnit.Framework;
using StructureMap;

namespace DipDemo.Cataloguer.Tests
{
    [TestFixture]
    public class StructureMapTestFixture
    {
        [SetUp]
        public void SetUp()
        {
            ObjectFactory.Configure(x => x.AddRegistry(new DefaultRegistry()));
        }

        [Test]
        public void TestRegistryConfiguration()
        {
            ObjectFactory.AssertConfigurationIsValid();
        }

        [Test]
        public void All_requests_should_have_handlers()
        {
            var requests = typeof(DefaultRegistry).Assembly.GetTypes().Where(x => x.GetInterfaces().Contains(typeof(IRequestData)));

            bool isOk = true;
            var sb = new StringBuilder();
            sb.AppendLine("Not all requests have handlers:");
            foreach (var request in requests)
            {
                var requestType = typeof(IRequestHandler<>).MakeGenericType(request);
                try
                {
                    ObjectFactory.GetInstance(requestType);
                }
                catch (Exception)
                {
                    isOk = false;
                    sb.AppendLine("No handler found for [" + requestType.FullName + "]");
                }
            }

            if (!isOk)
                throw new AssertionException(sb.ToString());
        }

        [Test, Explicit]
        public void TestWhatDoIHave()
        {
            System.Diagnostics.Trace.Write(ObjectFactory.WhatDoIHave());
        }
    }
}
