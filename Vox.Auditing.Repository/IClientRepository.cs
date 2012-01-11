using Vox.Auditing.Model;
using System;

namespace Vox.Auditing.Repository {

  #region Exceptions
  public class InvalidClientException : Exception {
    public InvalidClientException(string message)
      : base(message) {
    }
  }
  #endregion

  public interface IClientRepository : IRepository<Client> {

    /// <summary>
    /// Retrieve the client for the given name / identifier
    /// </summary>
    /// <param name="aName">A unique name / identifier for the client </param>
    /// <returns>The client for the given name or null</returns>
    Client GetByName(string aName);
  }
}
