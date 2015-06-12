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
        }

        public BrandingManager(System.IO.FileInfo file)
            : this()
        {
            if (file != null && file.Exists)
                this.Load(file);
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

        private string _Phone;
        public string Phone
        {
            get { return _Phone; }
        }

        private byte[] _Logo;
        public byte[] Logo
        {
            get { return _Logo; }
        }


        private Uri _URL;
        public Uri URL
        {
            get { return _URL; }
        }


        private string _EULA;
        public string EULA
        {
            get { return _EULA; }
        }

        #endregion

        public void Load(System.IO.DirectoryInfo directory)
        {
            System.IO.FileInfo _File = directory.GetFiles("*.sku").FirstOrDefault();
            this.Load(_File);
        }

        public void Load(System.IO.FileInfo file)
        {

            if (file != null)
            {
                XDocument _XDocument;
                _XDocument = XDocument.Load(file.FullName);

                var brand = _XDocument.Element("SKUID").Element("brand");

                this._Id = brand.Attribute("id").Value;
                this._Name = brand.Element("name").Attribute("value").Value;
                this._Description = brand.Attribute("id").Value;

                string _prelogo = brand.Element("logo").Attribute("value").Value;
                if (!string.IsNullOrEmpty(_prelogo))
                {
                    try
                    {
                        this._Logo = System.Convert.FromBase64String(_prelogo);
                    }
                    catch
                    {
                    }
                }
                this._Key = new Guid(brand.Element("key").Attribute("value").Value);

                string _preculture = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;


                IEnumerable<XElement> cultures = brand.Elements("cultures");
                XElement _element = cultures.Elements().FirstOrDefault(x => x.Attribute("value").Value == _preculture);
                if (_element != null)
                {
                    {
                        this._Phone = _element.Element("phone").Attribute("value").Value;
                        string _preurl = _element.Element("url").Attribute("value").Value;
                        if (!string.IsNullOrEmpty(_preurl))
                        {
                            this._URL = new Uri(_preurl);
                        }
                        this._EULA = _element.Element("eula").Attribute("value").Value;
                    }
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
                _Cache = new BrandingManager(_file);
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

        public static string BrandURL(this System.Reflection.Assembly assembly)
        {
            if (_Cache == null)
            {
                if (IsBranded(assembly))
                {
                    if (_Cache != null && _Cache.URL != null)
                    {
                        return _Cache.URL.ToString();
                    }
                }
            }

            if (_Cache.URL != null)
            {
                return _Cache.URL.ToString();
            }

            return null;
        }

        public static byte[] BrandLogo(this System.Reflection.Assembly assembly)
        {
            if (_Cache == null)
            {
                if (IsBranded(assembly))
                {
                    if (_Cache != null && _Cache.Logo != null)
                    {
                        return _Cache.Logo;
                    }
                }
            }
            return _Cache.Logo;
        }

        #endregion

        #region VB#

        public static bool IsBranded(this Microsoft.VisualBasic.ApplicationServices.AssemblyInfo assembly)
        {
            return IsBranded(assembly.LoadedAssemblies.FirstOrDefault(x => x.GetName().Name == assembly.AssemblyName));
        }

        public static string BrandName(this Microsoft.VisualBasic.ApplicationServices.AssemblyInfo assembly)
        {
            return BrandName(assembly.LoadedAssemblies.FirstOrDefault(x => x.GetName().Name == assembly.AssemblyName));
        }

        public static string BrandEULA(this Microsoft.VisualBasic.ApplicationServices.AssemblyInfo assembly)
        {
            return BrandEULA(assembly.LoadedAssemblies.FirstOrDefault(x => x.GetName().Name == assembly.AssemblyName));
        }

        public static string BrandURL(this Microsoft.VisualBasic.ApplicationServices.AssemblyInfo assembly)
        {
            return BrandURL(assembly.LoadedAssemblies.FirstOrDefault(x => x.GetName().Name == assembly.AssemblyName));
        }

        public static byte[] BrandLogo(this Microsoft.VisualBasic.ApplicationServices.AssemblyInfo assembly)
        {
            return BrandLogo(assembly.LoadedAssemblies.FirstOrDefault(x => x.GetName().Name == assembly.AssemblyName));
        }

        #endregion

    }

}
