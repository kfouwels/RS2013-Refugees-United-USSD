//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RS2013.RefugeesUnited.Data.Impl
{
  using System.Data.Entity;
  using Tam.Lib.Data;
  using Tam.Lib.Model;
  using RS2013.RefugeesUnited.Model;
  
  public partial class UserRepository : ARepository<User>, IUserRepository
  {
    public UserRepository(IDbContext database, IDbSet<User> data)
      : base(database, data)
    {
    }
  }
}