using System.Linq;
using UnityEngine;

public class Stand : MonoBehaviour
{
    [SerializeField] private MeshRenderer  _renderer;
    [SerializeField] private Texture2D     _hole;
    [SerializeField] private RenderTexture _rt;

    [Space]
    [SerializeField] private int _resolution = 1024;

    private Texture2D _texture;

    private void Awake()
    {
        RenderTexture.active = _rt;
        GL.Clear(false, true, Color.white);
        RenderTexture.active = null;
    }


    public void DrawHit(RaycastHit hit)
    {
        RenderTexture.active = _rt;
        GL.PushMatrix();
        GL.LoadPixelMatrix(0, _resolution, _resolution, 0);

        var uv = hit.textureCoord2;
        uv.y = 1 - uv.y;
        var x  = (int)(uv.x * _resolution) - _hole.width / 2;
        var y  = (int)(uv.y * _resolution) - _hole.height / 2;

        Graphics.DrawTexture(new Rect(x, y, _hole.width, _hole.height), _hole);
        GL.PopMatrix();
        RenderTexture.active = null;
    }
}