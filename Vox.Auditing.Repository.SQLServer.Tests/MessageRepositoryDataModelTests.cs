using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Vox.Auditing.Repository.SQLServer.DataModel.Moles;
using Vox.Auditing.Model;
using Vox.Auditing.Repository.SQLServer.Tests.Mocks;

namespace Vox.Auditing.Repository.SQLServer.Tests {
  [TestClass]
  public class MessageRepositoryDataModelTests {

    #region Private Variables
    protected IMessageRepository repository;
    #endregion

    #region Setup

    [TestInitialize()]
    public void Setup() {
      repository = new MessageRepositoryDataModel("Data Source=10.25.0.100;Initial Catalog=VoxAuditing;Persist Security Info=True;User ID=sa;Password=P!nkP3n;MultipleActiveResultSets=True");
      MVoxAuditingEntities.Constructor = (c) => { };
      MVoxAuditingEntities.ConstructorString = (c, s) => { };
    }

    #endregion

    [TestClass]
    public class MessageRepositoryDataModel_GetByGuid_Tests : MessageRepositoryDataModelTests {

      [TestMethod]
      [HostType("Moles")]
      [ExpectedException(typeof(ArgumentException), "aID")]
      public void MessageRepositoryDataModel_GetByGuid_Empty_Test() {
        var result = repository.GetByGuid(Guid.Empty);
      }

      [TestMethod]
      [HostType("Moles")]
      public void MessageRepositoryDataModel_GetByGuid_DoesnotExist_Test() {
        MVoxAuditingEntities.AllInstances.MessagesGet = (c) => { return new FakeObjectSet<DataModel.Message>(); };
        var result = repository.GetByGuid(Guid.NewGuid());
        Assert.IsNull(result);
      }

      [TestMethod]
      [HostType("Moles")]
      public void MessageRepositoryDataModel_GetByGuid_Success_Test() {
        Guid aGuid = Guid.NewGuid();
        MVoxAuditingEntities.AllInstances.MessagesGet = (c) => {
          FakeObjectSet<DataModel.Message> objectSet = new FakeObjectSet<DataModel.Message>();
          objectSet.Add(new DataModel.Message() {
            MessageID = 1,
            MessageNumber = aGuid.ToString()
          });
          return objectSet;
        };
        var result = repository.GetByGuid(aGuid);
        Assert.IsNotNull(result);
        Assert.AreEqual(1, result.ID);
      }
    }

    [TestClass]
    public class MessageRepositoryDataModel_GetMessageApplicationID_Tests : MessageRepositoryDataModelTests {

      [TestMethod]
      [HostType("Moles")]
      [ExpectedException(typeof(ArgumentNullException), "aGuid")]
      public void MessageRepositoryDataModel_GetMessageApplicationID_Empty_Test() {
        var result = repository.GetMessageApplicationID(Guid.Empty);
      }

      [TestMethod]
      [HostType("Moles")]
      [ExpectedException(typeof(Exception))]
      public void MessageRepositoryDataModel_GetMessageApplicationID_DoesnotExist_Test() {
        MVoxAuditingEntities.AllInstances.MessageApplicationsGet = (c) => { return new FakeObjectSet<DataModel.MessageApplication>(); };
        var result = repository.GetMessageApplicationID(Guid.NewGuid());        
      }

      [TestMethod]
      [HostType("Moles")]
      public void MessageRepositoryDataModel_GetMessageApplicationID_Success_Test() {
        Guid aGuid = Guid.NewGuid();
        MVoxAuditingEntities.AllInstances.MessageApplicationsGet = (c) => {
          FakeObjectSet<DataModel.MessageApplication> objectSet = new FakeObjectSet<DataModel.MessageApplication>();
          objectSet.Add(new DataModel.MessageApplication() {
            MessageApplicationID = 1,
            MessageApplicationGuid = aGuid,
            MessageApplicationName = "Test"
          });
          return objectSet;
        };
        var result = repository.GetMessageApplicationID(aGuid);
        Assert.AreEqual(1, result);
      }
    }

    [TestClass]
    public class MessageRepositoryDataModel_GetByID_Tests : MessageRepositoryDataModelTests {

      [TestMethod]
      [HostType("Moles")]
      [ExpectedException(typeof(ArgumentException), "aID")]
      public void MessageRepositoryDataModel_GetByID_Negative_Test() {
        var result = repository.GetByID(-1);
      }

      [TestMethod]
      [HostType("Moles")]
      [ExpectedException(typeof(ArgumentException), "aID")]
      public void MessageRepositoryDataModel_GetByID_Zero_Test() {
        var result = repository.GetByID(0);
      }

      [TestMethod]
      [HostType("Moles")]
      public void MessageRepositoryDataModel_GetByID_DoesnotExist_Test() {
        MVoxAuditingEntities.AllInstances.MessagesGet = (c) => { return new FakeObjectSet<DataModel.Message>(); };
        var result = repository.GetByID(1);
        Assert.IsNull(result);
      }

      [TestMethod]
      [HostType("Moles")]
      public void MessageRepositoryDataModel_GetByID_Success_Test() {
        MVoxAuditingEntities.AllInstances.MessagesGet = (c) => {
          FakeObjectSet<DataModel.Message> objectSet = new FakeObjectSet<DataModel.Message>();
          objectSet.Add(new DataModel.Message() {
             MessageID = 1,
            MessageNumber = "Test"
          });
          return objectSet;
        };
        var result = repository.GetByID(1);
        Assert.IsNotNull(result);
        Assert.AreEqual(1, result.ID);
      }
    }

    [TestClass]
    public class MessageRepositoryDataModel_Add_Tests : MessageRepositoryDataModelTests {

      [TestMethod]
      [HostType("Moles")]
      [ExpectedException(typeof(ArgumentNullException), "entity")]
      public void MessageRepositoryDataModel_Add_Null_Test() {
        repository.Add(null);
      }

      [TestMethod]
      [HostType("Moles")]
      [ExpectedException(typeof(InvalidMessageException), "Cannot add a message with no valid client!")]
      public void MessageRepositoryDataModel_Add_InvalidClient_Test() {
        repository.Add(new Model.Message(0, "Test"));
      }

      [TestMethod]
      [HostType("Moles")]
      [ExpectedException(typeof(InvalidMessageException), "Cannot add a message which has already been added!")]
      public void MessageRepositoryDataModel_Add_Existing_Test() {
        repository.Add(new Model.Message(1, "Test"));
      }

      [TestMethod]
      [HostType("Moles")]
      [ExpectedException(typeof(InvalidMessageException), "Cannot add a message with no valid batch!")]
      public void MessageRepositoryDataModel_Add_InvalidBatch_Test() { 
        bool result = repository.Add(new Model.Message(0, "Test1") { ClientID = 1 });
      }

      [TestMethod]
      [HostType("Moles")]
      public void MessageRepositoryDataModel_Add_Success_Test() {
        MVoxAuditingEntities.AllInstances.MessagesGet = (c) => {
          return new FakeObjectSet<DataModel.Message>();
        };
        MVoxAuditingEntities.AllInstances.SaveChanges = (c) => { return 1; };
        bool result = repository.Add(new Model.Message(0, "Test") { BatchID = 1, ClientID = 1 });
        Assert.IsTrue(result);
      }
    }

    [TestClass]
    public class MessageRepositoryDataModel_Remove_Tests : MessageRepositoryDataModelTests {

      [TestMethod]
      [HostType("Moles")]
      [ExpectedException(typeof(NotImplementedException))]
      public void MessageRepositoryDataModel_Remove_NotImplemented_Test() {
        repository.Remove(null);
      }
    }

    [TestClass]
    public class MessageRepositoryDataModel_Update_Tests : MessageRepositoryDataModelTests {

      [TestMethod]
      [HostType("Moles")]
      [ExpectedException(typeof(NotImplementedException))]
      public void MessageRepositoryDataModel_Update_NotImplemented_Test() {
        repository.Update(null);
      }
    }

    [TestClass]
    public class MessageRepositoryDataModel_AddMessageAudit_Tests : MessageRepositoryDataModelTests {

      [TestMethod]
      [HostType("Moles")]      
      public void MessageRepositoryDataModel_AddMessageAudit_Success_Test() {
        MVoxAuditingEntities.AllInstances.MessagesGet = (c) => {
          return new FakeObjectSet<DataModel.Message>();
        };
        MVoxAuditingEntities.AllInstances.MessageAuditsGet = (c) => {
          return new FakeObjectSet<DataModel.MessageAudit>();
        };
        MVoxAuditingEntities.AllInstances.SaveChanges = (c) => { return 1; };
        Model.Message message = new Model.Message(0, "Test") { BatchID = 1, ClientID = 1 };
        bool result = repository.Add(message);
        Assert.IsTrue(result);        
        result = message.AddAudit(Guid.NewGuid(), "Test");
        Assert.IsTrue(result);
      }
    }
  }
}
