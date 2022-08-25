// Decompiled with JetBrains decompiler
// Type: SheetGen.Drawings.Models.ViewModel
// Assembly: ukon-sheet-gen, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0A3EC11A-00F5-49C6-84D3-14D6114F312F
// Assembly location: C:\Users\bimproject\Desktop\ukon-sheet-gen_all ver\ukon-sheet-gen_19_final\ukon-sheet-gen.dll

using Autodesk.Revit.DB;
using System;
using System.Text.Json.Serialization;

namespace SheetGen.Drawings.Models
{
  public class ViewModel
  {
    public Type ViewType { get; set; }

    public string TemplateName { get; set; }

    public string ViewName { get; set; }

    public string ViewAnnotation { get; set; }

    public XYZ Coordinates { get; set; }

    public bool PlaceArrowAnnotation { get; set; }

    public bool PlaceLinesAnnotation { get; set; }

    public bool MoveLeftView { get; set; }

    public AssemblyDetailViewOrientation ViewOrientation { get; set; }

    [JsonIgnore]
    public View View { get; set; }

    [JsonIgnore]
    public Viewport Viewport { get; set; }
  }
}
