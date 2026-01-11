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
            get { return this.X.HasValue ? System.Convert.ToDouble(this.X.Value) : (double?)null; }
            set { this.X = value.HasValue ? System.Convert.ToDecimal(value.Value) : (decimal?)null; }
        }

        public double? YD
        {
            get { return this.Y.HasValue ? System.Convert.ToDouble(this.Y.Value) : (double?)null; }
            set { this.Y = value.HasValue ? System.Convert.ToDecimal(value.Value) : (decimal?)null; }
        }

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
