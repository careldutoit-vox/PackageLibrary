using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Vox.Auditing.Model;
using Vox.Auditing.Repository.SQLServer.DataModel;
using Vox.Auditing.Repository.SQLServer.DataModel.Moles;
using Vox.Auditing.Repository.SQLServer.Tests.Mocks;

namespace Vox.Auditing.Repository.SQLServer.Tests {
  [TestClass]
  public class ClientRepositoryDataModelTests {
    #region Private Variables
    protected IClientRepository repository;
    #endregion

    #region Setup

    [TestInitialize()]
    public void Setup() {
      repository = new ClientRepositoryDataModel("Data Source=10.25.0.100;Initial Catalog=VoxAuditing;Persist Security Info=True;User ID=sa;Password=P!nkP3n;MultipleActiveResultSets=True");
      MVoxAuditingEntities.Constructor = (c) => { };
      MVoxAuditingEntities.ConstructorString = (c, s) => { };
    }

    #endregion

    [TestClass]
    public class ClientRepositoryDataModel_GetByName_Tests : ClientRepositoryDataModelTests {

      [TestMethod]
      [HostType("Moles")]
      [ExpectedException(typeof(ArgumentException), "aName")]
      public void ClientRepositoryDataModel_GetByName_Null_Test() {
        var result = repository.GetByName(null);
      }

      [TestMethod]
      [HostType("Moles")]
      [ExpectedException(typeof(ArgumentException), "aName")]
      public void ClientRepositoryDataModel_GetByName_Empty_Test() {
        var result = repository.GetByName("");
      }

      [TestMethod]
      [HostType("Moles")]
      public void ClientRepositoryDataModel_GetByName_DoesnotExist_Test() {
        MVoxAuditingEntities.AllInstances.MessageClientsGet = (c) => { return new FakeObjectSet<MessageClient>(); };
        MVoxAuditingEntities.AllInstances.BatchesGet = (c) => { return new FakeObjectSet<DataModel.Batch>(); };
        var result = repository.GetByName("Test");
        Assert.IsNull(result);
      }

      [TestMethod]
      [HostType("Moles")]
      public void ClientRepositoryDataModel_GetByName_Success_Test() {
        MVoxAuditingEntities.AllInstances.MessageClientsGet = (c) => {
          FakeObjectSet<MessageClient> objectSet = new FakeObjectSet<MessageClient>();
          objectSet.Add(new MessageClient() {
            MessageClientID = 1,
            MessageClientIdentifier = "Test"
          });
          return objectSet;
        };
        MVoxAuditingEntities.AllInstances.BatchesGet = (c) => {
          FakeObjectSet<DataModel.Batch> objectSet = new FakeObjectSet<DataModel.Batch>();
          objectSet.Add(new DataModel.Batch() {
            BatchID = 1,
            BatchNumber = "TestBatch",
            MessageClientID = 1,
            FinishDate = null,
            StartDate = DateTime.Now
          });
          return objectSet;
        };
        var result = repository.GetByName("Test");
        Assert.IsNotNull(result);
        Assert.AreEqual(1, result.ID);
      }
    }

    [TestClass]
    public class ClientRepositoryDataModel_GetByID_Tests : ClientRepositoryDataModelTests {

      [TestMethod]
      [HostType("Moles")]
      [ExpectedException(typeof(ArgumentException), "aID")]
      public void ClientRepositoryDataModel_GetByID_Negative_Test() {
        var result = repository.GetByID(-1);
      }

      [TestMethod]
      [HostType("Moles")]
      [ExpectedException(typeof(ArgumentException), "aID")]
      public void ClientRepositoryDataModel_GetByID_Zero_Test() {
        var result = repository.GetByID(0);
      }

      [TestMethod]
      [HostType("Moles")]
      public void ClientRepositoryDataModel_GetByID_DoesnotExist_Test() {
        MVoxAuditingEntities.AllInstances.MessageClientsGet = (c) => { return new FakeObjectSet<MessageClient>(); };
        var result = repository.GetByID(1);
        Assert.IsNull(result);
      }

      [TestMethod]
      [HostType("Moles")]
      public void ClientRepositoryDataModel_GetByID_Success_Test() {
        MVoxAuditingEntities.AllInstances.MessageClientsGet = (c) => {
          FakeObjectSet<MessageClient> objectSet = new FakeObjectSet<MessageClient>();
          objectSet.Add(new MessageClient() {
            MessageClientID = 1,
            MessageClientIdentifier = "Test"
          });
          return objectSet;
        };
        var result = repository.GetByID(1);
        Assert.IsNotNull(result);
        Assert.AreEqual(1, result.ID);
      }
    }

    [TestClass]
    public class ClientRepositoryDataModel_Add_Tests : ClientRepositoryDataModelTests {

      [TestMethod]
      [HostType("Moles")]
      [ExpectedException(typeof(ArgumentNullException), "entity")]
      public void ClientRepositoryDataModel_Add_Null_Test() {
        repository.Add(null);
      }

      [TestMethod]
      [HostType("Moles")]
      [ExpectedException(typeof(InvalidClientException), "Cannot add a batch which has already been added!")]
      public void ClientRepositoryDataModel_Add_Existing_Test() {
        repository.Add(new Model.Client(1, "Test"));
      }     

      [TestMethod]
      [HostType("Moles")]
      public void ClientRepositoryDataModel_Add_Success_Test() {
        MVoxAuditingEntities.AllInstances.MessageClientsGet = (c) => {
          return new FakeObjectSet<MessageClient>();
        };
        MVoxAuditingEntities.AllInstances.SaveChanges = (c) => { return 1; };
        bool result = repository.Add(new Model.Client(0, "Test"));
        Assert.IsTrue(result);
      }
    }

    [TestClass]
    public class ClientRepositoryDataModel_Remove_Tests : ClientRepositoryDataModelTests {

      [TestMethod]
      [HostType("Moles")]
      [ExpectedException(typeof(NotImplementedException))]
      public void ClientRepositoryDataModel_Remove_NotImplemented_Test() {
        repository.Remove(null);
      }
    }

    [TestClass]
    public class ClientRepositoryDataModel_Update_Tests : ClientRepositoryDataModelTests {

      [TestMethod]
      [HostType("Moles")]
      [ExpectedException(typeof(NotImplementedException))]
      public void ClientRepositoryDataModel_Update_NotImplemented_Test() {
        repository.Update(null);
      }
    }
  }
}
