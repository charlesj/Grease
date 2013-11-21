// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IApplicationViewModel.cs" company="Developing Enterprises">
//   Josh Charles
// </copyright>
// <summary>
//   Defines the view model of the entire application.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Grease.ViewModels
{
	using System.ComponentModel;

	using ReactiveUI;

	/// <summary>
	/// Defines the view model of the entire application.
	/// </summary>
	public interface IApplicationViewModel : IScreen, INotifyPropertyChanged
	{
		/// <summary>
		/// Gets the status bar text of the status bar.
		/// </summary>
		string StatusBarText { get; }

		/// <summary>
		/// Writes a message to the status bar.
		/// </summary>
		/// <param name="message">
		/// The message to write.
		/// </param>
		void WriteToStatusBar(string message);

		void TriggerGlobalCommand(string commandName);

		event GlobalCommandHandler OnGlobalCommand;
	}

    public class GlobalCommandEventArgs
    {
        public string CommandName { get; set; }

        public object Args { get; set; }
    }

    public delegate void GlobalCommandHandler(object sender, GlobalCommandEventArgs args);
}