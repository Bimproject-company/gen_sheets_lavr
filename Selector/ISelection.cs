// Decompiled with JetBrains decompiler
// Type: SheetGen.Selector.ISelection
// Assembly: ukon-sheet-gen, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0A3EC11A-00F5-49C6-84D3-14D6114F312F
// Assembly location: C:\Users\bimproject\Desktop\ukon-sheet-gen_all ver\ukon-sheet-gen_19_final\ukon-sheet-gen.dll

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System.Collections.Generic;

namespace SheetGen.Selector
{
  public interface ISelection
  {
    IEnumerable<Element> GetElements(
      ExternalCommandData commandData,
      BuiltInCategory builtInCategory);

    IEnumerable<Element> GetFilteredElements(
      ExternalCommandData commandData,
      ISelectionFilter selectionFilter,
      BuiltInCategory builtInCategory);
  }
}
