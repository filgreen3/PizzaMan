using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class Link
{
    public LinkPointOut outPoint;
    public LinkPointIn inPoint;

    public Link(LinkPointIn inPoint)
    {
        this.inPoint = inPoint;
    }

    public void Draw()
    {
        if (inPoint == null) MenuEditor.CurrentWindow.Links.Remove(this);
        if (outPoint != null)
        {
            Handles.DrawBezier(
                    inPoint.worldBound.position + Vector2.right * inPoint.layout.size.x * 0.5f - Vector2.up * inPoint.layout.size.y * 0.5f,
                    outPoint.worldBound.position + Vector2.right * outPoint.layout.size.x * 0.5f - Vector2.up * outPoint.layout.size.y * 0.5f,
                    inPoint.worldBound.position + inPoint.layout.size * 0.5f - Vector2.left * 50f - Vector2.up * inPoint.layout.size.y,
                    outPoint.worldBound.position + outPoint.layout.size * 0.5f + Vector2.left * 50f - Vector2.up * outPoint.layout.size.y,
                    Color.white,
                    null,
                    5f
                );

            if (Handles.Button((inPoint.worldBound.position + Vector2.right * inPoint.layout.size.x * 0.5f - Vector2.up * inPoint.layout.size.y * 0.5f +
             outPoint.worldBound.position + Vector2.right * outPoint.layout.size.x * 0.5f - Vector2.up * outPoint.layout.size.y * 0.5f)
              * 0.5f, Quaternion.identity, 4, 8, Handles.RectangleHandleCap))
            {
                Unlink();
                MenuEditor.CurrentWindow.Links.Remove(this);
            }
        }
        else
        {
            var pos = Event.current.mousePosition;
            Handles.DrawBezier(
                    pos,
                    inPoint.worldBound.position + Vector2.right * inPoint.layout.size.x * 0.5f - Vector2.up * inPoint.layout.size.y * 0.5f,
                    pos + Vector2.left * 50f,
                    inPoint.worldBound.position + inPoint.layout.size * 0.5f - Vector2.left * 50f,
                    Color.white,
                    null,
                    5f
                );
        }

        GUI.changed = true;
    }

    public void CloseLine()
    {
        inPoint.myLink = this;
        inPoint.field.value = outPoint.field.value;
    }

    public void Unlink()
    {
        inPoint.myLink = null;
    }
}

