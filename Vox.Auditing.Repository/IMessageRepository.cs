using System;
using Vox.Auditing.Model;

namespace Vox.Auditing.Repository {
  #region Exceptions
  public class InvalidMessageException : Exception {
    public InvalidMessageException(string message)
      : base(message) {
    }
  }
  #endregion

  /// <summary>
  /// Message Repository Interface
  /// </summary>
  public interface IMessageRepository : IRepository<Message> {
    Message GetByGuid(Guid aGuid);

    int GetMessageApplicationID(Guid aGuid);
  }
}
