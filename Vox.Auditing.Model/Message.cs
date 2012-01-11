using System;
namespace Vox.Auditing.Model {

  #region Delegate
  /// <summary>
  /// Delegate method call which is used to handle the MessageAudit event handler
  /// </summary>
  /// <param name="message">Parent Message the audit is being made for</param>
  /// <param name="correlationID">The Message Guid</param>
  /// <param name="state">State of the message</param>
  /// <returns>True if the message was successfully added</returns>
  public delegate bool AddMessageAuditHandler(Message message, Guid correlationID, string state);
  #endregion

  /// <summary>
  /// Represents the message class stored in the Auditing repository
  /// </summary>
  public class Message {

    #region Constructor

    /// <summary>
    /// Default constructor for the message
    /// </summary>
    /// <param name="aID">Unqiue Identifier for the </param>
    /// <param name="aName">Name of the Message (Used to externally identify it)</param>
    public Message(int aID, string aName) {
      ID = aID;
      Name = aName;
    }

    #endregion

    #region Properties

    public int ID { get; set; }
    public string Name { get; private set; }
    public DateTime MessageDateTime { get; set; }
    public int MessageApplicationID { get; set; }
    public int ClientID { get; set; }
    public int BatchID { get; set; }

    #endregion

    #region Events

    public event AddMessageAuditHandler AddMessageAudit;

    #endregion

    #region Public Methods

    /// <summary>
    /// Adds the given communication message into the message audit queues
    /// </summary>
    /// <param name="correlationID">The Message GUID</param>
    /// <param name="state">State of the message</param>
    /// <returns>True if the message was audited</returns>
    public bool AddAudit(Guid correlationID, string state) {
      bool added = false;
      if (AddMessageAudit != null) {
        added = AddMessageAudit(this, correlationID, state);
      }
      return added;
    }

    #endregion
  }
}
