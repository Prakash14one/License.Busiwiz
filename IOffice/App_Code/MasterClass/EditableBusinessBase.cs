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
using System.ComponentModel;

/// <summary>
/// Summary description for EditableBusinessBase
/// </summary>
namespace DS.BO
{
    public abstract class EditableBusinessBase : IEditableObject, ICloneable
    {
        #region ICloneable Members
        public object Clone()
        {
            throw new Exception("The method or operation is not implemented.");
        }
        #endregion

        #region Director
        public virtual void Director_Delete()
        {
            throw new NotSupportedException("Invalid operation - delete not allowed");
        }
        public virtual EditableBusinessBase Director_Fetch(object poCriteria)
        {
            throw new NotSupportedException("Invalid operation - fetch not allowed");
        }
        public virtual EditableBusinessBase Director_Create(object poCriteria)
        {
            throw new NotSupportedException("Invalid operation - create not allowed");
        }
        public virtual void Director_Update()
        {
            throw new NotSupportedException("Invalid operation - Update not allowed");
        }
        #endregion

        #region IEditableObject Members

        public void BeginEdit()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void CancelEdit()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void EndEdit()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion
    }
}
