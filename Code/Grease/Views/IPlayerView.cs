// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPlayerView.cs" company="Developing Enterprises">
//   Josh Charles
// </copyright>
// <summary>
//   The PlayerView interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Grease.Views
{
	using Grease.ViewModels;

	using ReactiveUI;

	/// <summary>
	/// The PlayerView interface.
	/// </summary>
	public interface IPlayerView : IViewFor<IPlayerViewModel>
	{
	}
}