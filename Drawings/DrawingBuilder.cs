// Decompiled with JetBrains decompiler
// Type: SheetGen.Drawings.DrawingBuilder
// Assembly: ukon-sheet-gen, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0A3EC11A-00F5-49C6-84D3-14D6114F312F
// Assembly location: C:\Users\bimproject\Desktop\ukon-sheet-gen_all ver\ukon-sheet-gen_19_final\ukon-sheet-gen.dll

using Autodesk.Revit.DB;
using SheetGen.Drawings.Factory;
using SheetGen.Drawings.Models;
using SheetGen.Drawings.Transformation;
using SheetGen.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SheetGen.Drawings
{
  public class DrawingBuilder
  {
    private Element _element;
    private SheetModel _sheetModel;

    public DrawingBuilder(Element element) => this._element = element;

    public Element GetElement() => this._element;

    public DrawingBuilder BuildAssemblies()
    {
      if (this._element.AssemblyInstanceId == ElementId.InvalidElementId)
      {
        Transaction transaction = new Transaction(this._element.Document, "--> Создаём сборку для карниза " + this._element.Name);
        TransactionSettings.SetFailuresPreprocessor(transaction);
        FamilySymbol symbol = ((FamilyInstance) this._element).Symbol;
        ParameterMap parametersMap1 = this._element.ParametersMap;
        ParameterMap parametersMap2 = symbol.ParametersMap;
        string str;
        try
        {
          str = string.Format("K1-{0}{1}-F{2}-{3}.{4}", (object) parametersMap2.get_Item("Ukon_Порода камня").AsString(), (object) parametersMap2.get_Item("Ukon_Марка элемента").AsString(), (object) parametersMap1.get_Item("Ukon_Фасад").AsString(), (object) parametersMap1.get_Item("Ukon_Этаж").AsString(), (object) parametersMap1.get_Item("Ukon_Типоразмер").AsString());
        }
        catch (Exception ex)
        {
          str = string.Format("K1-{0}{1}-F{2}-{3}.{4}", (object) parametersMap2.get_Item("Ukon_Порода камня").AsString(), (object) parametersMap1.get_Item("Ukon_Марка элемента").AsString(), (object) parametersMap1.get_Item("Ukon_Фасад").AsString(), (object) parametersMap1.get_Item("Ukon_Этаж").AsString(), (object) parametersMap1.get_Item("Ukon_Типоразмер").AsString());
        }
        using (transaction)
        {
          int num1 = (int) transaction.Start();
          Autodesk.Revit.DB.Document document = this._element.Document;
          List<ElementId> assemblyMemberIds = new List<ElementId>();
          assemblyMemberIds.Add(this._element.Id);
          ElementId id = this._element.Category.Id;
          AssemblyInstance assemblyInstance = AssemblyInstance.Create(document, (ICollection<ElementId>) assemblyMemberIds, id);
          int num2 = (int) transaction.Commit();
          try
          {
            int num3 = (int) transaction.Start();
            assemblyInstance.AssemblyTypeName = str;
            int num4 = (int) transaction.Commit();
          }
          catch (Autodesk.Revit.Exceptions.ArgumentException ex)
          {
            if (transaction.GetStatus() != TransactionStatus.Committed)
            {
              int num5 = (int) transaction.Commit();
            }
            int num6 = (int) transaction.Start();
            assemblyInstance.AssemblyTypeName = string.Format("{0}-ID{1}", (object) str, (object) assemblyInstance.Id);
            int num7 = (int) transaction.Commit();
          }
        }
      }
      return this;
    }

    public DrawingBuilder SetAssembliesOrigin(ITransformation transformation)
    {
      AssemblyInstance element = this._element.Document.GetElement(this._element.AssemblyInstanceId) as AssemblyInstance;
      Transaction transaction = new Transaction(this._element.Document, "--> Создание листов карниза " + this._element.Name);
      TransactionSettings.SetFailuresPreprocessor(transaction);
      Transform trf = transformation.MoveAssemblyOrigin((FamilyInstance) this._element).Multiply(transformation.RotateAssemblyOrigin((FamilyInstance) this._element));
      using (transaction)
      {
        int num1 = (int) transaction.Start();
        element.SetTransform(trf);
        int num2 = (int) transaction.Commit();
      }
      return this;
    }

    public DrawingBuilder BuildSheetByModel(IFactory factory)
    {
      this._sheetModel = factory.GetSheetModel(this._element);
      Transaction transaction = new Transaction(this._element.Document, "--> Создание листов карниза " + this._element.Name);
      TransactionSettings.SetFailuresPreprocessor(transaction);
      using (transaction)
      {
        ElementId id = new FilteredElementCollector(this._element.Document).OfClass(typeof (FamilySymbol)).First<Element>((Func<Element, bool>) (x => x.Name == this._sheetModel.StampTeplateName)).Id;
        int num1 = (int) transaction.Start();
        this._sheetModel.RevitViewSheetInstance = AssemblyViewUtils.CreateSheet(this._element.Document, this._element.AssemblyInstanceId, id);
        int num2 = (int) transaction.Commit();
        int num3 = (int) transaction.Start();
        ParameterMap parametersMap1 = new FilteredElementCollector(this._element.Document, this._sheetModel.RevitViewSheetInstance.Id).OfClass(typeof (FamilyInstance)).FirstOrDefault<Element>().ParametersMap;
        parametersMap1.get_Item("Формат А").Set(4);
        parametersMap1.get_Item("Книжная ориентация").Set(1);
        ParameterMap parametersMap2 = this._sheetModel.RevitViewSheetInstance.ParametersMap;
        foreach (ParameterModel parameterModel in this._sheetModel.ParameterModels)
          parametersMap2.get_Item(parameterModel.Name).Set(parameterModel.Value);
        int num4 = (int) transaction.Commit();
      }
      return this;
    }

    public DrawingBuilder BuildViews()
    {
      Transaction transaction = new Transaction(this._element.Document, "--> Создание листов карниза " + this._element.Name);
      TransactionSettings.SetFailuresPreprocessor(transaction);
      using (transaction)
      {
        int num1 = (int) transaction.Start();
        foreach (ViewModel viewModel in this._sheetModel.ViewModels)
        {
          ViewModel item = viewModel;
          List<Element> list = new FilteredElementCollector(this._element.Document).OfClass(item.ViewType).Where<Element>((Func<Element, bool>) (v => v.Name.Equals(item.TemplateName))).ToList<Element>();
          item.View = list.Count != 0 ? (!(item.ViewType != typeof (View3D)) ? (View) AssemblyViewUtils.Create3DOrthographic(this._element.Document, this._element.AssemblyInstanceId, list.First<Element>().Id, true) : (View) AssemblyViewUtils.CreateDetailSection(this._element.Document, this._element.AssemblyInstanceId, item.ViewOrientation, list.First<Element>().Id, true)) : (!(item.ViewType != typeof (View3D)) ? (View) AssemblyViewUtils.Create3DOrthographic(this._element.Document, this._element.AssemblyInstanceId) : (View) AssemblyViewUtils.CreateDetailSection(this._element.Document, this._element.AssemblyInstanceId, item.ViewOrientation));
        }
        int num2 = (int) transaction.Commit();
        int num3 = (int) transaction.Start();
        foreach (ViewModel viewModel in this._sheetModel.ViewModels)
          viewModel.View.Name = viewModel.ViewName;
        int num4 = (int) transaction.Commit();
      }
      return this;
    }

    public DrawingBuilder PlaceViewsOnSheets()
    {
      List<Element> list = new FilteredElementCollector(this._element.Document).OfClass(typeof (ElementType)).Where<Element>((Func<Element, bool>) (o => o.Name == "Заголовок на листе")).ToList<Element>();
      Transaction transaction = new Transaction(this._element.Document, "--> Создание листов карниза " + this._element.Name);
      TransactionSettings.SetFailuresPreprocessor(transaction);
      using (transaction)
      {
        int num1 = (int) transaction.Start();
        foreach (ViewModel viewModel in this._sheetModel.ViewModels)
        {
          if (Viewport.CanAddViewToSheet(this._element.Document, this._sheetModel.RevitViewSheetInstance.Id, viewModel.View.Id))
            viewModel.Viewport = Viewport.Create(this._element.Document, this._sheetModel.RevitViewSheetInstance.Id, viewModel.View.Id, viewModel.Coordinates);
        }
        int num2 = (int) transaction.Commit();
        if (list.Count != 0)
        {
          int num3 = (int) transaction.Start();
          foreach (ViewModel viewModel in this._sheetModel.ViewModels)
          {
            viewModel.Viewport.ChangeTypeId(list.First<Element>().Id);
            if (!viewModel.Viewport.ParametersMap.get_Item("ADSK_Примечание к виду").IsReadOnly)
              viewModel.Viewport.ParametersMap.get_Item("ADSK_Примечание к виду").Set(viewModel.ViewAnnotation);
          }
          int num4 = (int) transaction.Commit();
        }
        FamilySymbol symbol = new FilteredElementCollector(this._element.Document).OfClass(typeof (FamilySymbol)).Cast<FamilySymbol>().First<FamilySymbol>((Func<FamilySymbol, bool>) (s => s.Name.Equals("Без кружка_Стрелка 3мм")));
        int num5 = (int) transaction.Start();
        foreach (ViewModel viewModel in this._sheetModel.ViewModels)
        {
          if (viewModel.PlaceArrowAnnotation)
          {
            Autodesk.Revit.Creation.Document create = this._element.Document.Create;
            XYZ origin = viewModel.View.Origin;
            double num6 = 200.0 / 304.8;
            XYZ center = (this._element.Document.GetElement(this._element.AssemblyInstanceId) as AssemblyInstance).GetCenter();
            XYZ endpoint1 = new XYZ(center.X, center.Y, center.Z + 0.1);
            Line bound = Line.CreateBound(endpoint1, endpoint1.Add(viewModel.View.RightDirection.Multiply(num6)));
            DetailCurve detailCurve = this._element.Document.Create.NewDetailCurve(viewModel.View, (Curve) bound);
            this._element.Document.Create.NewFamilyInstance(detailCurve.GeometryCurve as Line, symbol, viewModel.View);
            this._element.Document.Delete((ICollection<ElementId>) new List<ElementId>()
            {
              detailCurve.Id
            });
          }
          if (viewModel.PlaceLinesAnnotation)
          {
            Autodesk.Revit.Creation.Document create = this._element.Document.Create;
            XYZ origin = viewModel.View.Origin;
            double num7 = double.Parse(this._element.ParametersMap.get_Item("ADSK_Размер_Ширина").AsValueString()) / 304.8;
            XYZ center = (this._element.Document.GetElement(this._element.AssemblyInstanceId) as AssemblyInstance).GetCenter();
            XYZ xyz1 = new XYZ(center.X, center.Y, center.Z - 0.2);
            XYZ xyz2 = new XYZ(center.X, center.Y, center.Z - 0.3);
            Line bound1 = Line.CreateBound(xyz1.Subtract(viewModel.View.RightDirection.Multiply(num7 / 2.0)), xyz1.Add(viewModel.View.RightDirection.Multiply(num7 / 2.0)));
            Line bound2 = Line.CreateBound(xyz2.Subtract(viewModel.View.RightDirection.Multiply(num7 / 2.0)), xyz2.Add(viewModel.View.RightDirection.Multiply(num7 / 2.0)));
            DetailCurve detailCurve1 = this._element.Document.Create.NewDetailCurve(viewModel.View, (Curve) bound1);
            DetailCurve detailCurve2 = this._element.Document.Create.NewDetailCurve(viewModel.View, (Curve) bound2);
            Element element1 = new FilteredElementCollector(this._element.Document).OfClass(typeof (GraphicsStyle)).Where<Element>((Func<Element, bool>) (o => o.Name == "Гидрофобизация_ш8_п2_п2")).First<Element>();
            Element element2 = new FilteredElementCollector(this._element.Document).OfClass(typeof (GraphicsStyle)).Where<Element>((Func<Element, bool>) (o => o.Name == "ADSK_Сплошная_Красная_2")).First<Element>();
            detailCurve2.LineStyle = element1;
            detailCurve1.LineStyle = element2;
          }
        }
        int num8 = (int) transaction.Commit();
        int num9 = (int) transaction.Start();
        foreach (ViewModel viewModel in this._sheetModel.ViewModels)
        {
          if (viewModel.MoveLeftView)
          {
            viewModel.View.EnableRevealHiddenMode();
            Element element = new FilteredElementCollector(this._element.Document, viewModel.View.Id).OfCategory(BuiltInCategory.OST_Viewers).Where<Element>((Func<Element, bool>) (o => o.Name.Equals("Вид слева"))).First<Element>();
            (this._element.Document.GetElement(this._element.AssemblyInstanceId) as AssemblyInstance).GetCenter();
            SubTransaction subTransaction = new SubTransaction(this._element.Document);
            double num10 = 1900.0 / 304.8;
            XYZ translation = viewModel.View.RightDirection.Multiply(num10);
            if (element != null)
              ElementTransformUtils.MoveElement(this._element.Document, element.Id, translation);
            element.ParametersMap.get_Item("Смещение дальнего предела секущего диапазона").Set(0.01);
            viewModel.View.TemporaryViewModes.DeactivateAllModes();
          }
        }
        int num11 = (int) transaction.Commit();
      }
      return this;
    }

    public DrawingBuilder BuildParameters() => this;
  }
}
