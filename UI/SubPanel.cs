// Decompiled with JetBrains decompiler
// Type: SheetGen.UI.SubPanel
// Assembly: ukon-sheet-gen, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0A3EC11A-00F5-49C6-84D3-14D6114F312F
// Assembly location: C:\Users\bimproject\Desktop\ukon-sheet-gen_all ver\ukon-sheet-gen_19_final\ukon-sheet-gen.dll

using System.Collections.Generic;

namespace SheetGen.UI
{
  public class SubPanel
  {
    public Ribbon Ribbon { get; set; }

    public List<Button> Buttons { get; set; }

    public SubPanel(Ribbon ribbon, List<Button> buttons)
    {
      this.Ribbon = ribbon;
      this.Buttons = buttons;
    }

    public SubPanel()
    {
    }
  }
}
