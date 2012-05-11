﻿using ApprovalTests.Reporters;
using NUnit.Framework;

namespace SisoDb.UnitTests
{
    [TestFixture]
    [UseReporter(typeof(DiffReporter))]
    public abstract class UnitTestBase
    {
        [TestFixtureSetUp]
        public void FixtureInitializer()
        {
            OnFixtureInitialize();
        }

        protected virtual void OnFixtureInitialize()
        {
        }

        [TestFixtureTearDown]
        public void FixtureFinalizer()
        {
            OnFixtureFinalize();
        }

        protected virtual void OnFixtureFinalize()
        {
        }

        [SetUp]
        public void TestInitializer()
        {
            OnTestInitialize();
        }

        protected virtual void OnTestInitialize()
        {
        }

        [TearDown]
        public void TestFinalizer()
        {
            OnTestFinalize();
        }

        protected virtual void OnTestFinalize()
        {
        }
    }
}