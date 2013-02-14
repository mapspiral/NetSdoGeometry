using System;
using System.Text;
using Oracle.DataAccess.Types;

namespace NetSdoGeometry
{
    [Serializable]
    public static class sdogeometryTypes
    {
        //Oracle Documentation for SDO_ETYPE - SIMPLE
        //Point//Line//Polygon//exterior counterclockwise - polygon ring = 1003//interior clockwise  polygon ring = 2003
        public enum ETYPE_SIMPLE { POINT = 1, LINE = 2, POLYGON = 3, POLYGON_EXTERIOR = 1003, POLYGON_INTERIOR = 2003 }
        //Oracle Documentation for SDO_ETYPE - COMPOUND
        //1005: exterior polygon ring (must be specified in counterclockwise order)
        //2005: interior polygon ring (must be specified in clockwise order)
        public enum ETYPE_COMPOUND { FOURDIGIT = 4, POLYGON_EXTERIOR = 1005, POLYGON_INTERIOR = 2005 }
        //Oracle Documentation for SDO_GTYPE.
        //This represents the last two digits in a GTYPE, where the first item is dimension(ality) and the second is LRS
        public enum GTYPE { UNKNOWN_GEOMETRY = 00, POINT = 01, LINE = 02, CURVE = 02, POLYGON = 03, COLLECTION = 04, MULTIPOINT = 05, MULTILINE = 06, MULTICURVE = 06, MULTIPOLYGON = 07 }
        public enum DIMENSION { DIM2D = 2, DIM3D = 3, LRS_DIM3 = 3, LRS_DIM4 = 4 }
    }
    [Serializable]
    [OracleCustomTypeMappingAttribute("MDSYS.SDO_GEOMETRY")]
    public class sdogeometry : OracleCustomTypeBase<sdogeometry>
    {
        private enum OracleObjectColumns { SDO_GTYPE, SDO_SRID, SDO_POINT, SDO_ELEM_INFO, SDO_ORDINATES }

        private decimal? _sdo_gtype;
        [OracleObjectMappingAttribute(0)]
        public decimal? sdo_gtype
        {
            get { return _sdo_gtype; }
            set { _sdo_gtype = value;}
        }
        public int sdo_gtypeAsInt
        {
            get { return System.Convert.ToInt32(sdo_gtype); }
        }

        private decimal? _sdo_srid;
        [OracleObjectMappingAttribute(1)]
        public decimal? sdo_srid
        {
            get { return _sdo_srid; }
            set { _sdo_srid = value; }
        }
        public int sdo_sridAsInt
        {
            get { return System.Convert.ToInt32(sdo_srid); }
            set { sdo_srid = System.Convert.ToDecimal(value); }
        }

        private SdoPoint _sdopoint;
        [OracleObjectMappingAttribute(2)]
        public SdoPoint sdo_point
        {
            get { return _sdopoint; }
            set { _sdopoint = value; }
        }

        private decimal[] elemArray;
        [OracleObjectMappingAttribute(3)]
        public decimal[] ElemArray
        {
            get { return elemArray; }
            set { elemArray = value; }
        }

        private decimal[] ordinatesArray;
        [OracleObjectMappingAttribute(4)]
        public decimal[] OrdinatesArray
        {
            get { return ordinatesArray; }
            set { ordinatesArray = value; }
        }

        [OracleCustomTypeMappingAttribute("MDSYS.SDO_ELEM_INFO_ARRAY")]
        public class ElemArrayFactory : OracleArrayTypeFactoryBase<decimal> { }

        [OracleCustomTypeMappingAttribute("MDSYS.SDO_ORDINATE_ARRAY")]
        public class OrdinatesArrayFactory : OracleArrayTypeFactoryBase<decimal> { }

        public override void MapFromCustomObject()
        {
            SetValue((int)OracleObjectColumns.SDO_GTYPE, sdo_gtype);
            SetValue((int)OracleObjectColumns.SDO_SRID, sdo_srid);
            SetValue((int)OracleObjectColumns.SDO_POINT, sdo_point);
            SetValue((int)OracleObjectColumns.SDO_ELEM_INFO, ElemArray);
            SetValue((int)OracleObjectColumns.SDO_ORDINATES, OrdinatesArray);
        }

        public override void MapToCustomObject()
        {
            sdo_gtype = GetValue<decimal?>((int)OracleObjectColumns.SDO_GTYPE);
            sdo_srid = GetValue<decimal?>((int)OracleObjectColumns.SDO_SRID);
            sdo_point = GetValue<SdoPoint>((int)OracleObjectColumns.SDO_POINT);
            ElemArray = GetValue<decimal[]>((int)OracleObjectColumns.SDO_ELEM_INFO);
            OrdinatesArray = GetValue<decimal[]>((int)OracleObjectColumns.SDO_ORDINATES);
        }

        public int[] ElemArrayOfInts
        {
            get
            {
                int[] elems = null;
                if (this.elemArray != null)
                {
                    elems = new int[this.elemArray.Length];
                    for (int k = 0; k < this.elemArray.Length; k++)
                    {
                        elems[k] = System.Convert.ToInt32(this.elemArray[k]);
                    }
                }
                return elems;
            }
            set
            {
                if (value != null)
                {
                    int c = value.GetLength(0);
                    this.elemArray = new decimal[c];
                    for (int k = 0; k < c; k++)
                    {
                        elemArray[k] = System.Convert.ToDecimal(value[k]);
                    }
                }
                else
                {
                    this.elemArray = null;
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
                    elems = new double[this.ordinatesArray.Length];
                    for (int k = 0; k < this.ordinatesArray.Length; k++)
                    {
                        elems[k] = System.Convert.ToDouble(this.ordinatesArray[k]);
                    }
                }
                return elems;
            }
            set
            {
                if (value != null)
                {
                    int c = value.GetLength(0);
                    this.ordinatesArray = new decimal[c];
                    for (int k = 0; k < c; k++)
                    {
                        ordinatesArray[k] = System.Convert.ToDecimal(value[k]);
                    }
                }
                else
                {
                    this.ordinatesArray = null;
                }
            }
        }
        private int _Dimensionality;
        public int Dimensionality
        {
            get { return _Dimensionality; }
            set { _Dimensionality = value; }
        }
        private int _LRS;
        public int LRS
        {
            get { return _LRS; }
            set { _LRS = value; }
        }
        private int _GeometryType;
        public int GeometryType
        {
            get { return _GeometryType; }
            set { _GeometryType = value; }
        }
        public int PropertiesFromGTYPE()
        {
            if ((int)this._sdo_gtype != 0)
            {
                int v = (int)this._sdo_gtype;
                int dim = v / 1000;
                Dimensionality = dim;
                v -= dim * 1000;
                int lrsDim = v / 100;
                LRS = lrsDim;
                v -= lrsDim * 100;
                GeometryType = v;
                return (Dimensionality * 1000) + (LRS * 100) + GeometryType;
            }
            else
                return 0;
        }
        public int PropertiesToGTYPE()
        {
            int v = Dimensionality * 1000;
            v = v + (LRS * 100);
            v = v + GeometryType;
            this._sdo_gtype = System.Convert.ToDecimal(v);
            return (v);
        }
        public string AsText
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("MDSYS.SDO_GEOMETRY(");
                sb.Append((sdo_gtype != null) ? sdo_gtype.ToString() : "null");
                sb.Append(",");
                sb.Append((sdo_srid != null) ? sdo_srid.ToString() : "null");
                sb.Append(",");
                // begin point
                if (sdo_point != null)
                {
                    sb.Append("MDSYS.SDO_POINT_TYPE(");
                    string _tmp = string.Format("{0:#.##########},{1:#.##########}{2}{3:#.##########}",
                                                    sdo_point.X,
                                                    sdo_point.Y,
                                                    (sdo_point.Z == null) ? null : ",",
                                                    sdo_point.Z);
                    
                    sb.Append(_tmp.Trim());
                    sb.Append(")");
                }
                else
                {
                    sb.Append("null");
                }
                sb.Append(",");
                // begin element array
                if (elemArray != null)
                {
                    sb.Append("MDSYS.SDO_ELEM_INFO_ARRAY(");
                    for (int i = 0; i < elemArray.Length; i++)
                    {
                        string _tmp = string.Format("{0}", elemArray[i]);
                        sb.Append(_tmp);
                        if (i < (elemArray.Length - 1))
                            sb.Append(",");
                    }
                    sb.Append(")");
                }
                else
                {
                    sb.Append("null");
                }
                sb.Append(",");
                // begin ordinates array
                if (ordinatesArray != null)
                {
                    sb.Append("MDSYS.SDO_ORDINATE_ARRAY(");
                    for (int i = 0; i < ordinatesArray.Length; i++)
                    {
                        string _tmp = string.Format("{0:#.##########}", ordinatesArray[i]);
                        sb.Append(_tmp);
                        if (i < (ordinatesArray.Length - 1))
                            sb.Append(",");
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
        public override string ToString()
        {
            return this.AsText;
        }
    }
}