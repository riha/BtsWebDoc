using System;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Windows.Forms;
using btswebdoc.Model;
using btswebdoc.Shared.Extensions;
using btswebdoc.Shared.Logging;
using Microsoft.BizTalk.ExplorerOM;
using Microsoft.VisualStudio.EFT;
using Microsoft.VisualStudio.Forms.Internal;

namespace btswebdoc.CmdClient
{
    public enum ShapeType : int
    {
        Unknown,
        SendShape,
        MessageAssignment,
        VariableAssignment,
        ReceiveShape,
    }

    public class OrchShape
    {
        public string Text;
        public string Id;
        public SelectionArea SelectionArea;
        public string PortName;
        public string OperationName;
        public string MessageName;
        public ShapeType ShapeType;
        public int entryCount;
        public int exitCount;

        public OrchShape()
        {
            this.SelectionArea = new SelectionArea(0, 0, 0, 0);
        }

        public OrchShape(string text, string id, SelectionArea selectionArea)
        {
            this.Id = id;
            this.Text = text;
            this.SelectionArea = selectionArea;
        }
    }


    public class SelectionArea
    {
        public int X;
        public int Y;
        public int Width;
        public int Height;

        public SelectionArea()
        {
        }

        public SelectionArea(int x, int y, int w, int h)
        {
            this.X = x;
            this.Y = y;
            this.Width = w;
            this.Height = h;
        }

        public string Coordinates
        {
            get
            {
                return string.Format("{0},{1},{2},{3}", this.X, this.Y, this.X + this.Width, this.Y + this.Height);
            }
            set { }
        }

        public Rectangle GetRectangle()
        {
            return new Rectangle(this.X, this.Y, this.Width, this.Height);
        }
    }

    /// <summary>
    /// Summary description for OdViewWithSave.
    /// </summary>
    public class OrchViewer : ProcessView
    {
        public static Bitmap GetOrchestationImage(BtsOrchestration orchestration)
        {
            return GetOrchestationImage(orchestration);
        }

        private static Rectangle GetOrchImageSize(OrchestrationOverviewImage orchestration)
        {
            var ov = new OrchViewer();
            string text1 = string.Empty;
            var maxRect = new Rectangle(0, 0, 0, 0);

            if (orchestration.ViewData != string.Empty)
            {
                const int width = 1;
                const int height = 1;

                var bmp = new Bitmap(width, height);
                var memGraphic = Graphics.FromImage(bmp);

                XLANGView.XsymFile.ReadXsymFromString(ov.Root, orchestration.ViewData, false, ref text1);

                var ps = new PageSettings();
                var marginBounds = new Rectangle(0, 0, width, height);

                var args = new PrintPageEventArgs(
                    memGraphic,
                    marginBounds,
                    marginBounds,
                    ps);

                maxRect = ov.DoPrintWithSize(args);

                ps = null;
                args = null;
                memGraphic.Dispose();
                bmp.Dispose();
            }

            ov.Dispose();
            return maxRect;
        }

        //TODO: clean up futher
        public static Bitmap GetOrchestationImage(OrchestrationOverviewImage orchestrationImage, BtsOrchestration orchestration)
        {
            var ov = new OrchViewer();
            string text1 = string.Empty;

            if (!string.IsNullOrEmpty(orchestrationImage.ViewData))
            {
                XLANGView.XsymFile.ReadXsymFromString(ov.Root, orchestrationImage.ViewData, false, ref text1);

                var ps = new PageSettings();

                Rectangle maxRect = GetOrchImageSize(orchestrationImage);

                const int maxWidth = 6000;
                const int maxHeight = 6000;
                int w = maxRect.Width, h = maxRect.Height;

                float scaleX = 1, scaleY = 1;

                if (w > maxWidth)
                {
                    scaleX = maxWidth / (float)w;
                    scaleY = scaleX;
                    w = maxWidth;
                    h = (int)Math.Floor(h * scaleY);
                }

                if (h > maxHeight)
                {
                    scaleY *= maxWidth / (float)h;
                    scaleX *= scaleY;
                    h = maxHeight;
                    w = (int)Math.Floor(w * scaleX);
                }

                var realBmp = new Bitmap(w, h);
                var realGraphic = Graphics.FromImage(realBmp);
                realGraphic.ScaleTransform(scaleX, scaleY);

                realGraphic.SmoothingMode = SmoothingMode.AntiAlias;
                realGraphic.InterpolationMode = InterpolationMode.HighQualityBicubic;

                var realArgs = new PrintPageEventArgs(
                    realGraphic,
                    maxRect,
                    maxRect,
                    ps);

                // Set background colour
                //realGraphic.FillRectangle(new SolidBrush(Color.White), maxRect);
                realGraphic.Clear(Color.White);

                // Draw the orch image
                ov.DoPrintWithSize(realArgs);

                //bool drawHotspots = false;

                //try
                //{
                //    // Decide whether we need hotspots or not
                //    object drawHotspotsObj = new AppSettingsReader().GetValue("ShowHotSpots", typeof(int));

                //    if (drawHotspotsObj != null)
                //    {
                //        int tmp = Convert.ToInt32(drawHotspotsObj);
                //        if (tmp == 1) drawHotspots = true;
                //    }
                //}
                //catch(Exception ex)
                //{
                //    ErrorLogger.Log(TraceEventType.Error, ex.NestedExceptionMessage());
                //}

                GetSelectionAreas(realGraphic, ov.Root, orchestrationImage);

                ps = null;
                realArgs = null;
                ov.Dispose();
                realGraphic.Dispose();
                return realBmp;
            }
            else
            {
                const string errorText = "Unable to load image for orchestration";
                var f = new Font("Arial", 9, FontStyle.Bold);
                int fontHeight = (int)f.GetHeight() + 5;
                int errorWidth = MeasureStringWidth(errorText, f);
                int nameWidth = MeasureStringWidth(orchestration.FullName, f);
                int w = Math.Max(errorWidth, nameWidth) + 50;
                const int h = 100;

                var titleBrush = new SolidBrush(Color.Black);
                var bmp = new Bitmap(w, h);
                var g = Graphics.FromImage(bmp);

                g.FillRectangle(new SolidBrush(Color.White), 0, 0, w, h);
                g.DrawString(errorText, f, titleBrush, (bmp.Width / 2) - (errorWidth / 2), 10);
                g.DrawString(orchestration.FullName, f, titleBrush, (bmp.Width / 2) - (nameWidth / 2), 10 + fontHeight);
                return bmp;
            }
        }

        private static void GetSelectionAreas(Graphics g, BaseShape shape, OrchestrationOverviewImage orchestration)
        {
            try
            {
                foreach (BaseShape bc in shape.Shapes)
                {
                    if (bc is ReceiveShape ||
                        bc is SendShape ||
                        bc is MessageAssignmentShape ||
                        bc is VariableAssignmentShape)
                    {
                        OrchShape os = CreateOrchShape(bc);

                        if (os != null)
                        {
                            if (orchestration.ShapeMap != null)
                            {
                                orchestration.ShapeMap.Add(os);
                            }
                            //if (drawHotspots)
                            //{
                            //    DrawDebugRect(g, os.SelectionArea.GetRectangle());
                            //}
                        }
                    }

                    GetSelectionAreas(g, bc, orchestration);
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
        }

        private static OrchShape CreateOrchShape(BaseShape bc)
        {
            OrchShape os = null;

            var data = bc.Relationship as XLANGView.TrkMetadata;

            if (data != null)
            {
                os = new OrchShape { Text = bc.Text, Id = data.ShapeID };

                var selRect = new Rectangle(
                    bc.DesignSurfaceClientLocation.X - 5,
                    bc.DesignSurfaceClientLocation.Y - 7,
                    bc.Width,
                    bc.Height);

                os.SelectionArea = new SelectionArea(
                    selRect.X,
                    selRect.Y,
                    selRect.Width,
                    selRect.Height);

                switch (bc.GetType().ToString())
                {
                    case "Microsoft.VisualStudio.EFT.SendShape": os.ShapeType = ShapeType.SendShape; break;
                    case "Microsoft.VisualStudio.EFT.ReceiveShape": os.ShapeType = ShapeType.ReceiveShape; break;
                    case "Microsoft.VisualStudio.EFT.VariableAssignmentShape": os.ShapeType = ShapeType.VariableAssignment; break;
                    case "Microsoft.VisualStudio.EFT.MessageAssignmentShape": os.ShapeType = ShapeType.MessageAssignment; break;
                }
            }

            return os;
        }

        private static int MeasureStringWidth(string text, System.Drawing.Font font)
        {
            var bmp = new Bitmap(1, 1);
            var g = Graphics.FromImage(bmp);
            int i = (int)g.MeasureString(text, font).Width;
            g.Dispose();
            bmp.Dispose();
            return i;
        }

        private Rectangle DoPrintWithSize(PrintPageEventArgs e)
        {
            int maxX = 0;
            int maxY = 0;

            Point point1;
            var rectangle1 = new Rectangle(new Point(0, 0), base.Size);
            var args1 = new PaintEventArgs(e.Graphics, rectangle1);
            this.BackColor = Color.White;

            try
            {
                base.PrintingInProgress = true;

                GraphicsContainer container1 = e.Graphics.BeginContainer();
                point1 = base.AutoScrollPosition;
                point1 = base.AutoScrollPosition;
                e.Graphics.TranslateTransform(((float)-point1.X), ((float)-point1.Y));
                this.OnPaint(args1);

                maxX = point1.X;
                maxY = point1.Y;

                foreach (Control control1 in base.Controls)
                {
                    if (!(control1 is BaseControl))
                    {
                        continue;
                    }
                    GraphicsContainer container2 = e.Graphics.BeginContainer();
                    e.Graphics.TranslateTransform(((float)control1.Left) / 2, ((float)control1.Top) / 2);

                    maxX = Math.Max(control1.Left + control1.Width, maxX);
                    maxY = Math.Max(control1.Top + control1.Height, maxY);

                    ((BaseControl)control1).DoPrint(e);
                    e.Graphics.EndContainer(container2);
                }

                e.Graphics.EndContainer(container1);
            }
            finally
            {
                base.PrintingInProgress = false;
            }

            return new Rectangle(0, 0, maxX, maxY);
        }

        private static void DrawDebugRect(Graphics g, Rectangle r)
        {
            var p = new Pen(Color.Red);
            g.DrawRectangle(p, r);
            return;
        }


    }
}
