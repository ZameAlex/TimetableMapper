using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTimetableMapper
{
	public sealed class ExampleDiagnosticObserver :
	IObserver<DiagnosticListener>,
	IObserver<KeyValuePair<string, object>>
	{
		// IObserver<DiagnosticListener> implementation
		// ...
		private readonly List<IDisposable> _subscriptions = new List<IDisposable>();
		void IObserver<KeyValuePair<string, object>>.OnNext(KeyValuePair<string, object> pair)
		{
			Write(pair.Key, pair.Value);
		}

		void IObserver<KeyValuePair<string, object>>.OnError(Exception error)
		{ }

		void IObserver<KeyValuePair<string, object>>.OnCompleted()
		{ }

		public void Write(string name, object value)
		{
			Console.WriteLine(name);
			Console.WriteLine(value);
			Console.WriteLine();
		}

		void IObserver<DiagnosticListener>.OnNext(DiagnosticListener diagnosticListener)
		{
			var subscription = diagnosticListener.Subscribe(this);
			_subscriptions.Add(subscription);
		}

		void IObserver<DiagnosticListener>.OnError(Exception error)
		{ }

		void IObserver<DiagnosticListener>.OnCompleted()
		{ }
	}
}
