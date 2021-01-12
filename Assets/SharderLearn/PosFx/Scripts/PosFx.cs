using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode, RequireComponent(typeof(Camera))]
public class PosFx : MonoBehaviour
{
    public Shader Shader;

    [SerializeField] private Material material;
    public Material Material
    {
        get
        {
            if (material == null)
                material = GenerateMaterial(Shader);
            return material;
        }
    }
    protected Material GenerateMaterial(Shader shader)
    {
        if (shader == null || shader.isSupported == false)
            return null;

        var material = new Material(shader)
        {
            hideFlags = HideFlags.DontSave
        };
        return material ? material : null;
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (Material)
        {
            Graphics.Blit(source, destination, material);
        }
        else
        {
            Graphics.Blit(source, destination);
        }
    }
}
