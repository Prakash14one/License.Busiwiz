using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections;
using System.Data.SqlClient;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

/// <summary>
/// Summary description for DBParams
/// </summary>
namespace DS.Common
{
    public class DBParams : CollectionBase, ICloneable
    {

        public SqlParameter this[int index]
        {
            get { return ((SqlParameter)List[index]); }
            set { List[index] = value; }
        }

        public bool Contains(SqlParameter itemValue)
        {
            return List.Contains(itemValue);
        }
        public int Add(SqlParameter itemValue)
        {
            return (List.Add(itemValue));
        }
        public int AddAt(SqlParameter itemValue, int Index)
        {
            List.Insert(Index, itemValue);
            return Index;
        }

        public void Remove(SqlParameter itemValue)
        {
            List.Remove(itemValue);
        }

        /// <summary>
        /// Clone() function shall be used to make a clone of existing object and will return to the developer.
        /// </summary>
        /// <returns></returns>

        public object Clone()
        {
            MemoryStream oBuffer = new MemoryStream();
            BinaryFormatter oFormatter = new BinaryFormatter();
            oFormatter.Serialize(oBuffer, this);
            oBuffer.Position = 0;
            return oFormatter.Deserialize(oBuffer);
        }

        public int Add(string ParameterName, object Value, System.Data.ParameterDirection Direction, SqlDbType DbType)
        {
            SqlParameter oParameter = new SqlParameter();
            oParameter.ParameterName = ParameterName;
            oParameter.Value = Value;
            oParameter.Direction = Direction;
            oParameter.SqlDbType = DbType;
            return this.Add(oParameter);
        }

        public int Add(string ParameterName, object Value, System.Data.ParameterDirection Direction, SqlDbType DbType, int Size)
        {
            SqlParameter oParameter = new SqlParameter();
            oParameter.ParameterName = ParameterName;
            oParameter.Value = Value;
            oParameter.Direction = Direction;
            oParameter.SqlDbType = DbType;
            oParameter.Size = Size;
            return this.Add(oParameter);
        }

    }
}
