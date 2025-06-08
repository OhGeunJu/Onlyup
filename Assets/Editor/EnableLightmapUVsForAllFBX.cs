using UnityEngine;
using UnityEditor;
using System.IO;

public class EnableLightmapUVsForAllFBX : MonoBehaviour
{
    [MenuItem("Tools/Lightmapping/Enable Generate Lightmap UVs (All FBX)")]
    static void EnableGenerateLightmapUVs()
    {
        string[] guids = AssetDatabase.FindAssets("t:Model", new[] { "Assets" });
        int count = 0;

        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            if (!path.EndsWith(".fbx", System.StringComparison.OrdinalIgnoreCase)) continue;

            ModelImporter importer = AssetImporter.GetAtPath(path) as ModelImporter;
            if (importer == null) continue;

            if (!importer.generateSecondaryUV)
            {
                importer.generateSecondaryUV = true;
                importer.SaveAndReimport();
                Debug.Log($" Enabled Lightmap UVs: {path}");
                count++;
            }
        }

        Debug.Log($" Lightmap UVs 활성화 완료. 총 적용된 모델 수: {count}");
    }
}
