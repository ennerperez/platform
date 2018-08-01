using Platform.Support.Reflection;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace Platform.Support.Drawing.Colors
{
    public enum Palettes : short
    {
        [Description("Rosado")]
        pink,

        [Description("Púrpura")]
        purple,

        [Description("Morado oscuro")]
        deep_purple,

        [Description("Índigo")]
        indigo,

        [Description("Azul")]
        blue,

        [Description("Azul claro")]
        light_blue,

        [Description("Cían")]
        cyan,

        [Description("Verde azulado")]
        teal,

        [Description("Verde")]
        green,

        [Description("Verde claro")]
        light_green,

        [Description("Lima")]
        lime,

        [Description("Amarillo")]
        yellow,

        [Description("Ámbar")]
        amber,

        [Description("Naranja")]
        orange,

        [Description("Naranja profundo")]
        deep_orange,

        [Description("Marrón")]
        brown,

        [Description("Gris")]
        grey,

        [Description("Gris azulado")]
        blue_grey,
    }

    public class Palette : INotifyPropertyChanged
    {
        private Color primary;
        private Color primary_dark;
        private Color primary_light;
        private Color accent;
        private Color primary_text;
        private Color secondary_text;
        private Color icons;
        private Color divider;

        public Color Primary { get { return primary; } set { this.SetField(ref primary, Primary, "Primary"); } }
        public Color PrimaryDark { get { return primary_dark; } set { this.SetField(ref primary_dark, value, "PrimaryDark"); } }
        public Color PrimaryLight { get { return primary_light; } set { this.SetField(ref primary_light, value, "PrimaryLight"); } }
        public Color Accent { get { return accent; } set { this.SetField(ref accent, value, "Accent"); } }
        public Color PrimaryText { get { return primary_text; } set { this.SetField(ref primary_text, value, "PrimaryText"); } }
        public Color SecondaryText { get { return secondary_text; } set { this.SetField(ref secondary_text, value, "SecondaryText"); } }
        public Color Icons { get { return icons; } set { this.SetField(ref icons, value, "Icons"); } }
        public Color Divider { get { return divider; } set { this.SetField(ref divider, value, "Divider"); } }

        public Palette()
        {
        }

        public Palette(string name) : this()
        {
            if (Directory.Exists("palettes"))
            {
                var filename = Path.Combine("palettes", name + ".xml");
                if (File.Exists(filename))
                {
                    var readed = ReadFrom(filename);
                    primary = readed.primary;
                    primary_dark = readed.primary_dark;
                    primary_light = readed.primary_light;
                    primary_text = readed.primary_text;
                    secondary_text = readed.secondary_text;
                    icons = readed.icons;
                    divider = readed.divider;
                }
                else
                    throw new FileNotFoundException();
            }
            else
                throw new DirectoryNotFoundException();
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }

        #endregion INotifyPropertyChanged

        public static Palette ReadFrom(MemoryStream stream)
        {
            Palette result = new Palette();
            ReadFrom(stream, out result);
            return result;
        }

        public static Palette ReadFrom(byte[] buffer)
        {
            return ReadFrom(new MemoryStream(buffer));
        }

        public static Palette ReadFrom(string path)
        {
            return ReadFrom(File.ReadAllBytes(path));
        }

        public static void ReadFrom(MemoryStream stream, out Palette target)
        {
            try
            {
                Palette result = new Palette();
                XDocument xdoc = XDocument.Load(stream);
                var colors = (from XElement item in xdoc.Descendants("color")
                              select new string[] { item.Attribute("name").Value.ToString(), item.Value }).ToDictionary(key => key[0], value => value[1]);

                foreach (var item in result.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance).Where((f) => f.MemberType == MemberTypes.Field))
                {
                    if (colors.ContainsKey(item.Name))
                    {
                        var hex = colors[item.Name];
                        var color = ColorTranslator.FromHtml(hex);
                        item.SetValue(result, color);
                    }
                }

                target = result;
            }
            catch (Exception ex)
            {
                throw new InvalidDataException(ex.Message, ex);
            }
        }

        public static void ReadFrom(byte[] buffer, out Palette target)
        {
            ReadFrom(new MemoryStream(buffer), out target);
        }

        public static void ReadFrom(string path, out Palette target)
        {
            ReadFrom(File.ReadAllBytes(path), out target);
        }
    }
}