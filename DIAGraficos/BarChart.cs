using System;
using Gtk;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charts.Data;
namespace Charts
{
	public class BarChart : VBox
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
		/// Padding bettewn diferent Sets
		/// </summary>
		/// <value>The padding.</value>
		public int PaddingSets
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
		/// Gets o sets the String for value representation
		/// </summary>
		/// <value>The Values label.</value>
		public String ValueLabel
		{
			get;
			set;
		}
		/// <summary>
		/// Gets o sets the min value for Y Axis
		/// </summary>
		/// <value>The Minimun Y Axis represented value.</value>
		private double MinValue
		{
			get;
			set;
		}

		/// <summary>
		/// Gets o sets the min value for Y Axis
		/// </summary>
		/// <value>The Minimun Y Axis represented value.</value>
		private double MaxValue
		{
			get;
			set;
		}
		/// <summary>
		/// Gets o sets the number of intervals represented in the Values Axis
		/// </summary>
		/// <value>Number of intervals bettewn Min and Max value.</value>
		public int ValueResolution
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
		public BarChart(String title)
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
		public void AddData(BarData ld)
		{
			this.AddData(ld, "Default");
		}

		/// <summary>
		/// Adds data to the especified ID Set data.
		/// Id Set doesnt exist, will be created.
		/// </summary>
		/// <param name="ld">Line data to Add</param>
		public void AddData(BarData ld, String Id)
		{
			if (!data.Keys.Contains(Id))
			{
				data.Add(Id, new List<BarData>());
			}

			List<BarData> lista = GetDataValues(Id);
			if (!ColorSets.Keys.ToList().Contains(ld.Name))
			{
				ColorSets.Add(ld.Name, new Cairo.Color(
								(new Random((int)(DateTime.UtcNow - DateTime.UtcNow.Subtract(new TimeSpan(24, 24, 24))).TotalMilliseconds)).NextDouble(),
								(new Random((int)(DateTime.UtcNow - DateTime.UtcNow.Subtract(new TimeSpan(24, 24, 24))).TotalMilliseconds)).NextDouble(),
								(new Random((int)(DateTime.UtcNow - DateTime.UtcNow.Subtract(new TimeSpan(24, 24, 24))).TotalMilliseconds)).NextDouble()
							));
			}
			if (ld.Value > MaxValue)
			{
				MaxValue = ld.Value;
			}
			lista.Add(ld);
			lista.Sort();
		}


		/// <summary>
		/// Remove data from the Default set of data.
		/// </summary>
		/// <param name="ld">Line data to remove</param>
		public void RemoveData(BarData ld)
		{
			this.RemoveData(ld, "Default");
		}

		/// <summary>
		/// Remove data from the especified ID Set of data
		/// </summary>
		/// <param name="ld">Line data to Remove</param>
		public void RemoveData(BarData ld, String Id)
		{
			List<BarData> lista = GetDataValues(Id);
			if (lista != null)
			{
				lista.Remove(ld);
			}
		}

		/// <summary>
		/// Resturns a List of data from the Default Set.
		/// </summary>
		/// <returns>The data values.</returns>
		public List<BarData> GetDataValues()
		{
			return GetDataValues("Default");
		}

		/// <summary>
		/// Resturns a List of data from the especified Set.
		/// </summary>
		/// <param name="id">Set Identifier.</param>
		/// <returns>The data values.</returns>
		public List<BarData> GetDataValues(String id)
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

			Padding = 30;
			PaddingSets = 10;

			MinValue = 0;
			ColorSets = new Dictionary<string, Cairo.Color>();

			data = new Dictionary<string, List<BarData>>();
			data.Add("Default", new List<BarData>());

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
					drawingArea.QueueDraw();
				}
			};
			this.drawingArea.ButtonReleaseEvent += delegate (object o, ButtonReleaseEventArgs args)
			{
				drawingArea.QueueDraw();
			};

			//Control buttons for the resolution of the chart
			menosPadd = new Button("<");
			menosPadd.Clicked += (sender, e) =>
			{
				if (PaddingSets >= 20)
				{
					PaddingSets -= 20;
					drawingArea.QueueDraw();
				}
			};

			masPadd = new Button(">");
			masPadd.Clicked += (sender, e) =>
			{
				PaddingSets += 20;
				drawingArea.QueueDraw();
			};

			//Control buttons for the resolution of the chart
			menosRes = new Button("-");
			menosRes.Clicked += (sender, e) =>
			{
				if (this.ValueResolution > 2)
				{
					ValueResolution--;
					drawingArea.QueueDraw();
				}
			};

			masRes = new Button("+");
			masRes.Clicked += (sender, e) =>
			{
				ValueResolution++;
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
			axisControl.PackStart(menosPadd, false, false, 3);
			axisControl.PackStart(masPadd, false, false, 3);


			HBox controllButtons = new HBox(false, 5);
			controllButtons.PackStart(axisControl, false, false, 3);
			controllButtons.PackStart(showGrid, false, false, 3);
			controllButtons.PackStart(showLegend, false, false, 3);

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
			Char[] VALUE = this.ValueLabel.ToCharArray();

			//Draws the Legend of the chart
			DrawLegendAndTitle(height, width, canvas);

			canvas.SetSourceRGB(0, 0, 0);

			//Set the Axis labels
			canvas.MoveTo(XStart, Padding + 15);
			canvas.ShowText(Encoding.UTF8.GetBytes(VALUE));
			canvas.Stroke();

			//Create the Axis with the markers
			canvas.LineWidth = 4;
			canvas.MoveTo(XStart, YStart);
			canvas.LineTo(XStart, YEnd);
			canvas.LineTo(XEnd, YEnd);

			canvas.Stroke();

			// .... creates de Y markers

			double totalHeigth = (YEnd - YStart);
			double intervalHeight = totalHeigth / ValueResolution;

			for (double i = 0; i <= ValueResolution; i++)
			{
				canvas.LineWidth = 0.5;
				canvas.SetSourceRGB(0, 0, 0);
				string value = (((MinValue * PixelsPerUnit) + ((((MaxValue * PixelsPerUnit) - (MinValue * PixelsPerUnit)) / ValueResolution) * (ValueResolution - i))) / PixelsPerUnit).ToString();

				canvas.MoveTo(XStart - ((value.Length + 1) * widthByChar), YStart + i * intervalHeight);
				canvas.ShowText(Encoding.UTF8.GetBytes(value));

				canvas.MoveTo(XStart - lenghtLineValue, YStart + i * intervalHeight);
				canvas.LineTo((!ShowGridLines) ? XStart : XEnd, YStart + i * intervalHeight);

				canvas.Stroke();
			}

			// .... creates de X markers
			//If dont exist Custom Data Set, Default set wil be Draw.
			Boolean drawSetDefault = (GetSetCount() == 0);

			double totalWidth = (XEnd - XStart);
			if (drawSetDefault)
			{
				int numBars = data["Default"].Count;
				int actualBar = 0;
				double maxWidthPerBar = (totalWidth - PaddingSets * 2) / (double)(numBars);
				foreach (BarData bd in data["Default"])
				{
					canvas.LineWidth = 0.5;
					canvas.SetSourceRGB(0, 0, 0);
					string value = bd.Name;

					canvas.MoveTo(XStart + PaddingSets + (actualBar * maxWidthPerBar)+ (maxWidthPerBar / 2) - ((value.Length * PixelsPerUnit)/2), YEnd + 15);
					canvas.ShowText(Encoding.UTF8.GetBytes(value));

					canvas.MoveTo(XStart + PaddingSets + (actualBar * maxWidthPerBar) + (maxWidthPerBar), YEnd + lenghtLineValue);
					canvas.LineTo(XStart + PaddingSets + (actualBar * maxWidthPerBar) + (maxWidthPerBar), YEnd);

					canvas.Stroke();

					actualBar++;
				}

				return;
			}

			int Sets = GetSetCount();
			double maxWidthPerSet = (totalWidth / (double)Sets);

			int actualSet = 0;
			foreach (String key in GetSetIds())
			{
				if (key.Equals("Default"))
					continue;

				int numBars = data[key].Count;
				double XSetStart = XStart + maxWidthPerSet * actualSet;
				double XSetEnd = XSetStart + maxWidthPerSet;

				canvas.LineWidth = 0.5;
				canvas.SetSourceRGB(0, 0, 0);
				string value = key;

				canvas.MoveTo(XSetStart + maxWidthPerSet / 2, YEnd + 15);
				canvas.ShowText(Encoding.UTF8.GetBytes(value));

				canvas.MoveTo(XSetEnd, YEnd + lenghtLineValue);
				canvas.LineTo(XSetEnd, YEnd);

				canvas.Stroke();
				actualSet++;
			}

		}

		bool DrawLegendAndTitle(int height, int width, Cairo.Context canvas)
		{

			canvas.MoveTo((width / 2) - ((Title.Length / 2) * widthByChar), Padding);
			canvas.ShowText(Encoding.UTF8.GetBytes(Title));

			Char[] Y = this.ValueLabel.ToCharArray();

			if (ShowLegend)
			{
				int count = GetSetCount();
					IconosLeyenda = new Dictionary<string, Cairo.Rectangle>();

					List<String> keys = data.Keys.ToList();
					keys.Sort();

					int ItemLegendMaxWidth = 0;
					int i = 0;
					foreach (String set in ColorSets.Keys)
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
			double totalWidth = (XEnd - XStart);

			//If dont exist Custom Data Set, Default set wil be Draw.
			Boolean drawSetDefault = (GetSetCount() == 0);

			if (drawSetDefault)
			{
				int numBars = data["Default"].Count;
				int actualBar = 0;
				double XSetStart = XStart + PaddingSets;
				//double XSetEnd = XSetStart + maxWidthPerSet;
				double maxWidthPerBar = (totalWidth - (PaddingSets * 2)) / (double)(numBars);
				foreach (BarData bd in data["Default"])
				{

					double altura = ((YEnd - YStart) * ((bd.Value - MinValue) / (MaxValue - MinValue)));
					Cairo.Rectangle rect = new Cairo.Rectangle(new Cairo.Point((int)(XSetStart + (maxWidthPerBar * actualBar) + maxWidthPerBar * 0.05), (int)YEnd), maxWidthPerBar * 0.9, -altura);
					canvas.Rectangle(rect);
					canvas.SetSourceRGB(ColorSets[bd.Name].R, ColorSets[bd.Name].G, ColorSets[bd.Name].B);
					canvas.Fill();
					canvas.Stroke();

					actualBar++;
				}

				return;
			}

			int Sets = GetSetCount();
			double maxWidthPerSet = (totalWidth / (double)Sets);

			int actualSet= 0;
			foreach (String key in GetSetIds())
			{
				if (key.Equals("Default"))
					continue;
				
				int numBars = data[key].Count;
				int actualBar = 0;
				double XSetStart = XStart + maxWidthPerSet * actualSet + PaddingSets;
				//double XSetEnd = XSetStart + maxWidthPerSet;
				double maxWidthPerBar = (maxWidthPerSet - (PaddingSets*2)) / (double)(numBars);
				foreach (BarData bd in data[key])
				{

					double altura = ((YEnd - YStart) * ((bd.Value - MinValue) / (MaxValue - MinValue)));
					Cairo.Rectangle rect = new Cairo.Rectangle(new Cairo.Point((int)(XSetStart + (maxWidthPerBar * actualBar) + maxWidthPerBar*0.05), (int)YEnd ), maxWidthPerBar * 0.9, - altura);
					canvas.Rectangle(rect);
					canvas.SetSourceRGB(ColorSets[bd.Name].R, ColorSets[bd.Name].G, ColorSets[bd.Name].B);
					canvas.Fill();
					canvas.Stroke();

					actualBar++;
				}
				actualSet++;
			}
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
		private CheckButton showGrid;
		private CheckButton showLegend;
		private Button menosPadd;
		private Button masPadd;
		private Button menosRes;
		private Button masRes;
		private ScrolledWindow swScroll;
		private Gtk.DrawingArea drawingArea;
		private Dictionary<String, List<BarData>> data;

	}
}
