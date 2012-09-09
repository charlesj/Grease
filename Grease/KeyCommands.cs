// --------------------------------------------------------------------------------------------------------------------
// <copyright file="KeyCommands.cs" company="Developing Enterprises">
//   Josh Charles
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Grease
{
	using System.Diagnostics.CodeAnalysis;
	using System.Windows.Input;

	/// <summary>
	/// The key commands.
	/// </summary>
	public static class KeyCommands
	{
		/// <summary>
		/// The next track command.
		/// </summary>
		[SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. Suppression is OK here.")]
		public static RoutedCommand NextTrackCommand = new RoutedCommand();

		/// <summary>
		/// The play pause command.
		/// </summary>
		[SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. Suppression is OK here.")]
		public static RoutedCommand PlayPauseCommand = new RoutedCommand();

		/// <summary>
		/// The previous track command.
		/// </summary>
		[SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. Suppression is OK here.")]
		public static RoutedCommand PreviousTrackCommand = new RoutedCommand();

		/// <summary>
		/// The volume down command.
		/// </summary>
		[SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. Suppression is OK here.")]
		public static RoutedCommand VolumeDownCommand = new RoutedCommand();

		/// <summary>
		/// The volume up command.
		/// </summary>
		[SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. Suppression is OK here.")]
		public static RoutedCommand VolumeUpCommand = new RoutedCommand();
	}
}