using Bluemoon;
using log4net;
using RonftonCard.Core.CardReader;
using RonftonCard.Core.Config;
using RonftonCard.Core.Dongle;
using RonftonCard.Core.KeyService;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RonftonCard.Core
{
	/// <summary>
	/// configuration management
	/// </summary>
	#pragma warning disable 168
	public class ConfigManager
	{
		///////////////////////////////////////////////////////////////////////////////////

		private static IKeyService keyService;

		///////////////////////////////////////////////////////////////////////////////////

		private static IDictionary<String, CardTempleteDescriptor> templeteDescriptors;
		private const String CardTempleteConfigFileName = "CardTemplete.xml";

		public static bool Init()
		{
			try
			{
				templeteDescriptors = XmlConfigUtil.CreateEntity<IDictionary<String, CardTempleteDescriptor>>(CardTempleteConfigFileName);
				//keyService  = new LocalTestKeyService(logger, GetDongle(), "012345");
			}
			catch (Exception ex)
			{
				return false;
			}
			return true;
		}


		public static IKeyService GetKeyService()
		{
			return keyService;
		}

		///////////////////////////////////////////////////////////////////////////////////

		public static String TempleteSelected { get; set; }

		public static CardType CardType { get; set; }
		 
		/// <summary>
		/// get all config templete names
		/// </summary>
		public static String[] TempleteNames
		{
			get
			{
				return GetDescriptorKeys< CardTempleteDescriptor>(templeteDescriptors);
			}
		}

		private static String[] GetDescriptorKeys<T>( IDictionary<String, T> descriptor )
		{
			return descriptor == null || descriptor.Keys.Count == 0 ? new String[] { } : descriptor.Keys.ToArray();
		}


		#region "--- Create instance from config ---"
		

		public static CardTempleteDescriptor GetCardTemplete()
		{
			return templeteDescriptors[TempleteSelected];
		}
		
		#endregion
	}
}