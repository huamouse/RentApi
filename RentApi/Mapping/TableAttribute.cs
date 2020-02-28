using System;
using System.Collections.Generic;
using System.Text;

namespace CPTech.CustomORM.Mapping
{
    [AttributeUsage(AttributeTargets.Class)]
    public class TableAttribute : BaseMappingAttribute
    {
        //private string _TableName = null;
        public TableAttribute(string tableName) : base(tableName)
        {
            //this._TableName = tableName;
        }
        //public string GetMappingName()
        //{
        //    return this._TableName;
        //}
    }
}
