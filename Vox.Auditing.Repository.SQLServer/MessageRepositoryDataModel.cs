using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vox.Auditing.Model;

namespace Vox.Auditing.Repository.SQLServer {
  public class MessageRepositoryDataModel : IMessageRepository {

    #region Private Variables
    private Dictionary<int, Message> messages;
    private string connectionString;
    #endregion

    #region Constructor

    /// <summary>
    /// Default constructor for the Message Repository connecting to the SQL Server
    /// </summary>
    public MessageRepositoryDataModel(string aConnectionString) {
      if (string.IsNullOrEmpty(aConnectionString)) { throw new ArgumentNullException("aConnectionString"); }
      connectionString = aConnectionString;
      messages = new Dictionary<int, Message>();
    }

    #endregion

    #region IRepository<Message> Members

    /// <summary>
    /// Gets a message from the repository by the given message application ID
    /// </summary>
    /// <param name="aID">The Guid for the given message</param>
    /// <returns>A Message object or Null</returns>
    public Model.Message GetByGuid(Guid aGuid) {
      if (aGuid == Guid.Empty) { throw new ArgumentException("Cannot search for a Message Object with a empty Guid", "aGuid"); }
      Message result = null;
      using (DataModel.VoxAuditingEntities context = new DataModel.VoxAuditingEntities(connectionString)) {
        var selectResult = from m in context.Messages
                           where m.MessageNumber == aGuid.ToString()
                           select m;
        // Check the results. Can only have one result
        if (selectResult.Count() == 1) {
          DataModel.Message selectedMessage = selectResult.First();
          result = new Message(selectedMessage.MessageID, selectedMessage.MessageNumber) {
            ClientID = selectedMessage.MessageClientID,
            MessageDateTime = selectedMessage.MessageDateTime,
            BatchID = selectedMessage.BatchID,
            MessageApplicationID = selectedMessage.MessageApplicationID
          };
        }
      }
      return result;
    }

    /// <summary>
    /// Gets a message from the repository by the given ID
    /// </summary>
    /// <param name="aID">The integer identifier for the message</param>
    /// <returns>A Message object or Null</returns>
    public Model.Message GetByID(int aID) {
      if (aID <= 0) { throw new ArgumentException("Cannot search for a Message Object with a non positive ID", "aID"); }
      Message result = null;
      using (DataModel.VoxAuditingEntities context = new DataModel.VoxAuditingEntities(connectionString)) {
        var selectResult = from m in context.Messages
                           where m.MessageID == aID
                           select m;
        // Check the results. Can only have one result
        if (selectResult.Count() == 1) {
          DataModel.Message selectedMessage = selectResult.First();
          result = new Message(selectedMessage.MessageID, selectedMessage.MessageNumber) {
            ClientID = selectedMessage.MessageClientID,
            MessageDateTime = selectedMessage.MessageDateTime,
            BatchID = selectedMessage.BatchID,
            MessageApplicationID = selectedMessage.MessageApplicationID
          };
        }
      }
      return result;
    }

    /// <summary>
    /// Adds the message into the SQL database
    /// </summary>
    /// <param name="entity">A new message to add</param>
    /// <returns>True if the message was added</returns>
    public bool Add(Model.Message entity) {
      if (entity == null) { throw new ArgumentNullException("entity"); }
      if (entity.ID != 0) { throw new InvalidMessageException("Cannot add a message which has already been added!"); }
      if (entity.ClientID == 0) { throw new InvalidMessageException("Cannot add a message with no valid client!"); }
      if (entity.BatchID == 0) { throw new InvalidMessageException("Cannot add a message with no valid batch!"); }
      bool added = false;
      // Check if the batch is valid first
      using (DataModel.VoxAuditingEntities context = new DataModel.VoxAuditingEntities(connectionString)) {
        DataModel.Message newMessage = new DataModel.Message() {
          MessageNumber = entity.Name,
          BatchID = entity.BatchID,
          MessageDateTime = entity.MessageDateTime,
          MessageApplicationID = entity.MessageApplicationID,
          MessageClientID = entity.ClientID
        };
        context.Messages.AddObject(newMessage);
        added = context.SaveChanges() != 0;
        if (added) {
          AddMessageToDictionary(entity);
          entity.ID = newMessage.MessageID;
        }
      }
      return added;
    }

    /// <summary>
    /// Gets the application ID for the given Message Application Guid
    /// </summary>
    /// <param name="aGuid">Guid of the application auditing messages</param>
    /// <returns>A integer Id of the Application or 0 if the application does not exist</returns>
    public int GetMessageApplicationID(Guid aGuid) {
      if (aGuid == Guid.Empty) { throw new ArgumentNullException("aGuid"); }
      int applicationID = 0;
      using (DataModel.VoxAuditingEntities context = new DataModel.VoxAuditingEntities(connectionString)) {
        var result = from a in context.MessageApplications
                     where a.MessageApplicationGuid == aGuid
                     select a;
        if (result.Count() == 1) {
          applicationID = result.First().MessageApplicationID;
        } else {
          throw new Exception(string.Format("No message application has been configured for '{0}'", aGuid));
        }
      }
      return applicationID;
    }

    /// <summary>
    /// Cannot remove a message once it has been added
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public bool Remove(Model.Message entity) {
      throw new NotImplementedException();
    }

    /// <summary>
    /// Nothing to update on a message once it has been added
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public bool Update(Model.Message entity) {
      throw new NotImplementedException();
    }

    #endregion

    #region Private Methods

    private void AddMessageToDictionary(Model.Message entity) {
      entity.AddMessageAudit -= entity_AddMessageAudit;
      if (!messages.ContainsKey(entity.ID)) {
        messages.Add(entity.ID, entity);
      }
      entity.AddMessageAudit += new AddMessageAuditHandler(entity_AddMessageAudit);
    }

    private bool entity_AddMessageAudit(Message message, Guid correlationID, string state) {
      bool saved = false;
      using (DataModel.VoxAuditingEntities context = new DataModel.VoxAuditingEntities(connectionString)) {
        context.MessageAudits.AddObject(new DataModel.MessageAudit() {
          MessageCorrelationID = correlationID.ToString(),
          MessageID = message.ID,
          MessageDateTime = DateTime.Now,
          MessageState = state
        });
        saved = context.SaveChanges() > 0;
      }
      return saved;
    }

    #endregion
  }
}
