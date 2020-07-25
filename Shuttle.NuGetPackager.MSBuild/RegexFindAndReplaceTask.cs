using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Shuttle.NuGetPackager.MSBuild
{
	public delegate void LogDelegate(string message);

	public class RegexFindAndReplaceTask
	{
		private readonly List<string> _files = new List<string>();

		public string FindExpression { get; set; }
		public string ReplacementText { get; set; }

		public bool IgnoreCase { get; set; }
		public bool Multiline { get; set; }
		public bool Singleline { get; set; }

		public event LogDelegate LogMessage = delegate { }; 
		public event LogDelegate LogWarning = delegate { }; 

		public RegexFindAndReplaceTask()
		{
			ReplacementText = String.Empty;
		}

		public void AddFile(string file)
		{
			_files.Add(file);
		}

		public void Execute()
		{
			if (string.IsNullOrEmpty(FindExpression))
			{
				throw new ArgumentException("'FindExpression' is required.");
			}

			var options = RegexOptions.None;

			if (IgnoreCase)
			{
				options |= RegexOptions.IgnoreCase;
			}

			if (Multiline)
			{
				options |= RegexOptions.Multiline;
			}

			if (Singleline)
			{
				options |= RegexOptions.Singleline;
			}

			var replaceRegex = new Regex(FindExpression, options);

			foreach (var file in _files)
			{
				if (File.Exists(file))
				{
					var contents = File.ReadAllText(file);

					if (replaceRegex.IsMatch(contents) != true)
					{
						LogWarning.Invoke(
                            $"[find/replace - no matches] : file = '{file}' / find expression = '{FindExpression}'");
					}
					else
					{
						contents = replaceRegex.Replace(contents, ReplacementText);

						File.WriteAllText(file, contents);

						LogMessage($"[find/replace] : file = '{file}' / find expression = '{FindExpression}' / replacement text = '{ReplacementText}'");
					}
				}
				else
				{
					LogWarning.Invoke($"" +
                                      $"[find/replace - file not found] : file = '{file}'");
				}
			}
		}
	}
}