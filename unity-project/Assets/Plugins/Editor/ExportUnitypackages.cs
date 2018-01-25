using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
public class ExportAsset 
{

    [MenuItem("Export/Export I2 Localization", priority = 100)]
    public static void ExportPackages()
    {
		string name = "Unity-NodeCanvas-I2Localization.unitypackage";

        string[] paths =
            {
                "Assets/NodeCanvas Integrations/I2 Localization"
            };
        AssetDatabase.ExportPackage(paths, name, /*ExportPackageOptions.IncludeDependencies |*/ ExportPackageOptions.Recurse | ExportPackageOptions.Interactive);
        Debug.Log(string.Format("Export {0} success in: {1}", name, Directory.GetCurrentDirectory()));
    }
}
