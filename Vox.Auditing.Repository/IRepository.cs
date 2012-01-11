namespace Vox.Auditing.Repository {
  /// <summary>
  /// Base Interface for a repository pattern
  /// </summary>
  public interface IRepository<T> {
    /// <summary>
    /// Retrieves the entity from the database for the given id
    /// </summary>
    /// <param name="aID">Integer ID which references the entity</param>
    /// <returns>The entity which is referenced, else NULL</returns>
    T GetByID(int aID);

    /// <summary>
    /// Adds a new entity into the repository
    /// </summary>
    /// <param name="entity">A entity object to add to the repository</param>
    /// <returns>True if the entity was added, otherwise false</returns>
    bool Add(T entity);

    /// <summary>
    /// Removes the entity from the repository
    /// </summary>
    /// <param name="entity">Entity which will be removed</param>
    bool Remove(T entity);

    /// <summary>
    /// Updates the Repository with the given entity
    /// </summary>
    /// <param name="entity">Entity which is updated</param>
    /// <returns>True if the entity was updated in the repository</returns>
    bool Update(T entity);
  }
}
