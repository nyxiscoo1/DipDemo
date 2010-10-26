using NUnit.Framework;
using StructureMap.AutoMocking;

namespace DipDemo.Cataloguer.Tests
{
    [TestFixture]
    public class AutoMockingTestFixture<T> where T : class
    {
        protected AutoMocker<T> Mock { get; private set; }

        protected T SUT
        {
            get { return Mock.ClassUnderTest; }
        }

        [SetUp]
        public void SetUp()
        {
            Mock = new RhinoAutoMocker<T>();
            SharedContext();
            Context();
        }

        protected virtual void Context()
        {
        }

        protected virtual void SharedContext()
        {
        }
    }
}