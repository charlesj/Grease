// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMusicTagProvider.cs" company="Developing Enterprises">
//   Josh Charles
// </copyright>
// <summary>
//   The MusicTagProvider interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Grease.Core
{
	/// <summary>
	/// The MusicTagProvider interface.
	/// </summary>
	public interface IMusicTagProvider
	{
		///// <summary>
		///// The get info.
		///// </summary>
		///// <param name="path">
		///// The path.
		///// </param>
		///// <returns>
		///// The Grease.Core.ITrackInfo.
		///// </returns>
		//ITrackInfo GetInfo(string path);

		/// <summary>
		/// The get info.
		/// </summary>
		/// <param name="info">
		/// The info.
		/// </param>
		void LazyLoad(ITrackInfo info, string path);
	}
}