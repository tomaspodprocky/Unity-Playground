using UnityEngine;
using UnityEngine.UI;

// for this to work properly and you are using the new Input system,
// you must enable Both in Active Input Handling of the player settings.
public class SetCursor : MonoBehaviour
{
    [SerializeField] float scale = 1;
    [SerializeField] bool isSoftware = true;
    [SerializeField] Vector2Int hotSpot;

    private Vector2Int resizedHotspot;
    private Texture2D defaultPointerTexture;
    private Texture2D currentPointerTexture;
    // Update is called once per frame

    void Start()
    {
        resizedHotspot = hotSpot;
    }

    public void ResizeCursor(float scale) 
    {
        this.scale = scale;
        resizedHotspot.x = (int)(hotSpot.x * scale);
        resizedHotspot.y = (int)(hotSpot.y * scale);
        currentPointerTexture = Resize(defaultPointerTexture, 
                                    (int)(defaultPointerTexture.width * scale),
                                    (int)(defaultPointerTexture.height * scale));
        UpdateCursor();
    }

    public void ChangeCursorMode(bool toSoftware)
    {
        isSoftware = toSoftware;
        Cursor.SetCursor(currentPointerTexture, resizedHotspot, isSoftware ? CursorMode.ForceSoftware : CursorMode.Auto);
    }

    public void ResizeToPPU(string ppu)
    {
        int ppuValue;
        try
        {
            ppuValue = int.Parse(ppu);
        }
        catch (System.Exception)
        {
            return;
        }
        float cameraHeight= 2 * Camera.main.orthographicSize;
        float cameraWidth = cameraHeight * Camera.main.aspect;

        Vector2 targetSize = new Vector2(cameraWidth, cameraHeight) * ppuValue;

        float scale = Screen.width / targetSize.x;
        this.scale = scale;
        ResizeCursor(scale);
    }

    public void ResizeToResolution(string hres)
    {
        float horizontalRes;
        try
        {
            horizontalRes = float.Parse(hres);
        }
        catch (System.Exception)
        {
            return;
        }
        Vector2 targetSize = new Vector2(Screen.width, Screen.width / Camera.main.aspect) / horizontalRes;
        this.scale = targetSize.x;
        ResizeCursor(targetSize.x);
    }

    public void ConvertSprite(Image image)
    {   
        Sprite sprite = image.sprite;
        defaultPointerTexture = new Texture2D( (int)sprite.rect.width, (int)sprite.rect.height, TextureFormat.RGBA32, false);
        defaultPointerTexture.filterMode = FilterMode.Point;
        Graphics.CopyTexture(sprite.texture, defaultPointerTexture);

        if (scale > 1f)
        {
            ResizeCursor(scale);
        }
        else
        {
            currentPointerTexture = new Texture2D( (int)sprite.rect.width, (int)sprite.rect.height, TextureFormat.RGBA32, false);
            defaultPointerTexture.filterMode = FilterMode.Point;
            Graphics.CopyTexture(defaultPointerTexture, currentPointerTexture);
            UpdateCursor();
        }

    }

    private void UpdateCursor() 
    {
        Cursor.SetCursor(currentPointerTexture, resizedHotspot, isSoftware ? CursorMode.ForceSoftware : CursorMode.Auto);
    }

    private Texture2D Resize(Texture2D texture2D,int targetX,int targetY)
    {
        RenderTexture rt = new RenderTexture(targetX, targetY,32);
        Graphics.Blit(texture2D, rt);
        RenderTexture.active = rt;
        Texture2D result=new Texture2D(targetX,targetY, TextureFormat.RGBA32, false);
        result.filterMode = FilterMode.Point;
        result.ReadPixels(new Rect(0,0,targetX,targetY),0,0);
        result.Apply();
        RenderTexture.active = null;
        rt.Release();
        return result;
    }

}
