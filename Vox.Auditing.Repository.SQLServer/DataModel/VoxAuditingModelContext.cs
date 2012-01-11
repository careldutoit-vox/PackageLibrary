using System.Data.EntityClient;
using System.Data.Objects;

namespace Vox.Auditing.Repository.SQLServer.DataModel {

  #region Contexts
  /// <summary>
  /// No Metadata Documentation available.
  /// </summary>
  public partial class VoxAuditingEntities : ObjectContext, IEntityContext {
    #region Constructors

    /// <summary>
    /// Initializes a new VoxAuditingEntities object using the connection string found in the 'VoxAuditingEntities' section of the application configuration file.
    /// </summary>
    public VoxAuditingEntities()
      : base("name=VoxAuditingEntities", "VoxAuditingEntities") {
      this.ContextOptions.LazyLoadingEnabled = true;
      OnContextCreated();
    }

    /// <summary>
    /// Initialize a new VoxAuditingEntities object.
    /// </summary>
    public VoxAuditingEntities(string connectionString)
      : base(connectionString, "VoxAuditingEntities") {
      this.ContextOptions.LazyLoadingEnabled = true;
      OnContextCreated();
    }

    /// <summary>
    /// Initialize a new VoxAuditingEntities object.
    /// </summary>
    public VoxAuditingEntities(EntityConnection connection)
      : base(connection, "VoxAuditingEntities") {
      this.ContextOptions.LazyLoadingEnabled = true;
      OnContextCreated();
    }

    #endregion

    #region Partial Methods

    partial void OnContextCreated();

    #endregion

    #region ObjectSet Properties

    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    public IObjectSet<Batch> Batches {
      get {
        if ((_Batches == null)) {
          _Batches = base.CreateObjectSet<Batch>("Batches");
        }
        return _Batches;
      }
    }
    private IObjectSet<Batch> _Batches;

    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    public IObjectSet<Message> Messages {
      get {
        if ((_Messages == null)) {
          _Messages = base.CreateObjectSet<Message>("Messages");
        }
        return _Messages;
      }
    }
    private IObjectSet<Message> _Messages;

    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    public IObjectSet<MessageAudit> MessageAudits {
      get {
        if ((_MessageAudits == null)) {
          _MessageAudits = base.CreateObjectSet<MessageAudit>("MessageAudits");
        }
        return _MessageAudits;
      }
    }
    private IObjectSet<MessageAudit> _MessageAudits;

    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    public IObjectSet<MessageClient> MessageClients {
      get {
        if ((_MessageClients == null)) {
          _MessageClients = base.CreateObjectSet<MessageClient>("MessageClients");
        }
        return _MessageClients;
      }
    }
    private IObjectSet<MessageClient> _MessageClients;

    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    public IObjectSet<MessageApplication> MessageApplications {
      get {
        if ((_MessageApplications == null)) {
          _MessageApplications = base.CreateObjectSet<MessageApplication>("MessageApplication");
        }
        return _MessageApplications;
      }
    }
    private IObjectSet<MessageApplication> _MessageApplications;       

    #endregion

    public int SaveChanges() {
      return ((ObjectContext)this).SaveChanges();
    }
  }
  #endregion
}