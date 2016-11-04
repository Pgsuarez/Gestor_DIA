using System;
namespace Charts.Data
{
	public class LineData : IComparable
	{
		public double X
		{
			get;
			set;
		}
		public double Y
		{
			get;
			set;
		}

		public LineData(double x, double y)
		{
			Y = y;
			X = x;
		}

		public int CompareTo(object obj)
		{
			if (obj == null) return 1;

			LineData otherData = obj as LineData;
			if (otherData != null)
				return this.X.CompareTo(otherData.X);
			else
				throw new ArgumentException("Object is not a LineData");
		}
	}
}
