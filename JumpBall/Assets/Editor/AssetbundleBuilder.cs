using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

public class AssetbundleBuilder
{
    private static string AssetRoot = Application.dataPath.Replace('\\', '/');

    [MenuItem("Tools/资源打包")]
    public static void BuildAssetbundles()
    {
        ClearStreamingAssets();
        AssetBundleBuild[] buildList = Collect();
        string outputPath = Application.streamingAssetsPath + "/" + GameGlobal.ASSET_ROOT_NAME;
        if (!Directory.Exists(outputPath))
        {
            Directory.CreateDirectory(outputPath);
        }

        BuildPipeline.BuildAssetBundles(outputPath, buildList, BuildAssetBundleOptions.ChunkBasedCompression, EditorUserBuildSettings.activeBuildTarget);
        BuildScenes();
        AssetDatabase.Refresh();
        EditorUtility.DisplayDialog("提示", "打包完成", "确定");
    }

    public static void ClearStreamingAssets()
    {
        string path = Application.dataPath + "/StreamingAssets";
        if (Directory.Exists(path))
        {
            Directory.Delete(path, true);
            AssetDatabase.Refresh();
        }
    }

    private static AssetBundleBuild[] Collect()
    {
        string rootDir = Application.dataPath + "/" + GameGlobal.ASSET_ROOT_NAME;
        string[] allDirs = Directory.GetDirectories(rootDir, "*", SearchOption.AllDirectories);
        List<AssetBundleBuild> buildList = new List<AssetBundleBuild>();
        string tempDir = null;
        foreach (string dir in allDirs)
        {
            tempDir = dir.Replace('\\', '/');
            if (FilterPath(tempDir))
            {
                continue;
            }

            if (tempDir.ToLower().Contains("/debug/") || tempDir.ToLower().EndsWith("debug"))
            {
                continue;
            }

            CollectMultiBuildTask(dir, ref buildList);
        }
        return buildList.ToArray();
    }

    private static bool FilterPath(string path)
    {
        if (path.EndsWith(".meta") || path.EndsWith(".svn") || path.EndsWith(".DS_Store") || path.EndsWith(".unity") || path.EndsWith(".vscode") ||
           path.Contains("/lua/") || path.Contains("/behaviac/") || path.EndsWith("atlas.conf"))
        {
            return true;
        }

        return false;
    }

    private static void CollectMultiBuildTask(string path, ref List<AssetBundleBuild> buildList)
    {
        string[] files = Directory.GetFiles(path, "*.*", SearchOption.TopDirectoryOnly);

        foreach (string file in files)
        {
            string tempFile = file.Replace('\\', '/');
            if (FilterPath(tempFile))
            {
                continue;
            }

            tempFile = tempFile.Replace(AssetRoot, "Assets");
            List<string> assetNames = new List<string>();
            assetNames.Add(tempFile);
            AssetBundleBuild buildTask = new AssetBundleBuild();
            buildTask.assetNames = assetNames.ToArray();
            string dir = Path.GetDirectoryName(tempFile);
            string name = Path.GetFileNameWithoutExtension(tempFile);
            buildTask.assetBundleName = dir.Replace('\\', '/').Replace("Assets/" + GameGlobal.ASSET_ROOT_NAME + "/", "") + "/" + name + GameGlobal.ASSETBUNDLE_SUFFIX;
            buildList.Add(buildTask);
        }
    }

    public static void BuildScenes()
    {
        string[] scenes = Directory.GetFiles(Application.dataPath + "/" + GameGlobal.ASSET_ROOT_NAME + "/scene", "*.unity", SearchOption.AllDirectories);
        string outputDir = Application.streamingAssetsPath + "/" + GameGlobal.ASSET_ROOT_NAME + "/scene";
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        foreach (string scenePath in scenes)
        {
            string tempPath = scenePath.Replace('\\', '/').Replace(Application.dataPath, "Assets");
            string outputPath = outputDir + "/" + Path.GetFileNameWithoutExtension(scenePath) + "" + GameGlobal.ASSETBUNDLE_SUFFIX;
            BuildReport report = BuildPipeline.BuildPlayer(new string[] { tempPath }, outputPath, EditorUserBuildSettings.activeBuildTarget, BuildOptions.BuildAdditionalStreamedScenes);
        }
    }
}
