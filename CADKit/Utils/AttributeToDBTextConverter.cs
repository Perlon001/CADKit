using CADKit.Contracts.Services;
using CADProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZwSoft.ZwCAD.DatabaseServices;

namespace CADKit.Utils
{
    public class AttributeToDBTextConverter : IEntityConvert
    {
        public IEnumerable<Entity> Convert(IEnumerable<Entity> _entities)
        {
            IList<Entity> result = new List<Entity>();
            foreach (var item in _entities)
            {
                if (true && item.GetType().Equals(typeof(AttributeDefinition)))
                {
                    result.Add(ProxyCAD.ToDBText((AttributeDefinition)item));
                }
                else
                {
                    result.Add(item);
                }
            }

            return result;
        }

        //private DBText AttributeDefinitionToDBText(AttributeDefinition att)
        //{
        //    var result = new DBText();

        //    result.SetDatabaseDefaults();
            
        //    result.TextString = att.TextString;
        //    result.TextStyle = att.TextStyle;
        //    result.TextStyleId = att.TextStyleId;
            
        //    result.Height = att.Height;
        //    //result.Oblique = att.Oblique;
        //    //result.WidthFactor = att.WidthFactor;
        //    //result.Rotation = att.Rotation;
        //    //result.Thickness = att.Thickness;
        //    //result.Visible = att.Visible;
        //    //result.Transparency = att.Transparency;

        //    //result.Justify = att.Justify;
        //    //result.Position = att.Position;
        //    //result.AlignmentPoint = att.AlignmentPoint;

        //    //result.VerticalMode = att.VerticalMode;
        //    //result.HorizontalMode = att.HorizontalMode;
        //    //result.IsMirroredInX = att.IsMirroredInX;
        //    //result.IsMirroredInY = att.IsMirroredInY;
        //    //result.Layer = att.Layer;

        //    //result.Annotative = att.Annotative;
        //    //result.CastShadows = att.CastShadows;
        //    //result.Color = att.Color;
        //    //result.ColorIndex = att.ColorIndex;
        //    //result.HasSaveVersionOverride = att.HasSaveVersionOverride;
            

        //    //result.LayerId = att.LayerId;
        //    //result.Linetype = att.Linetype;
        //    //result.LinetypeId = att.LinetypeId;
        //    //result.LinetypeScale = att.LinetypeScale;
        //    //result.LineWeight = att.LineWeight;
        //    //result.Material = att.Material;
        //    //result.MaterialId = att.MaterialId;
        //    //result.Normal = att.Normal;
        //    //result.ReceiveShadows = att.ReceiveShadows;

        //    return result;
        //}
    }
}
