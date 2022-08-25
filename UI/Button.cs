// Decompiled with JetBrains decompiler
// Type: SheetGen.UI.Button
// Assembly: ukon-sheet-gen, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0A3EC11A-00F5-49C6-84D3-14D6114F312F
// Assembly location: C:\Users\bimproject\Desktop\ukon-sheet-gen_all ver\ukon-sheet-gen_19_final\ukon-sheet-gen.dll

using Autodesk.Revit.UI;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SheetGen.UI
{
  public class Button
  {
    private readonly RibbonPanel _ribbonPanel;
    private readonly Bitmap _bitmap;
    private readonly string _assemblyName;
    private readonly string _fullClassName;
    private readonly string _name;
    private readonly string _text;

    public Button(
      UIBuilder builder,
      Bitmap bitmap,
      string fullClassName,
      string name,
      string text)
    {
      this._ribbonPanel = builder.SubPanel.Ribbon.RibbonPanel;
      this._bitmap = bitmap;
      this._assemblyName = Assembly.GetExecutingAssembly().Location;
      this._fullClassName = fullClassName;
      this._name = name;
      this._text = text;
      ImageSource imageSource = (ImageSource) Button.Convert((Image) this._bitmap);
      (this._ribbonPanel.AddItem((RibbonItemData) new PushButtonData(this._name, this._text, this._assemblyName, this._fullClassName)) as PushButton).LargeImage = imageSource;
    }

    private static BitmapImage Convert(Image image)
    {
      using (MemoryStream memoryStream = new MemoryStream())
      {
        image.Save((Stream) memoryStream, ImageFormat.Png);
        memoryStream.Position = 0L;
        BitmapImage bitmapImage = new BitmapImage();
        bitmapImage.BeginInit();
        bitmapImage.StreamSource = (Stream) memoryStream;
        bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
        bitmapImage.EndInit();
        return bitmapImage;
      }
    }
  }
}
