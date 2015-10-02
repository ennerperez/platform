using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using Platform.Support.Reflection;

namespace Platform.Support.Branding
{

    public class BrandingManager : IDisposable
    {

        public BrandingManager()
        {
            this._Logos = new Dictionary<string, byte[]>();
            this._PhonesNumbers = new Dictionary<string, string>();
            this._URLs = new Dictionary<string, Uri>();
            this._MailAddress = new Dictionary<string, System.Net.Mail.MailAddress>();
            this._Products = new Dictionary<Guid, BrandedProduct>();
        }

        public BrandingManager(System.IO.FileInfo file, Guid guid)
            : this()
        {
            if (file != null && file.Exists)
                this.Load(file, guid);
        }

        #region Properties

        private string _Id;
        public string Id
        {
            get { return _Id; }
        }

        private string _Name;
        public string Name
        {
            get { return _Name; }
        }

        private string _Description;
        public string Description
        {
            get { return _Description; }
        }

        private Guid _Key;
        public Guid Key
        {
            get { return _Key; }
        }

        private Dictionary<string, byte[]> _Logos;
        public Dictionary<string, byte[]> Logos
        {
            get { return _Logos; }
        }

        private Dictionary<string, string> _PhonesNumbers;
        public Dictionary<string, string> PhonesNumbers
        {
            get { return _PhonesNumbers; }
        }

        private Dictionary<string, Uri> _URLs;
        public Dictionary<string, Uri> URLs
        {
            get { return _URLs; }
        }

        private Dictionary<Guid, BrandedProduct> _Products;
        public Dictionary<Guid, BrandedProduct> Products
        {
            get { return _Products; }
        }

        private Dictionary<string, System.Net.Mail.MailAddress> _MailAddress;
        public Dictionary<string, System.Net.Mail.MailAddress> MailAddress
        {
            get { return _MailAddress; }
        }

        private string _ProductName;
        public string ProductName
        {
            get { return _ProductName; }
        }

        private string _ProductDescription;
        public string ProductDescription
        {
            get { return _ProductDescription; }
        }

        private string _ProductEULA;
        public string ProductEULA
        {
            get { return _ProductEULA; }
        }

        private string _EULA;
        public string EULA
        {
            get { return _EULA; }
        }

        #endregion

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
                XNamespace _ns = "http://www.w3.org/2015/brandingSchema";

                _XDocument = XDocument.Load(file.FullName);

                var brand = _XDocument.Element(_ns + "SKUID").Element(_ns + "brand");

                this._Id = brand.Attribute("id").Value;
                this._Key = new Guid(brand.Attribute("key").Value);

                this._Name = brand.Element(_ns + "name").Value;
                this._Description = brand.Element(_ns + "description").Value;
                this._EULA = brand.Element(_ns + "eula").Value;

                foreach (var item in brand.Elements(_ns + "logos").Elements())
                {
                    string _prelogo = item.Value;
                    if (!string.IsNullOrEmpty(_prelogo))
                    {
                        try
                        {
                            this._Logos.Add(item.Attribute("id").Value, System.Convert.FromBase64String(_prelogo));
                        }
                        catch (Exception) { }
                    }
                }

                foreach (var item in brand.Elements(_ns + "phones").Elements())
                {
                    string _prephone = item.Value;
                    if (!string.IsNullOrEmpty(_prephone))
                    {
                        this._PhonesNumbers.Add(item.Attribute("id").Value, _prephone);
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
                            this._URLs.Add(item.Attribute("id").Value, _url);
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
                            this._MailAddress.Add(item.Attribute("id").Value, _email);
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

                        this._Products.Add(bpguid, bproduct);
                    }
                    catch (Exception) { }
                }

                XElement product = brand.Element(_ns + "products").Elements().Where<XElement>((x) => x.Attribute("guid").Value.ToLower() == guid.ToString().ToLower()).FirstOrDefault();
                if (product != null)
                {

                    if (product.Element(_ns + "name") != null)
                        this._ProductName = product.Element(_ns + "name").Value;
                    if (product.Element(_ns + "description") != null)
                        this._ProductDescription = product.Element(_ns + "description").Value;
                    if (product.Element(_ns + "eula") != null)
                        this._ProductEULA = product.Element(_ns + "eula").Value;
                }

            }

        }

        #region IDisposable Support
        // Para detectar llamadas redundantes
        private bool disposedValue;

        // IDisposable
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    // TODO: eliminar estado administrado (objetos administrados).
                }

                // TODO: liberar recursos no administrados (objetos no administrados) e invalidar Finalize() below.
                // TODO: Establecer campos grandes como Null.
            }
            this.disposedValue = true;
        }

        void IDisposable.Dispose()
        {
            // No cambie este código. Coloque el código de limpieza en Dispose(disposing As Boolean).
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

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

        private static BrandingManager _Cache;

        #region C#

        private static IEnumerable<System.IO.FileInfo> GetBrandFiles(System.Reflection.Assembly assembly)
        {
            if (assembly == null) assembly = System.Reflection.Assembly.GetExecutingAssembly();
            return new System.IO.FileInfo(assembly.Location).Directory.GetFiles("*.sku");
        }


        public static bool IsBranded(this System.Reflection.Assembly assembly)
        {
            System.IO.FileInfo _file = Brand.GetBrandFiles(assembly).FirstOrDefault();
            if (_file != null && _file.Exists)
            {
                _Cache = new BrandingManager(_file, assembly.GUID());
            }
            else
            {
                _Cache = null;
            }
            return _Cache != null;
        }

        public static string BrandName(this System.Reflection.Assembly assembly)
        {
            if (_Cache == null)
            {
                if (IsBranded(assembly))
                {
                    if (_Cache != null && !string.IsNullOrEmpty(_Cache.Name))
                    {
                        return _Cache.Name;
                    }
                }
            }

            return _Cache.Name;
        }

        public static string BrandEULA(this System.Reflection.Assembly assembly)
        {
            if (_Cache == null)
            {
                if (IsBranded(assembly))
                {
                    if (_Cache != null && !string.IsNullOrEmpty(_Cache.EULA))
                    {
                        return _Cache.EULA;
                    }
                }
            }

            return _Cache.EULA;
        }

        public static string BrandURL(this System.Reflection.Assembly assembly, string key = null)
        {
            if (_Cache == null)
            {
                if (IsBranded(assembly))
                {
                    if (key == null)
                    {
                        if (_Cache != null && _Cache.URLs.Count > 0)
                        {
                            var frist = _Cache.URLs.FirstOrDefault();
                            return frist.Value.ToString();
                        }
                    }
                    else
                    {
                        if (_Cache != null && _Cache.URLs.ContainsKey(key))
                        {
                            return _Cache.URLs[key].ToString();
                        }
                    }
                }
            }

            if (key == null)
            {
                if (_Cache.URLs.Count > 0)
                {
                    var frist = _Cache.URLs.FirstOrDefault();
                    return frist.Value.ToString();
                }
            }
            else
            {
                if (_Cache.URLs.ContainsKey(key))
                {
                    return _Cache.URLs[key].ToString();
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

            if (_Cache == null)
            {
                if (IsBranded(assembly))
                {
                    if (key == null)
                    {
                        if (_Cache != null && _Cache.Products.Count > 0)
                        {
                            var frist = _Cache.Products.FirstOrDefault();
                            return frist.Value;
                        }
                    }
                    else
                    {
                        if (_Cache != null && _Cache.Products.ContainsKey(key.Value))
                        {
                            return _Cache.Products[key.Value];
                        }
                    }
                }
            }

            if (key == null)
            {
                if (_Cache.URLs.Count > 0)
                {
                    var frist = _Cache.Products.FirstOrDefault();
                    return frist.Value;
                }
            }
            else
            {
                if (_Cache.Products.ContainsKey(key.Value))
                {
                    return _Cache.Products[key.Value];
                }
            }

            return null;
        }

        public static byte[] BrandLogo(this System.Reflection.Assembly assembly, string key = null)
        {

            if (_Cache == null)
            {
                if (IsBranded(assembly))
                {
                    if (key == null)
                    {
                        if (_Cache != null && _Cache.Logos.Count > 0)
                        {
                            var frist = _Cache.Logos.FirstOrDefault();
                            return frist.Value;
                        }
                    }
                    else
                    {
                        if (_Cache != null && _Cache.Logos.ContainsKey(key))
                        {
                            return _Cache.Logos[key];
                        }
                    }
                }
            }

            if (key == null)
            {
                if (_Cache.Logos.Count > 0)
                {
                    var frist = _Cache.Logos.FirstOrDefault();
                    return frist.Value;
                }
            }
            else
            {
                if (_Cache.Logos.ContainsKey(key))
                {
                    return _Cache.Logos[key];
                }
            }

            return null;

        }

        #endregion

        //#region VB

        //public static bool IsBranded(this Microsoft.VisualBasic.ApplicationServices.AssemblyInfo assembly)
        //{
        //    return IsBranded(assembly.LoadedAssemblies.FirstOrDefault(x => x.GetName().Name == assembly.AssemblyName));
        //}

        //public static string BrandName(this Microsoft.VisualBasic.ApplicationServices.AssemblyInfo assembly)
        //{
        //    return BrandName(assembly.LoadedAssemblies.FirstOrDefault(x => x.GetName().Name == assembly.AssemblyName));
        //}

        //public static string BrandEULA(this Microsoft.VisualBasic.ApplicationServices.AssemblyInfo assembly)
        //{
        //    return BrandEULA(assembly.LoadedAssemblies.FirstOrDefault(x => x.GetName().Name == assembly.AssemblyName));
        //}

        //public static string BrandURL(this Microsoft.VisualBasic.ApplicationServices.AssemblyInfo assembly)
        //{
        //    return BrandURL(assembly.LoadedAssemblies.FirstOrDefault(x => x.GetName().Name == assembly.AssemblyName));
        //}

        //public static byte[] BrandLogo(this Microsoft.VisualBasic.ApplicationServices.AssemblyInfo assembly)
        //{
        //    return BrandLogo(assembly.LoadedAssemblies.FirstOrDefault(x => x.GetName().Name == assembly.AssemblyName));
        //}

        //#endregion

    }

}
