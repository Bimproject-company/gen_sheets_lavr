// Decompiled with JetBrains decompiler
// Type: SheetGen.UI.UIBuilder
// Assembly: ukon-sheet-gen, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0A3EC11A-00F5-49C6-84D3-14D6114F312F
// Assembly location: C:\Users\bimproject\Desktop\ukon-sheet-gen_all ver\ukon-sheet-gen_19_final\ukon-sheet-gen.dll

using Autodesk.Revit.UI;
using System.Collections.Generic;

namespace SheetGen.UI
{
  public class UIBuilder
  {
    public virtual SubPanel SubPanel { get; set; }

    public virtual UIControlledApplication ControlledApplication { get; set; }

    public UIBuilder(UIControlledApplication application)
    {
      this.ControlledApplication = application;
      this.SubPanel = new SubPanel();
    }

    public virtual UIBuilder BuildRibbon(string tabName, string ribbonName)
    {
      this.SubPanel.Ribbon = new Ribbon(this.ControlledApplication, tabName, ribbonName);
      return this;
    }

    public virtual UIBuilder BuildButtons(List<Button> buttons)
    {
      this.SubPanel.Buttons = buttons;
      return this;
    }
  }
}
