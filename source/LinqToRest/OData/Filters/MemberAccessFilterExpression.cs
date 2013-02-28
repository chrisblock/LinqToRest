using System;

namespace LinqToRest.OData.Filters
{
	public class MemberAccessFilterExpression : FilterExpression, IEquatable<MemberAccessFilterExpression>
	{
		public override FilterExpressionType ExpressionType { get { return FilterExpressionType.MemberAccess; } }

		public FilterExpression Instance { get; private set; }

		public string Member { get; private set; }

		public MemberAccessFilterExpression(FilterExpression instance, string member)
		{
			Instance = instance;
			Member = member;
		}

		public bool Equals(MemberAccessFilterExpression other)
		{
			bool result;

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
				result = Equals(other.Instance, Instance) && Equals(other.Member, Member);
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
			else if (obj.GetType() != typeof (MemberAccessFilterExpression))
			{
				result = false;
			}
			else
			{
				result = Equals((MemberAccessFilterExpression) obj);
			}

			return result;
		}

		public override int GetHashCode()
		{
			var str = String.Format("Instance:{0};Member:{1};", Instance, Member);

			return str.GetHashCode();
		}

		public override string ToString()
		{
			var result = Member;

			if (Instance != null)
			{
				result = String.Format("{0}/{1}", Instance, Member);
			}
			return result;
		}
	}
}
