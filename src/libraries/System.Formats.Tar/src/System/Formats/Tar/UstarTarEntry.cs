﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Formats.Tar
{
    /// <summary>
    /// Represents a tar entry from an archive of the Ustar format.
    /// </summary>
    public sealed class UstarTarEntry : PosixTarEntry
    {
        // Constructor called when reading a TarEntry from a TarReader.
        internal UstarTarEntry(TarHeader header, TarReader readerOfOrigin)
            : base(header, readerOfOrigin, TarEntryFormat.Ustar)
        {
        }

        /// <summary>
        /// Initializes a new <see cref="UstarTarEntry"/> instance with the specified entry type and entry name.
        /// </summary>
        /// <param name="entryType">The type of the entry.</param>
        /// <param name="entryName">A string with the path and file name of this entry.</param>
        /// <exception cref="ArgumentException"><paramref name="entryName"/> is null or empty.</exception>
        /// <exception cref="InvalidOperationException">The entry type is not supported for creating an entry.</exception>
        /// <remarks>When creating an instance using the <see cref="UstarTarEntry(TarEntryType, string)"/> constructor, only the following entry types are supported:
        /// <list type="bullet">
        /// <item>In all platforms: <see cref="TarEntryType.Directory"/>, <see cref="TarEntryType.HardLink"/>, <see cref="TarEntryType.SymbolicLink"/>, <see cref="TarEntryType.RegularFile"/>.</item>
        /// <item>In Unix platforms only: <see cref="TarEntryType.BlockDevice"/>, <see cref="TarEntryType.CharacterDevice"/> and <see cref="TarEntryType.Fifo"/>.</item>
        /// </list>
        /// </remarks>
        public UstarTarEntry(TarEntryType entryType, string entryName)
            : base(entryType, entryName, TarEntryFormat.Ustar, isGea: false)
        {
            _header._prefix = string.Empty;
        }

        /// <summary>
        /// Initializes a new <see cref="UstarTarEntry"/> instance by converting the specified <paramref name="other"/> entry into the Ustar format.
        /// </summary>
        public UstarTarEntry(TarEntry other)
            : base(other, TarEntryFormat.Ustar)
        {
            if (other._header._format is TarEntryFormat.Ustar or TarEntryFormat.Pax)
            {
                _header._prefix = other._header._prefix;
            }
            else
            {
                _header._prefix = string.Empty;
            }
        }

        // Determines if the current instance's entry type supports setting a data stream.
        internal override bool IsDataStreamSetterSupported() => EntryType == TarEntryType.RegularFile;
    }
}
