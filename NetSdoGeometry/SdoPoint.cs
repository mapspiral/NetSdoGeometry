namespace NetSdoGeometry
{
    using System;
    using Oracle.DataAccess.Client;
    using Oracle.DataAccess.Types;

    [Serializable]
    [OracleCustomTypeMappingAttribute("MDSYS.SDO_POINT_TYPE")]
    public class SdoPoint : OracleCustomTypeBase<SdoPoint>
    {
        private decimal? x;
        
        [OracleObjectMappingAttribute("X")]
        public decimal? X
        {
            get { return this.x; }
            set { this.x = value; }
        }

        public double? XD
        {
            get { return System.Convert.ToDouble(this.x); }
            set { this.x = System.Convert.ToDecimal(value); }
        }

        private decimal? y;

        [OracleObjectMappingAttribute("Y")]
        public decimal? Y
        {
            get { return this.y; }
            set { this.y = value; }
        }
        
        public double? YD
        {
            get { return System.Convert.ToDouble(this.y); }
            set { this.y = System.Convert.ToDecimal(value); }
        }

        private decimal? z;
        
        [OracleObjectMappingAttribute("Z")]
        public decimal? Z
        {
            get { return this.z; }
            set { this.z = value; }
        }
        
        public double? ZD
        {
            get { return System.Convert.ToDouble(this.z); }
            set { this.z = System.Convert.ToDecimal(value); }
        }

        public override void MapFromCustomObject()
        {
            this.SetValue("X", this.x);
            this.SetValue("Y", this.y);
            this.SetValue("Z", this.z);
        }

        public override void MapToCustomObject()
        {
            this.X = this.GetValue<decimal?>("X");
            this.Y = this.GetValue<decimal?>("Y");
            this.Z = this.GetValue<decimal?>("Z");
        }
    }
}
