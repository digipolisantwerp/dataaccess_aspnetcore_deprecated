using System;

namespace Digipolis.DataAccess.Attributes
{
	[AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
	public sealed class HasPrecisionAttribute : Attribute
	{
		public HasPrecisionAttribute(byte precision, byte scale)
		{
			this.Precision = precision;
			this.Scale = scale;
		}

		public byte Precision { get; set; }
		public byte Scale { get; set; }
	}
}
