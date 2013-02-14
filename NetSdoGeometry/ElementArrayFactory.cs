// -----------------------------------------------------------------------
// <copyright file="ElementArrayFactory.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace NetSdoGeometry
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Oracle.DataAccess.Types;

    [OracleCustomTypeMappingAttribute("MDSYS.SDO_ELEM_INFO_ARRAY")]
    protected class ElemArrayFactory : OracleArrayTypeFactoryBase<decimal> 
    { 
    }
}
