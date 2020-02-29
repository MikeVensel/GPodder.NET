// <copyright file="EpisodeActionType.cs" company="MikeVensel">
// Copyright (c) MikeVensel. All rights reserved.
// </copyright>

namespace GPodder.NET.Enumerations
{
    /// <summary>
    /// The various possible events on episodes.
    /// </summary>
    public enum EpisodeActionType
    {
        /// <summary>
        /// Indicates that the episode has been dowloaded to a device.
        /// </summary>
        Download,

        /// <summary>
        /// Indicates that the episode has been deleted from a device.
        /// </summary>
        Delete,

        /// <summary>
        /// Indicates that an episode is being played on a device.
        /// </summary>
        Play,

        /// <summary>
        /// Indicates that a device has requested that all previous states for
        /// the episode be reset.
        /// </summary>
        New,

        /// <summary>
        /// gPodder.net does not document what this action means.
        /// </summary>
        Flattr,
    }
}
