﻿using BlueMoon.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RonftonCard.Common.Utils
{
	public class TypeUtil
	{
		private const char FULL_PATH_SPLIT = ',';
		public static Type GetType(String type)
		{
			Type rt = Type.GetType(type);

			if (rt != null)
				return rt;

			// full name
			if (type.IndexOf(FULL_PATH_SPLIT) > 0)
			{
				Assembly assembly = LoadAssembly(type);
				if (assembly != null)
					return assembly.GetType(type);
			}
			return null;
		}

		private const String DLL_SUFFIX = ".dll";

		private static Assembly LoadAssembly(String type)
		{
			String assemblyName = type.Substring(type.IndexOf(FULL_PATH_SPLIT) + 1);

			if (String.IsNullOrEmpty(assemblyName))
				return null;

			if (!assemblyName.EndsWith(DLL_SUFFIX))
				assemblyName = assemblyName + DLL_SUFFIX;

			String fullFileName;

			if( FileUtil.Locate(assemblyName, out fullFileName ))
			{
				return Assembly.LoadFile(fullFileName);
			}
			return null;
		}
	}
}
