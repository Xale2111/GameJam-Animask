using UnityEngine;

public class Parallax : MonoBehaviour
{
    private static readonly int MainTex = Shader.PropertyToID("_MainTex");
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    private Material _material;
    private float _distance;
    
    [Range(0.0f, 0.5f)]
    public float speed = 0.2f;
    
    void Start()
    {
        _material = GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        _distance += Time.deltaTime * speed;
        _material.SetTextureOffset(MainTex, Vector2.right * _distance);
    }
}
