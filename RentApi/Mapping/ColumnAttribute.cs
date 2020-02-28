using System;
using System.Collections.Generic;
using System.Text;

namespace CPTech.CustomORM.Mapping
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ColumnAttribute : BaseMappingAttribute
    {
        //private string _ColumnName = null;
        public ColumnAttribute(string columnName) : base(columnName)
        {
            //this._ColumnName = columnName;
        }
        //public string GetMappingName()
        //{
        //    return this._ColumnName;
        //}
    }
}
