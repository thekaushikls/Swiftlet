﻿using System;
using System.Collections.Generic;
using System.Linq;
using Grasshopper.Kernel;
using Newtonsoft.Json.Linq;
using Rhino.Geometry;
using Swiftlet.Goo;
using Swiftlet.Params;
using Swiftlet.Util;

namespace Swiftlet.Components
{
    public class ReadJsonArray : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the ParseJsonArray class.
        /// </summary>
        public ReadJsonArray()
          : base("Read JSON Array", "RJA",
              "Read a JSON Array into a series of searchable JSON objects",
              NamingUtility.CATEGORY, NamingUtility.READ_JSON)
        {
        }

        public override GH_Exposure Exposure => GH_Exposure.secondary;

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddParameter(new JTokenParam(), "Array", "JA", "JSON Array to be broken down into individual JSON objects", GH_ParamAccess.item);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddParameter(new JTokenParam(), "JTokens", "JT", "JSON Tokens as list", GH_ParamAccess.list);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            JTokenGoo goo = null;
            DA.GetData(0, ref goo);

            if (goo != null)
            {

                JToken token = goo.Value;
                if (token is JArray)
                {
                    JArray array = token as JArray;
                    List<JToken> tokens = new List<JToken>();
                    foreach (var t in array) tokens.Add(t);

                    DA.SetDataList(0, tokens.Select(o => new JTokenGoo(o)));
                }
                else
                {
                    this.AddRuntimeMessage(GH_RuntimeMessageLevel.Warning, "Input token is not an array");
                    return;
                }
            }

        }

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                //You can add image files to your project resources and access them like this:
                // return Resources.IconForThisComponent;
                return Properties.Resources.Icons_read_json_array_24x24;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("66bfbf37-4512-4f12-85b5-90003e224fb0"); }
        }
    }
}