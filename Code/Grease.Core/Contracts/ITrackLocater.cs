// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ITrackLocater.cs" company="Developing Enterprises">
//   Josh Charles
// </copyright>
// <summary>
//   Defines the ITrackLocater type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Grease.Core
{
	using System.Collections.ObjectModel;

	/// <summary>
	/// The GreaseFileSystemAccess interface.
	/// </summary>
	public interface ITrackLocater
	{
		/// <summary>
		/// Gets the found.  As tracks are located, they are put into the Found collection.
		/// </summary>
		ObservableCollection<ITrackInfo> Found { get; }
	}
}