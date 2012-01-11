using System;
using System.Collections.Generic;
using System.Linq;
using Vox.Auditing.Model;

namespace Vox.Auditing.Repository.SQLServer {
  public class BatchRepositoryDataModel : IBatchRepository {

    #region Private Variables
    private Dictionary<int, Batch> batches;
    private string connectionString;
    #endregion

    #region Constructor

    /// <summary>
    /// Default constructor for the Batch Repository connecting to the SQL Server
    /// </summary>
    public BatchRepositoryDataModel(string aConnectionString) {
      if (string.IsNullOrEmpty(aConnectionString)) { throw new ArgumentNullException("aConnectionString"); }
      connectionString = aConnectionString;
      batches = new Dictionary<int, Batch>();
    }

    #endregion

    #region IRepository<Vox.Auditing.Model.Batch> Members

    /// <summary>
    /// Retrieves the batch from the database by search on the BatchID. If a negative ID is used it will throw an ArgumentException
    /// </summary>
    /// <param name="aName">A batch Number</param>
    /// <returns>Null if the batch ID doesn't exist, otherwise a Batch Object with the given ID</returns>
    public Vox.Auditing.Model.Batch GetByName(string aName) {
      if (string.IsNullOrEmpty(aName)) { throw new ArgumentException("Cannot search for a Batch Object with a empty ID", "aName"); }
      Batch result = null;
      using (DataModel.VoxAuditingEntities context = new DataModel.VoxAuditingEntities(connectionString)) {
        var selectResult = from m in context.Batches
                           where m.BatchNumber == aName
                           select m;
        // Check the results. Can only have one result
        if (selectResult.Count() == 1) {
          DataModel.Batch selectedBatch = selectResult.First();
          result = new Batch(selectedBatch.BatchID, selectedBatch.BatchNumber) {
            ClientID = selectedBatch.MessageClientID,
            StartDate = selectedBatch.StartDate,
            FinishDate = selectedBatch.FinishDate
          };
        }
      }
      return result;
    }

    /// <summary>
    /// Retrieves the batch from the database by search on the BatchID. If a negative ID is used it will throw an ArgumentException
    /// </summary>
    /// <param name="aID">A batch ID</param>
    /// <returns>Null if the batch ID doesn't exist, otherwise a Batch Object with the given ID</returns>
    public Vox.Auditing.Model.Batch GetByID(int aID) {
      if (aID <= 0) { throw new ArgumentException("Cannot search for a Batch Object with a non positive ID", "aID"); }
      Batch result = null;
      using (DataModel.VoxAuditingEntities context = new DataModel.VoxAuditingEntities(connectionString)) {
        var selectResult = from m in context.Batches
                           where m.BatchID == aID
                           select m;
        // Check the results. Can only have one result
        if (selectResult.Count() == 1) {
          DataModel.Batch selectedBatch = selectResult.First();
          result = new Batch(selectedBatch.BatchID, selectedBatch.BatchNumber) {
            ClientID = selectedBatch.MessageClientID,
            StartDate = selectedBatch.StartDate,
            FinishDate = selectedBatch.FinishDate
          };
        }
      }
      return result;
    }

    /// <summary>
    /// Creates a new Batch entry for the given client in the table. The combination of BatchNumber and Client must be unique, if not a InvalidBatchException will
    /// be thrown. In addition existing batches must be closed first.
    /// </summary>
    /// <param name="entity">Batch which will be added.</param>
    /// <returns>True if the batch was succesfully added, otherwise false. If the batch is invalid an InvalidBatchException will be thrown</returns>
    public bool Add(Vox.Auditing.Model.Batch entity) {
      if (entity == null) { throw new ArgumentNullException("entity"); }
      if (entity.ID != 0) { throw new InvalidBatchException("Cannot add a batch which has already been added!"); }
      if (entity.ClientID == 0) { throw new InvalidBatchException("Cannot add a batch with no valid client!"); }
      bool added = false;
      // Check if the batch is valid first
      using (DataModel.VoxAuditingEntities context = new DataModel.VoxAuditingEntities(connectionString)) {
        // Check that no existing batches are open for the current client ID
        var validBatch = from m in context.Batches
                         where m.MessageClientID == entity.ClientID && m.FinishDate == null
                         select m;
        if (validBatch.Count() == 0) {
          // Try and add the new batch
          context.Batches.AddObject(new DataModel.Batch() {
            BatchID = entity.ID,
            BatchNumber = entity.Number,
            MessageClientID = entity.ClientID,
            StartDate = entity.StartDate
          });
          added = context.SaveChanges() != 0;
        } else {
          throw new InvalidBatchException(string.Format("Batch '{0}' is still open for the given client", validBatch.First().BatchNumber));
        }
      }
      return added;
    }

    /// <summary>
    /// Cannot remove a existing batch via the repository
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public bool Remove(Vox.Auditing.Model.Batch entity) {
      throw new NotImplementedException();
    }

    /// <summary>
    /// Updates an existing batch entity (Normally this will be to set the end date on the batch)
    /// </summary>
    /// <param name="entity">Batch to update</param>
    /// <returns>True if the batch was altered</returns>
    public bool Update(Vox.Auditing.Model.Batch entity) {
      if (entity == null) { throw new ArgumentNullException("entity"); }
      if (entity.ID == 0) { throw new InvalidBatchException("Cannot updated a batch which has not been added!"); }
      if (entity.ClientID == 0) { throw new InvalidBatchException("Cannot update a batch with no valid client!"); }
      bool updated = false;
      // Check if the batch is valid first
      using (DataModel.VoxAuditingEntities context = new DataModel.VoxAuditingEntities(connectionString)) {
        // Check that no existing batches are open for the current client ID
        var validBatch = from m in context.Batches
                         where m.BatchID == entity.ID
                         select m;
        if (validBatch.Count() == 1) {
          DataModel.Batch selectBatch = validBatch.First();
          selectBatch.FinishDate = entity.FinishDate;
          updated = context.SaveChanges() != 0;
        } else {
          throw new InvalidBatchException(string.Format("Batch '{0}' does not exist", entity.Number));
        }
      }
      return updated;
    }

    #endregion
  }
}
