using System;

namespace Vox.Auditing.Model {

  /// <summary>
  /// A batch is a container for a set of transactions performed. Each audited transaction must belong to a batch. And a batch can only be used as
  /// long as it is kept open.
  /// </summary>
  public class Batch {
    
    #region Constructor
    public Batch(int aID, string aNumber) {
      ID = aID;
      Number = aNumber;
      ClientID = 0;
    }
    #endregion

    #region Properties
    public int ID { get; private set; }
    public string Number { get; private set; }
    public DateTime StartDate { get; set; }
    public DateTime? FinishDate { get; set; }
    public int ClientID { get; set; }
    #endregion
  }
}
