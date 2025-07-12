using UnityEngine;
using UnityEditor;
using System.IO;

namespace Model2Icon
{
    public class Model2IconWindow : EditorWindow
    {
        GameObject targetModel;
        GameObject previousModel;

        Camera renderCam;
        RenderTexture renderTexture;
        Texture2D previewTexture;

        Color backgroundColor = Color.clear;
        Color lastBackgroundColor;

        Vector3 rotation = new Vector3(30, -45, 0);
        Vector3 lastRotation;

        float padding = 1.2f;
        float lastPadding;

        int resolution = 512;
        int lastResolution;

        bool transparentBackground = true;
        bool lastTransparent;

        string fileName = "icon";

        enum Format { PNG, JPG }
        Format format = Format.PNG;

        readonly int[] resolutionOptions = { 32, 64, 128, 256, 512, 1024, 2048, 4096 };
        readonly Vector2 previewSize = new Vector2(256, 256);

        [MenuItem("Tools/Model2Icon")]
        public static void ShowWindow() => GetWindow<Model2IconWindow>("Model2Icon");

        void OnGUI()
        {
            GUILayout.Label("🎯 Model2Icon – 3D Icon Generator", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            targetModel = (GameObject)EditorGUILayout.ObjectField("Target Model", targetModel, typeof(GameObject), false);

            rotation = EditorGUILayout.Vector3Field("Rotation", rotation);
            padding = EditorGUILayout.Slider("Camera Padding", padding, 1.0f, 5.0f);
            backgroundColor = EditorGUILayout.ColorField("Background Color", backgroundColor);
            transparentBackground = EditorGUILayout.Toggle("Transparent Background", transparentBackground);

            resolution = resolutionOptions[EditorGUILayout.Popup("Output Resolution",
                System.Array.IndexOf(resolutionOptions, resolution),
                System.Array.ConvertAll(resolutionOptions, x => x + "x" + x))];

            format = (Format)EditorGUILayout.EnumPopup("Image Format", format);
            fileName = EditorGUILayout.TextField("File Name", fileName);

            if (targetModel != null && previewTexture != null)
            {
                EditorGUILayout.Space();
                GUILayout.Label("Preview", EditorStyles.boldLabel);

                Rect previewRect = GUILayoutUtility.GetRect(previewSize.x, previewSize.y, GUILayout.ExpandWidth(true));

                // Center the preview texture horizontally
                float xOffset = (previewRect.width - previewSize.x) / 2;
                Rect centeredRect = new Rect(previewRect.x + xOffset, previewRect.y, previewSize.x, previewSize.y);

                GUI.DrawTexture(centeredRect, previewTexture, ScaleMode.ScaleToFit, true);
            }

            EditorGUILayout.Space();
            if (GUILayout.Button("💾 Save Icon"))
            {
                string path = EditorUtility.SaveFilePanel("Save Icon", "Assets", fileName, format.ToString().ToLower());
                if (!string.IsNullOrEmpty(path))
                    SaveIcon(path);
            }
        }

        void OnInspectorUpdate()
        {
            if (targetModel == null)
            {
                if (previewTexture != null)
                {
                    DestroyImmediate(previewTexture);
                    previewTexture = null;
                }
                return;
            }

            // Detect changes and regenerate preview if needed
            if (targetModel != previousModel ||
                rotation != lastRotation ||
                padding != lastPadding ||
                backgroundColor != lastBackgroundColor ||
                resolution != lastResolution ||
                transparentBackground != lastTransparent)
            {
                GeneratePreview();

                previousModel = targetModel;
                lastRotation = rotation;
                lastPadding = padding;
                lastBackgroundColor = backgroundColor;
                lastResolution = resolution;
                lastTransparent = transparentBackground;
            }

            Repaint(); // Continuously refresh GUI
        }

        void GeneratePreview()
        {
            if (!targetModel) return;

            if (previewTexture != null)
                DestroyImmediate(previewTexture);

            GameObject instance = Instantiate(targetModel);
            instance.hideFlags = HideFlags.HideAndDontSave;
            Bounds bounds = CalculateBounds(instance);

            GameObject camObj = new GameObject("IconCamera");
            camObj.hideFlags = HideFlags.HideAndDontSave;
            renderCam = camObj.AddComponent<Camera>();
            renderCam.backgroundColor = backgroundColor;
            renderCam.clearFlags = CameraClearFlags.SolidColor;
            renderCam.orthographic = true;
            renderCam.orthographicSize = Mathf.Max(bounds.size.x, bounds.size.y, bounds.size.z) * padding * 0.5f;
            renderCam.transform.position = bounds.center - Quaternion.Euler(rotation) * Vector3.forward * 10;
            renderCam.transform.rotation = Quaternion.Euler(rotation);

            renderTexture = new RenderTexture(resolution, resolution, 24);
            renderCam.targetTexture = renderTexture;
            renderCam.clearFlags = transparentBackground ? CameraClearFlags.SolidColor : CameraClearFlags.Color;

            renderCam.Render();

            RenderTexture.active = renderTexture;
            previewTexture = new Texture2D(resolution, resolution, TextureFormat.RGBA32, false);
            previewTexture.ReadPixels(new Rect(0, 0, resolution, resolution), 0, 0);
            previewTexture.Apply();

            RenderTexture.active = null;

            DestroyImmediate(camObj);
            DestroyImmediate(instance);
        }

        void SaveIcon(string path)
        {
            if (previewTexture == null)
            {
                Debug.LogWarning("Preview not generated. Generating before saving...");
                GeneratePreview();
            }

            byte[] bytes = format == Format.PNG
                ? previewTexture.EncodeToPNG()
                : previewTexture.EncodeToJPG();

            File.WriteAllBytes(path, bytes);
            Debug.Log($"Icon saved at: {path}");

            AssetDatabase.Refresh();

            string assetPath = "Assets" + path.Substring(Application.dataPath.Length);
            TextureImporter importer = AssetImporter.GetAtPath(assetPath) as TextureImporter;
            if (importer != null)
            {
                importer.textureType = TextureImporterType.Sprite;
                importer.alphaIsTransparency = true;
                importer.SaveAndReimport();
            }
        }

        Bounds CalculateBounds(GameObject obj)
        {
            Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();
            if (renderers.Length == 0)
                return new Bounds(obj.transform.position, Vector3.one);

            Bounds bounds = renderers[0].bounds;
            foreach (Renderer r in renderers)
                bounds.Encapsulate(r.bounds);
            return bounds;
        }
    }
}
