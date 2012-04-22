using System;

namespace LinqToRest.Tests
{
	[ServiceUrl("http://localhost/api/TestObjects")]
	public class TestObject
	{
		public string TestProperty { get; set; }

		public bool Equals(TestObject other)
		{
			if (ReferenceEquals(null, other))
			{
				return false;
			}

			if (ReferenceEquals(this, other))
			{
				return true;
			}

			return Equals(other.TestProperty, TestProperty);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj))
			{
				return false;
			}

			if (ReferenceEquals(this, obj))
			{
				return true;
			}

			if (obj.GetType() != typeof (TestObject))
			{
				return false;
			}

			return Equals((TestObject) obj);
		}

		public override int GetHashCode()
		{
			return String.Format("TestProperty:{0};", TestProperty).GetHashCode();
		}
	}
}
