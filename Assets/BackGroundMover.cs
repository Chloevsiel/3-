using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(Image))]
public class BackGroundMover : MonoBehaviour
{
    private const float k_maxLength = 1f;
    private const string k_proName = "_MainTex";

    [SerializeField]
    private Vector2 m_offsetSpeed;

    private Material m_copiedMaterial;

    private void Start()
    {
        var image = GetComponent<Image>();
        m_copiedMaterial = image.material;

        Assert.IsNotNull(m_copiedMaterial);
    }

    private void Update()
    {
        if (Time.timeScale == 0f) 
        {
            return;
        }

    }
}
   