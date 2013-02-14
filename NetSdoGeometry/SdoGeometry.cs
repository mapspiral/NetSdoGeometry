namespace NetSdoGeometry
{
    using System;
    using System.Text;
    using Oracle.DataAccess.Types;

    [Serializable]
    [OracleCustomTypeMappingAttribute("MDSYS.SDO_GEOMETRY")]
    public class SdoGeometry : OracleCustomTypeBase<SdoGeometry>
    {
        private int _GeometryType;
        private int dimensionality;

        [OracleObjectMappingAttribute(0)]
        public decimal? sdo_gtype { get; set; }

        public int sdo_gtypeAsInt
        {
            get { return System.Convert.ToInt32(this.sdo_gtype); }
        }

        [OracleObjectMappingAttribute(1)]
        public decimal? sdo_srid { get; set; }
        
        public int sdo_sridAsInt
        {
            get { return System.Convert.ToInt32(this.sdo_srid); }
            set { this.sdo_srid = System.Convert.ToDecimal(value); }
        }

        [OracleObjectMappingAttribute(2)]
        public SdoPoint sdo_point { get; set; }

        [OracleObjectMappingAttribute(3)]
        public decimal[] ElemArray { get; set; }

        [OracleObjectMappingAttribute(4)]
        public decimal[] OrdinatesArray { get; set; }

        public int[] ElemArrayOfInts
        {
            get
            {
                int[] elems = null;
                if (this.ElemArray != null)
                {
                    elems = new int[this.ElemArray.Length];
                    for (int k = 0; k < this.ElemArray.Length; k++)
                    {
                        elems[k] = System.Convert.ToInt32(this.ElemArray[k]);
                    }
                }

                return elems;
            }

            set
            {
                if (value != null)
                {
                    int c = value.GetLength(0);
                    this.ElemArray = new decimal[c];
            
                    for (int k = 0; k < c; k++)
                    {
                        this.ElemArray[k] = System.Convert.ToDecimal(value[k]);
                    }
                }
                else
                {
                    this.ElemArray = null;
                }
            }
        }

        public double[] OrdinatesArrayOfDoubles
        {
            get
            {
                double[] elems = null;
                if (this.OrdinatesArray != null)
                {
                    elems = new double[this.OrdinatesArray.Length];
                    for (int k = 0; k < this.OrdinatesArray.Length; k++)
                    {
                        elems[k] = System.Convert.ToDouble(this.OrdinatesArray[k]);
                    }
                }

                return elems;
            }

            set
            {
                if (value != null)
                {
                    int c = value.GetLength(0);
                    this.OrdinatesArray = new decimal[c];
                    for (int k = 0; k < c; k++)
                    {
                        this.OrdinatesArray[k] = System.Convert.ToDecimal(value[k]);
                    }
                }
                else
                {
                    this.OrdinatesArray = null;
                }
            }
        }

        public int Dimensionality
        {
            get { return this.dimensionality; }
            set { this.dimensionality = value; }
        }

        public int LRS { get; set; }
           
        public int GeometryType
        {
            get { return this._GeometryType; }
            set { this._GeometryType = value; }
        }

        public string AsText
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("MDSYS.SDO_GEOMETRY(");
                sb.Append((this.sdo_gtype != null) ? this.sdo_gtype.ToString() : "null");
                sb.Append(",");
                sb.Append((this.sdo_srid != null) ? this.sdo_srid.ToString() : "null");
                sb.Append(",");
                
                // begin point
                if (this.sdo_point != null)
                {
                    sb.Append("MDSYS.SDO_POINT_TYPE(");
                    string _tmp = string.Format(
                        "{0:#.##########},{1:#.##########}{2}{3:#.##########}",
                        this.sdo_point.X,
                        this.sdo_point.Y,
                        (this.sdo_point.Z == null) ? null : ",",
                        this.sdo_point.Z);
                    
                    sb.Append(_tmp.Trim());
                    sb.Append(")");
                }
                else
                {
                    sb.Append("null");
                }

                sb.Append(",");
            
                // begin element array
                if (this.ElemArray != null)
                {
                    sb.Append("MDSYS.SDO_ELEM_INFO_ARRAY(");
                    for (int i = 0; i < this.ElemArray.Length; i++)
                    {
                        string _tmp = string.Format("{0}", this.ElemArray[i]);
                        sb.Append(_tmp);
                        if (i < (this.ElemArray.Length - 1))
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
                if (this.OrdinatesArray != null)
                {
                    sb.Append("MDSYS.SDO_ORDINATE_ARRAY(");
                    for (int i = 0; i < this.OrdinatesArray.Length; i++)
                    {
                        string _tmp = string.Format("{0:#.##########}", this.OrdinatesArray[i]);
                        sb.Append(_tmp);
                        if (i < (this.OrdinatesArray.Length - 1))
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
            this.SetValue((int)OracleObjectColumns.SDO_GTYPE, this.sdo_gtype);
            this.SetValue((int)OracleObjectColumns.SDO_SRID, this.sdo_srid);
            this.SetValue((int)OracleObjectColumns.SDO_POINT, this.sdo_point);
            this.SetValue((int)OracleObjectColumns.SDO_ELEM_INFO, this.ElemArray);
            this.SetValue((int)OracleObjectColumns.SDO_ORDINATES, this.OrdinatesArray);
        }

        public override void MapToCustomObject()
        {
            this.sdo_gtype = this.GetValue<decimal?>((int)OracleObjectColumns.SDO_GTYPE);
            this.sdo_srid = this.GetValue<decimal?>((int)OracleObjectColumns.SDO_SRID);
            this.sdo_point = this.GetValue<SdoPoint>((int)OracleObjectColumns.SDO_POINT);
            this.ElemArray = this.GetValue<decimal[]>((int)OracleObjectColumns.SDO_ELEM_INFO);
            this.OrdinatesArray = this.GetValue<decimal[]>((int)OracleObjectColumns.SDO_ORDINATES);
        }

        public int PropertiesFromGTYPE()
        {
            if ((int)this.sdo_gtype != 0)
            {
                int v = (int)this.sdo_gtype;
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

        public int PropertiesToGTYPE()
        {
            int v = this.Dimensionality * 1000;
            v = v + (this.LRS * 100);
            v = v + this.GeometryType;

            this.sdo_gtype = System.Convert.ToDecimal(v);

            return v;
        }

        public override string ToString()
        {
            return this.AsText;
        }
    }
}