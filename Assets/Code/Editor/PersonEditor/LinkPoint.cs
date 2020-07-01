using UnityEngine.UIElements;
using UnityEditor.UIElements;
using UnityEngine;

public abstract class LinkPoint : Button
{
    public static Link currentLink;

    public readonly ObjectField field;

    public LinkPoint(ObjectField field)
    {
        this.field = field;
    }
}

public class LinkPointOut : LinkPoint
{

    public LinkPointOut(ObjectField field) : base(field)
    {
        this.clicked += (() =>
         {
             if (currentLink != null)
             {
                 currentLink.outPoint = this;
                 currentLink.CloseLine();
                 currentLink = null;
             }
         });
    }
}

public class LinkPointIn : LinkPoint
{
    public Link myLink;

    public void LinkInToOut(LinkPointOut outPoint)
    {
        MenuEditor.CurrentWindow.AddLink(currentLink = new Link(this));
        myLink = currentLink;
        currentLink.outPoint = outPoint;
        currentLink.CloseLine();
        currentLink = null;
    }

    public LinkPointIn(ObjectField field) : base(field)
    {
        this.clicked += (() =>
         {
             if (myLink == null)
             {
                 if (currentLink != null)
                     currentLink.inPoint = this;
                 else
                     MenuEditor.CurrentWindow.AddLink(currentLink = new Link(this));
             }
         });
    }
}

