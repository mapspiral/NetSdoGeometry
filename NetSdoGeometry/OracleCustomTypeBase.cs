namespace NetSdoGeometry
{
    using System;
    using Oracle.DataAccess.Client;
    using Oracle.DataAccess.Types;

    [Serializable]
    public abstract class OracleCustomTypeBase<T> : INullable, IOracleCustomType, IOracleCustomTypeFactory
    where T : OracleCustomTypeBase<T>, new()
    {
        private static string errorMessageHead = "Error converting Oracle User Defined Type to .Net Type " + typeof(T).ToString() + ", oracle column is null, failed to map to . NET valuetype, column ";
        [NonSerialized]
        private OracleConnection connection;
        private IntPtr pUdt;
        private bool isNull;

        public virtual bool IsNull
        {
            get
            {
                return this.isNull;
            }
        }

        public static T Null
        {
            get
            {
                T t = new T();
                t.isNull = true;
                return t;
            }
        }

        public IOracleCustomType CreateObject()
        {
            return new T();
        }

        protected void SetConnectionAndPointer(OracleConnection _connection, IntPtr _pUdt)
        {
            this.connection = _connection;
            this.pUdt = _pUdt;
        }

        public abstract void MapFromCustomObject();

        public abstract void MapToCustomObject();

        public void FromCustomObject(OracleConnection con, IntPtr pUdt)
        {
            this.SetConnectionAndPointer(con, pUdt);
            this.MapFromCustomObject();
        }

        public void ToCustomObject(OracleConnection con, IntPtr pUdt)
        {
            this.SetConnectionAndPointer(con, pUdt);
            this.MapToCustomObject();
        }

        protected void SetValue(string oracleColumnName, object value)
        {
            if (value != null)
            {
                OracleUdt.SetValue(this.connection, this.pUdt, oracleColumnName, value);
            }
        }

        protected void SetValue(int oracleColumn_Id, object value)
        {
            if (value != null)
            {
                OracleUdt.SetValue(this.connection, this.pUdt, oracleColumn_Id, value);
            }
        }

        protected U GetValue<U>(string oracleColumnName)
        {
            if (OracleUdt.IsDBNull(this.connection, this.pUdt, oracleColumnName))
            {
                if (default(U) is ValueType)
                {
                    throw new Exception(errorMessageHead + oracleColumnName.ToString() + " of value type " + typeof(U).ToString());
                }
                else
                {
                    return default(U);
                }
            }
            else
            {
                return (U)OracleUdt.GetValue(this.connection, this.pUdt, oracleColumnName);
            }
        }

        protected U GetValue<U>(int oracleColumn_Id)
        {
            if (OracleUdt.IsDBNull(this.connection, this.pUdt, oracleColumn_Id))
            {
                if (default(U) is ValueType)
                {
                    throw new Exception(errorMessageHead + oracleColumn_Id.ToString() + " of value type " + typeof(U).ToString());
                }
                else
                {
                    return default(U);
                }
            }
            else
            {
                return (U)OracleUdt.GetValue(this.connection, this.pUdt, oracleColumn_Id);
            }
        }
    }
}