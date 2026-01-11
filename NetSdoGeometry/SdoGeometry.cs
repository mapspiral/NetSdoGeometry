namespace NetSdoGeometry
{
    using System;
    using System.Text;
    using Oracle.DataAccess.Types;

    /// <summary>
    /// Represents an Oracle Spatial SDO_GEOMETRY object.
    /// Maps to the MDSYS.SDO_GEOMETRY Oracle User Defined Type.
    /// </summary>
    [Serializable]
    [OracleCustomTypeMappingAttribute("MDSYS.SDO_GEOMETRY")]
    public class SdoGeometry : OracleCustomTypeBase<SdoGeometry>
    {
        /// <summary>
        /// Gets or sets the geometry type information.
        /// Format: [Dimension][LRS][GeometryType] (e.g., 2003 = 2D Polygon).
        /// </summary>
        [OracleObjectMappingAttribute(0)]
        public decimal? SdoGtype { get; set; }

        /// <summary>
        /// Gets or sets the Spatial Reference System Identifier.
        /// Defines the coordinate system for the geometry.
        /// </summary>
        [OracleObjectMappingAttribute(1)]
        public decimal? SdoSRID { get; set; }

        /// <summary>
        /// Gets or sets the point geometry for simple point types.
        /// Only populated for point geometries; null for other types.
        /// </summary>
        [OracleObjectMappingAttribute(2)]
        public SdoPoint SdoPoint { get; set; }

        /// <summary>
        /// Gets or sets the element information array.
        /// Describes the geometry structure with triplets of (offset, element type, interpretation).
        /// </summary>
        [OracleObjectMappingAttribute(3)]
        public decimal[] SdoElemInfo { get; set; }

        /// <summary>
        /// Gets or sets the ordinates (coordinate values) array.
        /// Contains the actual coordinate data for the geometry.
        /// </summary>
        [OracleObjectMappingAttribute(4)]
        public decimal[] SdoOrdinates { get; set; }

        /// <summary>
        /// Gets the geometry type as an integer value.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when SdoGtype is null.</exception>
        public int SdoGtypeAsInt
        {
            get
            {
                if (!this.SdoGtype.HasValue)
                {
                    throw new InvalidOperationException("SdoGtype is null and cannot be converted to int.");
                }

                return System.Convert.ToInt32(this.SdoGtype.Value);
            }
        }

        /// <summary>
        /// Gets or sets the Spatial Reference System Identifier as an integer.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when getting and SdoSRID is null.</exception>
        public int SdoSRIDAsInt
        {
            get
            {
                if (!this.SdoSRID.HasValue)
                {
                    throw new InvalidOperationException("SdoSRID is null and cannot be converted to int.");
                }

                return System.Convert.ToInt32(this.SdoSRID.Value);
            }

            set
            {
                this.SdoSRID = System.Convert.ToDecimal(value);
            }
        }

        /// <summary>
        /// Gets or sets the element information array as integers.
        /// Convenience property that converts between decimal and int arrays.
        /// </summary>
        public int[] ElemArrayOfInts
        {
            get
            {
                if (this.SdoElemInfo == null)
                {
                    return null;
                }

                return System.Array.ConvertAll(this.SdoElemInfo, d => System.Convert.ToInt32(d));
            }

            set
            {
                if (value == null)
                {
                    this.SdoElemInfo = null;
                }
                else
                {
                    this.SdoElemInfo = System.Array.ConvertAll(value, i => System.Convert.ToDecimal(i));
                }
            }
        }

        /// <summary>
        /// Gets or sets the ordinates array as doubles.
        /// Convenience property that converts between decimal and double arrays.
        /// </summary>
        public double[] OrdinatesArrayOfDoubles
        {
            get
            {
                if (this.SdoOrdinates == null)
                {
                    return null;
                }

                return System.Array.ConvertAll(this.SdoOrdinates, d => System.Convert.ToDouble(d));
            }

            set
            {
                if (value == null)
                {
                    this.SdoOrdinates = null;
                }
                else
                {
                    this.SdoOrdinates = System.Array.ConvertAll(value, d => System.Convert.ToDecimal(d));
                }
            }
        }

        /// <summary>
        /// Gets or sets the dimensionality of the geometry (2D, 3D, etc.).
        /// Extracted from the SdoGtype value.
        /// </summary>
        public int Dimensionality { get; set; }

        /// <summary>
        /// Gets or sets the Linear Referencing System (LRS) dimension.
        /// Extracted from the SdoGtype value.
        /// </summary>
        public int LRS { get; set; }

        /// <summary>
        /// Gets or sets the geometry type (Point, Line, Polygon, etc.).
        /// Extracted from the SdoGtype value.
        /// </summary>
        public int GeometryType { get; set; }

        /// <summary>
        /// Gets a SQL-compatible string representation of the geometry.
        /// Returns the geometry in Oracle SDO_GEOMETRY constructor format.
        /// </summary>
        public string AsText
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("MDSYS.SDO_GEOMETRY(");
                sb.Append((this.SdoGtype != null) ? this.SdoGtype.ToString() : "null");
                sb.Append(",");
                sb.Append((this.SdoSRID != null) ? this.SdoSRID.ToString() : "null");
                sb.Append(",");
                
                // begin point
                if (this.SdoPoint != null)
                {
                    sb.Append("MDSYS.SDO_POINT_TYPE(");
                    sb.Append(string.Format(
                        "{0:#.##########},{1:#.##########}{2}{3:#.##########}",
                        this.SdoPoint.X,
                        this.SdoPoint.Y,
                        (this.SdoPoint.Z == null) ? null : ",",
                        this.SdoPoint.Z).Trim());
                    sb.Append(")");
                }
                else
                {
                    sb.Append("null");
                }

                sb.Append(",");
            
                // begin element array
                if (this.SdoElemInfo != null)
                {
                    sb.Append("MDSYS.SDO_ELEM_INFO_ARRAY(");
                    for (int i = 0; i < this.SdoElemInfo.Length; i++)
                    {
                        sb.Append(string.Format("{0}", this.SdoElemInfo[i]));
                        if (i < (this.SdoElemInfo.Length - 1))
                        {
                            sb.Append(",");
                        }
                    }

                    sb.Append(")");
                }
                else
                {
                    sb.Append("null");
                }
                
                sb.Append(",");
                
                // begin ordinates array
                if (this.SdoOrdinates != null)
                {
                    sb.Append("MDSYS.SDO_ORDINATE_ARRAY(");
                    for (int i = 0; i < this.SdoOrdinates.Length; i++)
                    {
                        sb.Append(string.Format("{0:#.##########}", this.SdoOrdinates[i]));
                        if (i < (this.SdoOrdinates.Length - 1))
                        {
                            sb.Append(",");
                        }
                    }

                    sb.Append(")");
                }
                else
                {
                    sb.Append("null");
                }
                
                sb.Append(")");
                
                return sb.ToString();
            }
        }

        public override void MapFromCustomObject()
        {
            this.SetValue((int)OracleObjectColumns.SDO_GTYPE, this.SdoGtype);
            this.SetValue((int)OracleObjectColumns.SDO_SRID, this.SdoSRID);
            this.SetValue((int)OracleObjectColumns.SDO_POINT, this.SdoPoint);
            this.SetValue((int)OracleObjectColumns.SDO_ELEM_INFO, this.SdoElemInfo);
            this.SetValue((int)OracleObjectColumns.SDO_ORDINATES, this.SdoOrdinates);
        }

        public override void MapToCustomObject()
        {
            this.SdoGtype = this.GetValue<decimal?>((int)OracleObjectColumns.SDO_GTYPE);
            this.SdoSRID = this.GetValue<decimal?>((int)OracleObjectColumns.SDO_SRID);
            this.SdoPoint = this.GetValue<SdoPoint>((int)OracleObjectColumns.SDO_POINT);
            this.SdoElemInfo = this.GetValue<decimal[]>((int)OracleObjectColumns.SDO_ELEM_INFO);
            this.SdoOrdinates = this.GetValue<decimal[]>((int)OracleObjectColumns.SDO_ORDINATES);
        }

        /// <summary>
        /// Extracts Dimensionality, LRS, and GeometryType from the SdoGtype value.
        /// Populates the Dimensionality, LRS, and GeometryType properties.
        /// </summary>
        /// <returns>The reconstructed GTYPE value, or 0 if SdoGtype is null or 0.</returns>
        public int PropertiesFromGTYPE()
        {
            if (this.SdoGtype.HasValue && this.SdoGtype.Value != 0)
            {
                int v = (int)this.SdoGtype.Value;
                int dim = v / 1000;
                this.Dimensionality = dim;
                v -= dim * 1000;
                int lrsDim = v / 100;
                this.LRS = lrsDim;
                v -= lrsDim * 100;
                this.GeometryType = v;
                return (this.Dimensionality * 1000) + (this.LRS * 100) + this.GeometryType;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Constructs the SdoGtype value from Dimensionality, LRS, and GeometryType properties.
        /// Updates the SdoGtype property with the computed value.
        /// </summary>
        /// <returns>The computed GTYPE value.</returns>
        public int PropertiesToGTYPE()
        {
            int v = this.Dimensionality * 1000;
            v = v + (this.LRS * 100);
            v = v + this.GeometryType;

            this.SdoGtype = System.Convert.ToDecimal(v);

            return v;
        }

        public override string ToString()
        {
            return this.AsText;
        }
    }
}