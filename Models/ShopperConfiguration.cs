using Microsoft.Azure.KeyVault;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ESolutions.Shopper.Models
{
	public class ShopperConfiguration
	{
		//Classes
		#region MagentoConfiguration
		public class MagentoConfiguration
		{
			#region ShopRootUrl
			[JsonProperty("shopRootUrl")]
			public Uri ShopRootUrl
			{
				get;
				internal set;
			}
			#endregion

			#region User
			[JsonProperty("user")]
			public String User
			{
				get;
				internal set;
			}
			#endregion

			#region Pass
			[JsonProperty("password")]
			public String Password
			{
				get;
				internal set;
			}
			#endregion

			#region ConsumerKey
			[JsonProperty("consumerKey")]
			public String ConsumerKey
			{
				get;
				internal set;
			}
			#endregion

			#region ConsumerSecret
			[JsonProperty("consumerSecret")]
			public String ConsumerSecret
			{
				get;
				internal set;
			}
			#endregion
		}
		#endregion

		#region EbayConfiguration
		public class EbayConfiguration
		{
			#region ApiServerUrl
			[JsonProperty("apiServerUrl")]
			public String ApiServerUrl
			{
				get;
				internal set;
			}
			#endregion

			#region ApiToken
			[JsonProperty("apiToken")]
			public String ApiToken
			{
				get;
				internal set;
			}
			#endregion
		}
		#endregion

		#region EmailConfiguration
		public class EmailConfiguration
		{
			#region SmtpServerHostname
			/// <summary>
			/// Gets the SMTP server hostname.
			/// </summary>
			/// <value>The SMTP server hostname.</value>
			[JsonProperty("smtpServerHostname")]
			public String SmtpServerHostname
			{
				get;
				internal set;
			}
			#endregion

			#region SmtpServerPort
			/// <summary>
			/// Gets the SMTP server port.
			/// </summary>
			/// <value>The SMTP server port.</value>
			[JsonProperty("smtpServerPort")]
			public Int32 SmtpServerPort
			{
				get;
				internal set;
			}
			#endregion

			#region SmtpAuthUser
			/// <summary>
			/// Gets the maximum age in days of sales that shall be imported.
			/// </summary>
			/// <value>The sales days back.</value>
			[JsonProperty("SmtpAuthUser")]
			public String SmtpAuthUser
			{
				get;
				internal set;
			}
			#endregion

			#region SmtpAuthPassword
			/// <summary>
			/// Gets the SMTP auth password.
			/// </summary>
			/// <value>The SMTP auth password.</value>
			[JsonProperty("SmtpAuthPassword")]
			public String SmtpAuthPassword
			{
				get;
				internal set;
			}
			#endregion

			#region EmailAddressOfOfferSender
			/// <summary>
			/// Gets the maximum age in days of sales that shall be imported.
			/// </summary>
			/// <value>The sales days back.</value>
			[JsonProperty("emailAddressOfOfferSender")]
			public MailAddress EmailAddressOfOfferSender
			{
				get;
				internal set;
			}
			#endregion
		}
		#endregion

		#region LocationsConfiguration
		public class LocationsConfiguration
		{
			#region InvoicePath
			/// <summary>
			/// Gets the path where the invoices to save pdf's of invoices.
			/// </summary>
			/// <value>
			/// The invoice path.
			/// </value>
			[JsonProperty("invoicePath")]
			public System.IO.DirectoryInfo InvoicePath
			{
				get;
				set;
			}
			#endregion

			#region ArticleImagePath
			/// <summary>
			/// Gets the path where the article images are saved.
			/// </summary>
			/// <value>
			/// The invoice path.
			/// </value>
			[JsonProperty("articleImagePath")]
			public System.IO.DirectoryInfo ArticleImagePath
			{
				get;
				set;
			}
			#endregion

			#region SyncerApplicationExe
			/// <summary>
			/// Gets the maximum age in days of sales that shall be imported.
			/// </summary>
			/// <value>The sales days back.</value>
			[JsonProperty("syncerApplicationExe")]
			public FileInfo SyncerApplicationExe
			{
				get;
				set;
			}
			#endregion
		}
		#endregion

		#region PrintingConfiguration
		public class PrintingConfiguration
		{
			#region User
			[JsonProperty("user")]
			public String User
			{
				get;
				internal set;
			}
			#endregion

			#region Password
			[JsonProperty("password")]
			public String Password
			{
				get;
				internal set;
			}
			#endregion
		}
		#endregion

		#region MandantorConfiguration
		public class MandantorConfiguration
		{
			#region Company
			[JsonProperty("company")]
			public String Company
			{
				get;
				internal set;
			}
			#endregion

			#region WebUrl
			[JsonProperty("webUrl")]
			public String WebUrl
			{
				get;
				internal set;
			}
			#endregion

			#region Email
			[JsonProperty("email")]
			public String Email
			{
				get;
				internal set;
			}
			#endregion

			#region Street
			[JsonProperty("street")]
			public String Street
			{
				get;
				internal set;
			}
			#endregion

			#region StreetNr
			[JsonProperty("streetNr")]
			public String StreetNr
			{
				get;
				internal set;
			}
			#endregion

			#region Zip
			[JsonProperty("zip")]
			public String Zip
			{
				get;
				internal set;
			}
			#endregion

			#region City
			[JsonProperty("city")]
			public String City
			{
				get;
				internal set;
			}
			#endregion

			#region Phone
			[JsonProperty("phone")]
			public String Phone
			{
				get;
				internal set;
			}
			#endregion

			#region Fax
			[JsonProperty("fax")]
			public String Fax
			{
				get;
				internal set;
			}
			#endregion

			#region ZipWithCity
			[JsonIgnore]
			public String ZipWithCity
			{
				get
				{
					return $"{this.Zip} {this.City}";
				}
			}
			#endregion

			#region StreetWithNumber
			[JsonIgnore]
			public String StreetWithNumber
			{
				get
				{
					return $"{this.Street} {this.StreetNr}";
				}
			}
			#endregion

			#region FullAddress
			[JsonIgnore]
			public String FullAddress
			{
				get
				{
					return $"{this.Company} - {this.StreetWithNumber} - {this.ZipWithCity}";
				}
			}
			#endregion

			#region DpdNr
			[JsonProperty("dpdNr")]
			public String DpdNr
			{
				get;
				internal set;
			}
			#endregion

			#region CountryIso2
			[JsonProperty("countryIso2")]
			public String CountryIso2
			{
				get;
				internal set;
			}
			#endregion

			#region CountryIso3
			[JsonProperty("countryIso3")]
			public String CountryIso3
			{
				get;
				internal set;
			}
			#endregion

			#region Tax
			[JsonProperty("tax")]
			public String Tax
			{
				get;
				internal set;
			}
			#endregion

			#region Bank
			[JsonProperty("bank")]
			public String Bank
			{
				get;
				internal set;
			}
			#endregion
		}
		#endregion

		//Properties
		#region Default
		/// <summary>
		/// Returns the applications default settings object.
		/// </summary>
		/// <value>The default.</value>
		public static ShopperConfiguration Default
		{
			get;
			set;
		}
		#endregion

		#region ImportDaysBack
		/// <summary>
		/// Gets the days that shall be synced with ebay and magento.
		/// </summary>
		/// <value>
		/// The import days back.
		/// </value>
		[JsonProperty("importDaysBack")]
		public Int32 ImportDaysBack
		{
			get;
			internal set;
		}
		#endregion

		#region ImageBaseUrl
		/// <summary>
		/// Gets the image base URL from where to load picture over http.
		/// </summary>
		/// <value>
		/// The image base URL.
		/// </value>
		[JsonProperty("imageBaseUrl")]
		public String ImageBaseUrl
		{
			get;
			internal set;
		}
		#endregion

		#region CurrentTaxRate
		/// <summary>
		/// Gets the current tax rate.
		/// </summary>
		/// <value>The current tax rate.</value>
		[JsonProperty("currentTaxRate")]
		public Decimal CurrentTaxRate
		{
			get;
			internal set;
		}
		#endregion

		#region Locations
		[JsonProperty("locations")]
		public LocationsConfiguration Locations
		{
			get;
			private set;
		} = new LocationsConfiguration();
		#endregion

		#region Email
		[JsonProperty("email")]
		public EmailConfiguration Email
		{
			get;
			private set;
		} = new EmailConfiguration();
		#endregion

		#region Printing
		[JsonProperty("printing")]
		public PrintingConfiguration Printing
		{
			get;
			private set;
		} = new PrintingConfiguration();
		#endregion

		#region Ebay
		public EbayConfiguration Ebay
		{
			get;
			set;
		} = new EbayConfiguration();
		#endregion

		#region Magento
		[JsonProperty("magento")]
		public MagentoConfiguration Magento
		{
			get;
			private set;
		} = new MagentoConfiguration();
		#endregion

		#region Mandantor
		[JsonProperty("mandantor")]
		public MandantorConfiguration Mandantor
		{
			get;
			private set;
		} = new MandantorConfiguration();
		#endregion
	}

	public class ShopperConfigurationReader : IConfigurationSectionHandler
	{
		//Consts
		#region AppId
		protected const String AppId = "";
		#endregion

		#region AppSecret
		protected const String AppSecret = "";
		#endregion

		#region SecretUrl
		protected const String SecretUrl = "";
		#endregion

		//Methods
		#region Create
		/// <summary>
		/// Creates a configuration section handler.
		/// </summary>
		/// <param name="parent">Parent object.</param>
		/// <param name="configContext">Configuration context object.</param>
		/// <param name="section">Section XML node.</param>
		/// <returns>The created section handler object.</returns>
		public object Create(object parent, object configContext, XmlNode section)
		{
			ShopperConfiguration result = new ShopperConfiguration();

			result.ImageBaseUrl = section[nameof(ShopperConfiguration.ImageBaseUrl)].InnerText;
			result.CurrentTaxRate = Convert.ToDecimal(section[nameof(ShopperConfiguration.CurrentTaxRate)].InnerText);
			result.ImportDaysBack = Convert.ToInt32(section[nameof(ShopperConfiguration.ImportDaysBack)].InnerText);

			result.Locations.InvoicePath = new DirectoryInfo(section[nameof(ShopperConfiguration.Locations)][nameof(ShopperConfiguration.LocationsConfiguration.InvoicePath)].InnerText);
			result.Locations.ArticleImagePath = new DirectoryInfo(section[nameof(ShopperConfiguration.Locations)][nameof(ShopperConfiguration.LocationsConfiguration.ArticleImagePath)].InnerText);
			result.Locations.SyncerApplicationExe = new FileInfo(section[nameof(ShopperConfiguration.Locations)][nameof(ShopperConfiguration.LocationsConfiguration.SyncerApplicationExe)].InnerText);

			result.Email.SmtpServerHostname = section[nameof(ShopperConfiguration.Email)][nameof(ShopperConfiguration.EmailConfiguration.SmtpServerHostname)].InnerText;
			result.Email.SmtpServerPort = Convert.ToInt32(section[nameof(ShopperConfiguration.Email)][nameof(ShopperConfiguration.EmailConfiguration.SmtpServerPort)].InnerText);
			result.Email.SmtpAuthUser = section[nameof(ShopperConfiguration.Email)][nameof(ShopperConfiguration.EmailConfiguration.SmtpAuthUser)].InnerText;
			result.Email.SmtpAuthPassword = section[nameof(ShopperConfiguration.Email)][nameof(ShopperConfiguration.EmailConfiguration.SmtpAuthPassword)].InnerText;
			result.Email.EmailAddressOfOfferSender = new MailAddress(section[nameof(ShopperConfiguration.Email)][nameof(ShopperConfiguration.EmailConfiguration.EmailAddressOfOfferSender)].InnerText);

			result.Printing.User = section[nameof(ShopperConfiguration.Printing)][nameof(ShopperConfiguration.PrintingConfiguration.User)].InnerText;
			result.Printing.Password = section[nameof(ShopperConfiguration.Printing)][nameof(ShopperConfiguration.PrintingConfiguration.Password)].InnerText;

			result.Ebay.ApiServerUrl = section[nameof(ShopperConfiguration.Ebay)][nameof(ShopperConfiguration.EbayConfiguration.ApiServerUrl)].InnerText;
			result.Ebay.ApiToken = section[nameof(ShopperConfiguration.Ebay)][nameof(ShopperConfiguration.EbayConfiguration.ApiToken)].InnerText;

			result.Magento.ShopRootUrl = new Uri(section[nameof(ShopperConfiguration.Magento)][nameof(ShopperConfiguration.MagentoConfiguration.ShopRootUrl)].InnerText);
			result.Magento.User = section[nameof(ShopperConfiguration.Magento)][nameof(ShopperConfiguration.MagentoConfiguration.User)].InnerText;
			result.Magento.Password = section[nameof(ShopperConfiguration.Magento)][nameof(ShopperConfiguration.MagentoConfiguration.Password)].InnerText;
			result.Magento.ConsumerKey = section[nameof(ShopperConfiguration.Magento)][nameof(ShopperConfiguration.MagentoConfiguration.ConsumerKey)].InnerText;
			result.Magento.ConsumerSecret = section[nameof(ShopperConfiguration.Magento)][nameof(ShopperConfiguration.MagentoConfiguration.ConsumerSecret)].InnerText;

			result.Mandantor.Company = section[nameof(ShopperConfiguration.Mandantor)][nameof(ShopperConfiguration.MandantorConfiguration.Company)].InnerText;
			result.Mandantor.WebUrl = section[nameof(ShopperConfiguration.Mandantor)][nameof(ShopperConfiguration.MandantorConfiguration.WebUrl)].InnerText;
			result.Mandantor.Email = section[nameof(ShopperConfiguration.Mandantor)][nameof(ShopperConfiguration.MandantorConfiguration.Email)].InnerText;
			result.Mandantor.Street = section[nameof(ShopperConfiguration.Mandantor)][nameof(ShopperConfiguration.MandantorConfiguration.Street)].InnerText;
			result.Mandantor.StreetNr = section[nameof(ShopperConfiguration.Mandantor)][nameof(ShopperConfiguration.MandantorConfiguration.StreetNr)].InnerText;
			result.Mandantor.Zip = section[nameof(ShopperConfiguration.Mandantor)][nameof(ShopperConfiguration.MandantorConfiguration.Zip)].InnerText;
			result.Mandantor.City = section[nameof(ShopperConfiguration.Mandantor)][nameof(ShopperConfiguration.MandantorConfiguration.City)].InnerText;
			result.Mandantor.Phone = section[nameof(ShopperConfiguration.Mandantor)][nameof(ShopperConfiguration.MandantorConfiguration.Phone)].InnerText;
			result.Mandantor.Fax = section[nameof(ShopperConfiguration.Mandantor)][nameof(ShopperConfiguration.MandantorConfiguration.Fax)].InnerText;
			result.Mandantor.DpdNr = section[nameof(ShopperConfiguration.Mandantor)][nameof(ShopperConfiguration.MandantorConfiguration.DpdNr)].InnerText;
			result.Mandantor.CountryIso2 = section[nameof(ShopperConfiguration.Mandantor)][nameof(ShopperConfiguration.MandantorConfiguration.CountryIso2)].InnerText;
			result.Mandantor.CountryIso3 = section[nameof(ShopperConfiguration.Mandantor)][nameof(ShopperConfiguration.MandantorConfiguration.CountryIso3)].InnerText;
			result.Mandantor.Tax = section[nameof(ShopperConfiguration.Mandantor)][nameof(ShopperConfiguration.MandantorConfiguration.Tax)].InnerText;
			result.Mandantor.Bank = section[nameof(ShopperConfiguration.Mandantor)][nameof(ShopperConfiguration.MandantorConfiguration.Bank)].InnerText;

			return result;
		}
		#endregion

		#region FromWebConfig
		public static ShopperConfiguration FromWebConfig()
		{
			return (ShopperConfiguration)ConfigurationManager.GetSection("esolutions.shopper");
		}
		#endregion

		#region FromVault
		public static ShopperConfiguration FromVault()
		{
			var jsonString = ShopperConfigurationReader.GetSecretValue().Result;
			var result = new ShopperConfiguration();
			JsonConvert.PopulateObject(jsonString, result);
			return result;
		}
		#endregion

		#region GetSecretValue
		//Grant access: Login-AzureRmAccount
		//Set-AzureRmKeyVaultAccessPolicy -VaultName 'my-vault' -ServicePrincipalName principal-guid-from-AD-application -PermissionsToSecrets get
		public static async Task<String> GetSecretValue()
		{
			var client = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(ShopperConfigurationReader.GetToken));
			var secret = await client.GetSecretAsync(ShopperConfigurationReader.SecretUrl);
			return secret.Value;
		}
		#endregion

		#region GetToken
		private static async Task<string> GetToken(String authority, String resource, String scope)
		{
			var authContext = new AuthenticationContext(authority);
			ClientCredential clientCred = new ClientCredential(ShopperConfigurationReader.AppId, ShopperConfigurationReader.AppSecret);
			AuthenticationResult result = await authContext.AcquireTokenAsync(resource, clientCred);
			return result.AccessToken;
		}
		#endregion
	}
}
