using Platform.Support.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Platform.Support.Branding
{
    public class BrandingManager : IDisposable
    {
        public BrandingManager()
        {
            logos = new Dictionary<string, byte[]>();
            colors = new Dictionary<string, string>();
            phonesNumbers = new Dictionary<string, string>();
            urls = new Dictionary<string, Uri>();
            mailAdress = new Dictionary<string, System.Net.Mail.MailAddress>();
            products = new Dictionary<Guid, BrandedProduct>();
        }

        public BrandingManager(System.IO.FileInfo file, Guid guid)
            : this()
        {
            if (file != null && file.Exists)
                this.Load(file, guid);
        }

        #region Properties

        private string id;

        public string Id
        {
            get { return id; }
        }

        private string name;

        public string Name
        {
            get { return name; }
        }

        private string description;

        public string Description
        {
            get { return description; }
        }

        private Guid key;

        public Guid Key
        {
            get { return key; }
        }

        private Dictionary<string, byte[]> logos;

        public Dictionary<string, byte[]> Logos
        {
            get { return logos; }
        }

        private Dictionary<string, string> colors;

        public Dictionary<string, string> Colors
        {
            get { return colors; }
        }

        private Dictionary<string, string> phonesNumbers;

        public Dictionary<string, string> PhonesNumbers
        {
            get { return phonesNumbers; }
        }

        private Dictionary<string, Uri> urls;

        public Dictionary<string, Uri> URLs
        {
            get { return urls; }
        }

        private Dictionary<Guid, BrandedProduct> products;

        public Dictionary<Guid, BrandedProduct> Products
        {
            get { return products; }
        }

        private Dictionary<string, System.Net.Mail.MailAddress> mailAdress;

        public Dictionary<string, System.Net.Mail.MailAddress> MailAddress
        {
            get { return mailAdress; }
        }

        private string productName;

        public string ProductName
        {
            get { return productName; }
        }

        private string productDescription;

        public string ProductDescription
        {
            get { return productDescription; }
        }

        private string productEULA;

        public string ProductEULA
        {
            get { return productEULA; }
        }

        private string eula;

        public string EULA
        {
            get { return eula; }
        }

        #endregion Properties

        public void Load(string directory, Guid guid)
        {
            this.Load(new System.IO.DirectoryInfo(directory), guid);
        }

        public void Load(System.IO.DirectoryInfo directory, Guid guid)
        {
            System.IO.FileInfo _File = directory.GetFiles("*.sku").FirstOrDefault();
            this.Load(_File, guid);
        }

        public void Load(System.IO.FileInfo file, Guid guid)
        {
            if (file != null)
            {
                XDocument _XDocument;
                XNamespace _ns = "http://www.w3.org/2016/brandingSchema";

                _XDocument = XDocument.Load(file.FullName);

                var brand = _XDocument.Element(_ns + "SKUID").Element(_ns + "brand");

                this.id = brand.Attribute("id").Value;
                this.key = new Guid(brand.Attribute("key").Value);

                this.name = brand.Element(_ns + "name").Value;
                this.description = brand.Element(_ns + "description").Value;
                this.eula = brand.Element(_ns + "eula").Value;

                foreach (var item in brand.Elements(_ns + "logos").Elements())
                {
                    string _prelogo = item.Value;
                    if (!string.IsNullOrEmpty(_prelogo))
                    {
                        try
                        {
                            this.logos.Add(item.Attribute("id").Value, System.Convert.FromBase64String(_prelogo));
                        }
                        catch (Exception) { }
                    }
                }

                foreach (var item in brand.Elements(_ns + "colors").Elements())
                {
                    string _precolor = item.Value;
                    if (!string.IsNullOrEmpty(_precolor))
                    {
                        this.colors.Add(item.Attribute("id").Value, _precolor);
                    }
                }

                foreach (var item in brand.Elements(_ns + "phones").Elements())
                {
                    string _prephone = item.Value;
                    if (!string.IsNullOrEmpty(_prephone))
                    {
                        this.phonesNumbers.Add(item.Attribute("id").Value, _prephone);
                    }
                }

                foreach (var item in brand.Elements(_ns + "urls").Elements())
                {
                    string _preurl = item.Value;
                    if (!string.IsNullOrEmpty(_preurl))
                    {
                        try
                        {
                            Uri _url = new Uri(_preurl);
                            this.urls.Add(item.Attribute("id").Value, _url);
                        }
                        catch (Exception) { }
                    }
                }

                foreach (var item in brand.Elements(_ns + "emails").Elements())
                {
                    string _preemail = item.Value;
                    if (!string.IsNullOrEmpty(_preemail))
                    {
                        try
                        {
                            System.Net.Mail.MailAddress _email = new System.Net.Mail.MailAddress(_preemail);
                            this.mailAdress.Add(item.Attribute("id").Value, _email);
                        }
                        catch (Exception) { }
                    }
                }

                foreach (var item in brand.Elements(_ns + "products").Elements())
                {
                    try
                    {
                        var bpguid = new Guid(item.Attribute("guid").Value);

                        string name = null;
                        string description = null;
                        string eula = null;

                        if (item.Element(_ns + "name") != null)
                            name = item.Element(_ns + "name").Value;
                        if (item.Element(_ns + "description") != null)
                            description = item.Element(_ns + "description").Value;
                        if (item.Element(_ns + "eula") != null)
                            eula = item.Element(_ns + "eula").Value;

                        var bproduct = new BrandedProduct(new Guid(item.Attribute("guid").Value), name, description, eula);

                        this.products.Add(bpguid, bproduct);
                    }
                    catch (Exception) { }
                }

                XElement product = brand.Element(_ns + "products").Elements().Where<XElement>((x) => x.Attribute("guid").Value.ToLower() == guid.ToString().ToLower()).FirstOrDefault();
                if (product != null)
                {
                    if (product.Element(_ns + "name") != null)
                        this.productName = product.Element(_ns + "name").Value;
                    if (product.Element(_ns + "description") != null)
                        this.productDescription = product.Element(_ns + "description").Value;
                    if (product.Element(_ns + "eula") != null)
                        this.productEULA = product.Element(_ns + "eula").Value;
                }
            }
        }

        #region IDisposable Support

        // Para detectar llamadas redundantes
        private bool disposedValue;

        // IDisposable
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: eliminar estado administrado (objetos administrados).
                }

                // TODO: liberar recursos no administrados (objetos no administrados) e invalidar Finalize() below.
                // TODO: Establecer campos grandes como Null.
            }
            disposedValue = true;
        }

        public void Dispose()
        {
            // No cambie este código. Coloque el código de limpieza en Dispose(disposing As Boolean).
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion IDisposable Support
    }

    public struct BrandedProduct
    {
        public BrandedProduct(Guid g, string n, string d, string e = null)
        {
            GUID = g;
            Name = n;
            Description = d;
            EULA = e;
        }

        public Guid GUID { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public String EULA { get; set; }
    }

    public static class Brand
    {
        private static BrandingManager cache;

        private static IEnumerable<System.IO.FileInfo> GetBrandFiles(System.Reflection.Assembly assembly)
        {
            if (assembly == null) assembly = System.Reflection.Assembly.GetEntryAssembly();
            return new System.IO.FileInfo(assembly.Location).Directory.GetFiles("*.sku");
        }

        public static bool IsBranded(this System.Reflection.Assembly assembly)
        {
            System.IO.FileInfo _file = Brand.GetBrandFiles(assembly).FirstOrDefault();
            if (_file != null && _file.Exists)
            {
                cache = new BrandingManager(_file, assembly.GUID().Value);
            }
            else
            {
                cache = null;
            }
            return cache != null;
        }

        public static string BrandName(this System.Reflection.Assembly assembly)
        {
            if (cache == null)
            {
                if (IsBranded(assembly))
                {
                    if (cache != null && !string.IsNullOrEmpty(cache.Name))
                    {
                        return cache.Name;
                    }
                }
            }

            return cache.Name;
        }

        public static string BrandEULA(this System.Reflection.Assembly assembly)
        {
            if (cache == null)
            {
                if (IsBranded(assembly))
                {
                    if (cache != null && !string.IsNullOrEmpty(cache.EULA))
                    {
                        return cache.EULA;
                    }
                }
            }

            return cache.EULA;
        }

        public static string BrandURL(this System.Reflection.Assembly assembly, string key = null)
        {
            if (cache == null)
            {
                if (IsBranded(assembly))
                {
                    if (key == null)
                    {
                        if (cache != null && cache.URLs.Count > 0)
                        {
                            var frist = cache.URLs.FirstOrDefault();
                            return frist.Value.ToString();
                        }
                    }
                    else
                    {
                        if (cache != null && cache.URLs.ContainsKey(key))
                        {
                            return cache.URLs[key].ToString();
                        }
                    }
                }
            }

            if (key == null)
            {
                if (cache.URLs.Count > 0)
                {
                    var frist = cache.URLs.FirstOrDefault();
                    return frist.Value.ToString();
                }
            }
            else
            {
                if (cache.URLs.ContainsKey(key))
                {
                    return cache.URLs[key].ToString();
                }
            }

            return null;
        }

        public static BrandedProduct? BrandProduct(this System.Reflection.Assembly assembly, Guid? key = null)
        {
            if (key == null)
            {
                key = assembly.GUID();
            }

            if (cache == null)
            {
                if (IsBranded(assembly))
                {
                    if (key == null)
                    {
                        if (cache != null && cache.Products.Count > 0)
                        {
                            var frist = cache.Products.FirstOrDefault();
                            return frist.Value;
                        }
                    }
                    else
                    {
                        if (cache != null && cache.Products.ContainsKey(key.Value))
                        {
                            return cache.Products[key.Value];
                        }
                    }
                }
            }

            if (key == null)
            {
                if (cache.URLs.Count > 0)
                {
                    var frist = cache.Products.FirstOrDefault();
                    return frist.Value;
                }
            }
            else
            {
                if (cache.Products.ContainsKey(key.Value))
                {
                    return cache.Products[key.Value];
                }
            }

            return null;
        }

        public static byte[] BrandLogo(this System.Reflection.Assembly assembly, string key = null)
        {
            if (cache == null)
            {
                if (IsBranded(assembly))
                {
                    if (key == null)
                    {
                        if (cache != null && cache.Logos.Count > 0)
                        {
                            var frist = cache.Logos.FirstOrDefault();
                            return frist.Value;
                        }
                    }
                    else
                    {
                        if (cache != null && cache.Logos.ContainsKey(key))
                        {
                            return cache.Logos[key];
                        }
                    }
                }
            }

            if (key == null)
            {
                if (cache.Logos.Count > 0)
                {
                    var frist = cache.Logos.FirstOrDefault();
                    return frist.Value;
                }
            }
            else
            {
                if (cache.Logos.ContainsKey(key))
                {
                    return cache.Logos[key];
                }
            }

            return null;
        }

        public static string BrandColor(this System.Reflection.Assembly assembly, string key = null)
        {
            if (cache == null)
            {
                if (IsBranded(assembly))
                {
                    if (key == null)
                    {
                        if (cache != null && cache.Colors.Count > 0)
                        {
                            var frist = cache.Colors.FirstOrDefault();
                            return frist.Value;
                        }
                    }
                    else
                    {
                        if (cache != null && cache.Colors.ContainsKey(key))
                        {
                            return cache.Colors[key];
                        }
                    }
                }
            }

            if (key == null)
            {
                if (cache.Colors.Count > 0)
                {
                    var frist = cache.Colors.FirstOrDefault();
                    return frist.Value;
                }
            }
            else
            {
                if (cache.Colors.ContainsKey(key))
                {
                    return cache.Colors[key];
                }
            }

            return null;
        }
    }
}