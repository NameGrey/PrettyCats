using System.Collections.Generic;
using System.Linq;
using BookIt.BLL.Entities;
using BookIt.DAL.Entities;
using BookIt.Repository.Mappers;

namespace BookIt.Repository
{
	public class OffersRepository : IGenericRepository<Offer>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly BookingOffersMapper _bookingOffersMapper = new BookingOffersMapper();

		private GenericRepository<BookingOffer> Repository
		{
			get { return _unitOfWork.OffersRepository; }
		}

		public OffersRepository(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public IEnumerable<Offer> Get()
		{
			return Repository.Get().Select(_bookingOffersMapper.Map).ToList();
		}

		public Offer GetByID(object id)
		{
			return _bookingOffersMapper.Map(Repository.GetByID(id));
		}

		public void Insert(Offer entity)
		{
			Repository.Insert(_bookingOffersMapper.UnMap(entity));
		}

		public void Delete(object id)
		{
			Repository.Delete(id);
		}

		public void Delete(Offer entityToDelete)
		{
			Repository.Delete(_bookingOffersMapper.UnMap(entityToDelete));
		}

		public void Update(Offer entityToUpdate)
		{
			Repository.Update(_bookingOffersMapper.UnMap(entityToUpdate));
		}
	}
}
