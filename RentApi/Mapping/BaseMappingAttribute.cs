using System;
using System.Collections.Generic;
using System.Text;

namespace CPTech.CustomORM.Mapping
{
    public class BaseMappingAttribute : Attribute
    {
        private string _MappingName = null;
        public BaseMappingAttribute(string mappingName)
        {
            this._MappingName = mappingName;
        }
        public string GetMappingName()
        {
            return this._MappingName;
        }
    }
}
