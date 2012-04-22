using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

using Newtonsoft.Json;

namespace LinqToRest.Delta
{
	public static class ChangeSet
	{
		public static ChangeSet<T> Generate<T>(T left, T right)
		{
			var result = new ChangeSet<T>();

			var type = typeof (T);
			var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);

			foreach (var property in properties)
			{
				var leftValue = property.GetValue(left, new object[0]);
				var rightValue = property.GetValue(right, new object[0]);

				if (leftValue.Equals(rightValue) == false)
				{
					result.AddChange(property.Name, rightValue);
				}
			}

			return result;
		}
	}

	public class ChangeSet<T>
	{
		private readonly IDictionary<string, object> _delta;

		public ChangeSet()
		{
			_delta = new Dictionary<string, object>();
		}

		public bool IsEmpty()
		{
			return _delta.Count == 0;
		}

		public void AddChange(string propertyName, object newValue)
		{
			_delta[propertyName] = newValue;
		}

		public void AddChange<TProperty>(Expression<Func<T, TProperty>> propertyAccessor, TProperty newValue)
		{
			var memberExpression = propertyAccessor.Body as MemberExpression;

			if (memberExpression == null)
			{
				throw new ArgumentException(String.Format("'{0}' is not a MemberExpression.", propertyAccessor.Body));
			}

			AddChange(memberExpression.Member.Name, newValue);
		}

		public T Apply(T instance)
		{
			foreach (var change in _delta)
			{
				typeof(T).GetProperty(change.Key).SetValue(instance, change.Value, new object[0]);
			}

			return instance;
		}

		public string ToJSON()
		{
			return JsonConvert.SerializeObject(_delta);
		}
	}
}
