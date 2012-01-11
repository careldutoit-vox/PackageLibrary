using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vox.Auditing.Repository.SQLServer.DataModel {
  public enum ChangeAction {
    Insert,
    Update,
    Delete
  }

  public interface IValidate {
    void Validate(ChangeAction action);
  }
}
