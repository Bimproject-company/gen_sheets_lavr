// Decompiled with JetBrains decompiler
// Type: SheetGen.Drawings.Transformation.ContextStoneTransformation
// Assembly: ukon-sheet-gen, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0A3EC11A-00F5-49C6-84D3-14D6114F312F
// Assembly location: C:\Users\bimproject\Desktop\ukon-sheet-gen_all ver\ukon-sheet-gen_19_final\ukon-sheet-gen.dll

using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SheetGen.Drawings.Transformation
{
  public class ContextStoneTransformation : ITransformation
  {
    private XYZ centerCurve;
    private PlanarFace biggestFace;
    private Curve lowestEdge;

    public Transform MoveAssemblyOrigin(FamilyInstance instance)
    {
      Solid solid = (instance.get_Geometry(new Options()).First<GeometryObject>() as GeometryInstance).GetInstanceGeometry().Cast<Solid>().First<Solid>((Func<Solid, bool>) (o => o.Volume > 0.0));
      SortedDictionary<double, PlanarFace> sortedDictionary = new SortedDictionary<double, PlanarFace>();
      foreach (Face face in solid.Faces)
      {
        if (face.GetType() == typeof (PlanarFace))
        {
          PlanarFace planarFace = (PlanarFace) face;
          double area = planarFace.Area;
          if (Math.Abs(planarFace.FaceNormal.Z) != 1.0 && !sortedDictionary.ContainsKey(area))
            sortedDictionary.Add(area, planarFace);
        }
      }
      this.biggestFace = sortedDictionary[sortedDictionary.Keys.Last<double>()];
      this.lowestEdge = this.biggestFace.GetEdgesAsCurveLoops().First<CurveLoop>().First<Curve>();
      Line line1 = (Line) this.lowestEdge;
      double z = line1.Origin.Z;
      foreach (Curve curve in this.biggestFace.GetEdgesAsCurveLoops().First<CurveLoop>().Where<Curve>((Func<Curve, bool>) (o => o.GetType() == typeof (Line))))
      {
        if ((GeometryObject) curve != (GeometryObject) null)
        {
          Line line2 = curve as Line;
          if (line2.Origin.Z <= z && Math.Abs(line2.Direction.Z) != 1.0 && curve.GetType() != typeof (Arc))
          {
            line1 = line2;
            z = line2.Origin.Z;
          }
        }
      }
      this.lowestEdge = (Curve) line1;
      XYZ endPoint1 = this.lowestEdge.GetEndPoint(0);
      XYZ endPoint2 = this.lowestEdge.GetEndPoint(1);
      this.centerCurve = new XYZ(0.5 * (endPoint1.X + endPoint2.X), 0.5 * (endPoint1.Y + endPoint2.Y), endPoint1.Z);
      return Transform.CreateTranslation(this.centerCurve);
    }

    public Transform RotateAssemblyOrigin(FamilyInstance instance)
    {
      XYZ endPoint1 = this.lowestEdge.GetEndPoint(0);
      XYZ endPoint2 = this.lowestEdge.GetEndPoint(1);
      XYZ xyz = new XYZ(endPoint2.X - endPoint1.X, endPoint2.Y - endPoint1.Y, endPoint2.Z - endPoint1.Z);
      XYZ basisX = XYZ.BasisX;
      double num = xyz.X * basisX.X + xyz.Y * basisX.Y + xyz.Z * basisX.Z;
      bool flag1 = xyz.X > 0.0 && xyz.Y > 0.0;
      bool flag2 = xyz.X > 0.0 && xyz.Y < 0.0;
      bool flag3 = xyz.X < 0.0 && xyz.Y < 0.0;
      bool flag4 = xyz.X < 0.0 && xyz.Y > 0.0;
      return Transform.CreateRotation(XYZ.BasisZ, !(flag1 | flag2) ? (!(flag3 | flag4) ? 0.0 : 2.0 * Math.PI - num / (xyz.GetLength() * basisX.GetLength())) : num / (xyz.GetLength() * basisX.GetLength()));
    }
  }
}
