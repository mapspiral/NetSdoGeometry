namespace NetSdoGeometry
{
    using System;
    using Oracle.DataAccess.Client;
    using Oracle.DataAccess.Types;

    [Serializable]
    [OracleCustomTypeMappingAttribute("MDSYS.SDO_POINT_TYPE")]
    public class SdoPoint : OracleCustomTypeBase<SdoPoint>
    {
        [OracleObjectMappingAttribute("X")]
        public decimal? X { get; set; }

        [OracleObjectMappingAttribute("Y")]
        public decimal? Y { get; set; }

        [OracleObjectMappingAttribute("Z")]
        public decimal? Z { get; set; }

        public double? XD
        {
            get { return System.Convert.ToDouble(this.x); }
            set { this.x = System.Convert.ToDecimal(value); }
        }

        public double? YD
        {
            get { return System.Convert.ToDouble(this.y); }
            set { this.y = System.Convert.ToDecimal(value); }
        }

        public double? ZD
        {
            get { return System.Convert.ToDouble(this.z); }
            set { this.z = System.Convert.ToDecimal(value); }
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
