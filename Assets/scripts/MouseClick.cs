
/*
Get mouseclick position -> screen position
Convert to world pos
Convert to Pixel pos -> Get localPos and calculate how many pixels in 1 unit in the sprite
Get px and py -> tells how far we clicked from the cnter of the sprite. 
*/
using UnityEngine;

public class MouseClick : MonoBehaviour
{
    public int radius = 1;
    public Texture2D baseTexture;
    private Texture2D cloneTexture;
    private SpriteRenderer sr;

    private Vector3 mousePosition;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        cloneTexture = Instantiate(baseTexture); // we make a clone so that we dont update the original texture
        UpdateTexture();
        gameObject.AddComponent<PolygonCollider2D>();



    }

    void UpdateTexture()
    {
        //SpriteCreate makes a new sprite with the given texture and data.
        Debug.Log("Update Texture");
        sr.sprite = Sprite.Create(cloneTexture, new Rect(0, 0, cloneTexture.width, cloneTexture.height),
            new Vector2(0.5f, 0.5f), 32);



    }

    void OnMouseDown()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2Int center = WorldToPixel(mousePosition);

        for (int x = -radius; x <= radius; x++)
        {
            for (int y = -radius; y <= radius; y++)
            {
                if (x * x + y * y <= radius * radius)
                {
                    int px = center.x + x;
                    int py = center.y + y;

                    // inside the circle
                    if (px >= 0 && px < cloneTexture.width &&
                    py >= 0 && py < cloneTexture.height)
                        cloneTexture.SetPixel(px, py, Color.clear);
                }

            }
        }
        cloneTexture.Apply();
        UpdateTexture();
        Destroy(gameObject.GetComponent<PolygonCollider2D>());
        gameObject.AddComponent<PolygonCollider2D>();
    }

    Vector2Int WorldToPixel(Vector2 worldPos)
    {
        //How far from the center of the sprite did I click, in world units
        Vector2 localPos = sr.transform.InverseTransformPoint(worldPos); //convert mouse world pos to  local pos? 
        Debug.Log(worldPos + " WorldPos");
        Debug.Log(localPos + " localPos");
        //bounds is the size of the box that encapsulates the sprite when we press T, in unity units.
        //texture width is in pixels. 
        //we calculate how many pixels in 1 unit. 
        float unitsToPixelsX = cloneTexture.width / sr.bounds.size.x;
        float unitsToPixelsY = cloneTexture.height / sr.bounds.size.y;

        Debug.Log(cloneTexture.width + " TextuerWidth");
        Debug.Log(cloneTexture.height + " TextuerHeight");

        Debug.Log(sr.bounds.size + " Size");
        Debug.Log(sr.bounds.size.x + " X");
        Debug.Log(sr.bounds.size.y + " Y");

        //we want to know how many pixels from the center have we clicked. so we get center of texture + the mouseclick in local pos
        //then we multiply it with the number of pixels in 1 unit so for example:
        //width = 10 unit, textuer widht =  512, units to Pixel = 512/10 = 51.2
        //if we clicked on a position 1 unit away from center then 1 * 51.2 = 51.2 pixels away from center
        //so px will be 51.2 pixels to the right from center.
        int px = Mathf.RoundToInt(cloneTexture.width * 0.5f + localPos.x * unitsToPixelsX);
        int py = Mathf.RoundToInt(cloneTexture.height * 0.5f + localPos.y * unitsToPixelsY);

        return new Vector2Int(px, py);
    }



}

