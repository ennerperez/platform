#if !PORTABLE

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Platform.Support.Configuration
{
    public class ConfigurationManager : IDisposable
    {
        public Dictionary<string, string> Connectionstrings { get; set; }

        private DirectoryInfo _Folder = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
        private string _File = Path.Combine(new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)).Parent.FullName, ".config");

        public ConfigurationManager()
        {
            this.Connectionstrings = new Dictionary<string, string>();
        }

        public void Load()
        {
            XDocument _XDocument;
            if (!File.Exists(_File))
                this.Save();

            _XDocument = XDocument.Load(_File);

            foreach (XElement item in _XDocument.Element("configuration").Elements("connectionstrings").Where(x => x.HasAttributes))
            {
                this.Connectionstrings.Add(item.Element("name").Value, item.Element("@connectionstring").Value);
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
            XElement _connectionstrings = new XElement("connectionstrings");

            if (this.Connectionstrings != null)
            {
                foreach (KeyValuePair<string, string> item in this.Connectionstrings)
                {
                    _connectionstrings.Add(new XElement("add", new object[] { new XAttribute("name", item.Key), new XAttribute("connectionstring", item.Value), new XAttribute("providerName", "System.Data.SqlClient") }));
                }

                _configuration.Add(_connectionstrings);
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
            if (!disposedValue)
                _Folder = null;
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
}

#endif