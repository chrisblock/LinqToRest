using System;
using System.Net.Http;
using System.Threading.Tasks;

using Changes;

namespace LinqToRest.Client.Http
{
	public abstract class HttpRequest
	{
		private Action<HttpContent> _successAction;
		private Action<Exception> _failureAction;

		public static HttpRequest Get(Uri uri)
		{
			return new HttpGetRequest(uri);
		}

		public static HttpRequest Put<T>(string url, ChangeSet<T> data)
		{
			return Put(new Uri(url), data);
		}

		public static HttpRequest Put<T>(Uri uri, ChangeSet<T> data)
		{
			return new HttpPutRequest<T>(uri, data);
		}

		public static HttpRequest Post<T>(string url, T data)
		{
			return Post(new Uri(url), data);
		}

		public static HttpRequest Post<T>(Uri uri, T data)
		{
			return new HttpPostRequest<T>(uri, data);
		}

		public static HttpRequest Delete(Uri uri)
		{
			return new HttpDeleteRequest(uri);
		}

		protected virtual void HandleRequestCompletion(Task<HttpResponseMessage> task)
		{
			if (task.IsFaulted || (task.Exception != null))
			{
				_failureAction(task.Exception);
			}
			else
			{
				var responseMessage = task.Result;

				if (responseMessage.IsSuccessStatusCode)
				{
					_successAction(task.Result.Content);
				}
				else
				{
					var exception = new HttpRequestException();

					_failureAction(exception);
				}
			}
		}

		protected abstract Task<HttpResponseMessage> PerformRequest();

		public virtual HttpRequest OnSuccess(Action<HttpContent> successAction)
		{
			_successAction = successAction;

			return this;
		}

		public virtual HttpRequest OnFailure(Action<Exception> failureAction)
		{
			_failureAction = failureAction;

			return this;
		}

		public virtual HttpRequest ThrowOnFailure()
		{
			_failureAction = x => { throw x; };

			return this;
		}

		public virtual void Execute()
		{
			PerformRequest()
				.ContinueWith(HandleRequestCompletion)
				.Start();
		}
	}
}
