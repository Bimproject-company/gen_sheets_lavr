// Decompiled with JetBrains decompiler
// Type: SheetGen.Properties.Resources
// Assembly: ukon-sheet-gen, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0A3EC11A-00F5-49C6-84D3-14D6114F312F
// Assembly location: C:\Users\bimproject\Desktop\ukon-sheet-gen_all ver\ukon-sheet-gen_19_final\ukon-sheet-gen.dll

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace SheetGen.Properties
{
  [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
  [DebuggerNonUserCode]
  [CompilerGenerated]
  internal class Resources
  {
    private static ResourceManager resourceMan;
    private static CultureInfo resourceCulture;

    internal Resources()
    {
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static ResourceManager ResourceManager
    {
      get
      {
        if (SheetGen.Properties.Resources.resourceMan == null)
          SheetGen.Properties.Resources.resourceMan = new ResourceManager("SheetGen.Properties.Resources", typeof (SheetGen.Properties.Resources).Assembly);
        return SheetGen.Properties.Resources.resourceMan;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static CultureInfo Culture
    {
      get => SheetGen.Properties.Resources.resourceCulture;
      set => SheetGen.Properties.Resources.resourceCulture = value;
    }

    internal static Icon s_createDrawings => (Icon) SheetGen.Properties.Resources.ResourceManager.GetObject(nameof (s_createDrawings), SheetGen.Properties.Resources.resourceCulture);

    internal static Icon settings => (Icon) SheetGen.Properties.Resources.ResourceManager.GetObject(nameof (settings), SheetGen.Properties.Resources.resourceCulture);
  }
}
