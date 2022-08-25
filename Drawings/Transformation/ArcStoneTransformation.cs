// Decompiled with JetBrains decompiler
// Type: SheetGen.Drawings.Transformation.ArcStoneTransformation
// Assembly: ukon-sheet-gen, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0A3EC11A-00F5-49C6-84D3-14D6114F312F
// Assembly location: C:\Users\bimproject\Desktop\ukon-sheet-gen_all ver\ukon-sheet-gen_19_final\ukon-sheet-gen.dll

using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SheetGen.Drawings.Transformation
{
  public class ArcStoneTransformation : ITransformation
  {
    private XYZ arcCenter;
    private XYZ arcChordeCenter;

    public Transform MoveAssemblyOrigin(FamilyInstance instance)
    {
      Solid solid = (instance.get_Geometry(new Options()).First<GeometryObject>() as GeometryInstance).GetInstanceGeometry().Cast<Solid>().First<Solid>((Func<Solid, bool>) (o => o.Volume > 0.0));
      SortedDictionary<double, Arc> sortedDictionary = new SortedDictionary<double, Arc>();
      foreach (Edge edge in solid.Edges)
      {
        if (edge.AsCurve().GetType() == typeof (Arc))
        {
          Arc arc = (Arc) edge.AsCurve();
          double key = arc.Center.Z * arc.Radius;
          if (!sortedDictionary.ContainsKey(key))
            sortedDictionary.Add(key, arc);
        }
      }
      Arc arc1 = sortedDictionary[sortedDictionary.Keys.First<double>()];
      XYZ endPoint1 = arc1.GetEndPoint(0);
      XYZ endPoint2 = arc1.GetEndPoint(1);
      this.arcCenter = arc1.Center;
      this.arcChordeCenter = new XYZ(0.5 * (endPoint1.X + endPoint2.X), 0.5 * (endPoint1.Y + endPoint2.Y), endPoint2.Z);
      return Transform.CreateTranslation(this.arcChordeCenter);
    }

    public Transform RotateAssemblyOrigin(FamilyInstance instance)
    {
      XYZ xyz = new XYZ(this.arcChordeCenter.X - this.arcCenter.X, this.arcChordeCenter.Y - this.arcCenter.Y, this.arcChordeCenter.Z - this.arcCenter.Z);
      XYZ basisY = XYZ.BasisY;
      double num = xyz.X * basisY.X + xyz.Y * basisY.Y + xyz.Z * basisY.Z;
      bool flag1 = this.arcChordeCenter.X > this.arcCenter.X && this.arcChordeCenter.Y > this.arcCenter.Y;
      bool flag2 = this.arcChordeCenter.X > this.arcCenter.X && this.arcChordeCenter.Y < this.arcCenter.Y;
      bool flag3 = this.arcChordeCenter.X < this.arcCenter.X && this.arcChordeCenter.Y < this.arcCenter.Y;
      bool flag4 = this.arcChordeCenter.X < this.arcCenter.X && this.arcChordeCenter.Y > this.arcCenter.Y;
      return Transform.CreateRotation(XYZ.BasisZ, !(flag1 | flag2) ? (!(flag3 | flag4) ? 0.0 : 2.0 * Math.PI - num / (xyz.GetLength() * basisY.GetLength()) + Math.PI / 2.0) : num / (xyz.GetLength() * basisY.GetLength()) - Math.PI / 2.0);
    }
  }
}
