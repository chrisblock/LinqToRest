using System;

using LinqToRest;

namespace TestWebApiService.Models
{
	[ServiceUrl("http://localhost:6789/api/TestModel/")]
	public class TestModel : IEquatable<TestModel>
	{
		public int Id { get; set; }
		public string TestProperty { get; set; }

		public bool Equals(TestModel other)
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
				result = (Id == other.Id) && (TestProperty == other.TestProperty);
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
			else if (obj.GetType() != GetType())
			{
				result = false;
			}
			else
			{
				result = Equals((TestModel) obj);
			}

			return result;
		}

		public override int GetHashCode()
		{
			return ToString().GetHashCode();
		}

		public override string ToString()
		{
			return String.Format("Id:{0};TestProperty:{1};", Id, TestProperty);
		}
	}
}
