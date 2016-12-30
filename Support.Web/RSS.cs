using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Platform.Support
{

#if PORTABLE
    namespace Core
    {
#endif

    namespace Web
    {

        public abstract class RSS
        {

            public class Channel
            {
                public string Title { get; set; }
                public string Link { get; set; }
                public string Description { get; set; }

                public string Copyright { get; set; }

                public string Language { get; set; }

                public string ImageURL { get; set; }

                public IEnumerable<Item> Items { get; set; }
            }
            public class Item
            {
                public string Title { get; set; }
                public string Link { get; set; }
                public string Description { get; set; }
                public string Guid { get; set; }

                public DateTime PubDate { get; set; }

                public string Enclosure { get; set; }
            }

            public static IEnumerable<Channel> GetChannel(string url)
            {
                XDocument _xdoc = XDocument.Load(url);
                return GetChannel(_xdoc);
            }

            public static IEnumerable<Channel> GetChannel(XDocument xdoc)
            {

                IEnumerable<Channel> _return = from channels in xdoc.Descendants("channel")
                                               select new Channel
                                               {
                                                   Title = channels.Element("title").Value,
                                                   Link = channels.Element("link").Value,
                                                   Description = channels.Element("description").Value,
                                                   Copyright = channels.Element("copyright").Value,
                                                   Language = channels.Element("language").Value,
                                                   ImageURL = channels.Element("image").Element("url").Value,
                                                   Items = from items in channels.Descendants("item")
                                                           select new Item()
                                                           {
                                                               Title = items.Element("title").Value,
                                                               Link = items.Element("link").Value,
                                                               Description = items.Element("description").Value,
                                                               PubDate = GetDate(items.Element("pubDate").Value),
                                                               Guid = items.Element("guid").Value,
                                                               Enclosure = items.Element("enclosure").Attribute("url").Value
                                                           }
                                               };


                return _return;

            }

            public static DateTime GetDate(string v)
            {
                DateTime _value;
                DateTime.TryParse(v, out _value);
                return _value;
            }

        }
    }

#if PORTABLE
    }
#endif

}
