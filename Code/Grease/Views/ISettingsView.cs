// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISettingsView.cs" company="Developing Enterprises">
//   Josh Charles
// </copyright>
// <summary>
//   The SettingsView interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Grease.Views
{
	using Grease.ViewModels;

	using ReactiveUI;

	/// <summary>
	/// The SettingsView interface.
	/// </summary>
	public interface ISettingsView : IViewFor<ISettingsViewModel>
	{		
	}
}