using System;
using Vox.Auditing.Model;

namespace Vox.Auditing.Repository {
  #region Exceptions  
  public class InvalidBatchException : Exception {
    public InvalidBatchException(string message)
      : base(message) {
    }
  }
  #endregion

  public interface IBatchRepository : IRepository<Batch> {
    Batch GetByName(string aName);
  }
}
