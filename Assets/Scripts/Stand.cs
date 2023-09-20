using System.Linq;
using UnityEngine;

public class Stand : MonoBehaviour
{
    [SerializeField] private MeshRenderer _renderer;
    [SerializeField] private Texture2D    _hole;

    [Space]
    [SerializeField] private int _resolution = 1024;

    private Texture2D _texture;

    private void Awake()
    {
        _texture = new Texture2D(_resolution, _resolution);
        _texture.SetPixels(Enumerable.Repeat(Color.white, _resolution * _resolution).ToArray());
        _texture.Apply();
        
        _renderer.material.mainTexture = _texture;
    }

    public void DrawHit(RaycastHit hit)
    {
        var uv = hit.textureCoord2;
        var x  = (int)(uv.x * _resolution) - _hole.width / 2;
        var y  = (int)(uv.y * _resolution) - _hole.height / 2;

        for (var i = 0; i < _hole.width; i++)
        for (var j = 0; j < _hole.height; j++)
        {
            var localX = x + i;
            var localY = y + j;

            if (localX >= 0 && localY >= 0 && localX < _resolution && localY < _resolution)
            {
                var pixel = _hole.GetPixel(i, j);
                if (pixel.r < 0.5f) // draw only black pixels
                {
                    _texture.SetPixel(localX, localY, pixel);
                }
            }
        }

        _texture.Apply();
    }
}