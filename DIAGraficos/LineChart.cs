using System;
using Gtk;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charts.Data;
namespace Charts
{
	public class LineChart : VBox
	{
		/// <summary>
		/// Padding to the Drawing Area borders
		/// </summary>
		/// <value>The padding.</value>
		public int Padding
		{
			get;
			set;
		}

		/// <summary>
		/// Gets o sets the title chart
		/// </summary>
		/// <value>The string title.</value>
		public String Title
		{
			get;
			set;
		}

		/// <summary>
		/// Gets o sets the String value for X Axis
		/// </summary>
		/// <value>The X Axis label.</value>
		public String XLabel
		{
			get;
			set;
		}

		/// <summary>
		/// Gets o sets the String value for Y Axis
		/// </summary>
		/// <value>The Y Axis label.</value>
		public String YLabel
		{
			get;
			set;
		}

		/// <summary>
		/// Gets o sets the min value for X Axis
		/// </summary>
		/// <value>The Minimun X Axis represented value.</value>
		public double MinXValue
		{
			get;
			set;
		}

		/// <summary>
		/// Gets o sets the max value for X Axis
		/// </summary>
		/// <value>The Maximun X Axis represented value.</value>
		public double MaxXValue
		{
			get;
			set;
		}

		/// <summary>
		/// Gets o sets the min value for Y Axis
		/// </summary>
		/// <value>The Minimun Y Axis represented value.</value>
		public double MinYValue
		{
			get;
			set;
		}

		/// <summary>
		/// Gets o sets the min value for Y Axis
		/// </summary>
		/// <value>The Minimun Y Axis represented value.</value>
		public double MaxYValue
		{
			get;
			set;
		}

		/// <summary>
		/// Gets o sets the number of intervals represented in the Y Axis
		/// </summary>
		/// <value>Number of intervals bettewn Min and Max value.</value>
		public int YResolution
		{
			get;
			set;
		}

		/// <summary>
		/// Gets o sets the number of intervals represented in the X Axis
		/// </summary>
		/// <value>Number of intervals bettewn Min and Max value.</value>
		public int XResolution
		{
			get;
			set;
		}

		/// <summary>
		/// Gets o sets if its visisble the legend of this chart.
		/// Only the Legend will be Show if exist other Sets that dont be the Default Set.
		/// If only Default set exists, this property its useless.
		/// </summary>
		/// <value>If its showed the legend.</value>
		public Boolean ShowLegend
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or Set if the grid lines of backround are visibles
		/// </summary>
		/// <value><c>true</c> if show grid lines; otherwise, <c>false</c>.</value>
		public Boolean ShowGridLines
		{
			get;
			set;
		}
		/// <summary>
		/// Relation bettewn pixel and axis values
		/// </summary>
		private double PixelsPerUnit = 5;

		/// <summary>
		/// Label Characters width in Pixels. (By Char).
		/// </summary>
		private int widthByChar = 6;

		//Variable for Drag and move Chart
		private double xAnterior = 0;
		private double yAnterior = 0;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:gtksharp.LineChart"/> class.
		/// </summary>
		/// <param name="title">Title of the chart.</param>
		public LineChart(String title)
		{
			Title = title;

			ShowLegend = true;
			ShowGridLines = true;

			this.InitializeComponents();
			this.Build();

			this.ShowAll();
		}

		/// <summary>
		/// Adds data to the "Default" set data.
		/// </summary>
		/// <param name="ld">Line data to Add</param>
		public void AddData(LineData ld)
		{
			this.AddData(ld, "Default");
		}

		/// <summary>
		/// Adds data to the especified ID Set data.
		/// Id Set doesnt exist, will be created.
		/// </summary>
		/// <param name="ld">Line data to Add</param>
		public void AddData(LineData ld, string Id)
		{
			if (!data.Keys.Contains(Id))
			{
				data.Add(Id, new List<LineData>());
				ColorSets.Add(Id, new Cairo.Color(
							(new Random((int)(DateTime.UtcNow - DateTime.UtcNow.Subtract(new TimeSpan(24, 24, 24))).TotalMilliseconds)).NextDouble(),
							(new Random((int)(DateTime.UtcNow - DateTime.UtcNow.Subtract(new TimeSpan(24, 24, 24))).TotalMilliseconds)).NextDouble(),
							(new Random((int)(DateTime.UtcNow - DateTime.UtcNow.Subtract(new TimeSpan(24, 24, 24))).TotalMilliseconds)).NextDouble()
						));
			}

			List<LineData> lista = GetDataValues(Id);
			lista.Add(ld);
			data[Id].Sort();
			drawingArea.QueueDraw();
		}


		/// <summary>
		/// Remove data from the Default set of data.
		/// </summary>
		/// <param name="ld">Line data to remove</param>
		public void RemoveData(LineData ld)
		{
			this.RemoveData(ld, "Default");
		}

		/// <summary>
		/// Remove data from the especified ID Set of data
		/// </summary>
		/// <param name="ld">Line data to Remove</param>
		public void RemoveData(LineData ld, String Id)
		{
			List<LineData> lista = GetDataValues(Id);
			if (lista != null)
			{
				lista.Remove(ld);
			}
		}


		/// <summary>
		/// Clear graphic Values
		/// </summary>
		public void Clear()
		{
			data = new Dictionary<string, List<LineData>>();

			data.Add("Default", new List<LineData>());
			ColorSets = new Dictionary<string, Cairo.Color>();
		}

		/// <summary>
		/// Resturns a List of data from the Default Set.
		/// </summary>
		/// <returns>The data values.</returns>
		public List<LineData> GetDataValues()
		{
			return GetDataValues("Default");
		}

		/// <summary>
		/// Resturns a List of data from the especified Set.
		/// </summary>
		/// <param name="id">Set Identifier.</param>
		/// <returns>The data values.</returns>
		public List<LineData> GetDataValues(String id)
		{
			return data[id];
		}

		/// <summary>
		/// Returs the number of diferent Set in the chart.
		/// Default its ignored.
		/// </summary>
		/// <returns>The set count.</returns>
		public int GetSetCount()
		{
			return data.Keys.Count - 1;
		}

		/// <summary>
		/// Returs the list with all Ids
		/// </summary>
		/// <returns>The sets Ids list</returns>
		public List<String> GetSetIds()
		{
			return data.Keys.ToList();
		}

		/// <summary>
		/// Removes the especified set.
		/// </summary>
		/// <param name="id">Set Identifier.</param>
		public void RemoveSet(String id)
		{
			if (id.Equals("Default"))
				return;

			data.Remove(id);
		}
		/// <summary>
		/// Initialices window's controls and widgets
		/// </summary>
		private void InitializeComponents()
		{
			XLabel = "X Axis";
			YLabel = "Y Axis";

			Padding = 30;

			MinXValue = 0;
			MinYValue = 0;

			MaxXValue = 100;
			MaxYValue = 100;

			YResolution = 5;
			XResolution = 5;

			ColorSets = new Dictionary<string, Cairo.Color>();

			data = new Dictionary<string, List<LineData>>();

			data.Add("Default", new List<LineData>());

			IconosLeyenda = new Dictionary<string, Cairo.Rectangle>();

			this.swScroll = new Gtk.ScrolledWindow();

			// Drawing area
			this.drawingArea = new Gtk.DrawingArea();
			this.drawingArea.ModifyBg(StateType.Normal, new Gdk.Color(255, 255, 255));
			this.drawingArea.ExposeEvent += (o, args) => this.OnExposeDrawingArea();

			//Create the system of events to control de drag and move chart,and other funcionalities
			this.drawingArea.AddEvents((int)Gdk.EventMask.ButtonPressMask);
			this.drawingArea.AddEvents((int)Gdk.EventMask.ButtonReleaseMask);
			this.drawingArea.AddEvents((int)Gdk.EventMask.PointerMotionMask);
			this.drawingArea.ButtonPressEvent += delegate (object o, ButtonPressEventArgs args)
			{
				String setPulsado = GetLegendIconPressed(args);
				if (setPulsado != null)
				{
					ColorSelectionDialog cdia = new ColorSelectionDialog("Selecciona un Color");
					cdia.Response += delegate (object ob, ResponseArgs resp)
					{
						if (resp.ResponseId == ResponseType.Ok)
						{
							Gdk.Color c = cdia.ColorSelection.CurrentColor;

							ColorSets[setPulsado] = ToCairoColor(c);
							drawingArea.QueueDraw();
						}
					};

					cdia.Run();
					cdia.Destroy();
				}
				else
				{
					pulsado = true;
					drawingArea.QueueDraw();
				}
			};
			this.drawingArea.ButtonReleaseEvent += delegate (object o, ButtonReleaseEventArgs args)
			{
				pulsado = false;
				drawingArea.QueueDraw();
			};

			// Modifing the Max and Min values When cursor moves, change the chart values showed
			this.drawingArea.MotionNotifyEvent += (object o, MotionNotifyEventArgs args) =>
			{
				if (pulsado)
				{

					MinXValue -= (args.Event.X - xAnterior);
					MaxXValue -= (args.Event.X - xAnterior);
					xAnterior = args.Event.X;

					MinYValue += (args.Event.Y - yAnterior);
					MaxYValue += (args.Event.Y - yAnterior);
					yAnterior = args.Event.Y;

					drawingArea.QueueDraw();
				}
				else
				{
					xAnterior = args.Event.X;
					yAnterior = args.Event.Y;
				}
			};

			//Zoom in and Zoom out in chart, its made modifing the Min an Max values of the axis
			zoomIN = new Button("-");
			zoomIN.Clicked += (sender, e) =>
			{
				int width = 0;
				int height = 0;
				drawingArea.GdkWindow.GetSize(out width, out height);
				double Xdiference = (MaxXValue - MinXValue) * 0.25;
				double Ydiference = (MaxYValue - MinYValue) * 0.25;
				MaxXValue += Xdiference;
				MaxYValue += Ydiference;
				MinXValue -= Xdiference;
				MinYValue -= Ydiference;
				drawingArea.QueueDraw();
			};
			zoomIN.SetSizeRequest(20, 20);
			zoomOut = new Button("+");
			zoomOut.Clicked += (sender, e) =>
			{
				int width = 0;
				int height = 0;
				drawingArea.GdkWindow.GetSize(out width, out height);
				double Xdiference = (MaxXValue - MinXValue) * 0.25;
				double Ydiference = (MaxYValue - MinYValue) * 0.25;
				MaxXValue -= Xdiference;
				MaxYValue -= Ydiference;
				MinXValue += Xdiference;
				MinYValue += Ydiference;
				drawingArea.QueueDraw();
			};
			zoomOut.SetSizeRequest(20, 20);


			//Control buttons for the resolution of the chart
			menosRes = new Button("<");
			menosRes.Clicked += (sender, e) =>
			{
				if (this.XResolution > 2)
				{
					XResolution--;
					YResolution--;
					drawingArea.QueueDraw();
				}
			};

			masRes = new Button(">");
			masRes.Clicked += (sender, e) =>
			{
				XResolution++;
				YResolution++;
				drawingArea.QueueDraw();
			};

			showGrid = new CheckButton("Grid");
			showGrid.Active = ShowGridLines;
			showGrid.Clicked += (object sender, EventArgs e) =>
			{
				this.ShowGridLines = ((CheckButton)sender).Active;
				this.drawingArea.QueueDraw();
			};
			showLegend = new CheckButton("Leyenda");
			showLegend.Active = ShowLegend;
			showLegend.Clicked += (object sender, EventArgs e) =>
			{
				this.ShowLegend = ((CheckButton)sender).Active;
				this.drawingArea.QueueDraw();
			};

			centerChart = new Button("Centrar");
			centerChart.Clicked += (sender, e) =>
			{
				Boolean primero = true;
				foreach (String keys in data.Keys)
				{
					foreach (LineData ld in data[keys])
					{
						if (primero)
						{
							MinXValue = ld.X;
							MaxXValue = ld.X;
							MinYValue = ld.Y;
							MaxYValue = ld.Y;
							primero = false;
							continue;
						}
						if (ld.X < MinXValue)
						{
							MinXValue = ld.X;
						}
						if (ld.X > MaxXValue)
						{
							MaxXValue = ld.X;
						}
						if (ld.Y < MinYValue)
						{
							MinYValue = ld.Y;
						}
						if (ld.Y > MaxYValue)
						{
							MaxYValue = ld.Y;
						}
					}
				}
				drawingArea.QueueDraw();
			};
		}
		/// <summary>
		/// Gets the Id of the set witch his id is pressed in click event
		/// </summary>
		/// <returns>The legend icon pressed. Null if not icon its pressed.</returns>
		/// <param name="args">Click even args.</param>
		string GetLegendIconPressed(ButtonPressEventArgs args)
		{
			foreach (String key in IconosLeyenda.Keys)
			{
				if (args.Event.X > IconosLeyenda[key].X
					&& args.Event.X < (IconosLeyenda[key].X + IconosLeyenda[key].Width)
					&& args.Event.Y > (IconosLeyenda[key].Y + IconosLeyenda[key].Height)
					&& args.Event.Y < IconosLeyenda[key].Y)
				{
					return key;
				}
			}
			return null;
		}

		/// <summary>
		/// Create the layout of the window
		/// </summary>
		private void Build()
		{

			// Layout
			this.swScroll.AddWithViewport(this.drawingArea);

			VBox main = new VBox();
			Frame Fmain = new Frame();

			Fmain.Add(main);

			HBox axisControl = new HBox();
			axisControl.PackStart(menosRes, false, false, 3);
			axisControl.PackStart(masRes, false, false, 3);

			HBox controllButtons = new HBox(false, 5);
			controllButtons.PackStart(zoomIN, false, true, 3);
			controllButtons.PackStart(zoomOut, false, true, 3);
			controllButtons.PackStart(axisControl, false, false, 3);
			controllButtons.PackStart(new Label("Pulsa y mueve para mover el grafico"), false, false, 3);
			controllButtons.PackStart(showGrid, false, false, 3);
			controllButtons.PackStart(showLegend, false, false, 3);
			controllButtons.PackStart(centerChart, false, false, 3);

			Frame f = new Frame("");
			f.Add(controllButtons);

			main.PackStart(swScroll, true, true, 0);
			main.PackStart(f, false, false, 0);

			this.PackStart(Fmain, true, true, 0);

			// Polish
			this.SetSizeRequest(540, 400);
		}

		//Envent of show drawing area
		private void OnExposeDrawingArea()
		{
			int height = 0;
			int width = 0;
			drawingArea.GdkWindow.GetSize(out width, out height);

			using (var canvas = Gdk.CairoHelper.Create(this.drawingArea.GdkWindow))
			{
				DrawBorder(height, width, canvas);

				// Axis
				DrawAxis(height, width, canvas);

				// Data
				DrawData(height, width, canvas);

				// Clean
				canvas.GetTarget().Dispose();
			}
		}

		/// <summary>
		/// Draws the border of the chart
		/// </summary>
		/// <param name="height">Height of drawing area.</param>
		/// <param name="width">Width of drawing area.</param>
		/// <param name="canvas">The cairo context.</param>
		void DrawBorder(int height, int width, Cairo.Context canvas)
		{
			canvas.MoveTo(0, 0);
			canvas.LineWidth = 4;
			canvas.LineTo(width, 0);
			canvas.LineTo(width, height);
			canvas.LineTo(0, height);
			canvas.LineTo(0, 0);
		}

		/// <summary>
		/// Draws the Axis and other componets of the chart
		/// </summary>
		/// <param name="height">Height of drawing area.</param>
		/// <param name="width">Width of drawing area.</param>
		/// <param name="canvas">The cairo context.</param>
		void DrawAxis(int height, int width, Cairo.Context canvas)
		{
			Char[] Y = this.YLabel.ToCharArray();
			Char[] X = this.XLabel.ToCharArray();

			//Draws the Legend of the chart
			DrawLegendAndTitle(height, width, canvas);

			canvas.SetSourceRGB(0, 0, 0);



			//Set the Axis labels
			canvas.MoveTo(XStart, Padding + 15);
			canvas.ShowText(Encoding.UTF8.GetBytes(Y));
			canvas.Stroke();
			//canvas.MoveTo(width - (Padding + (X.Length * widthByChar)), height - Padding);
			canvas.MoveTo(width - (Padding + (X.Length * widthByChar)), YEnd + 30);
			canvas.ShowText(Encoding.UTF8.GetBytes(X));
			canvas.Stroke();
			//Create the Axis X and Y with the markers
			canvas.LineWidth = 4;
			canvas.MoveTo(XStart, YStart);
			canvas.LineTo(XStart, YEnd);
			canvas.LineTo(XEnd, YEnd);

			canvas.Stroke();

			// .... creates de Y markers

			double totalHeigth = (YEnd - YStart);
			double intervalHeight = totalHeigth / YResolution;

			for (double i = 0; i <= YResolution; i++)
			{
				canvas.LineWidth = 0.5;
				canvas.SetSourceRGB(0, 0, 0);
				string value = (((MinYValue * PixelsPerUnit) + ((((MaxYValue * PixelsPerUnit) - (MinYValue * PixelsPerUnit)) / YResolution) * (YResolution - i))) / PixelsPerUnit).ToString();

				canvas.MoveTo(XStart - ((value.Length + 1) * widthByChar), YStart + i * intervalHeight);
				canvas.ShowText(Encoding.UTF8.GetBytes(value));

				canvas.MoveTo(XStart - lenghtLineValue, YStart + i * intervalHeight);
				canvas.LineTo((!ShowGridLines) ? XStart : XEnd, YStart + i * intervalHeight);

				canvas.Stroke();
			}

			// .... creates de X markers
			double totalWidth = (XEnd - XStart);
			double intervalWidth = totalWidth / XResolution;
			for (double i = 0; i <= XResolution; i++)
			{
				canvas.LineWidth = 0.5;
				canvas.SetSourceRGB(0, 0, 0);
				string value = (((MinXValue * PixelsPerUnit) + ((((MaxXValue * PixelsPerUnit) - (MinXValue * PixelsPerUnit)) / XResolution) * i)) / PixelsPerUnit).ToString();

				canvas.MoveTo(XStart + i * intervalWidth - ((value.Length * PixelsPerUnit) / 2), YEnd + 15);
				canvas.ShowText(Encoding.UTF8.GetBytes(value));

				canvas.MoveTo(XStart + i * intervalWidth, YEnd + lenghtLineValue);
				canvas.LineTo(XStart + i * intervalWidth, (!ShowGridLines) ? YEnd : YStart);

				canvas.Stroke();
			}
			canvas.SetSourceRGB(0, 0, 0);
		}

		bool DrawLegendAndTitle(int height, int width, Cairo.Context canvas)
		{

			canvas.MoveTo((width / 2) - ((Title.Length/2)*widthByChar), Padding);
			canvas.ShowText(Encoding.UTF8.GetBytes(Title));

			Char[] Y = this.YLabel.ToCharArray();

			if (ShowLegend)
			{
				int count = GetSetCount();
				if (count != 0)
				{
					IconosLeyenda = new Dictionary<string, Cairo.Rectangle>();

					List<String> keys = data.Keys.ToList();
					keys.Sort();

					int ItemLegendMaxWidth = 0;
					int i = 0;
					foreach (String set in keys)
					{
						canvas.SetSourceRGB(0, 0, 0);
						if (set.Equals("Default"))
							continue;


						canvas.MoveTo(Padding, height / 2 - (15 * (i - count / 2)));
						canvas.ShowText(Encoding.UTF8.GetBytes(set));
						canvas.Stroke();


						//Console.WriteLine(ColorSets[set].R + " " + ColorSets[set].G + " " + ColorSets[set].B);
						canvas.SetSourceRGB(ColorSets[set].R, ColorSets[set].G, ColorSets[set].B);

						canvas.LineWidth = 1;
						Cairo.Rectangle rect = new Cairo.Rectangle(new Cairo.Point(Padding - 14, height / 2 - (15 * (i - count / 2))), 10, -10);
						canvas.Rectangle(rect);
						IconosLeyenda.Add(set, rect);
						canvas.Fill();
						canvas.Stroke();


						if (set.Length > ItemLegendMaxWidth)
						{
							ItemLegendMaxWidth = set.Length;
						}
						i++;
					}

					XStart = Padding + ((ItemLegendMaxWidth + 1) * widthByChar) + ((Y.Length + 1) * widthByChar);
					XEnd = width - Padding;

					YStart = Padding + 30;
					YEnd = height - (Padding + 30);
					return true;
				}
			}
			XStart = Padding;
			XEnd = width - Padding;

			YStart = Padding + 30;
			YEnd = height - (Padding + 30);

			return false;
		}

		/// <summary>
		/// Prints the data of the chart
		/// </summary>
		/// <param name="height">Height of drawing area.</param>
		/// <param name="width">Width of drawing area.</param>
		/// <param name="canvas">The cairo context.</param>
		void DrawData(int height, int width, Cairo.Context canvas)
		{
			//Variable used for start a line
			Boolean Comienzo = true;

			canvas.LineWidth = 3;
			canvas.SetSourceRGB(0, 0, 0);

			//Functions for calculate the data
			// Se usa para calcular la ecuacion de la recta con Punto - Pendiente
			Func<LineData, LineData, double> Pendiente = (d1, d2) =>
				{
					double toret = (double)(((d2.Y - d1.Y) / (d2.X - d1.X)));
					return toret;
				};

			Func<LineData, LineData, double, double> Yvalue = (d1, d2, x) =>
			 {
				 double toret = Pendiente(d1, d2) * (x - d1.X) + d1.Y;
				 if (toret < MinYValue)
					 toret = MinYValue;
				 return toret;
			 };

			//If dont exist Custom Data Set, Default set wil be Draw.
			Boolean drawSetDefault = (GetSetCount() == 0);

			foreach (String key in data.Keys)
			{
				if (key.Equals("Default") && !drawSetDefault)
					continue;
				
				for (double x = MinXValue; x <= MaxXValue; x += ((MaxXValue - MinXValue) / ((double)(MaxXValue - MinXValue) * 10)))
				{

					double drawXPosition = ((XEnd - XStart) * ((x - MinXValue) / (MaxXValue - MinXValue)));
					drawXPosition += XStart;

					LineData[] lineData = GetDataBeteewn(x, key);
					if (lineData[0] != null && lineData[1] != null)
					{
						double yValue = Yvalue(lineData[0], lineData[1], x);
						double drawYPosition = ((YEnd - YStart) * ((Yvalue(lineData[0], lineData[1], x) - MinYValue) / (MaxYValue - MinYValue)));
						drawYPosition = YEnd - drawYPosition;
						//drawYPosition = 100;
						if (Comienzo)
						{
							if (yValue < MaxYValue)
							{
								canvas.MoveTo(drawXPosition, drawYPosition);
								Comienzo = false;
							}
						}
						else
						{
							if (yValue < MaxYValue)
							{
								if (key.Equals("Default"))
								{
									canvas.SetSourceRGB(0, 0, 0);
								}
								else
								{
									canvas.SetSourceRGB(ColorSets[key].R, ColorSets[key].G, ColorSets[key].B);
								}
								//Console.WriteLine(ColorSets[key].R + " " + ColorSets[key].G + " " + ColorSets[key].B);
								canvas.LineTo(drawXPosition, drawYPosition);
							}
							else
							{
								if (key.Equals("Default"))
								{
									canvas.SetSourceRGB(0, 0, 0);
								}
								else
								{
									canvas.SetSourceRGB(ColorSets[key].R, ColorSets[key].G, ColorSets[key].B);
								}
								canvas.Stroke();
								Comienzo = true;
							}
						}
					}
				}
				canvas.Stroke();
			}
		}



		/// <summary>
		/// Obtains the 2 data that the x value is bettewn in the especified Set
		/// </summary>
		/// <returns>The data beteewn.</returns>
		/// <param name="xValue">X value.</param>
		/// <param name="id">The Set of Data Identifier.</param>
		private LineData[] GetDataBeteewn(double xValue, String id)
		{
			LineData[] toret = new LineData[2];
			for (int i = 0; i < data[id].Count; i++)
			{
				if (xValue >= data[id][i].X)
				{
					toret[0] = data[id][i];
					if (i < data[id].Count - 1 && data[id][i + 1].X >= xValue)
					{
						toret[1] = data[id][i + 1];
						return toret;
					}
				}
			}
			return toret;
		}

		/// <summary>
		/// Obtains the 2 data that the x value is bettewn.
		/// </summary>
		/// <returns>The data beteewn.</returns>
		/// <param name="xValue">X value.</param>
		private LineData[] GetDataBeteewn(double xValue)
		{
			return GetDataBeteewn(xValue, "Default");
		}

		/// <summary>
		/// Converts to Cairo.Color from Gdk.Color
		/// </summary>
		/// <returns>The cairo color.</returns>
		/// <param name="color">Gdk.Color instance</param>
		public Cairo.Color ToCairoColor(Gdk.Color color)
		{
			return new Cairo.Color((double)color.Red / ushort.MaxValue,
				(double)color.Green / ushort.MaxValue, (double)color.Blue /
				ushort.MaxValue);
		}

		/// <summary>
		/// The lenght of the mark values in the Axis
		/// </summary>
		private int lenghtLineValue = 5;

		/// <summary>
		/// Represents the pixel where the X Axis Start
		/// </summary>
		private double XStart;

		/// <summary>
		/// Represents the pixel where the X Axis Ends
		/// </summary>
		private double XEnd;

		/// <summary>
		/// Represents the pixel where the Y Axis Start
		/// </summary>
		private double YStart;
		/// <summary>
		/// Represents the pixel where the Y Axis Ends
		/// </summary>
		private double YEnd;

		/// <summary>
		/// Contais the representative color of dataSets
		/// </summary>
		private Dictionary<String, Cairo.Color> ColorSets;

		/// <summary>
		/// Contains the rectangles of the chart legend.
		/// </summary>
		private Dictionary<String, Cairo.Rectangle> IconosLeyenda;

		// BUTTONS AND CONTROLS GUI
		private Button centerChart;
		private CheckButton showGrid;
		private CheckButton showLegend;
		private Button menosRes;
		private Button masRes;
		private Button zoomIN;
		private Button zoomOut;
		private ScrolledWindow swScroll;
		private Gtk.DrawingArea drawingArea;
		private Dictionary<String, List<LineData>> data;
		private Boolean pulsado = false;
	}
}
