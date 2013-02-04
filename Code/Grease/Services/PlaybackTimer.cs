namespace Grease.Services
{
	using System;
	using System.Diagnostics;
	using System.Windows.Threading;

	using ReactiveUI;

	public class PlaybackTimer : ReactiveObject
	{
		private DispatcherTimer internalTimer;

		private readonly Stopwatch stopwatch;

		private TimeSpan elapsed;

		public PlaybackTimer()
		{
			this.stopwatch = new Stopwatch();
			this.Reset();
		}

		public TimeSpan Elapsed
		{
			get
			{
				return this.elapsed;
			}
			set
			{
				this.RaiseAndSetIfChanged(model => model.Elapsed, ref this.elapsed, value);
			}
		}

		public void Start()
		{
			this.internalTimer.Start();
			this.stopwatch.Start();
		}

		public void Pause()
		{
			this.stopwatch.Stop();
			this.internalTimer.Stop();
		}

		public void Stop()
		{
			this.Pause();
			this.Reset();
		}
		
		public void Reset()
		{
			this.internalTimer = new DispatcherTimer();
			this.internalTimer.Interval = new TimeSpan(0, 0, 0, 0, 10);
			this.internalTimer.Tick += InternalTimerCallback;

			this.stopwatch.Reset();
		}

		private void InternalTimerCallback(object sender, EventArgs eventArgs)
		{
			this.Elapsed = this.stopwatch.Elapsed;
		}
	}
}
