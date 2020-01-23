using System;

#if ZwCAD
using ZwSoft.ZwCAD.Geometry;
#endif

#if AutoCAD
using Autodesk.AutoCAD.Geometry;
#endif

namespace CADKit.Utils
{
    public static class Bulge
    {

        public static double GetAngle(Point2d startPoint, Point2d endPoint)
        {
            double deltaX;
            double deltaY;

            deltaX = endPoint.X - startPoint.X;
            deltaY = endPoint.Y - startPoint.Y;

            double result = Math.Atan2(deltaY, deltaX);

            return result < 0 ? result + 2 * Math.PI : result;
        }

        public static double GetBulge(Point2d centerPoint, Point2d startPoint, Point2d endPoint)
        {
            return Math.Tan((GetAngle(centerPoint, startPoint) - GetAngle(centerPoint, endPoint))/4);
        }
    }
}
