using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vox.Auditing.Model;

namespace Vox.Auditing.Repository.SQLServer {

  public class ClientRepositoryDataModel : IClientRepository {

    #region Private Variables
    private string connectionString;
    #endregion

    #region Constructor

    /// <summary>
    /// Default Constructor for the Client Repository which connects to an SQL Database via Entity Framework
    /// </summary>
    public ClientRepositoryDataModel(string aConnectionString) {
      if (string.IsNullOrEmpty(aConnectionString)) { throw new ArgumentNullException("aConnectionString"); }
      connectionString = aConnectionString;
    }

    #endregion

    #region IRepository<Client> Members

    /// <summary>
    /// Gets the Client from the database using the unique name as the lookup parameter
    /// </summary>
    /// <param name="aName">A unique string Identifier</param>
    /// <returns>A Client for the given ID, other Null if the client does not exist</returns>
    public Client GetByName(string aName) {
      if (string.IsNullOrEmpty(aName)) { throw new ArgumentException("Cannot search for a Client Object with a empty ID", "aName"); }
      Client result = null;
      using (DataModel.VoxAuditingEntities context = new DataModel.VoxAuditingEntities(connectionString)) {
        var selectResult = from m in context.MessageClients
                           where m.MessageClientIdentifier == aName
                           select m;
        // Check the results. Can only have one result
        if (selectResult.Count() == 1) {
          var selectedClient = selectResult.First();
          result = new Client(selectedClient.MessageClientID, selectedClient.MessageClientIdentifier);
          var currentSelectBatch = from b in context.Batches
                                   where b.MessageClientID == result.ID && b.FinishDate == null
                                   select b;
          if (currentSelectBatch.Count() == 1) {
            var selectedBatch = currentSelectBatch.First();
            result.CurrentBatch = new Batch(selectedBatch.BatchID, selectedBatch.BatchNumber) {
              ClientID = selectedBatch.MessageClientID,
              StartDate = selectedBatch.StartDate,
              FinishDate = null
            };
          };
        }
      }
      return result;
    }

    /// <summary>
    /// Gets the Client from the database using the integer ID as the lookup parameter
    /// </summary>
    /// <param name="aID">A unique Integer Identifier which matches the MessageClientID in the MessageClient Table</param>
    /// <returns>A Client for the given ID, other Null if the client does not exist</returns>
    public Client GetByID(int aID) {
      if (aID <= 0) { throw new ArgumentException("Cannot search for a Client Object with a non positive ID", "aID"); }
      Client result = null;
      using (DataModel.VoxAuditingEntities context = new DataModel.VoxAuditingEntities(connectionString)) {
        var selectResult = from m in context.MessageClients
                           where m.MessageClientID == aID
                           select m;
        // Check the results. Can only have one result
        if (selectResult.Count() == 1) {
          DataModel.MessageClient selectedClient = selectResult.First();
          result = new Client(selectedClient.MessageClientID, selectedClient.MessageClientIdentifier);
        }
      }
      return result;
    }

    /// <summary>
    /// Adds a client to the repository. The client entity will be slightly validated before it can be added.
    /// </summary>
    /// <param name="entity">A client entity to add</param>
    /// <returns>True if the entity was added, otherwise false</returns>
    public bool Add(Model.Client entity) {
      if (entity == null) { throw new ArgumentNullException("entity"); }
      if (entity.ID != 0) { throw new InvalidClientException("Cannot add a client which has already been added!"); }
      bool added = false;
      // Check if the client is valid first
      using (DataModel.VoxAuditingEntities context = new DataModel.VoxAuditingEntities(connectionString)) {
        context.MessageClients.AddObject(new DataModel.MessageClient() {
          MessageClientIdentifier = entity.Name
        });
        added = context.SaveChanges() != 0;
      }
      return added;
    }

    /// <summary>
    /// Not implemented. Cannot remove
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public bool Remove(Model.Client entity) {
      throw new NotImplementedException();
    }

    /// <summary>
    /// Not implemented. Cannot update
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public bool Update(Model.Client entity) {
      throw new NotImplementedException();
    }

    #endregion
  }
}
