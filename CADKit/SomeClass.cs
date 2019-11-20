//using System;

//#if ZWCAD
//using ZwSoft.ZwCAD.Geometry;
//#endif

//#if AUTOCADCAD
//using Autodesk.AutoCAD.Geometry;
//#endif

//using Proxy.Geometry;

//// Każdy z poniższych namespace jest w innej .dll dla uproszczenia wrzucone do jednego kodu
//// Jak usunąć zależność SomeNamespace od ZwSoft.ZwCAD.Geometry i Autodesk.AutoCAD.Geometry
//// i pozostawić tylko zależność od Proxy.Geometry?
//// Samo Proxy może być kompilowane z uwzględnieniem zdefiniowanych symboli ZWCAD lub AUTOCAD
//// a jedynym źródłem typów dla modułu SomeNameSpace ( i innych ) powinien być Proxy.
//// Typy do "mapowania" to całe drzewo klas, klas statycznych, struktur, atrybutów etc.

//namespace SomeNamespace
//{
   
//    public class SomeClass
//    {
//        public void Test()
//        {
//            Point2d point = new Point2d();  // ?
//        }
//    }
//}

//namespace Proxy.Geometry
//{
//    public struct Point2d { }   // ?
//}

//namespace ZwSoft.ZwCAD.Geometry
//{
//    [DefaultMember("Coordinate")]
//    [Wrapper("ZcGePoint2d")]
//    public struct Point2d : IFormattable
//    {
//        public Point2d(double[] xy);
//        public Point2d(double x, double y);

//        public double this[int i] { get; }

//        public static Point2d Origin { get; }
//        public double Y { get; }
//        public double X { get; }

//        public Point2d Add(Vector2d value);
//        public Point2d DivideBy(double value);
//        public sealed override bool Equals(object obj);
//        public Vector2d GetAsVector();
//        public double GetDistanceTo(Point2d point);
//        public sealed override int GetHashCode();
//        public Vector2d GetVectorTo(Point2d point);
//        public bool IsEqualTo(Point2d a, Tolerance tolerance);
//        public bool IsEqualTo(Point2d a);
//        public Point2d Mirror(Line2d line);
//        public Point2d MultiplyBy(double value);
//        public Point2d RotateBy(double angle, Point2d origin);
//        public Point2d ScaleBy(double scaleFactor, Point2d origin);
//        public Point2d Subtract(Vector2d value);
//        public double[] ToArray();
//        public string ToString(IFormatProvider provider);
//        public sealed override string ToString();
//        public string ToString(string format, IFormatProvider provider);
//        public Point2d TransformBy(Matrix2d leftSide);

//        public static Point2d operator +(Point2d a, Vector2d vector);
//        public static Point2d operator -(Point2d a, Vector2d b);
//        public static Vector2d operator -(Point2d a, Point2d b);
//        public static Point2d operator *(Point2d a, double value);
//        public static Point2d operator *(double value, Point2d a);
//        public static Point2d operator *(Matrix2d mat, Point2d a);
//        public static Point2d operator /(Point2d a, double value);
//        public static bool operator ==(Point2d a, Point2d b);
//        public static bool operator !=(Point2d a, Point2d b);
//    }
//}

//namespace Autodesk.AutoCAD.Geometry
//{
//    [DefaultMember("Coordinate")]
//    [Wrapper("AcGePoint2d")]
//    public struct Point2d : IFormattable
//    {
//        public Point2d(double[] xy);
//        public Point2d(double x, double y);

//        public double this[int i] { get; }

//        public static Point2d Origin { get; }
//        public double Y { get; }
//        public double X { get; }

//        public Point2d Add(Vector2d value);
//        public Point2d DivideBy(double value);
//        public sealed override bool Equals(object obj);
//        public Vector2d GetAsVector();
//        public double GetDistanceTo(Point2d point);
//        public sealed override int GetHashCode();
//        public Vector2d GetVectorTo(Point2d point);
//        public bool IsEqualTo(Point2d a);
//        public bool IsEqualTo(Point2d a, Tolerance tolerance);
//        public Point2d Mirror(Line2d line);
//        public Point2d MultiplyBy(double value);
//        public Point2d RotateBy(double angle, Point2d origin);
//        public Point2d ScaleBy(double scaleFactor, Point2d origin);
//        public Point2d Subtract(Vector2d value);
//        public double[] ToArray();
//        public string ToString(string format, IFormatProvider provider);
//        public sealed override string ToString();
//        public string ToString(IFormatProvider provider);
//        public Point2d TransformBy(Matrix2d leftSide);

//        public static Point2d operator +(Point2d a, Vector2d vector);
//        public static Vector2d operator -(Point2d a, Point2d b);
//        public static Point2d operator -(Point2d a, Vector2d b);
//        public static Point2d operator *(Point2d a, double value);
//        public static Point2d operator *(double value, Point2d a);
//        public static Point2d operator *(Matrix2d mat, Point2d a);
//        public static Point2d operator /(Point2d a, double value);
//        public static bool operator ==(Point2d a, Point2d b);
//        public static bool operator !=(Point2d a, Point2d b);
//    }
//}
