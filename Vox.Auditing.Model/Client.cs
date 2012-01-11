namespace Vox.Auditing.Model {
  public class Client {

    /// <summary>
    /// Default constructor for the Client, each client must have a ID and a Name.
    /// </summary>
    /// <param name="aID">A Unique Integer Identifier for the client</param>
    /// <param name="aName">A String name for the client, which should also be unique</param>
    public Client(int aID, string aName) {
      ID = aID;
      Name = aName;
    }

    #region Properties
    /// <summary> Unique Identifier for the client </summary>
    public int ID { get; private set; }

    /// <summary> Unique name for the client </summary>
    public string Name { get; private set; }

    /// <summary> Indicates the current batch that the client is using. If Null it indicates that no batch is currently assigned. </summary>
    public Batch CurrentBatch { get; set; }
    #endregion
  }
}
