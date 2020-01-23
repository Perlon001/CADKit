using CADKit.Contracts;

#if ZwCAD
using ZwSoft.ZwCAD.DatabaseServices;
#endif

#if AutoCAD
using Autodesk.AutoCAD.DatabaseServices;
#endif

namespace CADKit.Services
{
    public class TextStyleService : SymbolTableService<TextStyleTable>, ITextStyleService
    {
        //protected override void FillDictionary()
        //{
            //TextStyleTableRecord style;

            //style = new TextStyleTableRecord();
            //style.Name = "ck_verysmall";
            //style.FileName = "simplex.shx";
            //style.TextSize = AppSettings.Instance.TextHigh[TextStyles.verysmall] * AppSettings.Instance.ScaleFactor;
            //style.XScale = 0.70;
            //style.ObliquingAngle = 0;
            //AddSymbol(TextStyles.verysmall, style);

            //style = new TextStyleTableRecord();
            //style.Name = "ck_small";
            //style.FileName = "simplex.shx";
            //style.TextSize = AppSettings.Instance.TextHigh[TextStyles.small] * AppSettings.Instance.ScaleFactor;
            //style.XScale = 0.70;
            //style.ObliquingAngle = 0;
            //AddSymbol(TextStyles.small, style);

            //style = new TextStyleTableRecord();
            //style.Name = "ck_normall";
            //style.FileName = "simplex.shx";
            //style.TextSize = AppSettings.Instance.TextHigh[TextStyles.normal] * AppSettings.Instance.ScaleFactor;
            //style.XScale = 0.70;
            //style.ObliquingAngle = 0;
            //AddSymbol(TextStyles.normal, style);

            //style = new TextStyleTableRecord();
            //style.Name = "ck_medium";
            //style.FileName = "romans.shx";
            //style.TextSize = AppSettings.Instance.TextHigh[TextStyles.medium] * AppSettings.Instance.ScaleFactor;
            //style.XScale = 0.70;
            //style.ObliquingAngle = 0;
            //AddSymbol(TextStyles.medium, style);

            //style = new TextStyleTableRecord();
            //style.Name = "ck_big";
            //style.FileName = "arial";
            //style.TextSize = AppSettings.Instance.TextHigh[TextStyles.big] * AppSettings.Instance.ScaleFactor;
            //style.XScale = 0.70;
            //style.ObliquingAngle = 0;
            //AddSymbol(TextStyles.big, style);

            //style = new TextStyleTableRecord();
            //style.Name = "ck_verybig";
            //style.FileName = "simplex.shx";
            //style.TextSize = AppSettings.Instance.TextHigh[TextStyles.verybig] * AppSettings.Instance.ScaleFactor;
            //style.XScale = 0.70;
            //style.ObliquingAngle = 0;
            //AddSymbol(TextStyles.verybig, style);

            //style = new TextStyleTableRecord();
            //style.Name = "ck_elevmark";
            //style.FileName = "simplex.shx";
            //style.TextSize = AppSettings.Instance.TextHigh[TextStyles.elevmark] * AppSettings.Instance.ScaleFactor;
            //style.XScale = 0.65;
            //style.ObliquingAngle = 10;
            //AddSymbol(TextStyles.elevmark, style);

            //style = new TextStyleTableRecord();
            //style.Name = "ck_dimension";
            //style.FileName = "simplex.shx";
            //style.TextSize = AppSettings.Instance.TextHigh[TextStyles.dim] * AppSettings.Instance.ScaleFactor;
            //style.XScale = 0.65;
            //style.ObliquingAngle = 10;
            //AddSymbol(TextStyles.dim, style);

        //}
    }
}
