namespace BookIt.Repository.Mappers
{
	public interface IMapper<TBusinessEntity, TDataEntity> 
						where TBusinessEntity : class
						where TDataEntity:class
	{
		TBusinessEntity Map(TDataEntity source);
		TDataEntity UnMap(TBusinessEntity source);
	}
}
