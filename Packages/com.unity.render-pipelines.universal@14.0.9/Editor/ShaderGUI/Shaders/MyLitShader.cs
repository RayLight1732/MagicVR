using UnityEngine;

namespace UnityEditor.Rendering.Universal.ShaderGUI
{
    internal class MyLitShader : LitShader
    {

        protected MaterialProperty dissolveTimeProp { get; set; }
        protected MaterialProperty dissolveEdgeColor { get; set; }

        // collect properties from the material properties
        public override void FindProperties(MaterialProperty[] properties)
        {
            base.FindProperties(properties);
            //ADD
            dissolveTimeProp = FindProperty("_DissolveTime", properties, false);
            dissolveEdgeColor = FindProperty("_DissolveEdgeColor", properties, false);
        }


        // material main advanced options
        public override void DrawAdvancedOptions(Material material)
        {
            base.DrawAdvancedOptions(material);
            //ADD
            materialEditor.RangeProperty(dissolveTimeProp,"DissolveTime");
            materialEditor.ColorProperty(dissolveEdgeColor, "DissolveEdgeColor");
        }

    }
}
