using System;
using System.Data.Objects;

namespace Vox.Auditing.Repository.SQLServer.DataModel {
  public interface IEntityContext : IDisposable {
    IObjectSet<Batch> Batches { get; }
    IObjectSet<Message> Messages { get; }
    IObjectSet<MessageAudit> MessageAudits { get; }
    IObjectSet<MessageClient> MessageClients { get; }
    IObjectSet<MessageApplication> MessageApplications { get; }    
    int SaveChanges();
  }
}
