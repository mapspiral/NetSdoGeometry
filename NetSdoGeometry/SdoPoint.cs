namespace NetSdoGeometry
{
    using System;
    using Oracle.DataAccess.Client;
    using Oracle.DataAccess.Types;

    /// <summary>
    /// Represents an Oracle Spatial SDO_POINT_TYPE object.
    /// Maps to the MDSYS.SDO_POINT_TYPE Oracle User Defined Type.
    /// Used for simple point geometries with X, Y, and optional Z coordinates.
    /// </summary>
    [Serializable]
    [OracleCustomTypeMappingAttribute("MDSYS.SDO_POINT_TYPE")]
    public class SdoPoint : OracleCustomTypeBase<SdoPoint>
    {
        /// <summary>
        /// Gets or sets the X coordinate (longitude or easting).
        /// </summary>
        [OracleObjectMappingAttribute("X")]
        public decimal? X { get; set; }

        /// <summary>
        /// Gets or sets the Y coordinate (latitude or northing).
        /// </summary>
        [OracleObjectMappingAttribute("Y")]
        public decimal? Y { get; set; }

        /// <summary>
        /// Gets or sets the Z coordinate (elevation or height).
        /// Optional; null for 2D points.
        /// </summary>
        [OracleObjectMappingAttribute("Z")]
        public decimal? Z { get; set; }

        /// <summary>
        /// Gets or sets the X coordinate as a double precision floating point value.
        /// Convenience property that converts between decimal and double.
        /// </summary>
        public double? XD
        {
            get { return this.X.HasValue ? System.Convert.ToDouble(this.X.Value) : (double?)null; }
            set { this.X = value.HasValue ? System.Convert.ToDecimal(value.Value) : (decimal?)null; }
        }

        /// <summary>
        /// Gets or sets the Y coordinate as a double precision floating point value.
        /// Convenience property that converts between decimal and double.
        /// </summary>
        public double? YD
        {
            get { return this.Y.HasValue ? System.Convert.ToDouble(this.Y.Value) : (double?)null; }
            set { this.Y = value.HasValue ? System.Convert.ToDecimal(value.Value) : (decimal?)null; }
        }

        /// <summary>
        /// Gets or sets the Z coordinate as a double precision floating point value.
        /// Convenience property that converts between decimal and double.
        /// </summary>
        public double? ZD
        {
            get { return this.Z.HasValue ? System.Convert.ToDouble(this.Z.Value) : (double?)null; }
            set { this.Z = value.HasValue ? System.Convert.ToDecimal(value.Value) : (decimal?)null; }
        }

        public override void MapFromCustomObject()
        {
            this.SetValue("X", this.X);
            this.SetValue("Y", this.Y);
            this.SetValue("Z", this.Z);
        }

        public override void MapToCustomObject()
        {
            this.X = this.GetValue<decimal?>("X");
            this.Y = this.GetValue<decimal?>("Y");
            this.Z = this.GetValue<decimal?>("Z");
        }
    }
}
