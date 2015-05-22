using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Platform.Support.Configuration
{
    public class ConfigurationManager : IDisposable
    {

        public Dictionary<String, String> ConnectionStrings { get; set; }

        private System.IO.DirectoryInfo _Folder = new System.IO.DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
        private String _File = System.IO.Path.Combine(new System.IO.DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)).Parent.FullName, ".config");

        public ConfigurationManager()
        {
            this.ConnectionStrings = new Dictionary<String, String>();
        }

        public void Load()
        {

            XDocument _XDocument;
            if (!System.IO.File.Exists(_File))
                this.Save();

            _XDocument = XDocument.Load(_File);

            foreach (XElement item in _XDocument.Element("configuration").Elements("connectionStrings").Where(x=> x.HasAttributes))
            {
                    this.ConnectionStrings.Add(item.Element("name").Value, item.Element("@connectionString").Value);
            }

        }

        public void Reset()
        {
            this.Save();
        }

        public void Save()
        {
            XDocument _XDocument;

            _XDocument = new XDocument(new XDeclaration("1.0", "utf-8", "yes"));
            XElement _configuration = new XElement("configuration");
            XElement _connectionStrings = new XElement("connectionStrings");

            if (this.ConnectionStrings != null)
            {
                foreach (KeyValuePair<string, string> item in this.ConnectionStrings)
                {
                    _connectionStrings.Add(new XElement("add", new object[] { new XAttribute("name", item.Key), new XAttribute("connectionString", item.Value), new XAttribute("providerName", "System.Data.SqlClient") }));
                }

                _configuration.Add(_connectionStrings);

            }

            _XDocument.Add(_configuration);
            _XDocument.Save(_File);

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
}
