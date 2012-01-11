using System.ComponentModel.Composition;
using T2.SendingMessage.Library;
using T2.Vox.RouteKey.Plugins.Interface;
using Vox.Auditing.Repository;
using Vox.Auditing.Repository.SQLServer;
using Vox.Auditing.Model;
using System;
using System.Configuration;

namespace Vox.Auditing {

  [RouteKey("#")]
  [Export(typeof(IRouteKeyPlugin))]
  public class AuditingPlugin : IRouteKeyPlugin {

    #region Private Variables

    private IMessageRepository messages;
    private IBatchRepository batches;
    private IClientRepository clients;

    #endregion

    #region Constructor

    public AuditingPlugin() {      
    }

    #endregion

    #region IRouteKeyPlugin Members

    /// <summary>
    /// Writes the message into the Auditing database if required and updates the correlation Guid for that message if
    /// it doesn't already exist
    /// </summary>
    /// <param name="message">Message to audit</param>
    /// <returns>A string result of the auditing</returns>
    public string ProcessWorker(T2.SendingMessage.Library.Message message) {
      try {
        if (clients == null) { throw new Exception("Please call the initialise call before calling the worker"); }
        // Get the client
        Client client = clients.GetByName(message.AdditionalInformation.UserId.ToString());
        if (client == null) { throw new Exception(string.Format("Could not find a matching client for user '{0}'", message.AdditionalInformation.UserName)); }
        // Check if the message already exists
        Vox.Auditing.Model.Message auditMessage = messages.GetByGuid(message.AdditionalInformation.MessageId);
        if (auditMessage != null) {
          // Add the current message onto the audit trail
          auditMessage.AddAudit(new Guid(message.CorrelationId), message.DestinationName);
        } else {
          // Message has not been seen before. Need to create the master auditing records for it
          // Check if the batch exists
          if (client.CurrentBatch != null) {
            // Create a new message for the batch
            Model.Message newMessage = new Model.Message(0, message.AdditionalInformation.MessageId.ToString()) {
              BatchID = client.CurrentBatch.ID,
              ClientID = client.ID,
              MessageDateTime = DateTime.Now,
              MessageApplicationID = messages.GetMessageApplicationID(new Guid(message.ApplicationId))
            };
            messages.Add(newMessage);
          } else {
            throw new Exception(string.Format("No available batch is open for client '{0}'", client.Name));
          }
        }
      } catch (Exception e) {
        T2.SendingMessage.Library.Interface.IMessageQueueConnector queue = T2.SendingMessage.Library.MessageQueueFactory.Create();
        queue.SendAsyncExceptionMessage(e, "Vox.Auditing", message);
      }
      return "";
    }

    public void InitializeConfiguration() {
      string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
      string fullPath = path + @"\Vox.Auditing.config";
      ExeConfigurationFileMap configFileMap = new ExeConfigurationFileMap();
      configFileMap.ExeConfigFilename = fullPath;      
      Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(configFileMap, ConfigurationUserLevel.None);            
      var connString = configuration.ConnectionStrings.ConnectionStrings["VoxAuditingEntities"];      
      if (connString != null) {
        messages = new MessageRepositoryDataModel(connString.ConnectionString);
        batches = new BatchRepositoryDataModel(connString.ConnectionString);
        clients = new ClientRepositoryDataModel(connString.ConnectionString);
      }
    }

    #endregion
  }
}
