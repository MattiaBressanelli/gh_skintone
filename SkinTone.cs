using System;
using System.Drawing;
using Grasshopper.Kernel;

namespace SkinTone
{
    public class SkinTone : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the SkinTone class.
        /// </summary>
        public SkinTone()
          : base("Skin Tone", "SkT",
              "Customize you canvas skin colors.",
              "Params", "Util")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddColourParameter("Canvas Background", "Cb", "The background color of the canvas.", GH_ParamAccess.item);
            pManager.AddColourParameter("Canvas Edge", "Ed", "The color of the canvas edges.", GH_ParamAccess.item);
            pManager.AddColourParameter("Canvas Grid", "Gr", "The color of the canvas grid.", GH_ParamAccess.item);
            pManager.AddBooleanParameter("Canvas Mono", "Mo", "Whether to use one color for the entire canvas.", GH_ParamAccess.item);
            pManager.AddColourParameter("Canvas Mono Color", "MoC", "The color to use for the entire canvas if Canvas Mono is true.", GH_ParamAccess.item);
            pManager.AddColourParameter("Canvas Shade Color", "Sh", "The color of the shade of the canvas.", GH_ParamAccess.item);

            pManager.AddColourParameter("Panel Background", "Pb", "The background color of the panels.", GH_ParamAccess.item);
            pManager.AddColourParameter("Group Background", "Gb", "The background color of the groups.", GH_ParamAccess.item);

            pManager.AddBooleanParameter("Defaults", "D", "Go back to default skin.", GH_ParamAccess.item);

            pManager[0].Optional = true;
            pManager[1].Optional = true;
            pManager[2].Optional = true;
            pManager[3].Optional = true;
            pManager[4].Optional = true;
            pManager[5].Optional = true;

            pManager[6].Optional = true;
            pManager[7].Optional = true;

            pManager[8].Optional = true;
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            // Input (defaults to current skin)
            Grasshopper.GUI.Canvas.GH_Skin.LoadSkin();

            Color canvasBackground = Grasshopper.GUI.Canvas.GH_Skin.canvas_back;
            Color canvasEdge = Grasshopper.GUI.Canvas.GH_Skin.canvas_edge;
            Color canvasGrid = Grasshopper.GUI.Canvas.GH_Skin.canvas_grid;
            bool canvasMono = Grasshopper.GUI.Canvas.GH_Skin.canvas_mono;
            Color monoColor = Grasshopper.GUI.Canvas.GH_Skin.canvas_mono_color;
            Color canvasShade = Grasshopper.GUI.Canvas.GH_Skin.canvas_shade;

            Color panelBackground = Grasshopper.GUI.Canvas.GH_Skin.panel_back;
            Color groupBackground = Grasshopper.GUI.Canvas.GH_Skin.group_back;

            bool defaults = false;

            DA.GetData(0, ref canvasBackground);
            DA.GetData(1, ref canvasEdge);
            DA.GetData(2, ref canvasGrid);
            DA.GetData(3, ref canvasMono);
            DA.GetData(4, ref monoColor);
            DA.GetData(5, ref canvasShade);

            DA.GetData(6, ref panelBackground);
            DA.GetData(7, ref groupBackground);

            DA.GetData(8, ref defaults);

            // Solve
            // If defaults set this values
            if (defaults)
            {
                Grasshopper.GUI.Canvas.GH_Skin.canvas_back = Color.FromArgb(255, 212, 208, 200);
                Grasshopper.GUI.Canvas.GH_Skin.canvas_edge = Color.FromArgb(255, 0, 0, 0);
                Grasshopper.GUI.Canvas.GH_Skin.canvas_grid = Color.FromArgb(30, 0, 0, 0);
                Grasshopper.GUI.Canvas.GH_Skin.canvas_mono = false;
                Grasshopper.GUI.Canvas.GH_Skin.canvas_mono_color = Color.FromArgb(255, 212, 208, 200);
                Grasshopper.GUI.Canvas.GH_Skin.canvas_shade = Color.FromArgb(80, 0, 0, 0);

                Grasshopper.GUI.Canvas.GH_Skin.panel_back = Color.FromArgb(255, 255, 250, 90);
                Grasshopper.GUI.Canvas.GH_Skin.group_back = Color.FromArgb(150, 170, 135, 225);

                Grasshopper.GUI.Canvas.GH_Skin.SaveSkin();
            }
            // Otherwise take from inputs
            else
            {
                Grasshopper.GUI.Canvas.GH_Skin.canvas_back = canvasBackground;
                Grasshopper.GUI.Canvas.GH_Skin.canvas_edge = canvasEdge;
                Grasshopper.GUI.Canvas.GH_Skin.canvas_grid = canvasGrid;
                Grasshopper.GUI.Canvas.GH_Skin.canvas_mono = canvasMono;
                Grasshopper.GUI.Canvas.GH_Skin.canvas_mono_color = monoColor;
                Grasshopper.GUI.Canvas.GH_Skin.canvas_shade = canvasShade;

                Grasshopper.GUI.Canvas.GH_Skin.panel_back = panelBackground;
                Grasshopper.GUI.Canvas.GH_Skin.group_back = groupBackground;

                Grasshopper.GUI.Canvas.GH_Skin.SaveSkin();
            }

            // No Output
        }

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override Bitmap Icon => Properties.Resources.icon;

        public override GH_Exposure Exposure => GH_Exposure.septenary;

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("7A12873E-5195-4CC8-AE17-FF647EB2C7B6"); }
        }
    }
}