﻿using System;
using System.IO;

namespace Shuttle.NuGetPackager.Tests
{
    public class FilePathExtensions
    {
        public static string BasePath(string relativePath)
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath);
        }
    }
}