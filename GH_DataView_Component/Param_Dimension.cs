﻿using System;
using System.Collections.Generic;
using Grasshopper.Kernel.Types;
using Grasshopper.Kernel;
using Rhino.Geometry;
using System.Drawing;
using Rhino;
using System.Collections;

namespace GH_DataView_Component
{
    public class Param_Dimension : GH_PersistentGeometryParam<GH_LinearDimension>, IGH_BakeAwareObject, IGH_PreviewObject
    {
        private bool m_hidden;
        protected override Grasshopper.Kernel.GH_GetterResult Prompt_Singular(ref GH_LinearDimension value)
  {
      value = GH_DimensionGetter.GetLinearDimension2();
      if (value == null)
      {
          return GH_GetterResult.cancel;
      }
      return GH_GetterResult.success;
  }
        protected override Grasshopper.Kernel.GH_GetterResult Prompt_Plural(ref List<GH_LinearDimension> values)
  {
      values = GH_DimensionGetter.GetLinearDimensions2();
      if ((values != null) && (values.Count != 0))
      {
          return GH_GetterResult.success;
      }
      return GH_GetterResult.cancel;  
  }
        public override GH_Exposure Exposure
        {
            get
            {
                return GH_Exposure.secondary;
            }
        }
        public Param_Dimension() : base(new GH_InstanceDescription("LinearDimension", "LD", "LinearDimension", "Params", "Zian")) { }
        public override Guid ComponentGuid
        {
            get { return new Guid("{835F558F-3CCC-4882-A05C-791439E2445E}"); }
        }
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                Bitmap bitmap2 = new Bitmap(24, 24);
                Graphics g = Graphics.FromImage(bitmap2);
                g.DrawImage(Properties.Resources.p5, new RectangleF(0, -1, 24, 24));
                /*
                for (int i = 0; i < 24; i++)
                {
                    for (int j = 0; j < 24; j++)
                    {
                        Color c = bitmap2.GetPixel(i, j);
                        if(c.R>200&& c.G > 200&& c.B > 200)
                        {
                            bitmap2.SetPixel(i, j, Color.FromArgb(0, Color.White)); 
                        }
                    }
                }
                */
                return bitmap2;
            }
        }
//////////////
        public BoundingBox ClippingBox
        {
            get
            {
                return this.Preview_ComputeClippingBox();
            }
        }
        public void DrawViewportMeshes(IGH_PreviewArgs args)
        {
           
        }
        public void DrawViewportWires(IGH_PreviewArgs args)
        {
            this.Preview_DrawWires(args);
        }
        public bool Hidden
        {
            get
            {
                return this.m_hidden;
            }
            set
            {
                this.m_hidden = value;
            }
        }
        public bool IsPreviewCapable
        {
            get
            {
                return true;
            }
        }
        public bool IsBakeCapable
        {
            get
            {
                return !base.m_data.IsEmpty;
            }
        }
        public void BakeGeometry(RhinoDoc doc, List<Guid> obj_ids)
        {
            this.BakeGeometry(doc, null, obj_ids);
        }
        public void BakeGeometry(RhinoDoc doc, Rhino.DocObjects.ObjectAttributes att, List<Guid> obj_ids)
        {
          
            if (att == null)
            {
                att = doc.CreateDefaultAttributes();
            }
            try
            {
                foreach (IGH_BakeAwareData data in base.m_data)
                {
                    Guid guid;
                    if ((data != null) && data.BakeGeometry(doc, att, out guid))
                    {
                        obj_ids.Add(guid);
                    }
                }
            }
            finally { }
        
        }
        /*
        public override void CreateAttributes()
        {
            base.m_attributes = new Param_DimensionAttributes(this);
        }
        */
    }
}