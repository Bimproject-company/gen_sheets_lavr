// Decompiled with JetBrains decompiler
// Type: SheetGen.Drawings.Models.SheetModel
// Assembly: ukon-sheet-gen, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0A3EC11A-00F5-49C6-84D3-14D6114F312F
// Assembly location: C:\Users\bimproject\Desktop\ukon-sheet-gen_all ver\ukon-sheet-gen_19_final\ukon-sheet-gen.dll

using Autodesk.Revit.DB;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SheetGen.Drawings.Models
{
  public class SheetModel
  {
    public int Number { get; set; }

    public string StampTeplateName { get; set; }

    public List<ViewModel> ViewModels { get; set; }

    public List<ParameterModel> ParameterModels { get; set; }

    [JsonIgnore]
    public ViewSheet RevitViewSheetInstance { get; set; }

    [JsonIgnore]
    public Element StampInstance { get; set; }

    [JsonIgnore]
    public AssemblyInstance RevitAssemblyInstance { get; set; }
  }
}
