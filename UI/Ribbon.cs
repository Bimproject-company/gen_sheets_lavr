// Decompiled with JetBrains decompiler
// Type: SheetGen.UI.Ribbon
// Assembly: ukon-sheet-gen, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0A3EC11A-00F5-49C6-84D3-14D6114F312F
// Assembly location: C:\Users\bimproject\Desktop\ukon-sheet-gen_all ver\ukon-sheet-gen_19_final\ukon-sheet-gen.dll

using Autodesk.Revit.Exceptions;
using Autodesk.Revit.UI;

namespace SheetGen.UI
{
  public class Ribbon
  {
    private readonly string _ribbonName;
    private readonly string _tabName;
    private readonly UIControlledApplication UIControlledApplication;

    public RibbonPanel RibbonPanel { get; private set; }

    public Ribbon(UIControlledApplication application, string tabName, string ribbonName)
    {
      this._tabName = tabName;
      this._ribbonName = ribbonName;
      this.UIControlledApplication = application;
      try
      {
        this.UIControlledApplication.CreateRibbonTab(this._tabName);
        this.RibbonPanel = this.UIControlledApplication.CreateRibbonPanel(this._tabName, this._ribbonName);
      }
      catch (ArgumentException ex)
      {
        this.RibbonPanel = this.UIControlledApplication.CreateRibbonPanel(this._tabName, this._ribbonName);
      }
    }
  }
}
