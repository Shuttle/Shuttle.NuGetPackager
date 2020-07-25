﻿using System;
using System.Collections.Generic;
using System.IO;
using Shuttle.NuGetPackager;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using Moq;
using NUnit.Framework;
using Shuttle.NuGetPackager.MSBuild;

namespace Shuttle.NuGetPackager.Tests
{
	[TestFixture]
	public class ZipFixture
	{
		[Test]
		public void Should_be_able_to_create_an_archive()
		{
			var task = new Zip
			{
				BuildEngine = new Mock<IBuildEngine>().Object,
				Files = new List<ITaskItem>
						{
							new TaskItem(@".\zip-files\compress-me.txt"),
							new TaskItem(@".\zip-files\test-folder\compress-me-too.txt")
						}.ToArray(),
				ZipFilePath = new TaskItem("test-archive.zip"),
				RelativeFolder = new TaskItem(AppDomain.CurrentDomain.BaseDirectory)
			};

			Assert.IsTrue(task.Execute());
			Assert.IsTrue(File.Exists("test-archive.zip"));
		}
	}
}