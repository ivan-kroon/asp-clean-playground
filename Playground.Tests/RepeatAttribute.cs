using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit.Sdk;

namespace Playground.Tests
{
    public sealed class RepeatAttribute : DataAttribute
    {
        private readonly int count;

        public RepeatAttribute(int count)
        {
            this.count = count;
        }

        public override System.Collections.Generic.IEnumerable<object[]> GetData(System.Reflection.MethodInfo testMethod)
        {
            foreach (var number in Enumerable.Range(start: 1, count: this.count))
            {
                yield return new object[] { number };
            }
        }
    }
}
