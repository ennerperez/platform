using Platform.Support.Drawing.Icons.EncodingFormats;
using Platform.Support.Drawing.Icons.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Platform.Support.Drawing.Icons
{
    public class MultiIcon : List<SingleIcon>
    {
        public MultiIcon()
        {
        }

        public MultiIcon(IEnumerable<SingleIcon> collection)
        {
            base.AddRange(collection);
        }

        public MultiIcon(SingleIcon singleIcon)
        {
            base.Add(singleIcon);
            this.SelectedName = singleIcon.Name;
        }

        public int SelectedIndex
        {
            get
            {
                return this.mSelectedIndex;
            }
            set
            {
                if (value >= base.Count)
                {
                    throw new ArgumentOutOfRangeException("SelectedIndex");
                }
                this.mSelectedIndex = value;
            }
        }

        public string SelectedName
        {
            get
            {
                if (this.mSelectedIndex < 0 || this.mSelectedIndex >= base.Count)
                {
                    return null;
                }
                return base[this.mSelectedIndex].Name;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("SelectedName");
                }
                for (int i = 0; i < base.Count; i++)
                {
                    if (base[i].Name.ToLower() == value.ToLower())
                    {
                        this.mSelectedIndex = i;
                        return;
                    }
                }
                throw new InvalidDataException("SelectedName does not exist.");
            }
        }

        public string[] IconNames
        {
            get
            {
                List<string> list = new List<string>();
                foreach (SingleIcon singleIcon in this)
                {
                    list.Add(singleIcon.Name);
                }
                return list.ToArray();
            }
        }

        public SingleIcon this[string name]
        {
            get
            {
                for (int i = 0; i < base.Count; i++)
                {
                    if (base[i].Name.ToLower() == name.ToLower())
                    {
                        return base[i];
                    }
                }
                return null;
            }
        }

        public SingleIcon Add(string iconName)
        {
            if (this.Contains(iconName))
            {
                throw new IconNameAlreadyExistException();
            }
            SingleIcon singleIcon = new SingleIcon(iconName);
            base.Add(singleIcon);
            return singleIcon;
        }

        public void Remove(string iconName)
        {
            if (iconName == null)
            {
                throw new ArgumentNullException("iconName");
            }
            int num = this.IndexOf(iconName);
            if (num == -1)
            {
                return;
            }
            base.RemoveAt(num);
        }

        public bool Contains(string iconName)
        {
            if (iconName == null)
            {
                throw new ArgumentNullException("iconName");
            }
            return this.IndexOf(iconName) != -1;
        }

        public int IndexOf(string iconName)
        {
            if (iconName == null)
            {
                throw new ArgumentNullException("iconName");
            }
            for (int i = 0; i < base.Count; i++)
            {
                if (base[i].Name.ToLower() == iconName.ToLower())
                {
                    return i;
                }
            }
            return -1;
        }

        public void Load(string fileName)
        {
            FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            try
            {
                this.Load(fileStream);
            }
            finally
            {
                if (fileStream != null)
                {
                    fileStream.Close();
                }
            }
        }

        public void Load(Stream stream)
        {
            ILibraryFormat libraryFormat;
            if ((libraryFormat = new IconFormat()).IsRecognizedFormat(stream))
            {
                if (this.mSelectedIndex == -1)
                {
                    base.Clear();
                    base.Add(libraryFormat.Load(stream)[0]);
                    base[0].Name = "Untitled";
                }
                else
                {
                    string name = base[this.mSelectedIndex].Name;
                    base[this.mSelectedIndex] = libraryFormat.Load(stream)[0];
                    base[this.mSelectedIndex].Name = name;
                }
            }
            else if ((libraryFormat = new NEFormat()).IsRecognizedFormat(stream))
            {
                this.CopyFrom(libraryFormat.Load(stream));
            }
            else
            {
                if (!(libraryFormat = new PEFormat()).IsRecognizedFormat(stream))
                {
                    throw new InvalidFileException();
                }
                this.CopyFrom(libraryFormat.Load(stream));
            }
            this.SelectedIndex = ((base.Count > 0) ? 0 : -1);
        }

        public void Save(string fileName, MultiIconFormat format)
        {
            FileStream fileStream = new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite);
            try
            {
                this.Save(fileStream, format);
            }
            finally
            {
                if (fileStream != null)
                {
                    fileStream.Close();
                }
            }
        }

        public void Save(Stream stream, MultiIconFormat format)
        {
            switch (format)
            {
                case MultiIconFormat.ICO:
                    if (this.mSelectedIndex == -1)
                    {
                        throw new InvalidIconSelectionException();
                    }
                    new IconFormat().Save(this, stream);
                    return;

                case MultiIconFormat.ICL:
                    new NEFormat().Save(this, stream);
                    return;

                case MultiIconFormat.DLL:
                    new PEFormat().Save(this, stream);
                    return;

                case MultiIconFormat.EXE:
                case MultiIconFormat.OCX:
                case MultiIconFormat.CPL:
                case MultiIconFormat.SRC:
                    throw new NotSupportedException("File format not supported");
                default:
                    throw new NotSupportedException("Unknow file type destination, Icons can't be saved");
            }
        }

        private void CopyFrom(MultiIcon multiIcon)
        {
            this.mSelectedIndex = multiIcon.mSelectedIndex;
            base.Clear();
            base.AddRange(multiIcon);
        }

        private int mSelectedIndex = -1;
    }
}