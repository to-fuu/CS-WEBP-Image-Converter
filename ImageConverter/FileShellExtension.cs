using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace ImageConverter
{
    public static class FileShellExtension
    {
        public static void Register(string fileType,
          string shellKeyName, string menuText, string menuCommand)
        {
            Debug.WriteLine("REGGGG " +Registry.ClassesRoot.GetSubKeyNames());
            // create path to registry location
            string regPath = string.Format(@"{0}\Shell\{1}",
                                           fileType, shellKeyName);

            // add context menu to the registry
            using (RegistryKey key =
                   Registry.ClassesRoot.CreateSubKey(regPath))
            {
                key.SetValue(null, menuText);
            }

            // add command that is invoked to the registry
            using (RegistryKey key = Registry.ClassesRoot.CreateSubKey(
                string.Format(@"{0}\command", regPath)))
            {
                key.SetValue(null, menuCommand);
            }
        }

        public static void Unregister(string fileType, string shellKeyName)
        {
            Debug.Assert(!string.IsNullOrEmpty(fileType) &&
                !string.IsNullOrEmpty(shellKeyName));

            // path to the registry location
            string regPath = string.Format(@"{0}\shell\{1}",
                                           fileType, shellKeyName);

            // remove context menu from the registry
            Registry.ClassesRoot.OpenSubKey("SystemFileAssociations").DeleteSubKeyTree(regPath);
        }
    }
}
