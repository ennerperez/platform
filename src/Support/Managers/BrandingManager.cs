#if !PORTABLE

using Platform.Support.Reflection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Xml.Linq;

namespace Platform.Support.Branding
{
    public class BrandingManager : IDisposable
    {
        public BrandingManager()
        {
            Information = new Information();
            Products = new List<Product>();
            Licenses = new List<License>();
        }

        public BrandingManager(FileInfo file, Guid guid) : this()
        {
            if (file != null && file.Exists)
                Load(file, guid);
        }

        #region Properties

        public string Id { get; private set; }

        public Guid Key { get; private set; }

        public Information Information { get; private set; }

        public List<Product> Products { get; private set; }

        public List<License> Licenses { get; private set; }

        #endregion Properties

        public void Load(string directory, Guid guid)
        {
            Load(new DirectoryInfo(directory), guid);
        }

        public void Load(DirectoryInfo directory, Guid guid)
        {
            var _File = directory.GetFiles("*.sku").FirstOrDefault();
            Load(_File, guid);
        }

        public void Load(FileInfo file, Guid guid)
        {
            if (file != null)
            {
                XDocument _XDocument;
                XNamespace _ns = "http://www.w3.org/2018/brandingSchema";

                _XDocument = XDocument.Load(file.FullName);

                var brand = _XDocument.Element(_ns + "SKUID").Element(_ns + "brand");

                Id = brand.Attribute("id").Value;
                Key = new Guid(brand.Attribute("key").Value);

                var info = brand.Element(_ns + "information");

                Information.Name = info.Element(_ns + "name").Value;
                Information.Description = info.Element(_ns + "description").Value;
                Information.EULA = info.Element(_ns + "eula").Value;

                #region Nested Information

                if (info.Elements(_ns + "images") != null)
                    foreach (var item in info.Elements(_ns + "images").Elements())
                    {
                        string _prelogo = item.Value;
                        if (!string.IsNullOrEmpty(_prelogo))
                        {
                            try
                            {
                                Information.Images.Add(item.Attribute("id").Value, Convert.FromBase64String(_prelogo));
                            }
                            catch (Exception ex)
                            {
                                ex.DebugThis();
                            }
                        }
                    }

                if (info.Elements(_ns + "colors") != null)
                    foreach (var item in info.Elements(_ns + "colors").Elements())
                    {
                        string _precolor = item.Value;
                        if (!string.IsNullOrEmpty(_precolor))
                        {
                            Information.Colors.Add(item.Attribute("id").Value, _precolor);
                        }
                    }

                if (info.Elements(_ns + "phones") != null)
                    foreach (var item in info.Elements(_ns + "phones").Elements())
                    {
                        string _prephone = item.Value;
                        if (!string.IsNullOrEmpty(_prephone))
                        {
                            Information.Phones.Add(item.Attribute("id").Value, _prephone);
                        }
                    }

                if (info.Elements(_ns + "urls") != null)
                    foreach (var item in info.Elements(_ns + "urls").Elements())
                    {
                        string _preurl = item.Value;
                        if (!string.IsNullOrEmpty(_preurl))
                        {
                            try
                            {
                                var _url = new Uri(_preurl);
                                Information.URLs.Add(item.Attribute("id").Value, _url);
                            }
                            catch (Exception ex)
                            {
                                ex.DebugThis();
                            }
                        }
                    }

                if (info.Elements(_ns + "emails") != null)
                    foreach (var item in info.Elements(_ns + "emails").Elements())
                    {
                        string _preemail = item.Value;
                        if (!string.IsNullOrEmpty(_preemail))
                        {
                            try
                            {
                                var _email = new MailAddress(_preemail);
                                Information.Emails.Add(item.Attribute("id").Value, _email);
                            }
                            catch (Exception ex)
                            {
                                ex.DebugThis();
                            }
                        }
                    }

                if (info.Elements(_ns + "contacts") != null)
                    foreach (var item in info.Elements(_ns + "contacts").Elements())
                    {
                        string _precontact = item.Value;
                        if (!string.IsNullOrEmpty(_precontact))
                        {
                            Information.Contacts.Add(item.Attribute("id").Value, _precontact);
                        }
                    }

                #endregion Nested Information

                if (brand.Elements(_ns + "products") != null)
                    foreach (var item in brand.Elements(_ns + "products").Elements())
                    {
                        try
                        {
                            var bpguid = new Guid(item.Attribute("key").Value);
                            var bpculture = string.Empty;
                            if (item.Attribute("culture") != null)
                                bpculture = item.Attribute("culture").Value;
                            var bpneutral = false;
                            if (item.Attribute("neutral") != null)
                                bpneutral = int.Parse(item.Attribute("neutral").Value) == 1;

                            string name = null;
                            string description = null;
                            string eula = null;

                            if (item.Element(_ns + "name") != null)
                                name = item.Element(_ns + "name").Value;
                            if (item.Element(_ns + "description") != null)
                                description = item.Element(_ns + "description").Value;
                            if (item.Element(_ns + "eula") != null)
                                eula = item.Element(_ns + "eula").Value;

                            var bproduct = new Product(bpguid, name, description, eula)
                            {
                                Culture = bpculture,
                                Neutral = bpneutral
                            };

                            if (item.Elements(_ns + "images") != null)
                                foreach (var subitem in item.Elements(_ns + "images").Elements())
                                {
                                    string _prelogo = subitem.Value;
                                    if (!string.IsNullOrEmpty(_prelogo))
                                    {
                                        try
                                        {
                                            bproduct.Images.Add(subitem.Attribute("id").Value, Convert.FromBase64String(_prelogo));
                                        }
                                        catch (Exception ex)
                                        {
                                            ex.DebugThis();
                                        }
                                    }
                                }

                            if (item.Elements(_ns + "colors") != null)
                                foreach (var subitem in item.Elements(_ns + "colors").Elements())
                                {
                                    string _precolor = subitem.Value;
                                    if (!string.IsNullOrEmpty(_precolor))
                                    {
                                        bproduct.Colors.Add(subitem.Attribute("id").Value, _precolor);
                                    }
                                }

                            Products.Add(bproduct);
                        }
                        catch (Exception ex)
                        {
                            ex.DebugThis();
                        }
                    }

                if (brand.Elements(_ns + "licenses") != null)
                    foreach (var item in brand.Elements(_ns + "licenses").Elements())
                    {
                        try
                        {
                            var blguid = item.Attribute("serial").Value;
                            var data = item.Value;
                            Licenses.Add(new License(blguid, data));
                        }
                        catch (Exception ex)
                        {
                            ex.DebugThis();
                        }
                    }
            }
        }

        #region IDisposable Support

        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Products.Clear();
                    Information.Images.Clear();
                    Information.Colors.Clear();
                }

                Products = null;
                Information.Images = null;
                Information.Colors = null;
                Information = null;
            }
            disposedValue = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion IDisposable Support
    }

    public class Information
    {
        public Information()
        {
            Images = new Dictionary<string, byte[]>();
            Colors = new Dictionary<string, string>();
            Phones = new Dictionary<string, string>();
            Emails = new Dictionary<string, MailAddress>();
            URLs = new Dictionary<string, Uri>();
            Contacts = new Dictionary<string, string>();
        }

        public Information(string n, string d, string e = null) : this()
        {
            Name = n;
            Description = d;
            EULA = e;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public string EULA { get; set; }

        public Dictionary<string, byte[]> Images { get; set; }
        public Dictionary<string, string> Colors { get; set; }
        public Dictionary<string, string> Phones { get; set; }
        public Dictionary<string, MailAddress> Emails { get; set; }
        public Dictionary<string, Uri> URLs { get; set; }
        public Dictionary<string, string> Contacts { get; set; }
    }

    public class Product
    {
        public Product()
        {
            Images = new Dictionary<string, byte[]>();
            Colors = new Dictionary<string, string>();
        }

        public Product(Guid k, string n, string d, string e = null) : this()
        {
            Key = k;
            Name = n;
            Description = d;
            EULA = e;
            Culture = string.Empty;
            Neutral = false;
        }

        public Guid Key { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string EULA { get; set; }

        public string Culture { get; set; }
        public bool Neutral { get; set; }

        public Dictionary<string, byte[]> Images { get; set; }

        public Dictionary<string, string> Colors { get; set; }
    }

    public class License
    {
        public License(string serial, string data)
        {
            Serial = serial;
            Data = data.Replace(" ", "");
            Compute();
        }

        public string Serial { get; private set; }
        public Guid Product { get; private set; }
        public string Data { get; private set; }
        public string Hash { get; private set; }

        private string Private { get; set; }

        public IEnumerable<string> GetData(string serial)
        {
            var result = new List<string>();
            var parts = Private.Split('|');
            if (parts.Any() && parts[1] == serial)
            {
                for (int i = 2; i < parts.Count(); i++)
                    result.Add(parts[i]);
                return result;
            }

            return null;
        }

        private void Compute()
        {
            try
            {
                var data = Data.Substring(0, Data.Length - 2);
                var lines = data.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
                var base64 = string.Empty;
                foreach (var line in lines)
                {
                    Hash += line.Substring(0, 2) + line.Substring(line.Length - 2, 2);
                    base64 += line.Substring(2, line.Length - 4);
                }
                base64 = base64 + "=";
                var buffer = Convert.FromBase64String(base64);
                Private = Encoding.UTF8.GetString(buffer);
                var parts = Private.Split('|');
                if (parts.Any())
                    Product = new Guid(parts[0]);
            }
            catch (Exception ex)
            {
                ex.DebugThis();
            }
        }
    }

    public static class Brand
    {
        private static BrandingManager cache;

        private static IEnumerable<FileInfo> GetBrandFiles(Assembly assembly)
        {
            if (assembly == null) assembly = Assembly.GetEntryAssembly();
            return new FileInfo(assembly.Location).Directory.GetFiles("*.sku");
        }

        public static bool IsBranded(this Assembly assembly)
        {
            if (cache == null)
            {
                var _file = Brand.GetBrandFiles(assembly).FirstOrDefault();
                if (_file != null && _file.Exists)
                    cache = new BrandingManager(_file, assembly.GUID().Value);
            }

            return cache != null;
        }

        #region Brand Information

        public static string BrandName(this Assembly assembly)
        {
            if (!IsBranded(assembly)) return null;
            return cache.Information.Name;
        }

        public static string BrandEULA(this Assembly assembly)
        {
            if (!IsBranded(assembly)) return null;
            return cache.Information.EULA;
        }

        public static string BrandURL(this Assembly assembly, string key = null)
        {
            if (!IsBranded(assembly)) return null;

            if (key == null && cache.Information.URLs.Any())
                return cache.Information.URLs.FirstOrDefault().Value.ToString();
            else if (cache.Information.URLs.ContainsKey(key))
                return cache.Information.URLs[key].ToString();

            return null;
        }

        public static byte[] BrandImage(this Assembly assembly, string key = null)
        {
            if (!IsBranded(assembly)) return null;

            if (key == null && cache.Information.Images.Any())
                return cache.Information.Images.FirstOrDefault().Value;
            else if (cache.Information.Images.ContainsKey(key))
                return cache.Information.Images[key];

            return null;
        }

        public static string BrandColor(this Assembly assembly, string key = null)
        {
            if (!IsBranded(assembly)) return null;

            if (key == null && cache.Information.Colors.Any())
                return cache.Information.Colors.FirstOrDefault().Value;
            else if (cache.Information.Colors.ContainsKey(key))
                return cache.Information.Colors[key];

            return null;
        }

        public static string BrandPhone(this Assembly assembly, string key = null)
        {
            if (!IsBranded(assembly)) return null;

            if (key == null && cache.Information.Phones.Any())
                return cache.Information.Phones.FirstOrDefault().Value;
            else if (cache.Information.Phones.ContainsKey(key))
                return cache.Information.Phones[key];

            return null;
        }

        public static MailAddress BrandEmail(this Assembly assembly, string key = null)
        {
            if (!IsBranded(assembly)) return null;

            if (key == null && cache.Information.Emails.Any())
                return cache.Information.Emails.FirstOrDefault().Value;
            else if (cache.Information.Emails.ContainsKey(key))
                return cache.Information.Emails[key];

            return null;
        }

        public static string BrandContact(this Assembly assembly, string key = null)
        {
            if (!IsBranded(assembly)) return null;

            if (key == null && cache.Information.Contacts.Any())
                return cache.Information.Contacts.FirstOrDefault().Value;
            else if (cache.Information.Contacts.ContainsKey(key))
                return cache.Information.Contacts[key];

            return null;
        }

        #endregion Brand Information

        public static Product BrandProduct(this Assembly assembly, Guid? key = null, string culture = "")
        {
            if (!IsBranded(assembly)) return null;

            if (key == null)
                key = assembly.GUID();

            var neutral = culture == string.Empty;

            if (key == null && cache.Products.Any())
                return cache.Products.FirstOrDefault();
            else if (cache.Products.Any())
                return (from item in cache.Products
                        where item.Key == key.Value &&
                        ((neutral && item.Neutral) || (!neutral && item.Culture.ToLower() == culture.ToLower()))
                        select item).FirstOrDefault();

            return null;
        }

        public static IEnumerable<License> BrandLicenses(this Assembly assembly, Guid? key = null)
        {
            if (!IsBranded(assembly)) return null;

            if (key == null)
                key = assembly.GUID();

            if (cache.Licenses.Any())
            {
                var licenses = (from item in cache.Licenses
                                where item.Product == key
                                select item);
                return licenses;
            }

            return null;
        }

        public static IEnumerable<string> BrandLicenseData(this Assembly assembly, Guid? key = null, string serial = "")
        {
            if (!IsBranded(assembly)) return null;

            if (key == null)
                key = assembly.GUID();

            if (cache.Licenses.Any())
            {
                var license = (from item in cache.Licenses
                               where item.Product == key && item.Serial == serial
                               select item).SingleOrDefault();
                if (license != null)
                    return license.GetData(serial);
            }

            return null;
        }
    }
}

#endif