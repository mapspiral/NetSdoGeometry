using System;
using Oracle.DataAccess.Types;
using Oracle.DataAccess.Client;

namespace NetSdoGeometry
{
    [Serializable]
    [OracleCustomTypeMappingAttribute("MDSYS.SDO_POINT_TYPE")]
    public class SDOPOINT : OracleCustomTypeBase<SDOPOINT>
    {
        private decimal? x;
        [OracleObjectMappingAttribute("X")]
        public decimal? X
        {
            get { return x; }
            set { x = value; }
        }
        public double? XD
        {
            get { return System.Convert.ToDouble(x); }
            set { x = System.Convert.ToDecimal(value); }
        }


        private decimal? y;
        [OracleObjectMappingAttribute("Y")]
        public decimal? Y
        {
            get { return y; }
            set { y = value; }
        }
        public double? YD
        {
            get { return System.Convert.ToDouble(y); }
            set { y = System.Convert.ToDecimal(value); }
        }


        private decimal? z;
        [OracleObjectMappingAttribute("Z")]
        public decimal? Z
        {
            get { return z; }
            set { z = value; }
        }
        public double? ZD
        {
            get { return System.Convert.ToDouble(z); }
            set { z = System.Convert.ToDecimal(value); }
        }


        public override void MapFromCustomObject()
        {
            SetValue("X", x);
            SetValue("Y", y);
            SetValue("Z", z);
        }

        public override void MapToCustomObject()
        {
            X = GetValue<decimal?>("X");
            Y = GetValue<decimal?>("Y");
            Z = GetValue<decimal?>("Z");
        }
    }
}
