using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Vox.Auditing.Repository.SQLServer.DataModel.Moles;
using Vox.Auditing.Repository.SQLServer.DataModel;
using Vox.Auditing.Repository.SQLServer.Tests.Mocks;

namespace Vox.Auditing.Repository.SQLServer.Tests {
  [TestClass]
  public class BatchRepositoryDataModelTests {

    #region Private Variables
    protected IBatchRepository repository;
    #endregion

    #region Setup

    [TestInitialize()]
    public void Setup() {
      repository = new BatchRepositoryDataModel("Data Source=10.25.0.100;Initial Catalog=VoxAuditing;Persist Security Info=True;User ID=sa;Password=P!nkP3n;MultipleActiveResultSets=True");
      MVoxAuditingEntities.Constructor = (c) => { };
      MVoxAuditingEntities.ConstructorString = (c, s) => { };
    }

    #endregion

    [TestClass]
    public class BatchRepositoryDataModel_GetByName_Tests : BatchRepositoryDataModelTests {

      [TestMethod]
      [HostType("Moles")]
      [ExpectedException(typeof(ArgumentException), "aID")]
      public void BatchRepositoryDataModel_GetByName_Null_Test() {
        var result = repository.GetByName(null);
      }

      [TestMethod]
      [HostType("Moles")]
      [ExpectedException(typeof(ArgumentException), "aID")]
      public void BatchRepositoryDataModel_GetByName_Empty_Test() {
        var result = repository.GetByName("");
      }

      [TestMethod]
      [HostType("Moles")]
      public void BatchRepositoryDataModel_GetByName_DoesnotExist_Test() {
        MVoxAuditingEntities.AllInstances.BatchesGet = (c) => { return new FakeObjectSet<Batch>(); };
        var result = repository.GetByName("Test");
        Assert.IsNull(result);
      }

      [TestMethod]
      [HostType("Moles")]
      public void BatchRepositoryDataModel_GetByName_Success_Test() {
        MVoxAuditingEntities.AllInstances.BatchesGet = (c) => {
          FakeObjectSet<Batch> objectSet = new FakeObjectSet<Batch>();
          objectSet.Add(new Batch() {
            BatchID = 1,
            BatchNumber = "Test"
          });
          return objectSet;
        };
        var result = repository.GetByName("Test");
        Assert.IsNotNull(result);
        Assert.AreEqual(1, result.ID);
      }
    }

    [TestClass]
    public class BatchRepositoryDataModel_GetByID_Tests : BatchRepositoryDataModelTests {

      [TestMethod]
      [HostType("Moles")]
      [ExpectedException(typeof(ArgumentException), "aID")]
      public void BatchRepositoryDataModel_GetByID_Negative_Test() {
        var result = repository.GetByID(-1);
      }

      [TestMethod]
      [HostType("Moles")]
      [ExpectedException(typeof(ArgumentException), "aID")]
      public void BatchRepositoryDataModel_GetByID_Zero_Test() {
        var result = repository.GetByID(0);
      }

      [TestMethod]
      [HostType("Moles")]
      [DeploymentItem(@"Vox.Auditing")]
      public void BatchRepositoryDataModel_GetByID_DoesnotExist_Test() {
        MVoxAuditingEntities.AllInstances.BatchesGet = (c) => { return new FakeObjectSet<Batch>(); };
        var result = repository.GetByID(1);
        Assert.IsNull(result);
      }

      [TestMethod]
      [HostType("Moles")]
      public void BatchRepositoryDataModel_GetByID_Success_Test() {
        MVoxAuditingEntities.AllInstances.BatchesGet = (c) => {
          FakeObjectSet<Batch> objectSet = new FakeObjectSet<Batch>();
          objectSet.Add(new Batch() {
            BatchID = 1,
            BatchNumber = "Test"
          });
          return objectSet;
        };
        var result = repository.GetByID(1);
        Assert.IsNotNull(result);
        Assert.AreEqual(1, result.ID);
      }
    }

    [TestClass]
    public class BatchRepositoryDataModel_Add_Tests : BatchRepositoryDataModelTests {

      [TestMethod]
      [HostType("Moles")]
      [ExpectedException(typeof(ArgumentNullException), "entity")]
      public void BatchRepositoryDataModel_Add_Null_Test() {
        repository.Add(null);
      }

      [TestMethod]
      [HostType("Moles")]
      [ExpectedException(typeof(InvalidBatchException), "Cannot add a batch with no valid client!")]
      public void BatchRepositoryDataModel_Add_InvalidClient_Test() {
        repository.Add(new Model.Batch(0, "Test"));
      }

      [TestMethod]
      [HostType("Moles")]
      [ExpectedException(typeof(InvalidBatchException), "Cannot add a batch which has already been added!")]
      public void BatchRepositoryDataModel_Add_Existing_Test() {
        repository.Add(new Model.Batch(1, "Test"));
      }

      [TestMethod]
      [HostType("Moles")]
      [ExpectedException(typeof(InvalidBatchException), "Batch 'Test' is still open for the given client")]
      public void BatchRepositoryDataModel_Add_ExistingBatchOpen_Test() {
        MVoxAuditingEntities.AllInstances.BatchesGet = (c) => {
          FakeObjectSet<Batch> objectSet = new FakeObjectSet<Batch>();
          objectSet.Add(new Batch() {
            BatchID = 1,
            BatchNumber = "Test",
            MessageClientID = 1,
            StartDate = DateTime.Now
          });
          return objectSet;
        };        
        bool result = repository.Add(new Model.Batch(0, "Test1") { ClientID = 1 });        
      }

      [TestMethod]
      [HostType("Moles")]
      public void BatchRepositoryDataModel_Add_Success_Test() {
        MVoxAuditingEntities.AllInstances.BatchesGet = (c) => {
          return new FakeObjectSet<Batch>();
        };
        MVoxAuditingEntities.AllInstances.SaveChanges = (c) => { return 1; };
        bool result = repository.Add(new Model.Batch(0, "Test") { ClientID = 1 });
        Assert.IsTrue(result);
      }
    }

    [TestClass]
    public class BatchRepositoryDataModel_Remove_Tests : BatchRepositoryDataModelTests {

      [TestMethod]
      [HostType("Moles")]
      [ExpectedException(typeof(NotImplementedException))]
      public void BatchRepositoryDataModel_Remove_NotImplemented_Test() {
        repository.Remove(null);
      }
    }

    [TestClass]
    public class BatchRepositoryDataModel_Update_Tests : BatchRepositoryDataModelTests {

      [TestMethod]
      [HostType("Moles")]
      [ExpectedException(typeof(ArgumentNullException), "entity")]
      public void BatchRepositoryDataModel_Update_Null_Test() {
        repository.Update(null);
      }

      [TestMethod]
      [HostType("Moles")]
      [ExpectedException(typeof(InvalidBatchException), "Cannot add a batch with no valid client!")]
      public void BatchRepositoryDataModel_Update_InvalidClient_Test() {
        repository.Update(new Model.Batch(1, "Test"));
      }

      [TestMethod]
      [HostType("Moles")]
      [ExpectedException(typeof(InvalidBatchException), "Cannot updated a batch which has not been added!")]
      public void BatchRepositoryDataModel_Update_InvalidBatch_Test() {
        repository.Update(new Model.Batch(0, "Test"));
      }
     
      [TestMethod]
      [HostType("Moles")]
      [ExpectedException(typeof(InvalidBatchException), "Batch 'Test1' does not exist")]
      public void BatchRepositoryDataModel_Update_DoesNotExist_Test() {
        MVoxAuditingEntities.AllInstances.BatchesGet = (c) => {
          FakeObjectSet<Batch> objectSet = new FakeObjectSet<Batch>();
          objectSet.Add(new Batch() {
            BatchID = 1,
            BatchNumber = "Test",
            MessageClientID = 1,
            StartDate = DateTime.Now
          });
          return objectSet;
        };        
        bool result = repository.Update(new Model.Batch(2, "Test1") { ClientID = 1, FinishDate = DateTime.Now });        
      }     

      [TestMethod]
      [HostType("Moles")]      
      public void BatchRepositoryDataModel_Update_Success_Test() {
        MVoxAuditingEntities.AllInstances.BatchesGet = (c) => {
          FakeObjectSet<Batch> objectSet = new FakeObjectSet<Batch>();
          objectSet.Add(new Batch() {
            BatchID = 1,
            BatchNumber = "Test",
            MessageClientID = 1,
            StartDate = DateTime.Now
          });
          return objectSet;
        };
        MVoxAuditingEntities.AllInstances.SaveChanges = (c) => { return 1; };
        bool result = repository.Update(new Model.Batch(1, "Test") { ClientID = 1, FinishDate = DateTime.Now });
        Assert.IsTrue(result);
      }     
    }
  }
}
