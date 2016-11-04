using System;
namespace Charts.Data
{
	public class BarData : IComparable
	{
		public double Value
		{
			get;
			set;
		}

		public String Name
		{
			get;
			set;
		}

		public BarData(String name, double value)
		{
			Value = value;
			Name = name;
		}

		public int CompareTo(object obj)
		{
			if (obj == null) return 1;

			BarData otherData = obj as BarData;
			if (otherData != null)
				return this.Name.CompareTo(otherData.Name);
			else
				throw new ArgumentException("Object is not a LineData");
		}
	}
}
