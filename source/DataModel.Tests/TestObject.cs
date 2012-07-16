using System;

using LinqToRest;

namespace DataModel.Tests
{
	[ServiceUrl("http://localhost:6789/api/TestObject/")]
	public class TestObject : IEquatable<TestObject>
	{
		public int Id { get; set; }

		public string TestProperty { get; set; }

		public bool Equals(TestObject other)
		{
			var result = false;

			if (ReferenceEquals(null, other))
			{
				result = false;
			}
			else if (ReferenceEquals(this, other))
			{
				result = true;
			}
			else
			{
				result = Equals(other.Id, Id);
			}

			return result;
		}

		public override bool Equals(object obj)
		{
			var result = false;

			if (ReferenceEquals(null, obj))
			{
				result = false;
			}
			else if (ReferenceEquals(this, obj))
			{
				result = true;
			}
			else if (obj.GetType() != typeof (TestObject))
			{
				result = false;
			}
			else
			{
				result = Equals((TestObject)obj);
			}

			return result;
		}

		public override int GetHashCode()
		{
			return String.Format("Id:{0};", Id).GetHashCode();
		}
	}
}
