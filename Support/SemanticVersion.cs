using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Platform.Support
{
#if PORTABLE

    namespace Core
    {
#endif
#if !PORTABLE

    [Serializable]
#endif

        public sealed class SemanticVersion : IComparable, IComparable<SemanticVersion>, IEquatable<SemanticVersion>
        {
            private const string SemanticVersionRegex = "^(?<Version>\\d+(\\s*\\.\\s*\\d+){0,3})(?<Release>-[a-z][0-9a-z-]*)?$";

            private const string StrictSemanticVersionRegex = "^(?<Version>\\d+(\\.\\d+){2})(?<Release>-[a-z][0-9a-z-]*)?$";

            private string _originalString;

            public static SemanticVersion VersionReplacementToken = SemanticVersion.CreateWithoutParse("$version$");

            public Version Version
            {
                get;
                private set;
            }

            public string SpecialVersion
            {
                get;
                private set;
            }

            private SemanticVersion()
            {
            }

            public SemanticVersion(string version) : this(SemanticVersion.Parse(version))
            {
            }

            public SemanticVersion(int major, int minor, int build, int revision) : this(new Version(major, minor, build, revision))
            {
            }

            public SemanticVersion(int major, int minor, int build, string specialVersion) : this(new Version(major, minor, build), specialVersion)
            {
            }

            public SemanticVersion(Version version) : this(version, string.Empty)
            {
            }

            public SemanticVersion(Version version, string specialVersion) : this(version, specialVersion, null)
            {
            }

            private SemanticVersion(Version version, string specialVersion, string originalString)
            {
                if (version == null)
                {
                    throw new ArgumentNullException("version");
                }
                if (specialVersion != null && specialVersion.Length > 20)
                {
                    throw new ArgumentException("Special version part cannot exceed 20 characters.", "specialVersion");
                }
                this.Version = SemanticVersion.NormalizeVersionValue(version);
                this.SpecialVersion = (specialVersion ?? string.Empty);
                this._originalString = (string.IsNullOrEmpty(originalString) ? (version.ToString() + ((!string.IsNullOrEmpty(specialVersion)) ? ("-" + specialVersion) : null)) : originalString);
            }

            internal SemanticVersion(SemanticVersion semVer)
            {
                this._originalString = semVer.ToString();
                this.Version = semVer.Version;
                this.SpecialVersion = semVer.SpecialVersion;
            }

            private static SemanticVersion CreateWithoutParse(string originalString)
            {
                return new SemanticVersion
                {
                    _originalString = originalString
                };
            }

            public int CompareTo(object obj)
            {
                if (obj == null)
                {
                    return 1;
                }
                SemanticVersion semanticVersion = obj as SemanticVersion;
                if (semanticVersion == null)
                {
                    throw new ArgumentException("Type to compare must be an instance of SemanticVersion.", "obj");
                }
                return this.CompareTo(semanticVersion);
            }

            public int CompareTo(SemanticVersion other)
            {
                if (other == null)
                {
                    return 1;
                }
                int num = this.Version.CompareTo(other.Version);
                if (num != 0)
                {
                    return num;
                }
                bool flag = string.IsNullOrEmpty(this.SpecialVersion);
                bool flag2 = string.IsNullOrEmpty(other.SpecialVersion);
                if (flag & flag2)
                {
                    return 0;
                }
                if (flag)
                {
                    return 1;
                }
                if (flag2)
                {
                    return -1;
                }
                return StringComparer.OrdinalIgnoreCase.Compare(this.SpecialVersion, other.SpecialVersion);
            }

            public bool Equals(SemanticVersion other)
            {
                return other != null && this.Version.Equals(other.Version) && this.SpecialVersion.Equals(other.SpecialVersion, StringComparison.OrdinalIgnoreCase);
            }

            public static SemanticVersion Parse(string version)
            {
                if (string.IsNullOrEmpty(version))
                {
                    throw new ArgumentException("Cannot be null or empty.", "version");
                }
                version = version.Trim();
                if ("$version$".Equals(version))
                {
                    return SemanticVersion.VersionReplacementToken;
                }
                SemanticVersion result;
                if (!SemanticVersion.TryParse(version, out result))
                {
                    throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, "'{0}' is not a valid version string.", new object[]
                    {
                    version
                    }), "version");
                }
                return result;
            }

            public static bool TryParse(string version, out SemanticVersion value)
            {
                return SemanticVersion.TryParseInternal(version, "^(?<Version>\\d+(\\s*\\.\\s*\\d+){0,3})(?<Release>-[a-z][0-9a-z-]*)?$", out value);
            }

            public static bool TryParseStrict(string version, out SemanticVersion value)
            {
                return SemanticVersion.TryParseInternal(version, "^(?<Version>\\d+(\\.\\d+){2})(?<Release>-[a-z][0-9a-z-]*)?$", out value);
            }

            private static bool TryParseInternal(string version, string regex, out SemanticVersion semVer)
            {
                semVer = null;
                if (string.IsNullOrEmpty(version))
                {
                    return false;
                }
#if PORTABLE
                Match match = new Regex(regex, RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture).Match(version.Trim());
#else
            Match match = new Regex(regex, RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled).Match(version.Trim());
#endif
                Version version2;
                if (!match.Success || !Version.TryParse(match.Groups["Version"].Value, out version2))
                {
                    return false;
                }
                semVer = new SemanticVersion(SemanticVersion.NormalizeVersionValue(version2), match.Groups["Release"].Value.TrimStart(new char[]
                {
                '-'
                }), version.Replace(" ", ""));
                return true;
            }

            public static SemanticVersion ParseOptionalVersion(string version)
            {
                SemanticVersion result;
                SemanticVersion.TryParse(version, out result);
                return result;
            }

            private static Version NormalizeVersionValue(Version version)
            {
                return new Version(version.Major, version.Minor, Math.Max(version.Build, 0), Math.Max(version.Revision, 0));
            }

            public static bool operator ==(SemanticVersion version1, SemanticVersion version2)
            {
                if (version1 == null)
                {
                    return version2 == null;
                }
                return version1.Equals(version2);
            }

            public static bool operator !=(SemanticVersion version1, SemanticVersion version2)
            {
                return !(version1 == version2);
            }

            public static bool operator <(SemanticVersion version1, SemanticVersion version2)
            {
                if (version1 == null)
                {
                    throw new ArgumentNullException("version1");
                }
                return version1.CompareTo(version2) < 0;
            }

            public static bool operator <=(SemanticVersion version1, SemanticVersion version2)
            {
                return version1 == version2 || version1 < version2;
            }

            public static bool operator >(SemanticVersion version1, SemanticVersion version2)
            {
                if (version1 == null)
                {
                    throw new ArgumentNullException("version1");
                }
                return version2 < version1;
            }

            public static bool operator >=(SemanticVersion version1, SemanticVersion version2)
            {
                return version1 == version2 || version1 > version2;
            }

            public override string ToString()
            {
                return this._originalString;
            }

            public override bool Equals(object obj)
            {
                SemanticVersion semanticVersion = obj as SemanticVersion;
                return semanticVersion != null && this.Equals(semanticVersion);
            }

            public override int GetHashCode()
            {
                if (!(this.Version == null))
                {
                    return this.Version.GetHashCode() * 3137 + ((this.SpecialVersion == null) ? 0 : this.SpecialVersion.GetHashCode());
                }
                return 0;
            }
        }

#if PORTABLE
    }

#endif
}