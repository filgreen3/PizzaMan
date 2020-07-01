using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System.Collections.Generic;


public class ParamNode : Node
{
    public LinkPointOut OutLinkPoint;
    public List<LinkPointIn> InLinkPoints = new List<LinkPointIn>();
    public ObjectField Paramfield;


    private Object entityCollection;
    private PersonParametr target;
    private VisualElement collector;
    private Label Name;
    private Button exitButton;


    public ParamNode()
    {
        this.Add(Name = new Label("Empty"));
        this.Add(exitButton = new Button(() =>
          {
              MenuEditor.CurrentWindow.pointer = null;
              MenuEditor.CurrentWindow.Nodes.Remove(this);
              this.RemoveFromHierarchy();
          }));
        exitButton.AddToClassList("exit");
        exitButton.text = "X";

        Paramfield = new ObjectField();
        Paramfield.objectType = typeof(PersonParametr);
        Paramfield.RegisterValueChangedCallback(x => UpdateDatas((PersonParametr)x.newValue));
        Paramfield.value = entityCollection;
        Paramfield.style.opacity = 0.7f;
        Paramfield.pickingMode = PickingMode.Ignore;
        Paramfield.focusable = false;

        OutLinkPoint = new LinkPointOut( Paramfield);
        OutLinkPoint.AddToClassList("buttonin");

        this.Add(OutLinkPoint);
        this.Add(Paramfield);
        this.Add(collector = new Foldout() { text = "...", value = false });
    }

    public void UpdateDatas(PersonParametr entityGiver)
    {
        Name.text = "Empty";
        collector.Clear();
        InLinkPoints.Clear();

        if (entityGiver != null)
        {
            Name.text = entityGiver.name;
            for (int i = 0; i < entityGiver.datas.Count; i++)
            {
                var box = new VisualElement();
                var index = i;

                var element = new ObjectField();
                element.objectType = typeof(PersonParametr);
                element.value = entityGiver.datas[i];
                element.RegisterCallback<MouseDownEvent>((x) => MenuEditor.CurrentWindow.WindowParametrMenu.focusDataBar.value = element.value);
                element.RegisterValueChangedCallback(x => entityGiver.datas[index] = (DataEntityGiver)x.newValue);

                var linkButtonin = new LinkPointIn(element);
                linkButtonin.AddToClassList("buttonout");
                InLinkPoints.Add(linkButtonin);

                box.Add(linkButtonin);
                box.Add(element);

                collector.Add(box);
            }
        }
    }
}

