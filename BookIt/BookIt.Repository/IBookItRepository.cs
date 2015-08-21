using System.Collections.Generic;
using BookIt.BLL.Entities;
using BookIt.DAL;

namespace BookIt.Repository
{
    public interface IBookItRepository
    {
		IEnumerable<CategoryDto> GetCategories();
        IEnumerable<UserDto> GetUsers();
        IEnumerable<BookingSubjectDto> GetSubjects();
        IEnumerable<BookingOfferDto> GetOffers();
        void CreateOffer(BookingOfferDto offer);
		void UpdateOffer(BookingOfferDto offer);

		void CreateSubject(BookingSubjectDto subject);
    }
}
