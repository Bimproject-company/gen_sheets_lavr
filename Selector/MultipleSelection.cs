// Decompiled with JetBrains decompiler
// Type: SheetGen.Selector.MultipleSelection
// Assembly: ukon-sheet-gen, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0A3EC11A-00F5-49C6-84D3-14D6114F312F
// Assembly location: C:\Users\bimproject\Desktop\ukon-sheet-gen_all ver\ukon-sheet-gen_19_final\ukon-sheet-gen.dll

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SheetGen.Selector
{
  public class MultipleSelection : ISelection
  {
    public IEnumerable<Element> GetElements(
      ExternalCommandData commandData,
      BuiltInCategory builtInCategory)
    {
      Document document = commandData.Application.ActiveUIDocument.Document;
      return (IEnumerable<Element>) commandData.Application.ActiveUIDocument.Selection.PickObjects(ObjectType.Element).Select<Reference, Element>((Func<Reference, Element>) (r => document.GetElement(r.ElementId))).ToList<Element>();
    }

    public IEnumerable<Element> GetFilteredElements(
      ExternalCommandData commandData,
      ISelectionFilter selectionFilter,
      BuiltInCategory builtInCategory)
    {
      Document document = commandData.Application.ActiveUIDocument.Document;
      return (IEnumerable<Element>) commandData.Application.ActiveUIDocument.Selection.PickObjects(ObjectType.Element, selectionFilter).Select<Reference, Element>((Func<Reference, Element>) (r => document.GetElement(r.ElementId))).ToList<Element>();
    }
  }
}
