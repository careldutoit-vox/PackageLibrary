using Microsoft.VisualStudio.TestTools.UnitTesting;
using Vox.Auditing.Repository.SQLServer.Moles;
using System;
using T2.SendingMessage.Library.RabbitMQ.Moles;
using Vox.Auditing.Moles;

namespace Vox.Auditing.Tests {

  [TestClass]
  public class AuditingPluginTests {
    #region Protected Variables

    protected AuditingPlugin plugin;

    #endregion

    #region Setup

    [TestInitialize()]    
    public void Setup() {      
      plugin = new AuditingPlugin();
      plugin.InitializeConfiguration();
    }

    #endregion

    [TestClass]
    public class AuditingPlugin_ProcessWorker_Tests : AuditingPluginTests {

      [TestMethod]
      [HostType("Moles")]
      [DeploymentItem(@"Vox.Auditing")]
      public void AuditingPlugin_ProcessWorker_NoClient_Test() {
        MClientRepositoryDataModel.AllInstances.GetByNameString = (c, s) => { return null; };
        MRabbitMQQueueConnector.AllInstances.SendAsyncExceptionMessageExceptionStringMessage = (c, e, s, m) => {
          Assert.IsTrue(e.Message.Equals("Could not find a matching client for user ''"));
        };
        plugin.ProcessWorker(new T2.SendingMessage.Library.Message() { AdditionalInformation = new T2.SendingMessage.Library.HeaderInformation() });
      }

      [TestMethod]
      [HostType("Moles")]
      [DeploymentItem(@"Vox.Auditing")]
      public void AuditingPlugin_ProcessWorker_NoBatch_Test() {
        MClientRepositoryDataModel.AllInstances.GetByNameString = (c, s) => { return new Model.Client(0, "Test"); };
        MMessageRepositoryDataModel.AllInstances.GetByGuidGuid = (c, g) => { return null; };
        MRabbitMQQueueConnector.AllInstances.SendAsyncExceptionMessageExceptionStringMessage = (c, e, s, m) => {
          Assert.IsTrue(e.Message.Equals("No available batch is open for client 'Test'"), e.Message);
        };
        plugin.ProcessWorker(new T2.SendingMessage.Library.Message() {
          AdditionalInformation = new T2.SendingMessage.Library.HeaderInformation() {
            UserId = Guid.NewGuid(),
            MessageId = Guid.NewGuid()
          }
        });
      }

      [TestMethod]
      [HostType("Moles")]
      [DeploymentItem(@"Vox.Auditing")]
      public void AuditingPlugin_ProcessWorker_CreateMessageWithNoValidApplication_Test() {
        MClientRepositoryDataModel.AllInstances.GetByNameString = (c, s) => {
          return new Model.Client(0, "Test") {
            CurrentBatch = new Model.Batch(0, "TestBatch")
          };
        };
        MMessageRepositoryDataModel.AllInstances.GetByGuidGuid = (c, g) => { return null; };
        MMessageRepositoryDataModel.AllInstances.GetMessageApplicationIDGuid = (c, g) => { throw new Exception("No valid Application Guid"); };
        MRabbitMQQueueConnector.AllInstances.SendAsyncExceptionMessageExceptionStringMessage = (c, e, s, m) => {
          Assert.IsTrue(e.Message.Equals("No valid Application Guid"), e.Message);
        };
        plugin.ProcessWorker(new T2.SendingMessage.Library.Message() {
          AdditionalInformation = new T2.SendingMessage.Library.HeaderInformation() {
            UserId = Guid.NewGuid(),
            MessageId = Guid.NewGuid()
          },
          ApplicationId = Guid.NewGuid().ToString()
        });
      }

      [TestMethod]
      [HostType("Moles")]
      [DeploymentItem(@"Vox.Auditing")]
      public void AuditingPlugin_ProcessWorker_CreateMessageWithValidApplication_Test() {
        MClientRepositoryDataModel.AllInstances.GetByNameString = (c, s) => {
          return new Model.Client(0, "Test") {
            CurrentBatch = new Model.Batch(0, "TestBatch")
          };
        };
        MMessageRepositoryDataModel.AllInstances.GetByGuidGuid = (c, g) => { return null; };
        MMessageRepositoryDataModel.AllInstances.GetMessageApplicationIDGuid = (c, g) => { return 0; };
        MMessageRepositoryDataModel.AllInstances.AddMessage = (c, m) => { return true; };
        plugin.ProcessWorker(new T2.SendingMessage.Library.Message() {
          AdditionalInformation = new T2.SendingMessage.Library.HeaderInformation() {
            UserId = Guid.NewGuid(),
            MessageId = Guid.NewGuid()
          },
          ApplicationId = Guid.NewGuid().ToString()
        });
      }

      [TestMethod]
      [HostType("Moles")]
      [DeploymentItem(@"Vox.Auditing")]
      public void AuditingPlugin_ProcessWorker_AddAudit_Test() {
        MClientRepositoryDataModel.AllInstances.GetByNameString = (c, s) => {
          return new Model.Client(0, "Test") {
            CurrentBatch = new Model.Batch(0, "TestBatch")
          };
        };
        MMessageRepositoryDataModel.AllInstances.GetByGuidGuid = (c, g) => { return new Model.Message(0, "Test"); };
        plugin.ProcessWorker(new T2.SendingMessage.Library.Message() {
          AdditionalInformation = new T2.SendingMessage.Library.HeaderInformation() {
            UserId = Guid.NewGuid(),
            MessageId = Guid.NewGuid()
          },
          ApplicationId = Guid.NewGuid().ToString(),
          CorrelationId = Guid.NewGuid().ToString(),
          DestinationName = "Test"
        });
      }
    }
  }
}
