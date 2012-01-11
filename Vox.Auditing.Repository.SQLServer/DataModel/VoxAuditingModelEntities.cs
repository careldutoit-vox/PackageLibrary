using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.ComponentModel;

namespace Vox.Auditing.Repository.SQLServer.DataModel {  

  /// <summary>
  /// No Metadata Documentation available.
  /// </summary>
  [EdmEntityTypeAttribute(NamespaceName = "VoxAuditingModel", Name = "Batch")]
  [Serializable()]
  [DataContractAttribute(IsReference = true)]
  public partial class Batch : EntityObject {

    #region Primitive Properties

    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmScalarPropertyAttribute(EntityKeyProperty = true, IsNullable = false)]
    [DataMemberAttribute()]
    public global::System.Int32 BatchID {
      get {
        return _BatchID;
      }
      set {
        if (_BatchID != value) {
          OnBatchIDChanging(value);
          ReportPropertyChanging("BatchID");
          _BatchID = StructuralObject.SetValidValue(value);
          ReportPropertyChanged("BatchID");
          OnBatchIDChanged();
        }
      }
    }
    private global::System.Int32 _BatchID;
    partial void OnBatchIDChanging(global::System.Int32 value);
    partial void OnBatchIDChanged();

    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmScalarPropertyAttribute(EntityKeyProperty = false, IsNullable = false)]
    [DataMemberAttribute()]
    public global::System.String BatchNumber {
      get {
        return _BatchNumber;
      }
      set {
        OnBatchNumberChanging(value);
        ReportPropertyChanging("BatchNumber");
        _BatchNumber = StructuralObject.SetValidValue(value, false);
        ReportPropertyChanged("BatchNumber");
        OnBatchNumberChanged();
      }
    }
    private global::System.String _BatchNumber;
    partial void OnBatchNumberChanging(global::System.String value);
    partial void OnBatchNumberChanged();

    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmScalarPropertyAttribute(EntityKeyProperty = false, IsNullable = false)]
    [DataMemberAttribute()]
    public global::System.DateTime StartDate {
      get {
        return _StartDate;
      }
      set {
        OnStartDateChanging(value);
        ReportPropertyChanging("StartDate");
        _StartDate = StructuralObject.SetValidValue(value);
        ReportPropertyChanged("StartDate");
        OnStartDateChanged();
      }
    }
    private global::System.DateTime _StartDate;
    partial void OnStartDateChanging(global::System.DateTime value);
    partial void OnStartDateChanged();

    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmScalarPropertyAttribute(EntityKeyProperty = false, IsNullable = true)]
    [DataMemberAttribute()]
    public global::System.DateTime? FinishDate {
      get {
        return _FinishDate;
      }
      set {
        OnFinishDateChanging(value);
        ReportPropertyChanging("FinishDate");
        _FinishDate = StructuralObject.SetValidValue(value);
        ReportPropertyChanged("FinishDate");
        OnFinishDateChanged();
      }
    }
    private global::System.DateTime? _FinishDate;
    partial void OnFinishDateChanging(global::System.DateTime? value);
    partial void OnFinishDateChanged();

    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmScalarPropertyAttribute(EntityKeyProperty = false, IsNullable = false)]
    [DataMemberAttribute()]
    public global::System.Int32 MessageClientID {
      get {
        return _MessageClientID;
      }
      set {
        OnMessageClientIDChanging(value);
        ReportPropertyChanging("MessageClientID");
        _MessageClientID = StructuralObject.SetValidValue(value);
        ReportPropertyChanged("MessageClientID");
        OnMessageClientIDChanged();
      }
    }
    private global::System.Int32 _MessageClientID;
    partial void OnMessageClientIDChanging(global::System.Int32 value);
    partial void OnMessageClientIDChanged();

    #endregion

    #region Navigation Properties

    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [XmlIgnoreAttribute()]
    [SoapIgnoreAttribute()]
    [DataMemberAttribute()]
    [EdmRelationshipNavigationPropertyAttribute("VoxAuditingModel", "FK_Batch_MessageClient", "MessageClient")]
    public MessageClient MessageClient {
      get {
        return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<MessageClient>("VoxAuditingModel.FK_Batch_MessageClient", "MessageClient").Value;
      }
      set {
        ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<MessageClient>("VoxAuditingModel.FK_Batch_MessageClient", "MessageClient").Value = value;
      }
    }
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [BrowsableAttribute(false)]
    [DataMemberAttribute()]
    public EntityReference<MessageClient> MessageClientReference {
      get {
        return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<MessageClient>("VoxAuditingModel.FK_Batch_MessageClient", "MessageClient");
      }
      set {
        if ((value != null)) {
          ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<MessageClient>("VoxAuditingModel.FK_Batch_MessageClient", "MessageClient", value);
        }
      }
    }

    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [XmlIgnoreAttribute()]
    [SoapIgnoreAttribute()]
    [DataMemberAttribute()]
    [EdmRelationshipNavigationPropertyAttribute("VoxAuditingModel", "FK_Message_Batch", "Message")]
    public EntityCollection<Message> Messages {
      get {
        return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<Message>("VoxAuditingModel.FK_Message_Batch", "Message");
      }
      set {
        if ((value != null)) {
          ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<Message>("VoxAuditingModel.FK_Message_Batch", "Message", value);
        }
      }
    }

    #endregion
  }

  /// <summary>
  /// No Metadata Documentation available.
  /// </summary>
  [EdmEntityTypeAttribute(NamespaceName = "VoxAuditingModel", Name = "Message")]
  [Serializable()]
  [DataContractAttribute(IsReference = true)]
  public partial class Message : EntityObject {

    #region Primitive Properties

    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmScalarPropertyAttribute(EntityKeyProperty = true, IsNullable = false)]
    [DataMemberAttribute()]
    public global::System.Int32 MessageID {
      get {
        return _MessageID;
      }
      set {
        if (_MessageID != value) {
          OnMessageIDChanging(value);
          ReportPropertyChanging("MessageID");
          _MessageID = StructuralObject.SetValidValue(value);
          ReportPropertyChanged("MessageID");
          OnMessageIDChanged();
        }
      }
    }
    private global::System.Int32 _MessageID;
    partial void OnMessageIDChanging(global::System.Int32 value);
    partial void OnMessageIDChanged();

    /// <summary>
    /// Unique message number used to identify the message externally
    /// </summary>
    [EdmScalarPropertyAttribute(EntityKeyProperty = false, IsNullable = false)]
    [DataMemberAttribute()]
    public global::System.String MessageNumber {
      get {
        return _MessageNumber;
      }
      set {
        if (_MessageNumber != value) {
          OnMessageNumberChanging(value);
          ReportPropertyChanging("MessageNumber");
          _MessageNumber = StructuralObject.SetValidValue(value, false);
          ReportPropertyChanged("MessageNumber");
          OnMessageIDChanged();
        }
      }
    }
    private global::System.String _MessageNumber;
    partial void OnMessageNumberChanging(global::System.String value);
    partial void OnMessageNumberChanged();

    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmScalarPropertyAttribute(EntityKeyProperty = false, IsNullable = false)]
    [DataMemberAttribute()]
    public global::System.DateTime MessageDateTime {
      get {
        return _MessageDateTime;
      }
      set {
        OnMessageDateTimeChanging(value);
        ReportPropertyChanging("MessageDateTime");
        _MessageDateTime = StructuralObject.SetValidValue(value);
        ReportPropertyChanged("MessageDateTime");
        OnMessageDateTimeChanged();
      }
    }
    private global::System.DateTime _MessageDateTime;
    partial void OnMessageDateTimeChanging(global::System.DateTime value);
    partial void OnMessageDateTimeChanged();

    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmScalarPropertyAttribute(EntityKeyProperty = false, IsNullable = false)]
    [DataMemberAttribute()]
    public global::System.Int32 MessageApplicationID {
      get {
        return _MessageApplicationID;
      }
      set {
        OnMessageApplicationIDChanging(value);
        ReportPropertyChanging("MessageApplicationID");
        _MessageApplicationID = StructuralObject.SetValidValue(value);
        ReportPropertyChanged("MessageApplicationID");
        OnMessageApplicationIDChanged();
      }
    }
    private global::System.Int32 _MessageApplicationID;
    partial void OnMessageApplicationIDChanging(global::System.Int32 value);
    partial void OnMessageApplicationIDChanged();

    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmScalarPropertyAttribute(EntityKeyProperty = false, IsNullable = false)]
    [DataMemberAttribute()]
    public global::System.Int32 MessageClientID {
      get {
        return _MessageClientID;
      }
      set {
        OnMessageClientIDChanging(value);
        ReportPropertyChanging("MessageClientID");
        _MessageClientID = StructuralObject.SetValidValue(value);
        ReportPropertyChanged("MessageClientID");
        OnMessageClientIDChanged();
      }
    }
    private global::System.Int32 _MessageClientID;
    partial void OnMessageClientIDChanging(global::System.Int32 value);
    partial void OnMessageClientIDChanged();

    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmScalarPropertyAttribute(EntityKeyProperty = false, IsNullable = false)]
    [DataMemberAttribute()]
    public global::System.Int32 BatchID {
      get {
        return _BatchID;
      }
      set {
        OnBatchIDChanging(value);
        ReportPropertyChanging("BatchID");
        _BatchID = StructuralObject.SetValidValue(value);
        ReportPropertyChanged("BatchID");
        OnBatchIDChanged();
      }
    }
    private global::System.Int32 _BatchID;
    partial void OnBatchIDChanging(global::System.Int32 value);
    partial void OnBatchIDChanged();

    #endregion
    #region Navigation Properties

    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [XmlIgnoreAttribute()]
    [SoapIgnoreAttribute()]
    [DataMemberAttribute()]
    [EdmRelationshipNavigationPropertyAttribute("VoxAuditingModel", "FK_Message_Batch", "Batch")]
    public Batch Batch {
      get {
        return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Batch>("VoxAuditingModel.FK_Message_Batch", "Batch").Value;
      }
      set {
        ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Batch>("VoxAuditingModel.FK_Message_Batch", "Batch").Value = value;
      }
    }
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [BrowsableAttribute(false)]
    [DataMemberAttribute()]
    public EntityReference<Batch> BatchReference {
      get {
        return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Batch>("VoxAuditingModel.FK_Message_Batch", "Batch");
      }
      set {
        if ((value != null)) {
          ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<Batch>("VoxAuditingModel.FK_Message_Batch", "Batch", value);
        }
      }
    }

    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [XmlIgnoreAttribute()]
    [SoapIgnoreAttribute()]
    [DataMemberAttribute()]
    [EdmRelationshipNavigationPropertyAttribute("VoxAuditingModel", "FK_Message_MessageClient", "MessageClient")]
    public MessageClient MessageClient {
      get {
        return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<MessageClient>("VoxAuditingModel.FK_Message_MessageClient", "MessageClient").Value;
      }
      set {
        ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<MessageClient>("VoxAuditingModel.FK_Message_MessageClient", "MessageClient").Value = value;
      }
    }
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [BrowsableAttribute(false)]
    [DataMemberAttribute()]
    public EntityReference<MessageClient> MessageClientReference {
      get {
        return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<MessageClient>("VoxAuditingModel.FK_Message_MessageClient", "MessageClient");
      }
      set {
        if ((value != null)) {
          ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<MessageClient>("VoxAuditingModel.FK_Message_MessageClient", "MessageClient", value);
        }
      }
    }

    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [XmlIgnoreAttribute()]
    [SoapIgnoreAttribute()]
    [DataMemberAttribute()]
    [EdmRelationshipNavigationPropertyAttribute("VoxAuditingModel", "FK_Message_MessageApplication", "MessageApplication")]
    public MessageApplication MessageApplication {
      get {
        return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<MessageApplication>("VoxAuditingModel.FK_Message_MessageApplication", "MessageApplication").Value;
      }
      set {
        ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<MessageApplication>("VoxAuditingModel.FK_Message_MessageApplication", "MessageApplication").Value = value;
      }
    }
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [BrowsableAttribute(false)]
    [DataMemberAttribute()]
    public EntityReference<MessageApplication> MessageApplicationReference {
      get {
        return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<MessageApplication>("VoxAuditingModel.FK_Message_MessageApplication", "MessageApplication");
      }
      set {
        if ((value != null)) {
          ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<MessageApplication>("VoxAuditingModel.FK_Message_MessageApplication", "MessageApplication", value);
        }
      }
    }

    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [XmlIgnoreAttribute()]
    [SoapIgnoreAttribute()]
    [DataMemberAttribute()]
    [EdmRelationshipNavigationPropertyAttribute("VoxAuditingModel", "FK_MessageAudit_Message", "MessageAudit")]
    public EntityCollection<MessageAudit> MessageAudits {
      get {
        return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<MessageAudit>("VoxAuditingModel.FK_MessageAudit_Message", "MessageAudit");
      }
      set {
        if ((value != null)) {
          ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<MessageAudit>("VoxAuditingModel.FK_MessageAudit_Message", "MessageAudit", value);
        }
      }
    }

    #endregion
  }

  /// <summary>
  /// No Metadata Documentation available.
  /// </summary>
  [EdmEntityTypeAttribute(NamespaceName = "VoxAuditingModel", Name = "MessageAudit")]
  [Serializable()]
  [DataContractAttribute(IsReference = true)]
  public partial class MessageAudit : EntityObject {

    #region Primitive Properties

    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmScalarPropertyAttribute(EntityKeyProperty = true, IsNullable = false)]
    [DataMemberAttribute()]
    public global::System.Int32 MessageAuditID {
      get {
        return _MessageAuditID;
      }
      set {
        if (_MessageAuditID != value) {
          OnMessageAuditIDChanging(value);
          ReportPropertyChanging("MessageAuditID");
          _MessageAuditID = StructuralObject.SetValidValue(value);
          ReportPropertyChanged("MessageAuditID");
          OnMessageAuditIDChanged();
        }
      }
    }
    private global::System.Int32 _MessageAuditID;
    partial void OnMessageAuditIDChanging(global::System.Int32 value);
    partial void OnMessageAuditIDChanged();

    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmScalarPropertyAttribute(EntityKeyProperty = false, IsNullable = false)]
    [DataMemberAttribute()]
    public global::System.Int32 MessageID {
      get {
        return _MessageID;
      }
      set {
        OnMessageIDChanging(value);
        ReportPropertyChanging("MessageID");
        _MessageID = StructuralObject.SetValidValue(value);
        ReportPropertyChanged("MessageID");
        OnMessageIDChanged();
      }
    }
    private global::System.Int32 _MessageID;
    partial void OnMessageIDChanging(global::System.Int32 value);
    partial void OnMessageIDChanged();

    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmScalarPropertyAttribute(EntityKeyProperty = false, IsNullable = false)]
    [DataMemberAttribute()]
    public global::System.String MessageCorrelationID {
      get {
        return _MessageCorrelationID;
      }
      set {
        OnMessageCorrelationIDChanging(value);
        ReportPropertyChanging("MessageCorrelationID");
        _MessageCorrelationID = StructuralObject.SetValidValue(value, false);
        ReportPropertyChanged("MessageCorrelationID");
        OnMessageCorrelationIDChanged();
      }
    }
    private global::System.String _MessageCorrelationID;
    partial void OnMessageCorrelationIDChanging(global::System.String value);
    partial void OnMessageCorrelationIDChanged();

    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmScalarPropertyAttribute(EntityKeyProperty = false, IsNullable = false)]
    [DataMemberAttribute()]
    public global::System.DateTime MessageDateTime {
      get {
        return _MessageDateTime;
      }
      set {
        OnMessageDateTimeChanging(value);
        ReportPropertyChanging("MessageDateTime");
        _MessageDateTime = StructuralObject.SetValidValue(value);
        ReportPropertyChanged("MessageDateTime");
        OnMessageDateTimeChanged();
      }
    }
    private global::System.DateTime _MessageDateTime;
    partial void OnMessageDateTimeChanging(global::System.DateTime value);
    partial void OnMessageDateTimeChanged();

    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmScalarPropertyAttribute(EntityKeyProperty = false, IsNullable = false)]
    [DataMemberAttribute()]
    public global::System.String MessageState {
      get {
        return _MessageState;
      }
      set {
        OnMessageStateChanging(value);
        ReportPropertyChanging("MessageState");
        _MessageState = StructuralObject.SetValidValue(value, false);
        ReportPropertyChanged("MessageState");
        OnMessageStateChanged();
      }
    }
    private global::System.String _MessageState;
    partial void OnMessageStateChanging(global::System.String value);
    partial void OnMessageStateChanged();

    #endregion

    #region Navigation Properties

    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [XmlIgnoreAttribute()]
    [SoapIgnoreAttribute()]
    [DataMemberAttribute()]
    [EdmRelationshipNavigationPropertyAttribute("VoxAuditingModel", "FK_MessageAudit_Message", "Message")]
    public Message Message {
      get {
        return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Message>("VoxAuditingModel.FK_MessageAudit_Message", "Message").Value;
      }
      set {
        ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Message>("VoxAuditingModel.FK_MessageAudit_Message", "Message").Value = value;
      }
    }
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [BrowsableAttribute(false)]
    [DataMemberAttribute()]
    public EntityReference<Message> MessageReference {
      get {
        return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Message>("VoxAuditingModel.FK_MessageAudit_Message", "Message");
      }
      set {
        if ((value != null)) {
          ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<Message>("VoxAuditingModel.FK_MessageAudit_Message", "Message", value);
        }
      }
    }

    #endregion
  }

  /// <summary>
  /// No Metadata Documentation available.
  /// </summary>
  [EdmEntityTypeAttribute(NamespaceName = "VoxAuditingModel", Name = "MessageClient")]
  [Serializable()]
  [DataContractAttribute(IsReference = true)]
  public partial class MessageClient : EntityObject {

    #region Primitive Properties

    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmScalarPropertyAttribute(EntityKeyProperty = true, IsNullable = false)]
    [DataMemberAttribute()]
    public global::System.Int32 MessageClientID {
      get {
        return _MessageClientID;
      }
      set {
        if (_MessageClientID != value) {
          OnMessageClientIDChanging(value);
          ReportPropertyChanging("MessageClientID");
          _MessageClientID = StructuralObject.SetValidValue(value);
          ReportPropertyChanged("MessageClientID");
          OnMessageClientIDChanged();
        }
      }
    }
    private global::System.Int32 _MessageClientID;
    partial void OnMessageClientIDChanging(global::System.Int32 value);
    partial void OnMessageClientIDChanged();

    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmScalarPropertyAttribute(EntityKeyProperty = false, IsNullable = false)]
    [DataMemberAttribute()]
    public global::System.String MessageClientIdentifier {
      get {
        return _MessageClientIdentifier;
      }
      set {
        OnMessageClientIdentifierChanging(value);
        ReportPropertyChanging("MessageClientIdentifier");
        _MessageClientIdentifier = StructuralObject.SetValidValue(value, false);
        ReportPropertyChanged("MessageClientIdentifier");
        OnMessageClientIdentifierChanged();
      }
    }
    private global::System.String _MessageClientIdentifier;
    partial void OnMessageClientIdentifierChanging(global::System.String value);
    partial void OnMessageClientIdentifierChanged();

    #endregion

    #region Navigation Properties

    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [XmlIgnoreAttribute()]
    [SoapIgnoreAttribute()]
    [DataMemberAttribute()]
    [EdmRelationshipNavigationPropertyAttribute("VoxAuditingModel", "FK_Batch_MessageClient", "Batch")]
    public EntityCollection<Batch> Batches {
      get {
        return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<Batch>("VoxAuditingModel.FK_Batch_MessageClient", "Batch");
      }
      set {
        if ((value != null)) {
          ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<Batch>("VoxAuditingModel.FK_Batch_MessageClient", "Batch", value);
        }
      }
    }

    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [XmlIgnoreAttribute()]
    [SoapIgnoreAttribute()]
    [DataMemberAttribute()]
    [EdmRelationshipNavigationPropertyAttribute("VoxAuditingModel", "FK_Message_MessageClient", "Message")]
    public EntityCollection<Message> Messages {
      get {
        return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<Message>("VoxAuditingModel.FK_Message_MessageClient", "Message");
      }
      set {
        if ((value != null)) {
          ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<Message>("VoxAuditingModel.FK_Message_MessageClient", "Message", value);
        }
      }
    }

    #endregion
  }

  /// <summary>
  /// No Metadata Documentation available.
  /// </summary>
  [EdmEntityTypeAttribute(NamespaceName = "VoxAuditingModel", Name = "MessageApplication")]
  [Serializable()]
  [DataContractAttribute(IsReference = true)]
  public partial class MessageApplication : EntityObject {

    #region Primitive Properties

    /// <summary>
    /// Primary Key
    /// </summary>
    [EdmScalarPropertyAttribute(EntityKeyProperty = true, IsNullable = false)]
    [DataMemberAttribute()]
    public global::System.Int32 MessageApplicationID {
      get {
        return _MessageApplicationID;
      }
      set {
        if (_MessageApplicationID != value) {
          OnMessageApplicationIDChanging(value);
          ReportPropertyChanging("MessageApplicationID");
          _MessageApplicationID = StructuralObject.SetValidValue(value);
          ReportPropertyChanged("MessageApplicationID");
          OnMessageApplicationIDChanged();
        }
      }
    }
    private global::System.Int32 _MessageApplicationID;
    partial void OnMessageApplicationIDChanging(global::System.Int32 value);
    partial void OnMessageApplicationIDChanged();

    /// <summary>
    /// External GUID Reference
    /// </summary>
    [EdmScalarPropertyAttribute(EntityKeyProperty = false, IsNullable = false)]
    [DataMemberAttribute()]
    public global::System.Guid MessageApplicationGuid {
      get {
        return _MessageApplicationGuid;
      }
      set {
        OnMessageApplicationGuidChanging(value);
        ReportPropertyChanging("MessageApplicationGuid");
        _MessageApplicationGuid = StructuralObject.SetValidValue(value);
        ReportPropertyChanged("MessageApplicationGuid");
        OnMessageApplicationGuidChanged();
      }
    }
    private global::System.Guid _MessageApplicationGuid;
    partial void OnMessageApplicationGuidChanging(global::System.Guid value);
    partial void OnMessageApplicationGuidChanged();

    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmScalarPropertyAttribute(EntityKeyProperty = false, IsNullable = false)]
    [DataMemberAttribute()]
    public global::System.String MessageApplicationName {
      get {
        return _MessageApplicationName;
      }
      set {
        OnMessageApplicationNameChanging(value);
        ReportPropertyChanging("MessageApplicationName");
        _MessageApplicationName = StructuralObject.SetValidValue(value, false);
        ReportPropertyChanged("MessageApplicationName");
        OnMessageApplicationNameChanged();
      }
    }
    private global::System.String _MessageApplicationName;
    partial void OnMessageApplicationNameChanging(global::System.String value);
    partial void OnMessageApplicationNameChanged();

    #endregion

    #region Navigation Properties

    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [XmlIgnoreAttribute()]
    [SoapIgnoreAttribute()]
    [DataMemberAttribute()]
    [EdmRelationshipNavigationPropertyAttribute("VoxAuditingModel", "FK_Message_MessageApplication", "Message")]
    public EntityCollection<Message> Messages {
      get {
        return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<Message>("VoxAuditingModel.FK_Message_MessageApplication", "Message");
      }
      set {
        if ((value != null)) {
          ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<Message>("VoxAuditingModel.FK_Message_MessageApplication", "Message", value);
        }
      }
    }

    #endregion
  }
}
