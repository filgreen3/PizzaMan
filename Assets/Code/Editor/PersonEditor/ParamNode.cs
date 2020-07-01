using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System.Collections.Generic;


public class ParamNode : Node
{
    public LinkPointOut OutLinkPoint;
    public List<LinkPointIn> InLinkPoints = new List<LinkPointIn>();
    public ObjectField Paramfield;


    private Object parametrObj;
    private PersonParametr target;
    private VisualElement collector;
    private Label Name;
    private Button exitButton;
    private Button addButton;


    public ParamNode()
    {
        this.Add(Name = new Label("Empty"));
        this.Add(addButton = new Button(() =>
          {
              var newdata = new PersonParametr();
              AssetDatabase.CreateAsset(newdata, "Assets/Prefabs/Parametrs/PersonParametrs/" + "newparametr" + ".asset");
              UpdateDatas(newdata);
              parametrObj = newdata;
              Paramfield.value = parametrObj;
          }));
        addButton.AddToClassList("exit");
        addButton.style.left = 210;
        addButton.style.backgroundColor = Color.green;
        addButton.style.opacity = 0.4f;
        addButton.text = "+";

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
        Paramfield.value = parametrObj;
        Paramfield.style.opacity = 0.7f;
        Paramfield.pickingMode = PickingMode.Ignore;
        Paramfield.focusable = false;

        OutLinkPoint = new LinkPointOut(Paramfield);
        OutLinkPoint.AddToClassList("buttonin");

        this.Add(OutLinkPoint);
        this.Add(Paramfield);
        this.Add(collector = new Foldout() { text = "...", value = false });

        AddButtons();

    }


    private void AddButtons()
    {
        var plusbutton = new Button(() =>
                          {
                              target.datas.Add(null);
                              UpdateDatas(target);
                          });

        plusbutton.AddToClassList("smallbutton");
        plusbutton.style.left = 210;
        plusbutton.style.backgroundColor = Color.green;
        plusbutton.style.opacity = 0.4f;
        plusbutton.text = "+";
        collector.Add(plusbutton);

        var minusbutton = new Button(() =>
                       {
                           target.datas.RemoveAt(target.datas.Count - 1);
                           UpdateDatas(target);
                       });

        minusbutton.AddToClassList("smallbutton");
        minusbutton.style.left = 210;
        minusbutton.style.backgroundColor = Color.red;
        minusbutton.style.opacity = 0.4f;
        minusbutton.text = "-";
        collector.Add(minusbutton);
    }


    public void UpdateDatas(PersonParametr entityGiver)
    {
        Name.text = "Empty";
        collector.Clear();
        InLinkPoints.Clear();

        if (entityGiver != null)
        {
            Name.text = entityGiver.name;
            target = entityGiver;
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
            AddButtons();
        }
    }
}

