using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IVMAnnotation
{
	internal static class Helper
	{
		internal static Stream GetCursorStream(string cursorName)
		{
			var assembly = typeof(Helper).Assembly;
			string[] resourceNames = assembly.GetManifestResourceNames();
			string resourceName = resourceNames.FirstOrDefault(name => name.EndsWith(cursorName));
			Stream stream = assembly.GetManifestResourceStream(resourceName);
			return stream;
		}
	}
}
