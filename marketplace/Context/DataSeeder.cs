using marketplace.Models;

namespace marketplace.Context
{
	public class DataSeeder
	{
		private readonly AppDbContext _appDbContext;

		public DataSeeder(AppDbContext appDbContext)
		{
			_appDbContext = appDbContext;
		}
		public  void SeedStates()
		{
			/*if (!_appDbContext.States.Any())
			{
				_appDbContext.States.Add(State.FREE);
				_appDbContext.States.Add(State.RESERVED);
				_appDbContext.States.Add(State.SOLDOUT);
				_appDbContext.SaveChanges();
			}*/
		}
	}
}
