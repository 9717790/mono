//
// System.Int32.cs
//
// Author:
//   Miguel de Icaza (miguel@ximian.com)
//
// (C) Ximian, Inc.  http://www.ximian.com
//

using System.Globalization;

namespace System {
	
	public struct Int32 : IComparable, IFormattable, IConvertible {
		private static Type Type = typeof (int);
		
		public const int MaxValue = 0x7fffffff;
		public const int MinValue = -2147483648;
		
		public int value;

		public int CompareTo (object v)
		{
			if (v == null)
				return 1;
			
			if (!(v is System.Int32))
				throw new ArgumentException (Locale.GetText ("Value is not a System.Int32"));

			int xv = (int) v;
			if (value == xv)
				return 0;
			if (value > xv)
				return 1;
			else
				return -1;
		}

		public override bool Equals (object o)
		{
			if (!(o is System.Int32))
				return false;

			return ((int) o) == value;
		}

		public override int GetHashCode ()
		{
			return value;
		}

		public static int Parse (string s)
		{
			int val = 0;
			int len;
			int i;
			bool neg = false;
			bool digits_seen = false;
			
			if (s == null)
				throw new ArgumentNullException (Locale.GetText ("s is null"));

			len = s.Length;

			char c;
			for (i = 0; i < len; i++){
				c = s [i];
				if (!Char.IsWhiteSpace (c))
					break;
			}
			
			if (i == len)
				throw new FormatException ();

			c = s [i];
			if (c == '+')
				i++;
			else if (c == '-'){
				neg = true;
				i++;
			}
			
			for (; i < len; i++){
				c = s [i];

				if (c >= '0' && c <= '9'){
					val = checked (val * 10 + (c - '0'));
					digits_seen = true;
				} else {
					if (Char.IsWhiteSpace (c)){
						for (i++; i < len; i++){
							if (!Char.IsWhiteSpace (s [i]))
								throw new FormatException ();
						}
						break;
					} else
						throw new FormatException ();
				}
			}
			if (!digits_seen)
				throw new FormatException ();
			
			if (neg)
				val = -val;

			return val;
		}

		public static int Parse (string s, IFormatProvider fp)
		{
			return Parse (s, NumberStyles.Integer, fp);
		}

		public static int Parse (string s, NumberStyles style)
		{
			return Parse (s, style, null);
		}

		[MonoTODO]
		public static int Parse (string s, NumberStyles style, IFormatProvider fp)
		{
			// FIXME: Better than nothing ;-)
			return Parse (s);
			//throw new NotImplementedException ();
		}

		public override string ToString ()
		{
			return ToString ("G", null);
		}

		public string ToString (IFormatProvider fp)
		{
			return ToString ("G", fp);
		}

		public string ToString (string format)
		{
			return ToString (format, null);
		}

		public string ToString (string format, IFormatProvider fp)
		{
			string fmt;
			NumberFormatInfo nfi;
			
			fmt = (format == null) ? "G" : format;
			
			if (fp == null)
				nfi = NumberFormatInfo.CurrentInfo;
			else {
				nfi = (NumberFormatInfo) fp.GetFormat (Type);
				
				if (nfi == null)
					nfi = NumberFormatInfo.CurrentInfo;
			}

			return IntegerFormatter.NumberToString (fmt, nfi, value);
		}

		// =========== IConvertible Methods =========== //

		public TypeCode GetTypeCode ()
		{
			return TypeCode.Int32;
		}
		
		public bool     ToBoolean  (IFormatProvider provider)
		{
			return System.Convert.ToBoolean (value);
		}
		public byte     ToByte     (IFormatProvider provider)
		{
			return System.Convert.ToByte (value);
		}
		public char     ToChar     (IFormatProvider provider)
		{
			return System.Convert.ToChar (value);
		}
		public DateTime ToDateTime (IFormatProvider provider)
		{
			throw new NotImplementedException ();
		}
		public decimal  ToDecimal  (IFormatProvider provider)
		{
			return System.Convert.ToDecimal (value);
		}
		public double   ToDouble   (IFormatProvider provider)
		{
			return System.Convert.ToDouble (value);
		}
		public short    ToInt16    (IFormatProvider provider)
		{
			return System.Convert.ToInt16 (value);
		}
		public int      ToInt32    (IFormatProvider provider)
		{
			return value;
		}
		public long     ToInt64    (IFormatProvider provider)
		{
			return System.Convert.ToInt64 (value);
		}
    		[CLSCompliant(false)]
		public sbyte    ToSByte    (IFormatProvider provider)
		{
			return System.Convert.ToSByte (value);
		}
		public float    ToSingle   (IFormatProvider provider)
		{
			return System.Convert.ToSingle (value);
		}
		public object   ToType     (Type conversionType, IFormatProvider provider)
		{
			throw new NotImplementedException ();
		}
    		[CLSCompliant(false)]
		public ushort   ToUInt16   (IFormatProvider provider)
		{
			return System.Convert.ToUInt16 (value);
		}
    		[CLSCompliant(false)]
		public uint     ToUInt32   (IFormatProvider provider)
		{
			return System.Convert.ToUInt32 (value);
		}
    		[CLSCompliant(false)]
		public ulong    ToUInt64   (IFormatProvider provider)
		{
			return System.Convert.ToUInt64 (value);
		}
	}
}
