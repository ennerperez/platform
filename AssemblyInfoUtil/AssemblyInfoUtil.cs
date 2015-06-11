using System;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;

/// <summary>
/// Summary description for Class1.
/// </summary>
public class AssemblyInfoUtil
{

    private enum Lang
    {
        none, vb, cs, fs
    };

    #region properties

    static string assemblyInfo { get; set; }

    const string cmd_increment = "/c";
    static string cmd_increment_help = "<parameter index>  - increases the specified index (1 to 4)";
    static bool has_increment { get; set; }
    static int increment { get; set; }

    const string cmd_changelog = "/l";
    static string cmd_changelog_help = "<changelog.md file> - get info from changelog.md file";
    static bool has_changelog { get; set; }
    static string changelog { get; set; }

    const string cmd_version = "/v";
    static string cmd_version_help = "<new version number> - set new version number (in NN.NN.NN.NN format)";
    static bool has_version { get; set; }
    static string version { get; set; }
    static string date { get; set; }

    const string cmd_info = "/i";
    static string cmd_info_help = "<information> - set assembly informational version";
    static bool has_info { get; set; }
    static string info { get; set; }

    #endregion

    static Lang lang
    {
        get
        {
            if (!string.IsNullOrEmpty(assemblyInfo))
            {
                try
                {
                    return (Lang)Enum.Parse(typeof(Lang), Path.GetExtension(assemblyInfo).ToLower().Substring(".".Length));
                }
                catch (Exception)
                {
                    return Lang.none;
                }
            }
            return Lang.none;
        }
    }

    static void showHelp()
    {
        System.Console.WriteLine("  ");
        System.Console.WriteLine("Usage: " + Process.GetCurrentProcess().ProcessName + " <path to AssemblyInfo file> [options]");
        System.Console.WriteLine("  ");
        System.Console.WriteLine("Options: ");
        System.Console.WriteLine("  " + cmd_increment + ":" + cmd_increment_help);
        System.Console.WriteLine("  " + cmd_changelog + ":" + cmd_changelog_help);
        System.Console.WriteLine("  " + cmd_version + ":" + cmd_version_help);
        System.Console.WriteLine("  " + cmd_info + ":" + cmd_info_help);
        System.Console.WriteLine("  ");
#if DEBUG
        System.Console.ReadKey();
#endif
    }

    static int showError(string error)
    {
        System.Console.WriteLine("[Error] " + error);
#if DEBUG
        System.Console.ReadKey();
#endif
        return -1;
    }

    static bool isValidVersion(string ver)
    {
        try
        {
            Version _version = new Version(ver);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    static int readFromChangeLog(string file)
    {
        System.Console.WriteLine("Reading \"" + file + "\"...");
        StreamReader reader = new StreamReader(file);

        String line;

        while ((line = reader.ReadLine()) != null)
        {
            line = line.Trim();
            var version_match = Regex.Matches(line, "\\d+(?:\\.\\d+)+");
            if (version_match.Count > 0)
            {
                var _version = version_match[0].Value;
                if (isValidVersion(_version))
                {
                    version = _version;
                    var date_match = Regex.Matches(line, "\\d{4}$|\\d{4}-((0?\\d)|(1[012]))-(((0?|[12])\\d)|3[01])+");
                    if (date_match.Count > 0)
                    {
                        date = date_match[0].Value;
                    }
                }
                else
                {
                    Console.WriteLine("[!] Invalid version format. Assuming default version [1.0.0.0]");
                    _version = "1.0.0.0";
                }

                List<string> infoList = new List<string>();
                if (!string.IsNullOrEmpty(date)) infoList.Add(date);
                if (!string.IsNullOrEmpty(info) && !infoList.Contains(info)) infoList.Add(info);

                var info_match = Regex.Matches(line, "\\[(.*?)\\]");
                foreach (var item in info_match)
                {
                    string _info = item.ToString().Replace("[", "").Replace("]", "");
                    if (!infoList.Contains(_info) && _info != version) infoList.Add(_info);
                }

                if (infoList.Count > 0)
                {
                    info = string.Join(", ", infoList.ToArray());
                }

            }

            if (!string.IsNullOrEmpty(version) || !string.IsNullOrEmpty(date) || !string.IsNullOrEmpty(info))
            {
                break;
            }

        }

        reader.Close();

        return 0;
    }

    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static int Main(string[] args)
    {

        if (args == null || args.Length <= 1)
        {
            showError("Not enough arguments.");
            showHelp();
            return -1;
        }

        // Reading options
        foreach (string item in args)
        {
            string litem = item.ToLower();

            // AssemblyVersion
            if (!has_version)
            {
                has_version = litem.StartsWith(cmd_version + ":");
                if (has_version) version = item.Substring((cmd_version + ":" as string).Length);
            }

            // AssemblyInformationalVersion
            if (!has_info)
            {
                has_info = litem.StartsWith(cmd_info + ":");
                if (has_info) info = item.Substring((cmd_info + ":" as string).Length);
            }

            // AssemblyVersion Increment
            if (!has_increment)
            {
                has_increment = litem.StartsWith(cmd_increment + ":");
                if (has_increment)
                {
                    int _incement;
                    int.TryParse(item.Substring((cmd_increment + ":" as string).Length), out _incement);
                    if (_incement > 0) increment = _incement;
                }
            }

            // Changelog file
            if (!has_changelog)
            {
                has_changelog = litem.StartsWith(cmd_changelog + ":");
                if (has_changelog) changelog = item.Substring((cmd_changelog + ":" as string).Length);
            }

        }

        if (!has_version && !has_increment && !has_changelog)
        {
            showError("Not enough arguments.");
            showHelp();
            return -1;
        }

        // AssemblyInfo
        assemblyInfo = args[0];
        if (!System.IO.File.Exists(assemblyInfo))
        {
            return showError("Can not find file \"" + assemblyInfo + "\"");
        }

        // Changelog
        if (has_changelog && !System.IO.File.Exists(changelog))
        {
            return showError("Can not find file \"" + changelog + "\"");
        }

        // Compatibility
        if (has_changelog && (has_version || has_increment))
        {
            return showError("Can't use " + cmd_changelog + " option with " + cmd_version + " or " + cmd_increment + ".");
        }

        if (has_version && has_increment)
        {
            return showError("Can't use " + cmd_increment + " option with " + cmd_version + ".");
        }

        // Increment value
        if (has_increment && (increment > 4 || increment < 0))
        {
            return showError("Can't use " + cmd_increment + " option with " + increment + ".");
        }

        // Version + Increment values
        if (has_increment)
        {
            version = getCurentVersion();
            if (isValidVersion(version) || version.Split('.').Length < (increment - 1))
            {
                return showError("The current version \"" + version + "\" does not have the indicated position.");
            }
            else if (!isValidVersion(version))
            {
                return showError("Can't use " + cmd_increment + " option with an invalid version format.");
            }

        }

        // Version value
        if (has_version && !isValidVersion(version))
        {
            return showError("Can't use " + cmd_version + " option with an invalid version format.");
        }
        

        // Processing
        if (has_changelog) readFromChangeLog(changelog);

        // Default values
        if (string.IsNullOrEmpty(info)) info = "";
        if (string.IsNullOrEmpty(version)) version = "1.0.0.0";


        return processAssemblyInfo(assemblyInfo);

    }

    static int processAssemblyInfo(string file)
    {
        System.Console.WriteLine("Processing \"" + assemblyInfo + "\"...");

        StreamReader reader = new StreamReader(assemblyInfo);
        StreamWriter writer = new StreamWriter(assemblyInfo + ".out");
        String line;

        while ((line = reader.ReadLine()) != null)
        {
            line = ProcessLine(line);
            writer.WriteLine(line);
        }
        reader.Close();
        writer.Close();

        File.Delete(assemblyInfo);
        File.Move(assemblyInfo + ".out", assemblyInfo);

        Console.WriteLine("Version updated: " + version + (!string.IsNullOrEmpty(info) ? " [" + info + "]" : ""));


#if DEBUG
        System.Console.ReadKey();
#endif

        return 0;

    }


    static string getCurentVersion()
    {
        string _return = null;
        StreamReader reader = new StreamReader(assemblyInfo);
        String line;

        string part = null;
        switch (lang)
        {
            case Lang.cs:
                part = "[assembly: AssemblyVersion(\"";
                break;
            case Lang.fs:
                part =  "<[assembly: AssemblyVersion(\"";
                break;
            default:
                part =  "<Assembly: AssemblyVersion(\"";
                break;
        }

        while ((line = reader.ReadLine()) != null)
        {
            int spos = line.IndexOf(part);

            string cmtChars = null;
            switch (lang)
            {
                case Lang.vb:
                    cmtChars = "'";
                    break;
                default:
                    cmtChars = "//";
                    break;
            }

            if (spos >= 0 && !line.Trim().StartsWith(cmtChars))
            {
                spos += part.Length;
                int epos = line.IndexOf('"', spos);
                _return = line.Substring(spos, epos - spos);
                break;
            }
 
        }
        reader.Close();
        
        return _return;
    }


    static string ProcessLine(string line)
    {
        switch (lang)
        {
            case Lang.cs:
                line = ProcessLinePart(line, "[assembly: AssemblyVersion(\"");
                line = ProcessLinePart(line, "[assembly: AssemblyFileVersion(\"");
                line = ProcessLinePart(line, "[assembly: AssemblyInformationalVersion(\"", info);
                break;
            case Lang.fs:
                line = ProcessLinePart(line, "<[assembly: AssemblyVersion(\"");
                line = ProcessLinePart(line, "<[assembly: AssemblyFileVersion(\"");
                line = ProcessLinePart(line, "<[assembly: AssemblyInformationalVersion(\"", info);
                break;
            default:
                line = ProcessLinePart(line, "<Assembly: AssemblyVersion(\"");
                line = ProcessLinePart(line, "<Assembly: AssemblyFileVersion(\"");
                line = ProcessLinePart(line, "<assembly: AssemblyInformationalVersion(\"", info);
                break;
        }

        return line;
    }


    static string ProcessLinePart(string line, string part, string data = null)
    {

        int spos = line.IndexOf(part);

        string cmtChars = null;
        switch (lang)
        {
            case Lang.vb:
                cmtChars = "'";
                break;
            default:
                cmtChars = "//";
                break;
        }

        if (spos >= 0 && !line.Trim().StartsWith(cmtChars))
        {
            spos += part.Length;
            int epos = line.IndexOf('"', spos);
            string oldVersion = line.Substring(spos, epos - spos);
            string newVersion = "";
            bool performChange = false;

            if (has_increment)
            {
                string[] nums = oldVersion.Split('.');
                if (nums.Length >= increment && nums[increment - 1] != "*")
                {
                    Int64 val = Int64.Parse(nums[increment - 1]);
                    val++;
                    nums[increment - 1] = val.ToString();
                    newVersion = nums[0];
                    for (int i = 1; i < nums.Length; i++)
                    {
                        newVersion += "." + nums[i];
                    }
                    performChange = true;
                }
            }
            else if (!string.IsNullOrEmpty(version))
            {

                newVersion = version;
                performChange = true;
            }

            if (performChange)
            {
                StringBuilder str = new StringBuilder(line);
                str.Remove(spos, epos - spos);
                if (data != null)
                {
                    str.Insert(spos, data);
                }
                else
                {
                    str.Insert(spos, newVersion);
                    version = newVersion;
                }
                line = str.ToString();
            }
        }
        return line;
    }



}
