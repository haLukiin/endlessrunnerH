using UnityEngine;
using UnityEditor;
using System.IO;

/// <summary>
/// Editor utility that creates a white square sprite asset and assigns it to all
/// SpriteRenderers in the project that have no sprite set.
/// Run via menu: Tools > Setup White Sprite
/// </summary>
public static class WhiteSpriteSetup
{
    private const string SpritePath = "Assets/Sprites/WhiteSquare.png";
    private const string SpriteDir = "Assets/Sprites";

    [MenuItem("Tools/Setup White Sprite")]
    public static void SetupWhiteSprite()
    {
        Sprite sprite = GetOrCreateWhiteSprite();
        AssignSpriteToPrefab(sprite);
        AssignSpriteToSceneObjects(sprite);
        AssetDatabase.SaveAssets();
        Debug.Log("[WhiteSpriteSetup] Done. White sprite assigned to all empty SpriteRenderers.");
    }

    private static Sprite GetOrCreateWhiteSprite()
    {
        // Check if it already exists
        Sprite existing = AssetDatabase.LoadAssetAtPath<Sprite>(SpritePath);
        if (existing != null) return existing;

        // Create a 32x32 white texture
        if (!Directory.Exists(SpriteDir))
            Directory.CreateDirectory(SpriteDir);

        Texture2D tex = new Texture2D(32, 32, TextureFormat.RGBA32, false);
        Color[] pixels = new Color[32 * 32];
        for (int i = 0; i < pixels.Length; i++)
            pixels[i] = Color.white;
        tex.SetPixels(pixels);
        tex.Apply();

        File.WriteAllBytes(SpritePath, tex.EncodeToPNG());
        AssetDatabase.ImportAsset(SpritePath);

        // Configure as sprite
        TextureImporter importer = AssetImporter.GetAtPath(SpritePath) as TextureImporter;
        if (importer != null)
        {
            importer.textureType = TextureImporterType.Sprite;
            importer.spriteImportMode = SpriteImportMode.Single;
            importer.filterMode = FilterMode.Point;
            importer.mipmapEnabled = false;
            importer.SaveAndReimport();
        }

        return AssetDatabase.LoadAssetAtPath<Sprite>(SpritePath);
    }

    private static void AssignSpriteToPrefab(Sprite sprite)
    {
        string prefabPath = "Assets/Prefabs/ObstaclePair.prefab";
        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
        if (prefab == null) return;

        bool changed = false;
        foreach (SpriteRenderer sr in prefab.GetComponentsInChildren<SpriteRenderer>(true))
        {
            if (sr.sprite == null)
            {
                sr.sprite = sprite;
                changed = true;
            }
        }

        if (changed)
            PrefabUtility.SavePrefabAsset(prefab);
    }

    private static void AssignSpriteToSceneObjects(Sprite sprite)
    {
        foreach (SpriteRenderer sr in Object.FindObjectsByType<SpriteRenderer>(FindObjectsSortMode.None))
        {
            if (sr.sprite == null)
            {
                sr.sprite = sprite;
                EditorUtility.SetDirty(sr);
            }
        }
    }
}
